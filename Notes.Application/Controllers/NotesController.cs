using Microsoft.AspNetCore.Mvc;
using Notes.Application.Modules;
using Notes.Application.Modules.Factories;
using Notes.Domain.Core;
using Notes.Domain.Core.Users;
using Notes.Domain.DTOS;
using Notes.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Application.Controllers
{
    public class NotesController : NodesControllerBase
    {


        private readonly INotesSvc _notes;
        private readonly IBodySvc _body;
        private readonly IEntry _entry;
        private readonly IUserRootSvc _userRoot;
        private readonly IAppUser _appUser;
       

        public NotesController(INotesSvc notes, IBodySvc body, IEntry entry, IUserRootSvc userRoot, IAppUser appUser)
        {
            _notes = notes;
            _body = body;
            _entry = entry;
            _appUser = appUser;
            _userRoot = userRoot;
        }



        /// <summary>
        /// 学程笔记入口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        ///  如果对应的学程
        /// 
        /// </remarks>
        [HttpPost, Route("d4l_entry")]

        public Task<string> D4LEntry(D4LDTO dto)
        {
            return this._entry.DoAsync(dto);
        }





        /// <summary>
        /// 创建新的笔记
        /// </summary>
        /// <param name="dto">创建笔记需要的参数</param>
        /// <returns>创建的笔记ID</returns>
        /// <remarks>
        ///    创建成功后返回创建的笔记ID,
        ///   
        ///    **约定：**
        ///    
        ///   其它时： {CatalogCode: "-2", CatalogName : "其它", GradeCode:"-1", GradeName="其它"}
        ///  
        ///    GradeCode： 年级代码， GadeName: 年级名称
        ///    
        ///    CatalogoCode： 科目代码  CatalogName： 科目名称
        /// 
        /// 
        /// </remarks>
        [HttpPost, Route("create")]

        public async Task<ApiResponseAsync<Guid>> CreateNotes(CreateNotesDTO dto)
        {


            if (dto.Section != null)
            {
                dto.Section.Id = Guid.NewGuid();
            }

            if (dto.Page != null)
            {
                dto.Page.Id = Guid.NewGuid();
            }

            if (String.IsNullOrEmpty(dto.ClassId))
            {
                //dto.ClassId = await this._appUser.ClassIdAsync();
            }

            var ret = await this._notes.CreateAsync(dto, Guid.NewGuid(), this._appUser.UserName, this._appUser.UserId);

            await this._userRoot.ReferenceCatalog(this._appUser.UserId, dto.CatalogCode, dto.CatalogName, dto.GradeCode, dto.GradeName);


            return ApiRes.OK(ret.Notes.Id);


        }



        /// <summary>
        /// 修改笔记名称 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回0
        /// </remarks>
        [HttpPost, Route("update_nodes_name")]
        public Task<ApiResponseAsync<bool>> UpdateNotesName(UpdatePropDTO dto)
        {

            var rev = this._notes.UpdateNotesName(dto.Id, dto.Body);

            return ApiRes.OKAsync(rev);

        }




        /// <summary>
        /// 添加分区
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Id 为 笔记本Id
        /// name 为分区名称
        /// </remarks>
        [HttpPost, Route("create_section")]
        public async Task<ApiResponseAsync<Guid>> CreateSection(UpdatePropDTO dto)
        {

            var id = Guid.NewGuid();

            var rev = await this._notes.CreateSection(dto.Id, id, dto.Body);

            return ApiRes.OK(id);

        }


        /// <summary>
        /// 修改区域名称
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回0
        /// </remarks>
        [HttpPost, Route("update_section_name")]
        public Task<ApiResponseAsync<bool>> UpdateSectionName(UpdateSectionDTO dto)
        {

            var rev = this._notes.UpdateSectionName(dto.NotesId, dto.Id, dto.Body);

            return ApiRes.OKAsync(rev);

        }


        /// <summary>
        /// 添加页
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回页Id
        ///  notesId 为笔记本Id
        ///  id 为页所属sectionId
        /// </remarks>
        [HttpPost, Route("create_page")]
        public async Task<ApiResponseAsync<Guid>> CreatePage(CreatePageDTO dto)
        {
            Guid pId = Guid.NewGuid();

            var rev = await this._notes.CreatePage(dto.NotesId, dto.Id, pId, dto.Body, "");

            await this._body.Create(dto.NotesId, dto.Id, pId, "");


            return ApiRes.OK(rev.CurrentSection.CurrentPage.Page.Id);
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回页Id
        ///  notesId 为笔记本Id
        ///  id 为页所属sectionId
        /// </remarks>
        [HttpPost, Route("create_content")]
        public async Task<ApiResponseAsync<bool>> CreatePageContent(CreateContentDTO dto)
        {


            await this._body.Create(dto.NotesId, dto.SectionId, dto.PageId, dto.Cotnent);


            return ApiRes.OK(true);
        }




        /// <summary>
        /// 修改页名称
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回true
        ///  参数中id 为页ID
        /// </remarks>
        [HttpPost, Route("update_page_name")]
        public Task<ApiResponseAsync<bool>> UpdatePageName(UpdatePageDTO dto)
        {
            var rev = this._notes.UpdatePageName(dto.NotesId, dto.SectionId, dto.Id, dto.Body);
            return ApiRes.OKAsync(rev);
        }








        /// <summary>
        /// 修改页内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  修改成功后返回0
        /// </remarks>
        [HttpPost, Route("update_page_content")]
        public Task<ApiResponseAsync<bool>> UpdatePageContent(UpdatePropDTO dto)
        {

            var rev = this._body.Update(dto.Id, dto.Body);



            return ApiRes.OKAsync(rev);
        }




        /// <summary>
        /// 删除页
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  删除成功后返回0
        /// </remarks>
        [HttpPost, Route("del_page")]
        public async Task<ApiResponseAsync<bool>> DelPage(ObjDTO dto)
        {

            var rev = await this._notes.DelPage(dto.Id, dto.SectionId, dto.PageId);

            await this._userRoot.DelPageAsync(this._appUser.UserId, dto.SectionId, dto.PageId);

            return ApiRes.OK(rev);





            //var ret = this._notes.DelSection(dto.Id, dto.SectionId);

        }




        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  删除成功后返回0
        /// </remarks>
        [HttpPost, Route("del_section")]
        public async Task<ApiResponseAsync<bool>> DelSection(ObjDTO dto)
        {

            var rev = await this._notes.DelSection(dto.Id, dto.SectionId);

            await this._body.Del(rev?.ToArray());

            await this._userRoot.DelSection(this._appUser.UserId, dto.SectionId);

            return ApiRes.OK(true);

        }



        /// <summary>
        /// 删除笔记
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        ///  删除成功后返回0
        /// </remarks>
        [HttpPost, Route("del_notes")]
        public async Task<ApiResponseAsync<bool>> DelNotes(ObjDTO dto)
        {

            var notes = await this._notes.DelNotes(dto.Id);

            await this._userRoot.CatalogItemSubstructAsync(this._appUser.UserId, notes.Notes.CalelogCode, notes.Notes.GradeCode);


            if (notes.HasSection() == false)
            {
                return  ApiRes.OK(true);
            }

            List<Guid> pageAryId = new List<Guid>();

            foreach (var item in notes.Sections)
            {
                if (item.HasPages() == false)
                {
                    continue;
                }
                pageAryId.AddRange(item.Pages.Select(p => p.Page.Id));
            }




            if (pageAryId.Count > 0)
            {
                await this._body.Del(pageAryId.ToArray());
                await this._userRoot.DelSection(this._appUser.UserId, pageAryId.ToArray());
            }

            return ApiRes.OK(true);
            //throw new NotImplementedException();

        }


        ///// <summary>
        ///// 获取笔记
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        ///// <remarks>
        ///// </remarks>
        //[HttpPost, Route("get")]
        //public Task<ApiResponseAsync<NotesVO>> GetNotes(ObjDTO dto)
        //{

        //    throw new NotImplementedException();

        //}















    }
}
