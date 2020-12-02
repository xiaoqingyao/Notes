using LinqToDB.DataProvider.SapHana;
using Notes.Data.EFProvider;
using System;
using System.Collections.Generic;
using ds = System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations.Schema;
using lq = LinqToDB.Mapping;
using LinqToDB.Mapping;

namespace Notes.Data
{

    [Table("NotesForCourse")]
    public class NotesForCourseEntity : EntityBase
    {


        [Index,lq.Column]
        public Guid DsId { get; set; }


        [lq.Column]
        public Guid TaskId { get; set; }


        [lq.Column]
        [ds.Column(TypeName = "nvarchar(10)")]
        public string ClassId { get; set; }


        [lq.Column]
        public Guid SectionId { get; set; }


        [lq.Column]
        public Guid PageId { get; set; }


        [lq.Column(DbType = "nvarhcar(10)"), ds.Column(TypeName = "nvarchar(10)")]
        public string Creator { get; set; }
    }
}
