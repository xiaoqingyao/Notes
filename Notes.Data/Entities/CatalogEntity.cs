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

    [Table("Catalogs"), lq.Table("Catalogs")]
    public class CatalogEntity : EntityBase 
    {



        [Column(TypeName = "nvarchar(100)")]
       [lq.Column(DataType = LinqToDB.DataType.NVarChar, Length = 100)] 
        public string Creator { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [lq.Column]
        public string Name { get; set; }

        [Index]
        [lq.Column]
        public string ParentCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [lq.Column]
        public string Code { get; set; }


        [lq.Column]
        public int Count { get; set; }

        

    }
}
