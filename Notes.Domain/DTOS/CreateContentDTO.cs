using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class CreateContentDTO
    {
        [Required]
        public Guid NotesId { get; set; }


        [Required]
        public Guid SectionId { get; set; }


        [Required]
        public Guid PageId { get; set; }

        public string Cotnent { get; set; }
    }

}
