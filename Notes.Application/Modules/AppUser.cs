using LinqToDB.DataProvider.SapHana;
using LinqToDB.Tools;
using Notes.Asset;
using Notes.Asset.ApiDTO;
using Notes.infrastructure;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Notes.Application.Modules
{
    public class AppUser : IAppUser
    {


        private readonly INotesHttpContext _ctx;
        private readonly IUserSvc _userSvc;

        public const string OrgRegion = "区域";

        public const string OrgSchool = "学校";

        public const string OrgSection = "学段";

        public const string OrgGrade = "年级";

        public AppUser(INotesHttpContext ctx, IUserSvc userSvc)
        {

            _ctx = ctx;
            _userSvc = userSvc;
        }

        private string _session;

        public string Session
        {
            get
            {
                if (String.IsNullOrEmpty(_session))
                {
                    this._session = this._ctx.Session;
                }
                return _session; //this._ctx.Session;
            }
        }//throw new NotImplementedException();






        private UserItem user;

        public string UserId
        {
            get
            {
                if (user == null)
                {
                    this.user = _userSvc.GetUser(this.Session);//.ConfigureAwait(false).GetAwaiter().GetResult();
                }
                return user.UserID.ToString();
            }
        }



        public string UserName
        {
            get
            {
                if (user == null)
                {
                    this.user = _userSvc.GetUser(this.Session);//.ConfigureAwait(false).GetAwaiter().GetResult();
                }
                return user.RealName;
            }
        }



        public string Role => throw new NotImplementedException();

        public bool IsTreacher()
        {
            throw new NotImplementedException();
        }

        public void SetSession(string session)
        {


        }

        public IDictionary<string, OrgValue> Org { get; set; }


        public OrgValue Region { get; set; }

        public IList<OrgValue> Schools { get; set; }


        public IList<OrgValue> Section { get; set; }


        public IList<OrgValue> Grade { get; set; }

        public IList<OrgValue> Class { get; set; }


        public IList<OrgValue> AddOrgItem(IList<OrgValue> source, OrgItem item, string parentCode, int deep)
        {
            if (source == null)
            {
                source = new List<OrgValue>();
            }
            source.Add(new OrgValue
            {
                Code = item.Idx.ToString(),
                Name = item.Name,
                Deep = deep,
                Parent = parentCode
            });
            return source;
        }


        public async Task<string> ClassIdAsync()
        {
            await this.LoadOrgAsync();

            return this.Class?.FirstOrDefault().Code;
        }



        //public 


        private bool orgLoaded = false;

        private async Task LoadOrgAsync()
        {

            if (orgLoaded)
            {
                return;
            }

            var rev = await _userSvc.GetOrg(this.Session);

            if (rev == null)
            {
                this.orgLoaded = true;
                return;
            }

            this.Region = new OrgValue
            {
                Code = rev.Idx.ToString(),
                Name = rev.Name,
                Deep = 1

            };


            if (rev.ChildList == null || rev.ChildList.Count == 0)
            {
                this.orgLoaded = true;
                return;
            }

            //学校...
            foreach (var item in rev.ChildList)
            {

                this.Schools = this.AddOrgItem(this.Schools, item, rev.Idx.ToString(), 2);

                // 学段
                if (item.HasChildern() == false)
                {
                    continue;

                }

                foreach (var sectionItem in item.ChildList)
                {
                    this.Section = this.AddOrgItem(this.Section, sectionItem, item.Idx.ToString(), 3);

                    if (sectionItem.HasChildern() == false)
                    {
                        continue;
                    }

                    //年级....
                    foreach (var grade in sectionItem.ChildList)
                    {
                        this.Grade = this.AddOrgItem(this.Grade, grade, sectionItem.Idx.ToString(), 4);

                        if (grade.HasChildern() == false)
                        {
                            continue;
                        }
                        foreach (var classItem in grade.ChildList)
                        {
                            this.Class = this.AddOrgItem(this.Class, classItem, grade.Idx.ToString(), 5);
                        }
                    }
                }

            }

            this.orgLoaded = true;



        }

    }
}
