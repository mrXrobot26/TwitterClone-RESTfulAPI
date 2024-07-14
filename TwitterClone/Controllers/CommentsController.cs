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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpPost("AddComment"), Authorize]
        public async Task<IActionResult> AddComment(int PostId , [FromBody] PostCommentDTO postCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid input data" }, null, false);
                return BadRequest(_response);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(_response);
            }

            var postComment = _mapper.Map<PostComment>(postCommentDTO);
            postComment.ApplicationUserId = userId;
            postComment.PostId = PostId;

            await _unitOfWork.PostComment.AddCommentAsync(postComment);
            await _unitOfWork.SaveAsync();

            _response.SetResponseInfo(HttpStatusCode.Created, new List<string> { "Comment added successfully" }, postCommentDTO, true);
            return Ok(_response);
        }

        [HttpDelete("DeleteComment/{commentId}"), Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized" }, null, false);
                return Unauthorized(_response);
            }

            try
            {
                await _unitOfWork.PostComment.DeleteCommentAsync(commentId, userId);
                await _unitOfWork.SaveAsync();
                _response.SetResponseInfo(HttpStatusCode.OK, new List<string> { "Comment deleted successfully" }, null, true);
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.SetResponseInfo(HttpStatusCode.NotFound, new List<string> { ex.Message }, null, false);
                return NotFound(_response);
            }
        }

        [HttpGet("GetCommentsByPost/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var postComments = await _unitOfWork.PostComment.GetAllAsync(pc => pc.PostId == postId);
            var postCommentDTOs = _mapper.Map<List<PostCommentDTO>>(postComments);

            _response.SetResponseInfo(HttpStatusCode.OK, null, postCommentDTOs, true);
            return Ok(_response);
        }
    }
}
