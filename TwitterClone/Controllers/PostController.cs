using AutoMapper;
using Azure;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Post;
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

        [HttpPost("AddTweet") , Authorize]
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

        [HttpDelete("DeleteTweet"), Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var response = new APIResponse();
            var post = await db.Post.GetAsync(x => x.PostId == id);

            if (post == null)
            {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "Post not found" }, null, false);
                return NotFound(response);
            }

            var appUserId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appUserId == null || post.ApplicationUserId != appUserId)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(response);
            }

            await db.Post.DeleteAsync(post);
            await db.SaveAsync();

            response.SetResponseInfo(HttpStatusCode.OK, new List<string> { "Post deleted successfully" }, null, true);
            return Ok(response);
        }

        [HttpGet("GetPostDetails")]
        public async Task<IActionResult> GetPostInDetails(int id)
        {
            var response = new APIResponse();
            var post = await db.Post.GetAsync(x => x.PostId == id);

            if (post == null)
            {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "Post not found" }, null, false);
                return NotFound(response);
            }
            var postDto = mapper.Map<PostDetailsDTO>(post);
            response.SetResponseInfo(HttpStatusCode.OK , null , postDto ,true);
            return Ok(response);

        }


        [HttpPut("UpdateTweet"), Authorize]
        public async Task<IActionResult> UpdatePost(int id , [FromBody] PostUpdatesDTO postUpdatesDTO)
        {
            var response = new APIResponse();
            var post = await db.Post.GetAsync(x => x.PostId == id);

            if (post == null)
            {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { "Post not found" }, null, false);
                return NotFound(response);
            }
                
            var appUserId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appUserId == null || post.ApplicationUserId != appUserId)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(response);
            }

            post.postContant = postUpdatesDTO.postContant;

            await db.Post.UpdateAsync(post);
            await db.SaveAsync();

            response.SetResponseInfo(HttpStatusCode.OK, new List<string> { "Post updated successfully" }, postUpdatesDTO, true);
            return Ok(response);
        }


    }
}
