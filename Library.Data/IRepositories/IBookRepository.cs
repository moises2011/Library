using Library.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Data.IRepositories
{
    public interface IBookRepository : IERepository<long, Book>
    {

    }
}
