using Notes.Data.EFProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations.Schema;
using lq = LinqToDB.Mapping;

namespace Notes.Data.Entities
{
    [Table("NotesPage"), lq.Table("NotesPage")]
    public class NotesPageEntity : EntityBase
    {



        [Index]
        [lq.Column]
        public Guid SectionId { get; set; }

        [lq.Column(DataType = LinqToDB.DataType.NVarChar, Length = 200)]
        public string Name { get; set; }


        [Index]
        [lq.Column]
        public Guid NotesId { get; set; }


        [Index]
        [lq.Column]
        [Column(TypeName = "nvarchar(100)")]
        public string ClassId { get; set; }



        [lq.Column]
        [Column(TypeName = "nvarchar(100)")]
        public string Creator { get; set; }

    }
}
