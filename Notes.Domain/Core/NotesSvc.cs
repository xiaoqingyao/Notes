using AspectCore.DynamicProxy;
using DotNetCore.CAP;
using LinqToDB.Common.Internal.Cache;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Options;
using Notes.Data;
using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.Domain.Core.Factories;
using Notes.Domain.DTOS;
using Notes.Events;
using Notes.Events.Events;
using Notes.infrastructure;
using Notes.infrastructure.Validators;
using Notes.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Notes.Domain.Core
{
    public class NotesSvc : INotesSvc
    {


        private readonly IAppUser _user;



        private readonly INotesLoader _loader;

        private readonly IEventSender _envSender;



        public NotesSvc(IAppUser user, INotesLoader loader, IEventSender envSedner)
        {
            _user = user;
            _loader = loader;
            _envSender = envSedner;
        }



        public async Task<NotesObj> Get(Guid nid, Guid sectionId, Guid pid)
        {
            var notes = await this._loader.LoadAsync(nid);
            Prosecutor.NotNull(notes);
            notes
                ?.SetSection(sectionId)
                ?.SetCurrentPage(pid);



            return notes;
        }



        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public async Task<NotesObj> CreateAsync(CreateNotesDTO dto, Guid notesId, string userName,string userId)
        {

            var notes = new NotesObj();

            notes.Create(dto, notesId, userId, userName);

            //using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //try
            //{

            await this._envSender.SendAsync(new NotesCreated
            {
                Notes = notes.Notes,
                Sections = notes.CurrentSection?.Sections,
                Page = notes.CurrentSection?.CurrentPage?.Page
            });

            await this._loader.Save(notes);

            //scope.Complete();

            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            return notes;
        }



        /// <summary>
        /// 更新笔记本内容...
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateNotesName(Guid id, string name)
        {
            var notes = await this._loader.LoadAsync(id);

            if (notes.UpdateName(name) == false)
            {
                return false;
            }


            await this._envSender.SendAsync(new NotesNameUpdated
            {
                Id = id,
                Name = name
            });


            await this._loader.Save(notes);

            return true;

        }



        /// <summary>
        /// 更新区域名称
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateSectionName(Guid notesId, Guid sid, string name)
        {

            var notes = await this._loader.LoadAsync(notesId);

            if (notes.UpdateSectionName(sid, name) == false)
            {
                return false;
            }


            //await this._unitOfWork.GetRepositoryAsync<NotesSectionEntity>()
            //     .UpdateBach(s => s.Id == sid,
            //     s => new NotesSectionEntity
            //     {
            //         Name = name
            //     });

            //this._unitOfWork.SaveChanges();

            await this._envSender.SendAsync(new NotesSectionNameUpdated
            {
                Id = sid,
                Name = name,
                NotesId = notesId
            });

            await this._loader.Save(notes);


            return true;
        }



        /// <summary>
        /// 更新页名称
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdatePageName(Guid nid, Guid sid, Guid pid, string name)
        {

            var notes = await this._loader.LoadAsync(nid);

            if (notes.UpdatePageName(sid, pid, name) == false)
            {
                return false;
            }


            await this._envSender.SendAsync(new NotesPageNameUpdated
            {
                Id = pid,
                Name = name,
                NotesId = nid
                
            });

            //await this._unitOfWork.GetRepositoryAsync<NotesPageEntity>()
            //    .UpdateBach(s => s.Id == pid
            //    , p => new NotesPageEntity
            //    {
            //        Name = name
            //    });

            //this._unitOfWork.SaveChanges();


            await this._loader.Save(notes);

            return true;
        }

        ///// <summary>
        ///// 更新内容
        ///// </summary>
        ///// <param name="pid"></param>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //public async Task<bool> UpdateContent(Guid pid, string content)
        //{
        //    await this._unitOfWork.GetRepositoryAsync<NotesContentEntity>()
        //            .UpdateBach(c => c.PageId == pid
        //            , p => new NotesContentEntity
        //            {
        //                Content = content
        //            });

        //    this._unitOfWork.SaveChanges();


        //    return true;
        //}


        /// <summary>
        /// 创建区域
        /// </summary>
        /// <returns></returns>
        public async Task<NotesObj> CreateSection(Guid noteId,Guid sId, string name)
        {

            var notes = await this._loader.LoadAsync(noteId);


            var sectionEntity = new NotesSectionEntity
            {
                Id = sId,
                NotesId = noteId,
                Name = name
            };

            notes.AddSection(new SectionValue(sectionEntity));


            await this._envSender.SendAsync(new NotesSectionCreated
            {
                NotesId = noteId,
                SectionId = sId,
                Name = name
            });


            //await this._unitOfWork.GetRepositoryAsync<NotesSectionEntity>()
            //    .AddAsync(sectionEntity);

            //this._unitOfWork.SaveChanges();

            await this._loader.Save(notes);

            return notes; // sectionEntity.Id;
        }

        /// <summary>
        /// 创建页
        /// </summary>
        /// <returns></returns>
        public async Task<NotesObj> CreatePage(Guid notesId, Guid sectionId,Guid pId, string name, string content)
        {

           

            var notes = await this._loader.LoadAsync(notesId);

            var page = notes.AddPages(sectionId,pId, name, content);

            notes.SetSection(sectionId)
                .SetCurrentPage(pId);

            await this._envSender.SendAsync(new NotesPageCreated
            {
                NotesId = notesId,
                SectionId = sectionId,
                PageId = pId, //page.Page.Id,
                Name =  name,
                ClassId = notes.Notes.ClassId,
                Creator = notes.Notes.CreatorId
            });

            await this._loader.Save(notes);

            //using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //try
            //{
            //    await this._unitOfWork.GetRepositoryAsync<NotesPageEntity>()
            //.AddAsync(page.Page);

            //    //await this._unitOfWork.GetRepositoryAsync<NotesContentEntity>()
            //    //    .AddAsync(page.Content);

            //    this._unitOfWork.SaveChanges();

            //    await this._loader.Save(notes);

            //    scope.Complete();

            return notes; //page.Page.Id;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


        }


        /// <summary>
        /// 删除区域
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Guid>> DelSection(Guid nid, Guid sId)
        {


            var notes = await this._loader.LoadAsync(nid);

            var delObj = notes.DelSection(sId);


            await this._envSender.SendAsync(new NotesSectionDeleted
            {
                SectionId = sId,
                NotesId = nid
            });


            await this._loader.Save(notes);



            return delObj.Pages?.Select(p => p.Page.Id).ToList();



        }


        /// <summary>
        /// 删除页
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DelPage(Guid nid, Guid sid, Guid pid)
        {


            var notes = await this._loader.LoadAsync(nid);


            notes.SetSection(sid)
                    .DeletePage(pid);



            await this._envSender.SendAsync(new NotesPageDeleted
            {
                PageId = pid,
                NotesId = nid
            });


            await this._loader.Save(notes);

            return true;

        }

        /// <summary>
        /// 删除笔记
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public async Task<NotesObj> DelNotes(Guid nid)
        {
            var notes = await this._loader.LoadAsync(nid);

            await this._envSender.SendAsync(new NotesDeleted
            {
                NotesId = nid
            });

            await this._loader.Remove(nid);


            return notes;



        }

    }
}
