using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Core.Interfaces;
using Library.Core.Dtos;
using Microsoft.Extensions.Options;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookServices bookServices;

        public BooksController(IBookServices _bookServices)
        {
            bookServices = _bookServices;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return bookServices.GetAll();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public Book FindById(long id)
        {
            return bookServices.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task CreateAsync([FromBody]Book book)
        {
            await bookServices.AddAsync(book);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task UpdateAsync(long id, [FromBody]Book book)
        {
            book.Id = id;
            await bookServices.UpdateAsync(book);
        }

        // DELETE api/values/5
       /* [HttpDelete("{id}")]
        public async Task DeleteAsync(long id)
        {
            await bookServices.DeleteAsync(id);
        }*/

        [HttpGet, Route("/Error")]
        public ContentResult Error()
        {
            return Content("La petición no ha sido procesada.");
        }
    }
}
