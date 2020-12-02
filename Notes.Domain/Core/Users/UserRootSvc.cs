using Notes.Domain.Core.Factories;
using Notes.Events;
using Notes.Events.CatalogEvent;
using Notes.infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Users
{
    public class UserRootSvc : IUserRootSvc
    {

        private readonly IUserLoader _loader;
        private readonly IEventSender _sender;

        public UserRootSvc(IUserLoader loader, IEventSender sender)
        {
            _loader = loader;
            _sender = sender;
        }


        public async Task DelPageAsync(string userId, Guid sectionId, Guid pageId)
        {
            if (pageId == Guid.Empty)
            {
                return;
            }
            var u = await this._loader.Load(userId);

           var rev = u.DelPage(sectionId, pageId);

            if (rev == false)
            {
                return;
            }

            await this._loader.Save(u);


            foreach (var item in u.Events)
            {
                await this._sender.SendAsync(item, item.GetType().Name);
            }

        }


        public async Task DelSection(string userId, params Guid[] sectionId)
        {
            if (sectionId == null)
            {
                return;
            }


            var u = await this._loader.Load(userId);


            foreach (var item in sectionId)
            {

                u.DeleteSection(item);
            }

            if (u.HasEvent() == false)
            {
                return;
            }

            await this._loader.Save(u);

            foreach (var item in u.Events)
            {
                await this._sender.SendAsync(item, item.GetType().Name);
            }

        }

        public async Task<(Guid sid, Guid pid)> EntryFromD4LAsync(string userId, string classId, Guid dsId, Guid taskId)
        {
            var u = await this._loader.Load(userId);

            var (sid, pid) = u.CourseRelation(classId, dsId, taskId);

            await this._loader.Save(u);

            if (u.HasEvent())
            {
                foreach (var item in u.Events)
                {
                    await this._sender.SendAsync(item, item.GetType().Name);
                }

            }


            return (sid, pid);
        }


        public async Task ReferenceCatalog(string userId, string catalogCode, string catalogName,
                                string gradecode, string gradeName)
        {
            var u = await this._loader.Load(userId);
            Prosecutor.NotNull(u);
            u.ReferenceCatalog(catalogCode, catalogName, gradecode, gradeName);

            await this._loader.Save(u);

            foreach (var item in u.Events)
            {
                await this._sender.SendAsync(item, item.GetType().Name);
            }
        }


        public async Task CatalogItemSubstructAsync(string userId, string catalogCode, string gradeCode)
        {
            var u = await this._loader.Load(userId);
            Prosecutor.NotNull(u);
            u.CatalogItemSubstruct(catalogCode, gradeCode);

            await this._loader.Save(u);

            foreach (var item in u.Events)
            {
                await
                    this._sender.SendAsync(item, item.GetType().Name);
            }



        }
    }
}
