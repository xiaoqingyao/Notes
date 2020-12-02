using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
     interface IEntity
    {

        int IndentityId { get; set; }


        Guid Id { get; set; }

         int Deleted { get; set; }


         DateTime? CreationTime { get; set; }// = DateTime.Now;


         DateTime? UpdateTime { get; set; }// = DateTime.Now;
    }
}
