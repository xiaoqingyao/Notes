using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Notes.Application.Modules;
using Notes.Domain.Core;
using Notes.Domain.Core.Factories;
using Notes.Domain.Core.NotesBody;
using Notes.Domain.Core.Users;
using Notes.Domain.DTOS;
using Notes.Domain.VO;
using Notes.infrastructure;

namespace Notes.Application.Controllers
{
    public class NotesQueryController : NodesControllerBase
    {

        private readonly INotesQuerySvc _qs;
        private readonly IBodySvc _body;
        private readonly IAppUser _user;
        private readonly IUserLoader _userLoader;



        public NotesQueryController(INotesQuerySvc qs, IBodySvc body, IAppUser user, IUserLoader userLoader)
        {
            _qs = qs;
            _body = body;
            _user = user;
            _userLoader = userLoader;
        }




        /// <summary>
        /// 获取一个笔记
        /// </summary>
        /// <param name="id">笔记Id</param>
        /// <returns></returns>
        /// <remarks>
        /// 如果默认区为空则默认第一个区
        /// </remarks>
        [HttpPost, Route("get/{id}")]

        public async Task<ApiResponseAsync<NotesVO>> Get(string id)
        {


            var sh = new StringHelper(id).Split(',');

            var nid = sh.As<Guid>(0);

            var pid = sh.As<Guid>(2);

            var nv = await this._qs.Get(nid, sh.As<Guid>(1), pid);

            var content = await this._body.Get(pid);

            nv.Body = content;

            return ApiRes.OK(nv);

        }

        /// <summary>
        /// 获取当前用户笔记中的分类
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get/catalog")]
        public Task<CatalogVO> CatalogAsync(TermSubjectDTO dto)
        {
            return this._qs.CatalogEnableFilterAsync(this._user.UserId, dto.TermIdx, dto.SubjectIdx);
            //var u = await this._userLoader.Load(this._user.UserId);
            //return u.Catalog;
        }


        /// <summary>
        /// 笔记列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果是全部 catalogcode 和 gradecode为 空  
        /// catalogcode 科目级别代码
        /// gradecode 年级级别代码 
        /// 
        /// pageIndex and pageSize > 0
        /// 
        /// **返回值**
        /// data 为数据列表
        /// itemCount 总条数
        /// 
        /// </remarks>
        [HttpPost, Route("list")]
        public Task<ApiResponseAsync<Pagnation<NotesVOItem>>> NotesList(ListDTO dto)
        {

            if (dto.PageIndex <= 0)
            {
                dto.PageIndex = 1;
            }
            if (dto.PageSize <= 0)
            {
                dto.PageSize = 10;
            }

            var rev = this._qs.ListAll(dto.CatalogCode, dto.GradeCode, this._user.UserId, dto.PageSize, dto.PageIndex);

            return ApiRes.OKAsync(rev);

        }

        /// <summary>
        /// 获取分页内容
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///  id 为页ID 
        /// </remarks>
        [HttpPost, Route("page_content/{id}")]
        public Task<ApiResponseAsync<string>> PageContent(Guid id)
        {
            var rev = this._body.Get(id);

            return ApiRes.OKAsync(rev);
        }


        /// <summary>
        /// 统计班下学程页数
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///  
        /// </remarks>
        [HttpPost, Route("page_count")]
        public Task<ApiResponseAsync<List<ClassPageCountVO>>> PageCount(CourseCountDTO dto)
        {
            var rev = this._qs.CountPagesAsync(dto.DsId, dto.ClassId);

            return ApiRes.OKAsync(rev);
        }

    }
}
