using Notes.Domain.Core.Users;
using Notes.Domain.VO;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;

namespace Notes.Domain.Core
{
    public class CatalogHelper
    {

        private readonly UserObj uobj;

        private readonly string termIdx;
        private readonly string subIdx;

        public CatalogHelper(UserObj uobj, string termIdx, string subIdx)
        {
            this.uobj = uobj;
            this.termIdx = termIdx;
            this.subIdx = subIdx;

            this.addAllitem();
        }

        public IList<CatalogItem> Term { get; private set; }

        public IList<CatalogItem> Subject { get; private set; }

        private IDictionary<string, CatalogValue> termData;

        private IDictionary<string, IList<CatalogValue>> subData;


        private IList<CatalogItem> AddItem(IList<CatalogItem> source, CatalogItem item, string code)
        {
            if (item.Idx == code)
            {
                item.Prop = "selected";
            }
            if (source == null)
            {
                source = new List<CatalogItem>
                {
                    item
                };
                return source;
            }
            if (source.Any(s => s.Idx == item.Idx))
            {
                return source;
            }

            source.Add(item);
            return source;

        }

        private void AddTermItem(CatalogItem item)
        {
            this.Term = this.AddItem(this.Term, item, termIdx);
        }

        private void AddSubItem(CatalogItem item)
        {
            this.Subject = this.AddItem(this.Subject, item, subIdx);
        }



        private void addAllitem()
        {
            this.AddTermItem( new CatalogItem
            {
                Idx = "0",
                Name = "全部"
            });

            this.AddSubItem(new CatalogItem
            {
                Idx = "0",
                Name = "全部"
            });
        }




        public CatalogHelper Filter()
        {

            this.termData = this.uobj.Catalog.Grade;

            this.subData = this.uobj.Catalog.Subject;




            if (termIdx == "0" && subIdx == "0")
            {
                this.ReturnAll();
            }

            if (termIdx != "0" && subIdx != "0")
            {

                this.FiterBySubIdx();

            }

            if (termIdx == "0" && subIdx != "0")
            {
                this.ShowAllSubj();
                //this.FiterBySubIdx();
            }

            if (termIdx != "0" && subIdx == "0")
            {

                // 返回所有年级

                this.FilterByTerm();

            }

            return this;
        }


        public CatalogHelper OrderBy()
        {


            this.Subject = this.Subject.OrderBy(s => s.Pos).ToList();
            this.Term = this.Term.OrderBy(t => t.Pos).ToList();

            var sother = this.Subject.Where(s => s.Idx == "-2").FirstOrDefault();

            if (sother != null)
            {
                this.Subject.Remove(sother);


                this.Subject.Add(sother);
            }

            var tOther = this.Term.Where(s => s.Idx == "-1").FirstOrDefault();

            if (tOther != null)
            {
                this.Term.Remove(tOther);
                this.Term.Add(tOther);
            }
            return this;

        }

        private void ShowAllSubj()
        {

            HashSet<string> termHash = new HashSet<string>();

            foreach (var item in this.subData)
            {
                foreach (var sv in item.Value)
                {
                    if (sv.Count <= 0)
                    {
                        continue;
                    }
                    this.AddSubItem(new CatalogItem
                    {
                        Idx = sv.Code,
                        Name = sv.Name
                    });
                    if (sv.Code == subIdx)
                    {
                        termHash.Add(item.Key);
                    }
                }

            }

            foreach (var item in termHash)
            {
                if(this.termData.TryGetValue(item, out CatalogValue val))
                {
                    this.AddTermItem(new CatalogItem
                    {
                        Idx = val.Code,
                        Name = val.Name
                    });
                }
            }

        }

        private void ReturnAll()
        {
            foreach (var item in termData)
            {
                if (item.Value.Count <= 0)
                {
                    continue;
                }

                this.AddTermItem(new CatalogItem
                {
                    Idx = item.Value.Code,
                    Name = item.Value.Name
                });
            }

            foreach (var item in subData)
            {
                foreach (var vitem in item.Value)
                {
                    if (vitem.Count <= 0)
                    {
                        continue;
                    }
                    this.AddSubItem(new CatalogItem
                    {
                        Idx = vitem.Code,
                        Name = vitem.Name
                    });
                }
            }
        }


        private void FilterByTerm()
        {
            foreach (var item in this.termData)
            {
                if (item.Value.Count <= 0)
                {
                    continue;
                }

                this.AddTermItem(new CatalogItem
                {
                    Idx = item.Value.Code,
                    Name = item.Value.Name
                });
            }


            // 只包含当前年级的学科...

            if (this.subData.TryGetValue(termIdx, out IList<CatalogValue> value))
            {
                foreach (var vItem in value)
                {
                    if (vItem.Count <= 0)
                    {
                        continue;
                    }
                    this.AddSubItem(new CatalogItem
                    {
                        Idx = vItem.Code,
                        Name = vItem.Name
                    });
                }
            }
        }


        private void FiterBySubIdx()
        {

            foreach (var item in subData)
            {
                if (item.Value.Any(c => c.Count > 0 && c.Code == subIdx) == false)
                {
                    continue;
                }

                if (termData.TryGetValue(item.Key, out CatalogValue value))
                {
                    this.AddTermItem(new CatalogItem
                    {
                        Idx = value.Code,
                        Name = value.Name

                    });
                }

                if (item.Key != termIdx)
                {
                    continue;
                }

                foreach (var vitem in item.Value)
                {
                    if (vitem.Count == 0)
                    {
                        continue;
                    }
                    this.AddSubItem(new CatalogItem
                    {
                        Idx = vitem.Code,
                        Name = vitem.Name
                    });
                }

              
            }

        }

    }
}
