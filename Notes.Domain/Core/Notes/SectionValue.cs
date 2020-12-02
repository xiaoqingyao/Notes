using Notes.Data.Entities;
using Notes.infrastructure.Exceptions;
using Notes.infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Notes.Domain.Core
{
    //区域
    public class SectionValue
    {

        public SectionValue() { }

        public SectionValue(NotesSectionEntity se)
        {
            this.Sections = se;
        }


        public SectionValue(Guid noteId, Guid gid, string sectionName)
        {
            this.Sections = new NotesSectionEntity
            {
                NotesId = noteId,
                Name = sectionName,
                Id = gid
            };



         //   var pg = String.IsNullOrEmpty(pageName) ? null : new PageValue(gid, noteId, pageName);


        }




        public NotesSectionEntity Sections { get; set; }

        public IList<PageValue> Pages { get; set; } = null;

        public PageValue CurrentPage { get; private set; }




        public SectionValue DeletePage(Guid pid)
        {
            this.Pages.Remove(new PageValue {
                Page = new NotesPageEntity
                {
                    Id = pid
                }
            });

            return this;
        }


        public PageValue SetCurrentPage(Guid pid)
        {
            if (pid == Guid.Empty || this.HasPages() == false)
            {
                return null;
            }
            this.CurrentPage = this.Pages.SingleOrDefault(p => p.Page.Id == pid);
            return CurrentPage;
        }


        public void AddPages(PageValue item)
        {

            if (item == null)
            {
                return;
            }
            if (this.Pages == null)
            {
                this.Pages = new List<PageValue>();
            }
            this.Pages.Add(item);
            this.CurrentPage = item;
        }


        public bool HasPages()
        {
            return this.Pages != null && this.Pages.Count > 0;
        }

        internal bool UpdatePageName(Guid pid, string name)
        {
            if (this.HasPages() == false)
            {
                throw new NotesValidateExceptions("对象不存在");
            }
            var page = this.Pages.FirstOrDefault(p => p.Page.Id == pid);

            Prosecutor.NotNull(page);

            if (page.Page.Name.Equals(name))
            {
                return false;
            }

            page.Page.Name = name;

            return true;
        }


        public override bool Equals(object obj)
        {
            if (obj is SectionValue value)
            {
                if (value.Sections != null
                    && this.Sections != null 
                    && value.Sections.Id == this.Sections.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.Sections.Id);
        }
    }






}
