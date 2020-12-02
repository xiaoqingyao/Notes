using Notes.Asset;
using Notes.Domain.Core;
using Notes.Domain.Core.Users;
using Notes.Domain.DTOS;
using Notes.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Notes.Application.Modules.Factories
{
    public class EntryFromD4L : IEntry
    {


        private readonly INotesSvc _notes;
        private readonly IBodySvc _body;
        //private readonly IQueryBridge _bridge;
        private readonly IUserRootSvc _userRoot;
        private readonly IAppUser _user;
        private readonly INotesQuerySvc _query;


        public EntryFromD4L(INotesSvc notes, IBodySvc body,/* IQueryBridge bridge, */IUserRootSvc userRoot, IAppUser user, INotesQuerySvc query)
        {
            _notes = notes;
            _body = body;
            //_bridge = bridge;
            _userRoot = userRoot;
            _user = user;
            _query = query;
        }

        

        /// <summary>
        /// 对应的分区Id 
        /// </summary>

        private Guid sId { get; set; }



        /// <summary>
        /// 对应的页Id
        /// </summary>
        private Guid pid { get; set; }

        public async Task<NotesObj> GetDesignNotes(string classId, string creator, string catalogCode, string gradeCode,
                                                    Guid dsId
                                                   , Guid taskId)
        {



            var (s, p) = await this._userRoot.EntryFromD4LAsync(this._user.UserId, classId, dsId, taskId);

            this.sId = s;
            this.pid = p;


            var notesid = await _query.GetBySubject(catalogCode, gradeCode, creator, "");
            if (notesid == Guid.Empty)
            {
                return null;
            }

            var notes = await this._notes.Get(notesid,/* dsId, taskId*/sId, pid);

            return notes;
        }


        public async Task<string> DoAsync(D4LDTO dto)
        {
            var code = new StringHelper(dto.CatalogCode).Split('/');
            var name = new StringHelper(dto.CatalogName).Split('/');

            string catalogCode = code.Get(1);
            string gradCode = code.Get(3);
            string gradeName = name.Get(3);
            string catalogName = name.Get(1);

            var notes = await this.GetDesignNotes(dto.ClassId, this._user.UserId, catalogCode, gradCode, dto.DsId, dto.TaskId);//this._bridge.GetDesignNotes(dto.ClassId, this._user.UserId, catalogCode, gradCode, dto.DsId, dto.TaskId);


            //如果笔记已存在...
            if (notes != null)
            {
                var section = notes.SetSection(this.sId);
                if (section == null)
                {
                    //以学程创建
                    notes = await this._notes.CreateSection(notes.Id, this.sId, dto.DsName);
                }



            }

            var pv = notes?.SetSection(this.sId)?
                    .SetCurrentPage(this.pid);



            //创建页
            if (notes != null && dto.TaskId != Guid.Empty && pv == null)
            {
                notes = await this._notes.CreatePage(notes.Id, sId, pid, dto.TaskName, null);
                notes.SetSection(sId)
                    .SetCurrentPage(pid);
            }



            if (notes == null)
            {

               Guid nid = Guid.NewGuid();

                notes = await this._notes.CreateAsync(new CreateNotesDTO
                {

                    CatalogCode = catalogCode, //code.Get(1),
                    CatalogName = catalogName, //name.Get(1),
                    GradeCode = gradCode, //code.Get(0),
                    GradeName = gradeName, //name.Get(0),
                    ClassId = dto.ClassId,
                    Name = String.Concat(name.Get(3), name.Get(1), name.Get(2), name.Get(4)),//"新建笔记本",
                    Section = new NotesComponent
                    {
                        Id = sId, //dto.DsId,
                        Value = dto.DsName
                    },
                    Page = new NotesComponent
                    {
                        Id = pid,//dto.TaskId,
                        Value = dto.TaskName
                    },
                    Content = "",


                }, nid
                , this._user.UserName
                , this._user.UserId);

                if (dto.TaskId != Guid.Empty)
                {

                    await this._body.Create(nid, this.sId, this.pid, "");
                }

                //用户使用分类
                await this._userRoot.ReferenceCatalog(this._user.UserId, catalogCode, catalogName, gradCode, gradeName);

            }

            return $"{notes.Id},{notes.CurrentSection?.Sections.Id},{notes.CurrentSection?.CurrentPage?.Page.Id}";
        }

    }
}
