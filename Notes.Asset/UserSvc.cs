using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Notes.Asset.ApiDTO;
using Notes.infrastructure.Exceptions;
using Notes.infrastructure.Validators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Asset
{
    public class UserSvc : IUserSvc
    {

        private readonly IOptions<ApiOptions> _opt;

        public UserSvc(IOptions<ApiOptions> opt)
        {
            _opt = opt;
        }

        public async Task<OrgItem> GetOrg(string session)
        {
            var rev = await this.GetAsync<OrgDTO>(session, this._opt.Value.UserOrganizationUriPart);

            return rev.Result.FirstOrDefault();

        }

        public async Task<UserItem> GetUserAsync(string session)
        {

            //await Task.CompletedTask;

            //return new UserItem
            //{
            //    UserID = 1222,
            //    RealName = "hello"
            //};

            var rev = await this.GetAsync<UserInfoDTO>(session, this._opt.Value.UserSessionUriPart);

            var user =  rev.Result.FirstOrDefault();

            Prosecutor.NotNull(user);


            return user;
           
        }

        public UserItem GetUser(string session)
        {

            //await Task.CompletedTask;

            //return new UserItem
            //{
            //    UserID = 1222,
            //    RealName = "hello"
            //};

            var rev = this.Get<UserInfoDTO>(session, this._opt.Value.UserSessionUriPart);

            var user = rev.Result.FirstOrDefault();

            Prosecutor.NotNull(user);


            return user;

        }



        private async Task<T> GetAsync<T>(string session, string urlPart, Action<IRestRequest> rqBuilder = null) where T : class, new()
        {

            IRestResponse rs = null;

            try
            {
                rs = await this.Client(urlPart).ExecuteAsync(this.Rq(session, rqBuilder));

                var rev = JsonConvert.DeserializeObject<T>(rs.Content);

             
                return rev;

            }
            catch (Exception)
            {

                throw new Notes3PartApiException($"用户接口异常:{rs?.Content}");
            }
        }


        private T Get<T>(string session, string urlPart, Action<IRestRequest> rqBuilder = null) where T : class, new()
        {

            IRestResponse rs = null;

            try
            {
                rs = this.Client(urlPart).Execute(this.Rq(session, rqBuilder));

                var rev = JsonConvert.DeserializeObject<T>(rs.Content);


                return rev;

            }
            catch (Exception)
            {

                throw new Notes3PartApiException($"用户接口异常:{rs?.Content}");
            }
        }


        private IRestRequest Rq(string session, Action<IRestRequest> rqBuild)
        {
            var ret = new RestRequest(Method.POST).AddParameter("session", session);
            rqBuild?.Invoke(ret);
            return ret;
        }

        private IRestClient Client(string urlPart)
        {
            return new RestClient(string.Concat(this._opt.Value.UserHostBase, urlPart));
        }


    }
}
