using LinqToDB;
using Microsoft.Extensions.Options;
using Notes.Data;
using Notes.Domain.Core.Users;
using Notes.infrastructure.Validators;
using Notes.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{
    public class UserLoader : IUserLoader
    {

        private readonly ICachingProvider _cachor;
        private readonly IOptions<DomainOptions> _opt;
        private readonly ReaderConnection _reaer;

        public UserLoader(ICachingProvider cachor, IOptions<DomainOptions> opt, ReaderConnection reader)
        {
            _cachor = cachor;
            _opt = opt;
            _reaer = reader;
        }

        string Key(string id)
        {
            return string.Concat(this._opt.Value.UserCachePrifex, id);
        }


        public bool IsFromDb { get; private set; }

        public async Task<UserObj> Load(string userId)
        {
            var u = await this._cachor.Get<UserObj>(Key(userId));
            if (u == null)
            {
                u = new UserObj
                {
                    UserId = userId
                };

                await this.SetCatalogAsync(u);

                await this.SetNotesForCourseAsync(u);

                this.IsFromDb = true;
            }

            return u;
        }

        private async Task SetNotesForCourseAsync(UserObj u)
        {
            var relationDb = await this._reaer.NotesForCourse
                                .Qa()
                                .Where(n => n.Creator == u.UserId)
                                .ToListAsync();

            if (relationDb == null || relationDb.Count == 0)
            {
                return;
            }

            u.Course = new List<D4LValue>();

            foreach (var item in relationDb.Where(r => r.TaskId == Guid.Empty))
            {
                var dv = new D4LValue
                {
                    ClassId = item.ClassId,
                    DsId = item.DsId,
                    SectionId = item.SectionId
                };


                dv.TaskToPage = new Dictionary<Guid, Guid>();

                // 同班级同学程下任务
                foreach (var task in relationDb.Where(r => r.TaskId != Guid.Empty && r.DsId == item.DsId && r.ClassId == item.ClassId))
                {
                    dv.TaskToPage.TryAdd(task.TaskId, task.PageId);
                }

                u.Course.Add(dv);
            }
        }

        private async Task SetCatalogAsync(UserObj u)
        {

            var catalogDb = await this._reaer.Catalogs
                            .Qa()
                            .Where(c => c.Creator == u.UserId)
                            .OrderByDescending(c => c.Code)
                            .ToListAsync();

            u.Catalog = new CatalogData()
            {
                Grade = new Dictionary<string, CatalogValue>(),
                Subject = new Dictionary<string, IList<CatalogValue>>()
            };

            foreach (var item in catalogDb)
            {

                var valueItem = new CatalogValue
                {
                    Code = item.Code,
                    Count = item.Count,
                    Name = item.Name
                };


                if (item.ParentCode == "")
                {
                    u.Catalog.Grade.TryAdd(item.Code, valueItem);
                    continue;
                }

                if (u.Catalog.Subject.TryGetValue(item.ParentCode, out IList<CatalogValue> value))
                {
                    if (value.Any(v => v.Code == item.Code) == false)
                    {
                        value.Add(valueItem);
                    }
                    continue;
                }
                u.Catalog.Subject[item.ParentCode] = new List<CatalogValue> { valueItem };
            }

            //throw new NotImplementedException();
        }

        public async Task Save(UserObj u)
        {
            if (u.IsNull(u => u.UserId))
            {
                return;
            }

            await this._cachor.SetAsync(Key(u.UserId), u);
        }


    }
}
