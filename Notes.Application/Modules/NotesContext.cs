using Microsoft.AspNetCore.Http;
using Notes.infrastructure;
using Notes.infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Application.Modules
{
    public class NotesContext : INotesHttpContext
    {

        private string session;

        private IHttpContextAccessor _http;


        public const string HeaderToken = "Author";

        public NotesContext(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string Session
        {
            get
            {
                if (String.IsNullOrEmpty(session))
                {
                    session = _http.HttpContext.Request.Headers[HeaderToken].SingleOrDefault();
                }
                if (String.IsNullOrEmpty(session))
                {
                    throw new NotesValidateExceptions("session token lost");
                }
                return session;
            }
        }//throw new NotImplementedException();
    }
}
