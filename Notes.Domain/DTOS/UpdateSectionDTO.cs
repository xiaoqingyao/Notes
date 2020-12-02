using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class UpdateSectionDTO : UpdatePropDTO
    {
        [Required]
        public Guid NotesId { get; set; }
    }
}
