using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyModel.Resolution;
using Notes.Data.Entities;
using Notes.Domain.DTOS;
using Notes.infrastructure;
using Notes.infrastructure.Exceptions;
using Notes.infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Domain.Core
{
    public class NotesObj
    {



        public Guid Id
        {
            get
            {
                if (Notes == null)
                {
                    return Guid.Empty;
                }

                return Notes.Id;

            }

        }




        public NotesEntity Notes { get; set; }


        public IList<SectionValue> Sections { get; set; } = null;

        public bool HasSection()
        {
            return this.Sections != null && this.Sections.Count > 0;
        }


        public void AddSection(SectionValue value)
        {
            if (value == null)
            {
                return;
            }

            if (this.Sections == null)
            {
                this.Sections = new List<SectionValue>();
            }
            this.Sections.Add(value);

            this.CurrentSection = value;
        }


        internal void Create(CreateNotesDTO dto,Guid notesId, string userId, string userName)
        {
            this.Notes = new NotesEntity
            {
                Id = notesId,
                Name = dto.Name,
                CreatorId = userId,//user.UserId,
                CreatorName = userName,// user.UserName,
                CalelogCode = dto.CatalogCode,
                Catelog = dto.CatalogName,
                ClassId = dto.ClassId,
                Grade = dto.GradeName,
                GradeCode = dto.GradeCode,
                Source = dto.Source

            };

            //if (String.IsNullOrEmpty(this.Notes.Name))
            //{
            //    this.Notes.Name = dto.CatalogName.Replace('/', '-');
            //}


            var sc = dto.Section == null ? null : new SectionValue(this.Notes.Id, dto.Section.Id, dto.Section.Value);

            if (sc == null)
            {
                return;
            }

            this.CurrentSection = sc;


            var pv = (dto.Page == null || dto.Page.Id ==  Guid.Empty) ? null : new PageValue(new NotesPageEntity
            {
                NotesId = this.Notes.Id,
                SectionId = sc.Sections.Id,//dto.Section.Id,
                Id = dto.Page.Id,
                Name = dto.Page.Value
            });


            sc.AddPages(pv);

            this.AddSection(sc);

        }

        public SectionValue CurrentSection { get; private set; }


        public SectionValue SetSection(Guid sid)
        {
            if (sid == Guid.Empty || this.HasSection() == false)
            {
                return null;
            }
            if (sid == Guid.Empty)
            {
                sid = this.Sections.FirstOrDefault().Sections.Id;
            }
            this.CurrentSection = this.Sections.SingleOrDefault(s => s.Sections.Id == sid);
            return this.CurrentSection;
        }



        internal bool UpdateName(string name)
        {
            if (this.Notes.Name.Equals(name))
            {
                return false;
            }
            this.Notes.Name = name;
            return true;
        }

        internal bool UpdateSectionName(Guid sid, string name)
        {

            var st = this.GetSection(sid);

            if (st.Sections.Name.Equals(name))
            {
                return false;
            }

            st.Sections.Name = name;

            return true;


        }

        internal PageValue AddPages(Guid sectionId,Guid pid, string name, string content)
        {

            var st = this.GetSection(sectionId);
            var page = new PageValue(sectionId, this.Id,pid, name);

            st.AddPages(page);


            return page;

        }


        SectionValue GetSection(Guid sid)
        {
            if (this.HasSection() == false)
            {
                throw new NotesValidateExceptions("该笔记不包含分区");
            }

            var st = this.Sections.FirstOrDefault(s => s.Sections.Id == sid);

            Prosecutor.NotNull(st);

            return st;

        }

        internal bool UpdatePageName(Guid sid, Guid pid, string name)
        {
            var st = this.GetSection(sid);

            return st.UpdatePageName(pid, name);
        }

        internal SectionValue DelSection(Guid sId)
        {
            var section = this.Sections.FirstOrDefault(s => s.Sections.Id == sId);
            Prosecutor.NotNull(section);
            this.Sections.Remove(new SectionValue(new NotesSectionEntity { Id = sId }));

            return section;
        }
    }

}
