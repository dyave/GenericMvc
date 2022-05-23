using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericMvc.Context;
using GenericMvc.Models;
using AutoMapper;
using GenericMvc.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GenericMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiProductsDiffController : ControllerBase
    {
        private readonly AdventureWorksLT2019Context _context;
        private readonly IMapper _mapper;

        public ApiProductsDiffController(AdventureWorksLT2019Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ApiProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDiffDto>>> GetProduct()
        {
            var products = await _context.Product.ToListAsync();
            return _mapper.Map<List<ProductDiffDto>>(products);
        }

        // GET: api/ApiProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDiffDto>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDiffDto>(product);
        }
    }
}
