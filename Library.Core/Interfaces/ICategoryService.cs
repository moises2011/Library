using Library.Core.Dtos;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface ICategoryService : IService<long, Data.Entities.Category, Category>
    {
    }
}
