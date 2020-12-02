using AspectCore.DynamicProxy.Parameters;
using Newtonsoft.Json;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.VO
{
    public class CatalogVO
    {


        [JsonProperty(Order = 0)]
        public IList<CatalogItem> Term { get; set; }

        [JsonProperty(Order = 1)]
        public IList<CatalogItem> Subject { get; set; }
    }

    public class CatalogItem
    {
        public string Idx { get; set; }


        public string Name { get; set; }


        public string Prop { get; set; }

        public int Pos
        {
            get
            {
                return int.Parse(Idx);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is CatalogItem val)
            {

                if (val.Idx == this.Idx)
                {
                    return true;
                }

            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.Idx.GetHashCode());
        }

    }

    public class CatalogComparer : IComparer<CatalogItem>
    {
        public int Compare(CatalogItem x, CatalogItem y)
        {

            if (x.Pos > y.Pos)
            {
                return 1;
            }
            return -1;

           // throw new NotImplementedException();
        }
    }



}
