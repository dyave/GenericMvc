using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenericMvc.Context;
using GenericMvc.Models;
using AutoMapper;
using GenericMvc.Dtos;

namespace GenericMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiProductsController : ControllerBase
    {
        private readonly AdventureWorksLT2019Context _context;
        private readonly IMapper _mapper;

        public ApiProductsController(AdventureWorksLT2019Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ApiProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
        {
            var products = await _context.Product.ToListAsync();

            // No sé por qué pero no funciona con IEnumerable, pero sí con List:
            return _mapper.Map<List<ProductDto>>(products);
        }

        // GET: api/ApiProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            //Example of simple sync mapping:
            //var prod = _context.Product.ToList().FirstOrDefault();
            //var prodDto = _mapper.Map<ProductDto>(prod);

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDto>(product);
        }

        // PUT: api/ApiProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/ApiProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
