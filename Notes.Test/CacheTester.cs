using Autofac;
using Autofac.Core;
using Notes.Infrastructure.Caching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test
{

    [TestFixture]
    public class CacheTester
    {


        [Test]
        public async Task GetSetTest()
        {
            var c = ContainerHelper.Builder();

            ICachingProvider _cp = c.Resolve<ICachingProvider>();

            var val = await _cp.Get("a", async () =>
            {

                await Task.CompletedTask;

                return "aab";
            });


            Assert.AreEqual(val, "aab");

        }


    }
}
