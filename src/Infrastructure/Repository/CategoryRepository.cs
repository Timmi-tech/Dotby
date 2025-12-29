using Dotby.Domain.Contracts;
using Dotby.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotby.Infrastructure.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
         await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Category?> GetCategoryAsync(Guid categoryId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);

    }
}