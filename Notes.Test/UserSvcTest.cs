using Autofac;
using Autofac.Core;
using Notes.Asset;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test
{

    [TestFixture]
    public class UserSvcTest
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetUserTester()
        {
            var c = ContainerHelper.Builder();

            var us = c.Resolve<IUserSvc>();

            var u = await us.GetUserAsync("05986DDB458B8F10A31D59B613D");

            Assert.NotNull(u);

        }


        

    }
}
