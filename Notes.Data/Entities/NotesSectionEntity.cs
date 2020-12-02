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
    [Table("NotesSection")]
    public class NotesSectionEntity :EntityBase
    {


        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public int IndentityId { get; set; }//{ get ; set ; }

        //[Index]
        //public Guid Id { get; set; }

        //[Index]
        //public int Deleted { get; set; }
        //public DateTime? CreationTime { get; set; }
        //public DateTime? UpdateTime { get; set; }

        [lq.Column("NotesId")]
        [Index]
        public Guid NotesId { get; set; }


        [lq.Column(Name="Name",DataType = LinqToDB.DataType.NVarChar,Length = 200)]
        public string Name { get; set; }




    }

}
