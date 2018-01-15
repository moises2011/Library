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
        public async Task<IEnumerable<Book>> GetAll()
        {
            return await bookServices.GetAllAsync();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<Book> GetById(int id)
        {
            return await bookServices.FindByIdAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Book book)
        {
            //await bookServices.Add(book);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Book book)
        {
            //await bookServices.Update(book);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            //await bookServices.Delete(id);
        }

        [Route("/Error")]
        public ContentResult Error()
        {
            return Content("La aplicacion no está funcionando correctamente.");
        }
    }
}
