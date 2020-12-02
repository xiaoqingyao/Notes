using DotNetCore.CAP;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Linq;
using LinqToDB.Tools;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.Events;
using Notes.Events.CatalogEvent;
using Notes.Events.CourseEvents;
using Notes.Events.Events;
using System;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Transactions;

namespace Notes.Domain.BusServices
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {


        //private readonly IUnitOfWork<NotesWriteDbContext> _unitOfWork;

        //private readonly INotesWriter writer;

        //private readonly IUnitOfWork<NotesWriteDbContext> _unitOfWork;

        private readonly WritterConnection _con;



        public SubscriberService(WritterConnection con)
        {
            //this._unitOfWork = unitOfwork;
            _con = con;

        }



        #region Notes for Course....


        [CapSubscribe(nameof(CourseNotesCreated))]
        public Task<int> NotesForCusre(CourseNotesCreated @event)
        {


            return this._con.NotesForCourse
                .AddAsync(() => new NotesForCourseEntity
                {
                    Creator = @event.Creator,
                    DsId = @event.DsId,
                    SectionId = @event.SectionId,
                    TaskId = @event.TaskId,
                    PageId = @event.PageId,
                    ClassId = @event.ClassId,
                    Id = Guid.NewGuid()
                });

        }


        [CapSubscribe(nameof(CourseDsNotesRemoved))]
        public Task<int> CourseSectionDel(CourseDsNotesRemoved @event)
        {
            return this._con.NotesForCourse
                 .Where(n => n.DsId == @event.DsId && n.ClassId == @event.ClassId && n.Creator == @event.Creaotor)
                 .DelAsync();
        }


        [CapSubscribe(nameof(CourseTaskPageRemoved))]
        public Task CoursePageDelete(CourseTaskPageRemoved @event)
        {
            return this._con.NotesForCourse
                .Where(n => n.PageId == @event.PageId)
                .DelAsync();
        }


        #endregion





        //IDbConnection Con()
        //{
        //    if (con == null)
        //    {

        //    }
        //    return con;
        //}

        [CapSubscribe(nameof(NotesContentDeleted))]
        public Task<int> DeleteContent(NotesContentDeleted @event)
        {






            return this._con.PageContent

                .Where(p => p.PageId == @event.PId)
                .DelAsync();

            //await this.writer.DelAsync<NotesContentEntity>(n => n.PageId == @event.PId);

            //this.writer.Save();
        }


        [CapSubscribe(nameof(NotesSectionCreated))]
        public Task<int> CreateSectdion(NotesSectionCreated @event)
        {


            return this._con.NotesSections
                        .AddAsync(() => new NotesSectionEntity
                        {

                            NotesId = @event.NotesId,
                            Id = @event.SectionId,
                            Name = @event.Name
                        });



            //await this.writer.AddAsync<NotesSectionEntity>(new NotesSectionEntity
            //{
            //    NotesId = @event.NotesId,
            //    Id = @event.SectionId,
            //    Name = @event.Name
            //});
            //this.writer.Save();
        }


        [CapSubscribe(nameof(NotesContentUpdated))]
        public Task<int> UpdateNotesContent(NotesContentUpdated @event)
        {


            return this._con.PageContent
                            .Where(p => p.PageId == @event.PId)
                            .Set(p => p.Content, @event.Content)
                            .UpAsync();

            //await this._unitOfWork.GetRepositoryAsync<NotesContentEntity>()
            //    .UpdateBach(n => n.PageId == @event.PId
            //,

            //n => new NotesContentEntity
            //{
            //    Content = @event.Content
            //}); ;

            //this.writer.Save();
        }


        [CapSubscribe(nameof(NotesContentCreated))]
        public Task<int> CreateNotesContent(NotesContentCreated @event)
        {


            return this._con.PageContent
                 .AddAsync(() => new NotesContentEntity
                 {
                     Id = @event.Id,
                     NotesId = @event.NotesId,
                     PageId = @event.Pid,
                     SectionId = @event.SecitonId,
                     Content = @event.Content
                 });

            //await this.writer
            //        .AddAsync(new NotesContentEntity
            //        {
            //            Id = @event.Id,
            //            NotesId = @event.NotesId,
            //            PageId = @event.Pid,
            //            SectionId = @event.SecitonId,
            //            Content = @event.Content
            //        });

            //this.writer.Save();

        }



        /// <summary>
        /// Delete a notes.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(NotesDeleted))]
        public async Task DeleteNotes(NotesDeleted @event)
        {
            //using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await this._con.Notes
                            .Where(n => n.Id == @event.NotesId)
                            .DelAsync();


                //await this.writer.DelAsync<NotesSectionEntity>(s => s.NotesId == @event.NotesId);
                await this._con.NotesSections
                        .Where(n => n.NotesId == @event.NotesId)
                        .DelAsync();
                //await this.writer.DelAsync<NotesPageEntity>(p => p.NotesId == @event.NotesId);

                await this._con.NotesPage
                    .Where(p => p.NotesId == @event.NotesId)
                    .DelAsync();


                //await this.writer.DelAsync<NotesContentEntity>(c => c.NotesId == @event.NotesId);

                await this._con.PageContent
                    .Where(p => p.NotesId == @event.NotesId)
                    .DelAsync();


                //this.writer.Save();

                //scope.Complete();

            }
            catch (Exception)
            {

                throw;
            }
        }



        [CapSubscribe(nameof(NotesPageCreated))]
        public async Task CreateNotesPage(NotesPageCreated @event)
        {

            //await this.writer.AddAsync(new NotesPageEntity
            //{
            //    Id = @event.PageId,
            //    NotesId = @event.NotesId,
            //    SectionId = @event.SectionId,
            //    Name = @event.Name
            //});

            await this._con.NotesPage
                .AddAsync(() => new NotesPageEntity
                {
                    Id = @event.PageId,
                    NotesId = @event.NotesId,
                    SectionId = @event.SectionId,
                    Name = @event.Name,
                    ClassId = @event.ClassId,
                    Creator = @event.Creator
                });
        }



        [CapSubscribe(nameof(NotesPageDeleted))]
        public async Task DeleteNotePage(NotesPageDeleted @event)
        {
            //var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //try
            //{
            //await this.writer.DelAsync<NotesPageEntity>(p => p.Id == @event.PageId && p.NotesId == @event.NotesId);

            await this._con.NotesPage
                 .Where(p => p.Id == @event.PageId && p.NotesId == @event.NotesId)
                 .DelAsync();

            //await this.writer.DelAsync<NotesContentEntity>(c => c.PageId == @event.PageId && c.NotesId == @event.NotesId);

            await this._con.PageContent
                .Where(p => p.PageId == @event.PageId && p.NotesId == @event.NotesId)
                .DelAsync();

            //this.writer.Save();
            //scope.Complete();

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }




        /// <summary>
        /// delete notes's section...
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>

        [CapSubscribe(nameof(NotesSectionDeleted))]
        public async Task DeleteNotesScetion(NotesSectionDeleted @event)
        {
            //var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //try
            //{
            //    //await this.writer.DelAsync<NotesSectionEntity>(s => s.Id == @event.SectionId && s.NotesId == @event.NotesId);

            await this._con.NotesSections
                    .Where(s => s.Id == @event.SectionId && s.NotesId == @event.NotesId)
                    .DelAsync();


            //await this.writer.DelAsync<NotesPageEntity>(s => s.SectionId == @event.SectionId && s.NotesId == @event.NotesId);

            await this._con.NotesPage
                    .Where(p => p.SectionId == @event.SectionId && p.NotesId == @event.NotesId)
                    .DelAsync();


            //await this.writer.DelAsync<NotesContentEntity>(s => s.SectionId == @event.SectionId && s.NotesId == @event.NotesId);

            await this._con.PageContent
                    .Where(c => c.SectionId == @event.SectionId && c.NotesId == @event.NotesId)
                    .DelAsync();

            //this.writer.Save();

            //scope.Complete();

            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }


        //IUpdatable<TSource> UpdateAsync<TSource>(Expression<Func<TSource, bool>> predicate)
        //{
        //    return this._con.
        //}



        /// <summary>
        /// 更新页名称
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>

        [CapSubscribe(nameof(NotesPageNameUpdated))]
        public async Task UpdatePageName(NotesPageNameUpdated @event)
        {


            await this._con.NotesPage
                  .Where(n => n.Id == @event.Id && n.NotesId == @event.NotesId)
                  .Set(n => n.Name, @event.Name)
                  .UpAsync();
            //.Set(n => n.UpdateTime, DateTime.Now)
            //.UpdateAsync();

            //this._con.UpdateAsync(new NotesPageEntity
            //{
            //    Name = @event.Name,
            //    UpdateTime = DateTime.Now
            //}, new UpdateColumnFilter)


            // var builder = new LinqToDbConnectionOptionsBuilder();
            // builder.UseSqlServer(this.con.ConnectionString);
            // var op = builder.Build();

            //builder. 



            //return this.con.UpdateAsync(new NotesPageEntity
            //{
            //    Id = @event.Id,
            //    Name = @event.Name,
            //    UpdateTime = DateTime.Now
            //});

            //await this._unitOfWork.GetRepositoryAsync<NotesPageEntity>()
            //    .UpdateBach(p => p.Id == @event.Id
            //        ,
            //        n => new NotesPageEntity
            //        {
            //            Name = @event.Name
            //        });
            //this._unitOfWork.SaveChanges();
        }











        /// <summary>
        /// 更新区域名称
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(NotesSectionNameUpdated))]
        public async Task UpdateSectionName(NotesSectionNameUpdated @event)
        {
            //await this._unitOfWork.UpdateAsync<NotesSectionEntity>(s => s.Id == @event.Id
            //, new NotesSectionEntity
            //{
            //    Name = @event.Name
            //});

            //await this._unitOfWork.GetRepositoryAsync<NotesSectionEntity>()
            //    .UpdateBach(
            //    n => n.Id == @event.Id,
            //    n => new NotesSectionEntity
            //    {
            //        Name = @event.Name,
            //        UpdateTime = DateTime.Now
            //    });

            //this._unitOfWork.SaveChanges();

            await this._con.NotesSections
                .Where(s => s.Id == @event.Id && s.NotesId == @event.NotesId)
                .Set(s => s.Name, @event.Name)
                .UpAsync();

        }






        /// <summary>
        /// Update notes's name...
        /// </summary>
        [CapSubscribe(nameof(NotesNameUpdated))]
        public async Task UpdateNotes(NotesNameUpdated @event)
        {
            //await this.writer.UpdateAsync<NotesEntity>(n => n.Id == @event.Id
            //            , new NotesEntity
            //            {
            //                Name = @event.Name
            ////            });
            //await this._unitOfWork.GetRepositoryAsync<NotesEntity>()
            //      .UpdateBach(n => n.Id == @event.Id,
            //      n => new NotesEntity
            //      {
            //          Name = @event.Name,
            //          UpdateTime = DateTime.Now
            //      });
            //this._unitOfWork.SaveChanges();


            await this._con.Notes
                .Where(n => n.Id == @event.Id)
                .Set(n => n.Name, @event.Name)
                .UpAsync();

        }




        #region Create notes..
        /// <summary>
        /// create notes...
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        [CapSubscribe(nameof(NotesCreated))]
        public async Task CreateNotes(NotesCreated @event)
        {

            if (@event.Notes == null)
            {
                return;
            }

            //using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


            try
            {

                await this._con.Notes
                    .AddAsync(() => new NotesEntity
                    {
                        Id = @event.Notes.Id,
                        Name = @event.Notes.Name,
                        CreatorId = @event.Notes.CreatorId,
                        CreatorName = @event.Notes.CreatorName,
                        CalelogCode = @event.Notes.CalelogCode,
                        Catelog = @event.Notes.Catelog,
                        ClassId = @event.Notes.ClassId,
                        Grade = @event.Notes.Grade,
                        GradeCode = @event.Notes.GradeCode,
                        Source = @event.Notes.Source,
                        ClassName = @event.Notes.ClassName

                    });


                //await this.writer.AddAsync(@event.Notes);

                if (@event.Sections != null)
                {

                    await this._con.NotesSections
                         .AddAsync(() => new NotesSectionEntity
                         {
                             Name = @event.Sections.Name,
                             NotesId = @event.Sections.NotesId,
                             Id = @event.Sections.Id
                         });

                    //await this.writer
                    //            .AddAsync(@event.Sections);



                }
                if (@event.Page != null)
                {

                    await this._con.NotesPage
                        .AddAsync(() => new NotesPageEntity
                        {
                            Name = @event.Page.Name,
                            NotesId = @event.Page.NotesId,
                            SectionId = @event.Page.SectionId,
                            Id = @event.Page.Id
                        });

                    //await this.writer
                    //            .AddAsync(@event.Page);

                }

                //this.writer.Save();

                //scope.Complete();

            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion



        #region 用户分类信息.....



        [CapSubscribe(nameof(CatalogItemReferenced))]
        public async Task CatalogItemReference(CatalogItemReferenced @event)
        {
            await this._con.Catalogs
                    .AddAsync(() => new CatalogEntity
                    {
                        Code = @event.Code,
                        Name = @event.Name,
                        Count = @event.Count,
                        ParentCode = @event.ParentCode,
                        Creator = @event.Creator,
                        Id = Guid.NewGuid()

                    });
        }



        [CapSubscribe(nameof(CatalogItemCountChanged))]
        public async Task CatalogItemCountChange(CatalogItemCountChanged @event)
        {
            await this._con.Catalogs
                    .Where(c => c.Code == @event.Code && c.Creator == @event.UserId)
                    .Set(c => c.Count, @event.Count)
                    .UpAsync();
        }


        //[CapSubscribe(nameof(CatalogChildUpdated))]
        //public async Task CatalogChildUpdate(CatalogChildUpdated @event)
        //{

        //    //年级
        //    await this._con.Catalogs
        //            .Where(c => c.Code == @event.GradeCode && c.Creator == @event.Creator)
        //            .Set(c => c.Count, @event.GradeItemCount)
        //            .UpAsync();


        //    //科目
        //    await this._con.Catalogs
        //        .Where(c => c.Code == @event.CatalogCode && c.Creator == @event.Creator)
        //        .Set(c => c.Count, @event.CatalogItemCount)
        //        .UpAsync();

        //}



        //[CapSubscribe(nameof(CatalogChildAdded))]
        //public async Task CatalogChildAdd(CatalogChildAdded @event)
        //{
        //    await this._con.Catalogs
        //             .AddAsync(() => new CatalogEntity
        //             {
        //                 Creator = @event.Creator,
        //                 Name = @event.CatalogName,
        //                 ParentCode = @event.GradeCode,
        //                 Code = @event.CatalogCode,
        //                 Count = @event.CatalogItemCount

        //             });

        //    // 更新父级总数....
        //    await this._con.Catalogs
        //          .Where(c => c.Creator == @event.Creator && c.Code == @event.GradeCode)
        //          .Set(c => c.Count, c => c.Count + 1)
        //          .UpAsync();
        //}



        //[CapSubscribe(nameof(CatalogReferenced))]
        //public async Task CatalogReference(CatalogReferenced @event)
        //{

        //    await this._con.Catalogs
        //                   .AddAsync(() => new CatalogEntity
        //                   {
        //                       Code = @event.GradeCode,
        //                       Name = @event.GradeName,
        //                       Count = @event.GradeItemCount,
        //                       ParentCode = ""
        //                   });


        //    await this._con.Catalogs
        //        .AddAsync(() => new CatalogEntity
        //        {
        //            Code = @event.CatalogCode,
        //            Name = @event.CatalogName,
        //            Count = @event.CatalogItemCount,
        //            ParentCode = @event.GradeCode

        //        });

        //}



        #endregion



    }
}
