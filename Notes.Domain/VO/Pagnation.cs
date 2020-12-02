using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain.VO
{
    public class Pagnation<T>
    {
        public int ItemCount { get; set; }

        public IList<T> Data{ get; set; }
    }
}
