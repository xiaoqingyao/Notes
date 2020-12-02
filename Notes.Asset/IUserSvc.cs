using Notes.Asset.ApiDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Asset
{
    public interface IUserSvc
    {

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<UserItem> GetUserAsync(string session);



        /// <summary>
        /// 获取用户组织结构...
        /// </summary>
        /// <returns></returns>
        Task<OrgItem> GetOrg(string session);
        UserItem GetUser(string session);
    }
}
