using AutoMapper;
using Azure;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.MyModels.ProfileModels;
using Models.Response;
using System.Net;

namespace TwitterClone.Controllers
{
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
        public async Task<IActionResult> AddPost(string userName, [FromBody] PostDTO postDTO)
        {
            var userProfile = await db.Profile.GetAsync(x=>x.UserName == userName);
            var response = new APIResponse();
            if (userProfile == null) {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "There is no user like this." }, null, false);
                return NotFound(response);
            }

            var post = mapper.Map<Post>(postDTO);
            post.UserName = userProfile.UserName;
            post.ProfileId = userProfile.Id;
            post.Name = userProfile.Name;


            await db.Post.AddAsync(post);
            await db.SaveAsync();
            response.SetResponseInfo(HttpStatusCode.Created, null, post);
            return Ok(response);
        }






    }
}
