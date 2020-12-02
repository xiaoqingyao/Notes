using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class UpdatePageDTO : UpdatePropDTO
    {

        /// <summary>
        /// 笔记Id
        /// </summary>
        public Guid NotesId { get; set; }


        /// <summary>
        /// 区域Id 
        /// </summary>
        public Guid SectionId { get; set; }
    }

}
