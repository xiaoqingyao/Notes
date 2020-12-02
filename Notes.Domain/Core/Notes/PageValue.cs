using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.Core
{
    public class PageValue
    {


        public PageValue() { }

        public PageValue(NotesPageEntity pe)
        {
            this.Page = pe;
        }



       


        public NotesPageEntity Page { get; set; }

        public Guid ContentId { get; set; }


        public PageValue(Guid sectionId,Guid notesId,Guid pId, string pageName)
        {


            this.Page = new NotesPageEntity
            {
                SectionId = sectionId,
                Name = pageName,
                NotesId = notesId,
                Id = pId
            };
            //this.Content = new NotesContentEntity
            //{
            //    Id = Guid.NewGuid(),
            //    PageId = pId,
            //    Content = content,
            //    SectionId = sectionId,
            //    NotesId = notesId
            //};
        }

        public override bool Equals(object obj)
        {
            if (obj is PageValue value)
            {
                if (value.Page.Id == this.Page.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.Page.Id);
        }

    }

}
