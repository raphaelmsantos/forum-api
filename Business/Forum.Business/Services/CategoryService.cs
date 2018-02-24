using Forum.Business.Entities;
using Forum.Business.Filters;
using Forum.Business.Interfaces.Repositories;
using Forum.Business.Interfaces.Services;
using RaphaelSantos.Framework.Collections;
using System.Linq;

namespace Forum.Business.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private ICategoryRepository CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
            : base(categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }

        /// <summary>
		/// Categories list
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>The filtered and sorted list of records</returns>
		public PagedList<Category> List(CategoryFilter filter)
        {
            var query = base.GetAll();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(i => i.Name.Contains(filter.Name));

            var result = query.ToPaged(filter);

            return result;
        }
    }
}
