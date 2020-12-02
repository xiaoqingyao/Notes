using LinqToDB;
using Microsoft.Extensions.Options;
using Notes.Data;
using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.infrastructure;
using Notes.infrastructure.Exceptions;
using Notes.infrastructure.Validators;
using Notes.Infrastructure.Caching;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{

    public class NotesLoader : INotesLoader
    {

        //private readonly INotesReader _reader;


        private readonly IAppUser _user;

        private readonly ICachingProvider _cachor;

        private readonly IOptions<DomainOptions> _opt;

        private readonly ReaderConnection _conReader;



        public NotesLoader(IAppUser user, ICachingProvider cachor, IOptions<DomainOptions> opt, /*INotesReader reader,*/ ReaderConnection readerCon)
        {
            _user = user;
            _cachor = cachor;
            _opt = opt;
            //_reader = reader;
            _conReader = readerCon;
        }


        async Task<NotesObj> FromDb(Guid id)
        {


            var ne = await this._conReader.Notes
                .Qa()
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
                

            //var ne = await this._reader.Qa<NotesEntity>(n => n.Id == id)
            //        .FirstOrDefaultAsync();

            if (ne == null)
            {
                throw new NotesNotFoundException($"笔记不存在{id}");
            }

            var notes = new NotesObj
            {
                Notes = ne
            };

            //查询分区

            var se = await this._conReader.NotesSections
                    .Qa()
                    .Where(s => s.NotesId == id)
                    .OrderBy(o => o.CreationTime)
                    .ToListAsync(); 

            //var se = await this._reader.Qa<NotesSectionEntity>(s => s.NotesId == id)
            //        .OrderBy(o => o.CreationTime)
            
            //        .ToListAsync();



            if (se == null || se.Count == 0)
            {
                return notes;
            }

            // 查询页


            var pe = await this._conReader.NotesPage
                .Qa()
                .Where(p => p.NotesId == id)
                .OrderBy(o => o.CreationTime)
                .ToListAsync();

            //var pe = await this._reader.Qa<NotesPageEntity>(p => p.NotesId == id)
            //    .OrderBy(o => o.CreationTime)
            //    .ToListAsync();

            IDictionary<Guid, IList<NotesPageEntity>> dict = null;

            if (pe != null && pe.Count > 0)
            {
                dict = new Dictionary<Guid, IList<NotesPageEntity>>();


                foreach (var item in pe)
                {
                    if (!dict.TryGetValue(item.SectionId, out IList<NotesPageEntity> value))
                    {
                        dict[item.SectionId] = new List<NotesPageEntity>();
                    }
                    dict[item.SectionId].Add(item);
                }

                //return notes;
            }


            foreach (var item in se)
            {
                var sv = new SectionValue(item);
                if (dict != null && dict.TryGetValue(item.Id, out IList<NotesPageEntity> value))
                {
                    foreach (var page in value)
                    {
                        var po = new PageValue(page);
                        sv.AddPages(po);
                    }
                }
                notes.AddSection(sv);
            }


            return notes;


        }


        public async Task<NotesObj> LoadAsync(Guid id)
        {

            string key = string.Concat(this._opt.Value.NotesCachePrefix, id);

            var rev = await this._cachor.Get<NotesObj>(key);

            if (rev == null)
            {
                rev = await this.FromDb(id);

            }

            Prosecutor.NotNull(rev);

            return rev;


        }


        public Task Save(NotesObj notes)
        {
            string key = String.Concat(this._opt.Value.NotesCachePrefix, notes.Id);

            return this._cachor.SetAsync(key, notes);
        }

        public Task<bool> Remove(Guid nid)
        {

            string key = String.Concat(this._opt.Value.NotesCachePrefix, nid);

            return this._cachor.Remove(key);

            //throw new NotImplementedException();
        }
    }
}