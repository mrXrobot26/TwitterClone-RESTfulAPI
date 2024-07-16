using AutoMapper;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.PostComment;
using Models.MyModels.PostFolder;
using Models.Response;
using System.Net;
using System.Security.Claims;

namespace TwitterClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;
        private readonly APIResponse response;

        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            db = unitOfWork;
            this.mapper = mapper;
            response = new APIResponse();
        }

        [HttpPost("AddComment"), Authorize]
        public async Task<IActionResult> AddComment(int PostId , [FromBody] AddPostCommentDTO postCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid input data" }, null, false);
                return BadRequest(response);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(response);
            }

            var postComment = mapper.Map<PostComment>(postCommentDTO);
            postComment.ApplicationUserId = userId;
            postComment.PostId = PostId;

            await db.PostComment.AddCommentAsync(postComment);
            await db.SaveAsync();

            response.SetResponseInfo(HttpStatusCode.Created, new List<string> { "Comment added successfully" }, postCommentDTO, true);
            return Ok(response);
        }

        [HttpDelete("DeleteComment/{commentId}"), Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(response);
            }

            try
            {
                await db.PostComment.DeleteCommentAsync(commentId, userId);
                await db.SaveAsync();
                response.SetResponseInfo(HttpStatusCode.OK, new List<string> { "Comment deleted successfully" }, null, true);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { ex.Message }, null, false);
                return NotFound(response);
            }
        }

    }
}
