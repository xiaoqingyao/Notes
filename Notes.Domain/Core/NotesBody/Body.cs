using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.Core.NotesBody
{
    public class Body
    {


        public Guid Id { get; set; }


        public string Content { get; set; }


        //public Body(NotesContentEntity cnt)
        //{
        //    this.Content = cnt;
        //}

        //public Guid Id
        //{
        //    get
        //    {

        //        if (Content == null)
        //        {
        //            return Guid.Empty;
        //        }

        //        return Content.Id;
        //    }
        //}

        //public NotesContentEntity Content { get; set; } = null;

    }
}
