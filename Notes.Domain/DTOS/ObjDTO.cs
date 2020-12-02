using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class ObjDTO
    {


        /// <summary>
        /// 笔记本ID
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid SectionId { get; set; }

        /// <summary>
        /// 页ID
        /// </summary>
        public Guid PageId { get; set; }
    }

}
