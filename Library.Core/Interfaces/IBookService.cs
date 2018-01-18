using Library.Core.Dtos;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IBookService : IService<long, Data.Entities.Book, Book>
    {
    }
}
