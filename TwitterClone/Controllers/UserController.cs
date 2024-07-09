using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.User;
using Models.Response;
using System.Net;

namespace TwitterClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private APIResponse response;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            response = new APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var responseLogin = await unitOfWork.User.Login(model);
            if (responseLogin.User == null || String.IsNullOrEmpty(responseLogin.Token))
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Username or password is incorrect" }, null, false);
                return BadRequest(response);
            }
            response.SetResponseInfo(HttpStatusCode.OK, null, responseLogin, true);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            bool isUniqe = await unitOfWork.User.IsUniqe(model.UserName);
            if (!isUniqe)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "User Name Already Exist" }, null, false);
                return BadRequest(response);
            }
            var user = await unitOfWork.User.Register(model);
            if (user == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Error While Registering" }, null, false);
                return BadRequest(response);
            }
            response.SetResponseInfo(HttpStatusCode.OK, null, user, true);
            return Ok(response);

        }


    }
}
