using AutoMapper;
using Forum.Business.DTO;
using Forum.Business.Entities;
using Forum.Business.Filters;
using Forum.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaphaelSantos.Framework.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace Forum.Api.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]    
    public class PostController : Controller
    {
        private IPostService PostService { get; set; }
        private readonly IMapper Mapper;
        private int userId { get; set; }

        public PostController(IPostService postService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.PostService = postService;
            this.Mapper = mapper;

            this.userId = 0;

            if (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !string.IsNullOrEmpty(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                this.userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        [HttpGet]
        // GET: api/Post
        public IActionResult Get(PostFilter filter)
        {
            var list = PostService.List(filter);

            var result = Mapper.Map<IEnumerable<PostDTO>>(list.Items);

            return Ok(new PagedList<PostDTO>(result, list));
        }

        [Authorize]
        [HttpGet("{id}")]
        // GET: api/Post/5
        public IActionResult Get(int id)
        {
            var obj = PostService.GetById(id);

            if (obj == null)
                return NotFound();

            var mapped = Mapper.Map<Post, PostDTO>(obj);

            return Ok(mapped);
        }

        // POST: api/Post
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PostDTO postDTO)
        {
            var obj = Mapper.Map<PostDTO, Post>(postDTO);

            PostService.Add(obj);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        // PUT: api/Post/5
        public IActionResult Put([FromBody]PostDTO postDTO)
        {
            var obj = Mapper.Map<PostDTO, Post>(postDTO);

            var success = PostService.Update(obj, this.userId);

            if (!success)
                return NotFound();

            return Ok();

        }

        [Authorize]
        // DELETE: api/Post/5
        public IActionResult Delete(int id)
        {
            var success = PostService.Remove(id);

            return Ok();
        }

        [Authorize]
        //PUT: /Post/1/Activate
        [HttpPut]
        [Route("Post/{id:int}/Activate")]
        public IActionResult Activate(int id)
        {
            var success = PostService.Activate(id);

            if (!success)
                return NotFound();

            return Ok();
        }
        [Authorize]
        //PUT: /Post/1/Activate
        [HttpPut]
        [Route("Post/{id:int}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            var success = PostService.Deactivate(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
