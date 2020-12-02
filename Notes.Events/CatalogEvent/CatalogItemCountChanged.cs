using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Notes.Events.CatalogEvent
{
    public class CatalogItemCountChanged : INotesEvent
    {
        public string UserId { get; set; }

        public string Code { get; set; }

        public int Count { get; set; }

    }

}
