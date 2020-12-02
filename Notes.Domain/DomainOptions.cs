using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain
{
    public class DomainOptions
    {

        public const string OptionsSection = "DomainOptions";

        public string NotesCachePrefix { get; set; }

        public string BodyCachePrefix { get; set; }

        public string UserCachePrifex { get; set; } = "NotesUser_0_";

    }

}
