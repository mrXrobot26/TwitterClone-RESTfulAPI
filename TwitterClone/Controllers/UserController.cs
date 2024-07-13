using AutoMapper;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.AppUsers;
using Models.DTOs.User;
using Models.MyModels.App;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
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
        public async Task<IActionResult> Register([FromBody] ApplicationUserToAddUserDTO model)
        {
            var response = new APIResponse();

            if (model == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Registration data is null." }, null, false);
                return BadRequest(response);
            }

            bool isUnique = await unitOfWork.User.IsUniqueUserName(model.UserName);
            if (!isUnique)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "User Name Already Exists" }, null, false);
                return BadRequest(response);
            }

            var user = mapper.Map<ApplicationUser>(model);


            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    response.SetResponseInfo(HttpStatusCode.BadRequest, result.Errors.Select(e => e.Description).ToList(), null, false);
                    return BadRequest(response);
                }


                response.SetResponseInfo(HttpStatusCode.Created, null, user, true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetResponseInfo(HttpStatusCode.InternalServerError, new List<string> { "An error occurred while registering the user.", ex.Message }, null, false);
                return StatusCode(500, response);
            }
        }



    }
}
