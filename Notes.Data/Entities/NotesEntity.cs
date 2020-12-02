using Notes.Data.EFProvider;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Entities
{

    [Table("Notes")]
    public class NotesEntity :EntityBase 


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

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Index]
        public string CreatorId { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string CreatorName { get; set; }


        [Column(TypeName = "nvarchar(10)")]

        public string ClassId { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        public string ClassName { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Source { get; set; }


        /// <summary>
        /// 
        /// </summary>

        [Column(TypeName = "nvarchar(300)")]
        public string Catelog { get; set; }


        
        [Column(TypeName = "nvarchar(300)")]
        public string CalelogCode { get; set; }


        /// <summary>
        /// 班级
        /// </summary>
        
        [Column(TypeName = "nvarchar(50)")]
        public string Grade { get; set; }

        
        /// <summary>
        /// 科目
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string GradeCode { get; set; }
    }

}
