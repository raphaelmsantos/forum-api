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
    public class CommentController : Controller
    {
        private ICommentService CommentService { get; set; }
        private readonly IMapper Mapper;
        private int userId { get; set; }

        public CommentController(ICommentService commentService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.CommentService = commentService;
            this.Mapper = mapper;

            this.userId = 0;

            if (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !string.IsNullOrEmpty(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                this.userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }            
        }

        [HttpGet]
        // GET: api/Comment
        public IActionResult Get(CommentFilter filter)
        {
            var list = CommentService.List(filter);

            var result = Mapper.Map<IEnumerable<CommentDTO>>(list.Items);

            return Ok(new PagedList<CommentDTO>(result, list));
        }

        [HttpGet("{id}")]
        [Authorize]
        // GET: api/Comment/5
        public IActionResult Get(int id)
        {
            var obj = CommentService.GetById(id);

            if (obj == null)
                return NotFound();

            var mapped = Mapper.Map<Comment, CommentDTO>(obj);

            return Ok(mapped);
        }

        [HttpPost]
        [Authorize]
        // POST: api/Comment
        public IActionResult Post([FromBody]CommentDTO commentDTO)
        {
            var obj = Mapper.Map<CommentDTO, Comment>(commentDTO);

            CommentService.Add(obj);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        // PUT: api/Comment/5
        public IActionResult Put([FromBody]CommentDTO commentDTO)
        {
            var obj = Mapper.Map<CommentDTO, Comment>(commentDTO);

            var success = CommentService.Update(obj, this.userId);

            if (!success)
                return NotFound();

            return Ok();

        }

        [Authorize]
        // DELETE: api/Comment/5
        public IActionResult Delete(int id)
        {
            var success = CommentService.Remove(id);

            return Ok();
        }

        [Authorize]
        //PUT: /Comment/1/Activate
        [HttpPut]
        [Route("Comment/{id:int}/Activate")]
        public IActionResult Activate(int id)
        {
            var success = CommentService.Activate(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        [Authorize]
        //PUT: /Comment/1/Activate
        [HttpPut]
        [Route("Comment/{id:int}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            var success = CommentService.Deactivate(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
