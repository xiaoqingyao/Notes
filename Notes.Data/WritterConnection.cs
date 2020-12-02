using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public class WritterConnection : NotesConnection 
    {
        public WritterConnection(LinqToDbConnectionOptions<WritterConnection> options) : base(ChangeType(options))
        {

        }

    }
}
