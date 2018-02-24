using AutoMapper;
using Forum.Business.DTO;
using Forum.Business.Entities;
using Forum.Business.Filters;
using Forum.Business.Interfaces.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RaphaelSantos.Framework.Collections;
using System.Collections.Generic;

namespace Forum.Api.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryService CategoryService { get; set; }
        private readonly IMapper Mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.CategoryService = categoryService;
            this.Mapper = mapper;
        }


        [HttpGet]
        // GET: api/Category
        public IActionResult Get(CategoryFilter filter)
        {
            var list = CategoryService.List(filter);

            var result = Mapper.Map<IEnumerable<CategoryDTO>>(list.Items);

            return Ok(new PagedList<CategoryDTO>(result, list));
        }

        [HttpGet("{id}")]
        // GET: api/Category/5
        public IActionResult Get(int id)
        {
            var obj = CategoryService.GetById(id);

            if (obj == null)
                return NotFound();

            var mapped = Mapper.Map<Category, CategoryDTO>(obj);

            return Ok(mapped);
        }

        [HttpPost]
        // POST: api/Category
        public IActionResult Post([FromBody] CategoryDTO categoryDTO)
        {
            var obj = Mapper.Map<CategoryDTO, Category>(categoryDTO);

            CategoryService.Add(obj);

            return Ok();
        }

        [HttpPut("{id}")]
        // PUT: api/Category/5
        public IActionResult Put(int id, [FromBody]CategoryDTO categoryDTO)
        {
            var obj = Mapper.Map<CategoryDTO, Category>(categoryDTO);

            var success = CategoryService.Update(obj);

            if (!success)
                return NotFound();

            return Ok();

        }

        [HttpDelete("{id}")]
        // DELETE: api/Category/5
        public IActionResult Delete(int id)
        {
            var success = CategoryService.Remove(id);

            return Ok();
        }

        //PUT: /Category/1/Activate
        [HttpPut]
        [Route("Category/{id:int}/Activate")]
        public IActionResult Activate(int id)
        {
            var success = CategoryService.Activate(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        //PUT: /Categoy/1/Activate
        [HttpPut]
        [Route("Category/{id:int}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            var success = CategoryService.Deactivate(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
