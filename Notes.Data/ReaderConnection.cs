using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public class ReaderConnection : NotesConnection
    {
        public ReaderConnection(LinqToDbConnectionOptions<ReaderConnection> options) : base(ChangeType(options))
        {

        }
    }
}
