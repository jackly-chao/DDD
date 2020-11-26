using CApplication.IService;
using CApplication.ViewModel;
using CCommon;
using CCommon.Attributes;
using CCommon.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CAPI.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [Authorize(Policy = "ApiScope")]
        [HttpGet]
        public async Task<Response<List<UserViewModel>>> Get()
        {
            return new Response<List<UserViewModel>> { Data = await _userService.GetAllAsync() };
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [CustomRoute(ApiVersionEnum.v2)]
        [HttpGet]
        public async Task<Response<List<UserViewModel>>> Get_v2()
        {
            return new Response<List<UserViewModel>> { Data = await _userService.GetAllAsync() };
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="viewModel">用户视图模型</param>
        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] UserViewModel viewModel)
        {
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// 根据ID删除用户
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
