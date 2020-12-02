using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Notes.Application.Controllers;
using Notes.Application.Modules.Factories;
using Notes.Domain.Core;
using Notes.Domain.Core.Factories;
using Notes.Domain.Core.Users;
using Notes.Domain.DTOS;
using Notes.Domain.VO;
using Notes.infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test
{
    [TestFixture]
    public class NotesTester
    {

        [Test]
        public async Task CreateTest()
        {
            var dto = new CreateNotesDTO
            {
                CatalogName = "/小学/语文/人教版/一年级上",
                CatalogCode = "/1/1/2/1",
                Content = "",
                Section = new NotesComponent
                {
                    Id = Guid.Parse("809953A6-EFF7-45E1-BAC7-477D2D0332A9"),
                    Value = "我是一个任务"
                },
                Page = new NotesComponent
                {
                    Id = Guid.Parse("13CF28FC-25DA-4B86-96E7-FD61DA29734C"),
                    Value = "我是一个分页"
                },
            };

            var c = ContainerHelper.Builder();

            var noteSvc = c.Resolve<INotesSvc>();

            var nc = this.NC(); //new NotesController(noteSvc ,c.Resolve<IBodySvc>()
                              //           ,c.Resolve<IEntry>()
                               //          ,c.Resolve<);


            var id = await nc.CreateNotes(dto);

            Assert.IsNotNull(id);

        }


        [Test]
        public async Task CreateNotesTest()
        {

            string name = "I'm a note";

            var id = await this.CreateNotes(name);

            var vo = await this.Get(id);

            Assert.AreEqual(vo.Name, name);

            name = "I'm a new note";

            var nc = this.NC();

            //更新名称...
            await nc.UpdateNotesName(new UpdatePropDTO
            {
                Id = id,
                Body = name
            });

            vo = await this.Get(id);

            Assert.AreEqual(vo.Name, name);

            name = "I'm a section..";

            //添加分区
            var rev = await nc.CreateSection(new UpdatePropDTO
            {
                Id = id,
                Body = name
            });

            vo = await this.Get(id);

            Assert.That(vo.Sections.Any(s => s.Id == rev.Data));


            name = "I'm new section";

            //修改分区名称
            await nc.UpdateSectionName(new UpdateSectionDTO
            {
                Id = rev.Data,
                NotesId = id,
                Body = name
            });

            vo = await this.Get(id);

            var section = vo.Sections.First();

            Assert.AreEqual(section.Name, name);

            name = "I'm a page...";

            //添加页
            var pid = await nc.CreatePage(new CreatePageDTO
            {
                Id = rev.Data,
                NotesId = id,
                Body = name
            });

            vo = await this.Get(id);

            Assert.AreEqual(name, vo.Sections.First().Pages.First().Name);


            name = "I'm a new page...";

            //修改页名称
            await nc.UpdatePageName(new UpdatePageDTO
            {
                Id = pid.Data,
                NotesId = id,
                SectionId = rev.Data,
                Body = name
            });

            vo = await this.Get(id);


            Assert.AreEqual(name, vo.Sections.First().Pages.First().Name);

        }


        public NotesController NC()
        {
            var c = ContainerHelper.Builder();

            var noteSvc = c.Resolve<INotesSvc>();

            var nc = new NotesController(noteSvc
                                        ,c.Resolve<IBodySvc>()
                                        ,c.Resolve<IEntry>()
                                        ,c.Resolve<IUserRootSvc>()
                                        ,c.Resolve<IAppUser>());
            return nc;
        }


        public async Task<Guid> CreateNotes(string name)
        {

            var dto = new CreateNotesDTO
            {
                CatalogName = "/小学/语文/人教版/一年级上",
                CatalogCode = "/1/1/2/1",
                Name = name
            };

            var c = ContainerHelper.Builder();

            var noteSvc = c.Resolve<INotesSvc>();

            var nc = this.NC(); //new NotesController(noteSvc);


            var id = await nc.CreateNotes(dto);

            return id.Data;

        }

        public async Task<NotesVO> Get(Guid id)
        {
            var c = ContainerHelper.Builder();

            var qc = new NotesQueryController(c.Resolve<INotesQuerySvc>()
                ,c.Resolve<IBodySvc>()
                ,c.Resolve<IAppUser>()
                ,c.Resolve<IUserLoader>());

            var vo = await qc.Get(id.ToString());

            return vo.Data;

        }



    }
}
