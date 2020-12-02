using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.Core.Users
{


    public class CatalogData
    {
        public IDictionary<string, CatalogValue> Grade { get; set; }

        public IDictionary<string, IList<CatalogValue>> Subject { get; set; }




    }



    /// <summary>
    /// 用户创建笔记所使用的目录数据....
    /// </summary>
    public class CatalogValue
    {

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 包含笔记的数量
        /// </summary>
        public int Count { get; set; }


        public void SetCount(int number)
        {
            this.Count += number;
        }

        public override bool Equals(object obj)
        {
            if (obj is CatalogValue value)
            {
                if (value.Code == this.Code)
                {
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), this.Code.GetHashCode());
        }
    }

}
