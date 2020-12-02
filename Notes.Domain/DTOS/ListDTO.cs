using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Notes.Domain.DTOS
{

    /// <summary>
    /// 列表
    /// </summary>
    public class ListDTO
    {

        /// <summary>
        /// 科目代码
        /// </summary>
        public string CatalogCode { get; set; }


        /// <summary>
        /// 班级代码
        /// </summary>
        public string GradeCode { get; set; }


        public int PageIndex { get; set; }



        public int PageSize { get; set; }

    }
}
