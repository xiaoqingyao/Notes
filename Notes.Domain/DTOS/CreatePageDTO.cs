using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class CreatePageDTO : UpdatePropDTO
    {

        /// <summary>
        /// 笔记Id
        /// </summary>

        [Required]
        public Guid NotesId { get; set; }


    }
}
