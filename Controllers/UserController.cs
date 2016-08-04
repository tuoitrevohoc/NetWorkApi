using Microsoft.AspNetCore.Mvc;
using NetWorkApi.Controllers.Base;
using NetWorkApi.Models;
using NetWorkApi.Repositories;

namespace NetWorkApi.Controllers
{


    /// <summary>
    /// 
    /// </summary>
    [Route("/api/user")]
    public class UserController: EditableResourceController<AppUser>
    {

        /// <summary>
        /// The repository
        /// </summary>
        /// <param name="repository"></param>
        public UserController(IRepository<AppUser> repository): base(repository)
        {

        }
    }
}
