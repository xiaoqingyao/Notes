using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Notes.Domain.VO
{
    public class NotesVO
    {

        public Guid Id { get; set; }


        public string Name { get; set; }

        public IList<NotesSectionVO> Sections { get; set; }

        public void AddSection(NotesSectionVO vo)
        {
            if (this.Sections == null)
            {
                this.Sections = new List<NotesSectionVO>();
            }
            this.Sections.Add(vo);
        }

        public string Body { get; set; }

    }


    public class NotesSectionVO
    {

        public Guid Id { get; set; }


        public string Name { get; set; }


        public IList<NotesPageVO> Pages { get; set; }


        public void AddPage(NotesPageVO vo)
        {
            if (this.Pages == null)
            {
                this.Pages = new List<NotesPageVO>();
            }
            this.Pages.Add(vo);
        }

    }


    public class NotesPageVO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


        public string Content { get; set; }
    }


}
