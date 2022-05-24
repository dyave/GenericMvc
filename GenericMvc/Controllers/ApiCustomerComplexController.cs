using AutoMapper;
using GenericMvc.Context;
using GenericMvc.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCustomerComplexController : ControllerBase
    {
        private readonly AdventureWorksLT2019Context _context;
        private readonly IMapper _mapper;
        public ApiCustomerComplexController(AdventureWorksLT2019Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ApiCustomerComplex
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerComplexDto>>> GetCustomerAddress()
        {
            var customers = await _context.Customer.ToListAsync();
            return _mapper.Map<List<CustomerComplexDto>>(customers);
        }
    }
}
