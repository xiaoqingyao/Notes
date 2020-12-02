using LinqToDB;
using Microsoft.Extensions.Options;
using Notes.Data;
using Notes.Data.EFProvider;
using Notes.Data.Entities;
using Notes.Domain.Core.NotesBody;
using Notes.infrastructure.Validators;
using Notes.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Core.Factories
{
    public class BodyLoader : IBodyLoader
    {

        private readonly ReaderConnection _reader;
        private readonly IOptions<DomainOptions> _opt;
        private readonly ICachingProvider _cachor;

        public BodyLoader(IOptions<DomainOptions> opt, ICachingProvider cachor, ReaderConnection reader)
        {
            _opt = opt;
            _cachor = cachor;
            _reader = reader;
        }

        string key(Guid cid)
        {
            return string.Concat(this._opt.Value.BodyCachePrefix, cid);
        }

        public async Task<Body> LoadAsync(Guid cid)
        {
            var k = this.key(cid);
            var rev = await this._cachor.Get<Body>(k);
            if (rev == null)
            {
                rev = await this.FromDb(cid);
            }

            if (rev == null)
            {
                return null;
            }

            await this.Save(rev);

            return rev;
        }


        public Task Save(Body body)
        {
            var k = this.key(body.Id);
            return this._cachor.SetAsync(k, body);
        }

        public Task<bool> Del(Guid gid)
        {
            var k = this.key(gid);
            return this._cachor.Remove(k);
        }

        async Task<Body> FromDb(Guid cid)
        {
            var cnt = await this._reader.PageContent
                            .Qa()
                            .Where(c => c.Id == cid)
                           .FirstOrDefaultAsync();
            if (cnt == null)
            {
                return null;
            }
            return new Body { 
                Id = cid,
                Content = cnt.Content
            };

        }

    }
}
