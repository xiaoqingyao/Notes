using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Application.Modules
{
    public  class OrgValue
    {
        public string Code { get; set; }

        public string Name { get; set; }


        public int Deep { get; set; }

        public string Parent { get; set; }

        public string Prop { get; set; }
    }
}
