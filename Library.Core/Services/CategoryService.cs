using Library.Core.Dtos;
using Library.Core.Interfaces;
using Library.Data.Helpers;
using Library.Data.IRepositories;

namespace Library.Core.Services
{
    public class CategoryService : Service<long, Data.Entities.Category, Category>, ICategoryService
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository) : base(repository, new LoggerHelper())
        {
            this.repository = repository;
        }
    }
}
