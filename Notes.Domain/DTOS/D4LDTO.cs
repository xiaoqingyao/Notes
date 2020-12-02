using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.DTOS
{
    public class D4LDTO
    {

        public string ClassId { get; set; }

        public string CatalogCode { get; set; }

        public string CatalogName { get; set; }

        public Guid DsId { get; set; }

        public string DsName { get; set; }


        public string TaskName { get; set; }


        public Guid TaskId { get; set; }
    }
}
