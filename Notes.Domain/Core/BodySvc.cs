using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.Domain.Core.Factories;
using Notes.Domain.Core.NotesBody;
using Notes.Events;
using Notes.Events.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Notes.Domain.Core
{
    public class BodySvc : IBodySvc
    {


        private readonly IBodyLoader _loader;

        private readonly IEventSender _sender;

        public BodySvc(IBodyLoader loader, IEventSender sender)
        {
            _loader = loader;
            _sender = sender;
        }

        public async Task<string> Get(Guid pid)
        {
            if (pid == Guid.Empty)
            {
                return null;
            }

            var body = await this._loader.LoadAsync(pid);

            return body?.Content;
        }

        public async Task<bool> Del(params Guid[] pid)
        {
            if (pid == null || pid.Length == 0)
            {
                return false;
            }

            foreach (var item in pid)
            {


                bool rev = await this._loader.Del(item);

                await this._sender.SendAsync(new NotesContentDeleted
                {
                    PId = item
                });
            }

            return true;
        }

        public async Task<bool> Update(Guid pid, string content)
        {
            var body = await this._loader.LoadAsync(pid);

            body.Content = content;


            //await this._unitOfWork.Update<NotesContentEntity>(n => n.Id == cid, new NotesContentEntity
            //{
            //    Content = content
            //});


            //this._unitOfWork.SaveChanges();

            await this._sender.SendAsync(new NotesContentUpdated
            {

                PId = pid,
                Content = content

            });

            await this._loader.Save(body);

            return true;

        }


        public async Task<bool> Create(Guid notesId, Guid sectionId, Guid pid, string content)
        {

            if (pid == Guid.Empty)
            {
                return false;
            }

            var body = await this._loader.LoadAsync(pid);

            Guid cid = pid; //Guid.NewGuid();

            if (body == null)
            {


                body = new Body()
                {
                    Id = cid,
                };


                await this._sender.SendAsync(new NotesContentCreated
                {

                    Id = cid,
                    Pid = pid,
                    SecitonId = sectionId,
                    Content = content,
                    NotesId = notesId

                });


            }
            else
            {

                await this._sender.SendAsync(new NotesContentUpdated
                {
                    Content = content,
                    PId = pid
                });

            }


            body.Content = content;

            await this._loader.Save(body);

            return true;

        }
    }
}
