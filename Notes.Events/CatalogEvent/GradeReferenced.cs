using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Notes.Events.CatalogEvent
{
    public class CatalogItemReferenced : INotesEvent
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public int Count { get; set; }

        public string ParentCode { get; set; }

    }
}
