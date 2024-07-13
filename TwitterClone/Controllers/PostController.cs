using AutoMapper;
using Azure;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.MyModels.App;
using Models.MyModels.ProfileModels;
using Models.Response;
using System.Net;
using System.Security.Claims;

namespace TwitterClone.Controllers
{

    //[Authorize] 
    [Route("api/[controller]")]

    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public PostController(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] PostDTO postDTO)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = new APIResponse();
            if (id == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(response);
            }


            var userProfile = await db.User.GetAsync(x => x.Id == id);
            if (userProfile == null)
            {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "User profile not found." }, null, false);
                return NotFound(response);
            }

            var post = mapper.Map<Post>(postDTO);
            post.ApplicationUserId = userProfile.Id;

            await db.Post.AddAsync(post);
            await db.SaveAsync();
            response.SetResponseInfo(HttpStatusCode.Created, null, postDTO);
            return Ok(response);
        }




    }
}
