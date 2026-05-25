using Demo22.Models;
using Demo22.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo22.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repo;

        public ProductsController(IRepository<Product> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();

            // simple approach: copy props (for demo)
            // existing.GetType().GetProperty("Name")!.SetValue(existing, product.Name);
            //existing.GetType().GetProperty("Price")!.SetValue(existing, product.Price);
            existing.Name = product.Name;
            existing.Price = product.Price;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> Yogendra(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return NotFound();

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
