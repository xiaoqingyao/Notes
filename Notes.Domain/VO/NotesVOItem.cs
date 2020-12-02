using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.VO
{
    public class NotesVOItem
    {
        public Guid Id { get; set; }


        public string Name { get; set; }
    }



    public class NotesVoContent : NotesVOItem
    {
       public string Content { get; set; } 
    }
}
