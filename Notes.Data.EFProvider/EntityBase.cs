using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations.Schema;
using lq = LinqToDB.Mapping;

namespace Notes.Data.EFProvider
{
    public abstract class EntityBase
    {


        /// <summary>
        /// 自增Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [lq.Column("IndentityId")]
        [lq.PrimaryKey, lq.Identity] public int IndentityId { get; set; }


        [Index]
        [lq.Column("Id")]
        public Guid Id { get; set; }


        [Index]
        [lq.Column("Deleted"), DefaultValue(0)]
        public int Deleted { get; set; }


        [lq.Column("CreationTime")]
        public DateTime? CreationTime { get; set; }// = DateTime.Now;


        [lq.Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }// = DateTime.Now;
    }
}
