using LinqToDB.Extensions;
using LinqToDB.Tools;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using Notes.Domain.Core.Users.Factories;
using Notes.Events;
using Notes.Events.CatalogEvent;
using Notes.Events.CourseEvents;
using Notes.Events.Events;
using Notes.infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Users
{
    public class UserObj
    {



        public UserObj()
        {
        }


        public string UserId { set; get; }


        [JsonIgnore]
        public IList<INotesEvent> Events { get; private set; }


        internal void AddEvent(INotesEvent @evnet)
        {
            if (this.Events == null)
            {
                this.Events = new List<INotesEvent>();
            }
            this.Events.Add(evnet);
        }

        internal bool DelPage(Guid sectionId, Guid pageId)
        {
            if (this.HasCourse() == false)
            {
                return false;
            }
            var item = this.Course.FirstOrDefault(c => c.SectionId == sectionId);
            if (item == null)
            {
                return false;
            }
            if (item.TaskToPage == null)
            {
                return false;
            }
            var pages = item.TaskToPage.FirstOrDefault(kv => kv.Value == pageId);
            if (pages.IsNull(p => p.Value))
            {
                return false;
            }

            if(item.TaskToPage.Remove(pages.Key) == false)
            {
                return false;
            }


            this.AddEvent(new CourseTaskPageRemoved
            {
                Creator = this.UserId,
                PageId = pageId
            });

            return true;
        }

        public bool HasCourse()
        {
            if (this.Course == null || this.Course.Count == 0)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteSection(Guid sectionId)
        {
            if (this.Course == null || this.Course.Count == 0)
            {
                return false;
            }

            var item = this.Course.FirstOrDefault(c => c.SectionId == sectionId);
            if (item == null)
            {
                return false;
            }

            this.Course.Remove(new D4LValue
            {
                ClassId = item.ClassId,
                SectionId = item.SectionId
            });

            this.AddEvent(new CourseDsNotesRemoved
            {
                ClassId = item.ClassId,
                DsId = item.DsId,
                Creaotor = this.UserId
            }); ;

            return true;
        }

        public CatalogData Catalog { get; set; }



        public IList<D4LValue> Course { get; set; }


        public (Guid, Guid) CourseRelation(string classId, Guid dsId, Guid taskId)
        {
            var binder = new BindCourse(this, classId, dsId, taskId);

            binder.Bind();

            return (binder.SecitonId, binder.PageId);
        }

        public bool HasEvent()
        {
            return this.Events != null && this.Events.Count > 0;
        }



        void AddCatalogValue(CatalogValue parent, CatalogValue item)
        {

            item.Count = 1;
            parent.Count = 1;


            //第一次创建
            if (Catalog == null)
            {
                Catalog = new CatalogData()
                {
                    Grade = new Dictionary<string, CatalogValue>(),
                    Subject = new Dictionary<string, IList<CatalogValue>>()
                };
            }




            //是否使用过Grade....

            if (Catalog.Grade.TryGetValue(parent.Code, out CatalogValue value))
            {

                //如果已经使用过
                value.Count += 1;


                this.AddEvent(new CatalogItemCountChanged
                {
                    UserId = this.UserId,
                    Code = parent.Code,
                    Count = value.Count
                }); ;


            }
            else
            {
                Catalog.Grade[parent.Code] =
                    new CatalogValue
                    {
                        Code = parent.Code,
                        Name = parent.Name,
                        Count = 1
                    };


                this.AddEvent(new CatalogItemReferenced
                {
                    Count = 1,
                    Code = parent.Code,
                    Name = parent.Name,
                    Creator = this.UserId,
                    ParentCode = ""
                });


            }

            if (Catalog.Subject.TryGetValue(parent.Code, out IList<CatalogValue> subjVal))
            {
                var subj = subjVal.FirstOrDefault(c => c.Code == item.Code);
                if (subj == null)
                {
                    subjVal.Add(new CatalogValue
                    {
                        Code = item.Code,
                        Count = 1,
                        Name = item.Name
                    });

                    this.Events.Add(new CatalogItemReferenced
                    {
                        Code = item.Code,
                        Count = 1,
                        Creator = this.UserId,
                        Name = item.Name,
                        ParentCode = parent.Code
                    });
                }
                else
                {
                    subj.Count += 1;

                    this.AddEvent(new CatalogItemCountChanged
                    {
                        Count = subj.Count,
                        UserId = this.UserId,
                        Code = subj.Code
                    });
                }
            }
            else
            {
                this.Catalog.Subject[parent.Code] = new List<CatalogValue> { new CatalogValue {

                    Code = item.Code,
                    Count = 1,
                    Name = item.Name

                } };

                this.AddEvent(new CatalogItemReferenced
                {
                    Code = item.Code,
                    Count = 1,
                    Creator = this.UserId,
                    Name = item.Name,
                    ParentCode = parent.Code
                });

            }

        }

        //删除笔记后，相应目录对应的笔记数减少....
        internal void CatalogItemSubstruct(string catalogCode, string gradeCode)
        {
            if (this.Catalog.Grade.TryGetValue(gradeCode, out CatalogValue grade))
            {

                if (grade.Count > 0)
                {
                    grade.Count -= 1;
                }

                this.AddEvent(new CatalogItemCountChanged
                {
                    UserId = this.UserId,
                    Count = grade.Count,
                    Code = gradeCode
                });
            }
            if (this.Catalog.Subject.TryGetValue(gradeCode, out IList<CatalogValue> subj))
            {
                var item = subj.FirstOrDefault(c => c.Code == catalogCode);
                if (item != null)
                {
                    if (item.Count > 0)
                    {
                        item.Count -= 1;
                    }
                  
                    this.AddEvent(new CatalogItemCountChanged
                    {
                        UserId = this.UserId,
                        Code = item.Code,
                        Count = item.Count
                    });
                }
            }


        }

        internal void ReferenceCatalog(string catalogCode, string catalogName, string gradecode, string gradeName)
        {
            this.AddCatalogValue(new CatalogValue
            {
                Name = gradeName,
                Code = gradecode
            }
            , new CatalogValue
            {
                Name = catalogName,
                Code = catalogCode
            });

        }
    }




}
