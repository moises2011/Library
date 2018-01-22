using Library.Data.IRepositories;
using Library.Data.Entities;

namespace Library.Data.Repository
{
    public class CategoryRepository : ERepository<long, Category>, ICategoryRepository
    {
        private readonly IQueryableUnitOfWork unitOfWork;
        public CategoryRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
