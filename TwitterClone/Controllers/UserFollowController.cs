using AutoMapper;
using Azure;
using DataAcess.Repo.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Follower;
using Models.DTOs.User;
using Models.Response;
using System.Net;
using System.Security.Claims;

namespace TwitterClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserFollowController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("FollowUser"), Authorize]
        public async Task<IActionResult> FollowUser(string followedUserName)
        {
            var response = new APIResponse();
            var userToFollow = await _unitOfWork.User.GetAsync(x => x.UserName == followedUserName);
            if (userToFollow == null)
            {
                
                    response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid userName." }, null, false);
                    return BadRequest(response);
            }
            var followedUserId = userToFollow.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid user ID." }, null, false);
                return BadRequest(response);
            }

            await _unitOfWork.UserFollow.FollowUser(userId, followedUserId);
            var followAdd = new
            {
                Follower = userId,
                Following = followedUserId,
                msg ="Follow Added Sucsesfuly"
            };
            response.SetResponseInfo(HttpStatusCode.OK, new List<string> {  }, followAdd, true);
            return Ok(response);
        }

        [HttpPost("UnfollowUser"), Authorize]
        public async Task<IActionResult> UnfollowUser(string followedUserName)
        {
            var response = new APIResponse();
            var userToUnfollow = await _unitOfWork.User.GetAsync(x => x.UserName == followedUserName);
            if (userToUnfollow == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid username." }, null, false);
                return BadRequest(response);
            }

            var followedUserId = userToUnfollow.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid user ID." }, null, false);
                return BadRequest(response);
            }

            await _unitOfWork.UserFollow.UnfollowUser(userId, followedUserId);
            var followRemove = new
            {
                Unfollower = userId,
                Unfollowing = followedUserId,
                msg = "Follow Removed Sucsesfuly"


            };
            response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, followRemove, true);
            return Ok(response);
        }

        [HttpGet("IsFollowing"), Authorize]
        public async Task<IActionResult> IsFollowing(string followedUserName)
        {
            var response = new APIResponse();
            var userToCheck = await _unitOfWork.User.GetAsync(x => x.UserName == followedUserName);
            if (userToCheck == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid username." }, null, false);
                return BadRequest(response);
            }

            var followedUserId = userToCheck.Id;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "UnAuthorized." }, null, false);
                return BadRequest(response);
            }

            var isFollowing = await _unitOfWork.UserFollow.IsFollowing(userId, followedUserId);
            var followingStatus = new
            {
                UserId = userId,
                FollowingUserId = followedUserId,
                IsFollowing = isFollowing
            };
            response.SetResponseInfo(HttpStatusCode.OK, new List<string> { }, followingStatus, true);
            return Ok(response);
        }


        [HttpGet("MutualFollowers/{otherUserName}"), Authorize]
        public async Task<IActionResult> GetMutualFollowers(string otherUserName)
        {
            var response = new APIResponse();
            var otherUser = await _unitOfWork.User.GetAsync(x => x.UserName == otherUserName);
            if (otherUser == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid username." }, null, false);
                return BadRequest(response);
            }

            var otherUserId = otherUser.Id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "Invalid user ID." }, null, false);
                return BadRequest(response);
            }

            var mutualFollowers = await _unitOfWork.UserFollow.GetMutualFollowers(userId, otherUserId);
            var mutualFollowersDto = _mapper.Map<List<FollowerDTO>>(mutualFollowers);

            response.SetResponseInfo(HttpStatusCode.OK, null, mutualFollowersDto, true);
            return Ok(response);
        }






        [HttpGet("GetFollowers"), Authorize]
        public async Task<IActionResult> GetFollowers()
        {
            var response = new APIResponse();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized." }, null, false);
                return BadRequest(response);
            }
            var followers = await _unitOfWork.UserFollow.GetFollowers(userId);
            if (followers == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "No Followers." }, null, false);
                return BadRequest(response);
            }
            var FollowersDto = _mapper.Map<List<FollowerDTO>>(followers);

            response.SetResponseInfo(HttpStatusCode.OK, null, FollowersDto, true);
            return Ok(response);

        }
        [HttpGet("GetFollowing"), Authorize]
        public async Task<IActionResult> GetFollowing()
        {
            var response = new APIResponse();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized." }, null, false);
                return BadRequest(response);
            }
            var following = await _unitOfWork.UserFollow.GetFolloweing(userId);
            if (following == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "No Followers." }, null, false);
                return BadRequest(response);
            }
            var FollowingDto = _mapper.Map<List<FollowerDTO>>(following);

            response.SetResponseInfo(HttpStatusCode.OK, null, FollowingDto, true);
            return Ok(response);

        }

        

        [HttpGet("GetFollowersCount"), Authorize]
        public async Task<IActionResult> GetFollowersCount()
        {
            var response = new APIResponse();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized." }, null, false);
                return BadRequest(response);
            }
            var followersCont =  _unitOfWork.UserFollow.GetFollowersCount(userId);
            if (followersCont == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "No Followers." }, null, false);
                return BadRequest(response);
            }
            var followerCountObj = new
            {
                msg = $"you have : {followersCont}"
            };
            response.SetResponseInfo(HttpStatusCode.OK, null, followerCountObj, true);
            return Ok(response);

        }
        [HttpGet("GetFollowingCount"), Authorize]
        public async Task<IActionResult> GetFollowingCount()
        {
            var response = new APIResponse();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                response.SetResponseInfo(HttpStatusCode.Unauthorized, new List<string> { "Unauthorized." }, null, false);
                return BadRequest(response);
            }
            var followingCont = _unitOfWork.UserFollow.GetFollowingCount(userId);
            if (followingCont == null)
            {
                response.SetResponseInfo(HttpStatusCode.BadRequest, new List<string> { "No Followers." }, null, false);
                return BadRequest(response);
            }
            var followingCountObj = new
            {
                msg = $"you have : {followingCont}"
            };
            response.SetResponseInfo(HttpStatusCode.OK, null, followingCountObj, true);
            return Ok(response);

        }







    }


}

