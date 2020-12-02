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
    [Table("NotesContent")]
    public class NotesContentEntity : EntityBase 
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

        [Index]
        [lq.Column]
        public Guid PageId { get; set; }


        [Column(TypeName = "nvarchar(max)")]
        
        [lq.Column]
        public string Content { get; set; }



        [Index]
        [lq.Column]
        public Guid SectionId { get; set; }


        [Index]
        
        [lq.Column]
        public Guid NotesId { get; set; }
    }

}
