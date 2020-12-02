using AspectCore.DynamicProxy.Parameters;
using LinqToDB;
using LinqToDB.Tools;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Data;
using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.Domain.Core.Factories;
using Notes.Domain.Core.Users;
using Notes.Domain.VO;
using Notes.infrastructure;
using Notes.infrastructure.Validators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core
{
    public class NotesQuerySvc : INotesQuerySvc
    {

        private readonly ReaderConnection _reader;

        private readonly IAppUser _user;

        private readonly INotesLoader _loader;


        private readonly IUserLoader _userLoader;

        public NotesQuerySvc(IAppUser user, INotesLoader loader, ReaderConnection reader, IUserLoader userLoader)
        {
            _user = user;
            _loader = loader;
            _reader = reader;
            _userLoader = userLoader;
        }


        public Task<List<ClassPageCountVO>> CountPagesAsync(Guid dsId, string classid)
        {
            var rev = from c in this._reader.NotesForCourse.Qa()
                      join p in this._reader.NotesPage on c.PageId equals p.Id
                      where c.ClassId == classid && c.DsId == dsId
                      group p by new { p.Creator } into gorupPages
                      select new ClassPageCountVO
                      {
                          Count = gorupPages.Count(),
                          StdId = gorupPages.Key.Creator
                      };

            return rev.ToListAsync();
        }

        public Task<Guid> GetBySubject(string catalogCode, string gradeCode, string creator, string classId)
        {
            var q = this._reader.Notes
                    .Qa()
                    .Where(n => n.CalelogCode == catalogCode
                                                            && n.GradeCode == gradeCode
                                                            && n.CreatorId == creator);
            if (!String.IsNullOrEmpty(classId))
            {
                q = q.Where(n => n.ClassId == classId);
            }
            return q.Select(n => n.Id)
               .FirstOrDefaultAsync();
        }


        public async Task<CatalogVO> CatalogEnableFilterAsync(string userId, string termIdx, string subjectIdx)
        {
            var u = await this._userLoader.Load(userId);

            if (this._userLoader.IsFromDb)
            {
                await this._userLoader.Save(u);
            }

            Prosecutor.NotNull(u);


            if (u.Catalog == null)
            {
                return null;
            }


            var helper = new CatalogHelper(u, termIdx, subjectIdx)
                .Filter()
                .OrderBy();


            return new CatalogVO
            {
                Subject = helper.Subject, //subject,
                Term = helper.Term 
            };


        }


        public async Task<Pagnation<NotesVOItem>> ListAll(string catalogCode, string gradeCode, string createor, int pageSize, int pageIndex)
        {
            var query = this._reader.Notes
                    .Where(n => n.CreatorId == createor && n.Deleted == 0);
            if (!String.IsNullOrEmpty(catalogCode))
            {
                query = query.Where(n => n.CalelogCode == catalogCode);
            }
            if (!String.IsNullOrEmpty(gradeCode))
            {
                query = query.Where(n => n.GradeCode == gradeCode);
            }

            var pagnation = new Pagnation<NotesVOItem>
            {
                ItemCount = query.Count(),

                Data = await query.OrderByDescending(n => n.IndentityId).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(n => new NotesVOItem
                {
                    Id = n.Id,
                    Name = n.Name
                }).ToListAsync()
            };


            return pagnation;

        }


        public async Task<NotesVO> Get(Guid id, Guid sectionid, Guid pageId)
        {

            var notes = await this._loader.LoadAsync(id);

            notes.SetSection(sectionid)?
                .SetCurrentPage(pageId);

            var nv = new NotesVO
            {
                Id = id,
                Name = notes.Notes.Name
            };


            if (notes.HasSection() == false)
            {
                return nv;
            }

            foreach (var item in notes.Sections)
            {

                var so = new NotesSectionVO
                {
                    Id = item.Sections.Id,
                    Name = item.Sections.Name
                };

                if (item.HasPages())
                {
                    foreach (var page in item.Pages)
                    {
                        var po = new NotesPageVO
                        {
                            Id = page.Page.Id,
                            Name = page.Page.Name
                        };
                        so.AddPage(po);
                    }
                }

                nv.AddSection(so);

            }

            return nv;


        }


    }
}
