using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Core.Interfaces;
using Library.Core.Dtos;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryServices;

        public CategoryController(ICategoryService _categoryServices)
        {
            categoryServices = _categoryServices;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return categoryServices.GetAll();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public Category FindById(long id)
        {
            return categoryServices.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task CreateAsync([FromBody]Category book)
        {
            await categoryServices.AddAsync(book);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task UpdateAsync(long id, [FromBody]Category book)
        {
            book.Id = id;
            await categoryServices.UpdateAsync(book);
        }// POST api/values
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await categoryServices.DeleteAsync(id);
        }

        // PUT api/values
        [HttpPut]
        public async Task BulkUpdSertAsync(long id, [FromBody]List<Category> books)
        {
            await categoryServices.BulkUpsertAsync(books);
        }

        [HttpOptions("{id}")]
        public void GetOptions(long id)
        {
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
