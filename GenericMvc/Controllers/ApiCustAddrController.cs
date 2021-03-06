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
    public class ApiCustAddrController : ControllerBase
    {
        private readonly AdventureWorksLT2019Context _context;
        private readonly IMapper _mapper;

        public ApiCustAddrController(AdventureWorksLT2019Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ApiCustAddr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddressDto>>> GetCustomerAddress()
        {
            var cas = await _context.CustomerAddress.Include(b => b.Address).Include(b => b.Customer).ToListAsync();
            return _mapper.Map<List<CustomerAddressDto>>(cas);
        }

        // GET: api/ApiCustAddr/29485/1086
        [HttpGet("{customerId}/{addressId}")]
        public async Task<ActionResult<CustomerAddressMergedDto>> GetCustomerAddress(int customerId, int addressId)
        {
            var caRequest = await _context.CustomerAddress.Include(b => b.Address).Include(b => b.Customer).FirstOrDefaultAsync(x => x.CustomerId == customerId && x.AddressId == addressId);

            if (caRequest == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerAddressMergedDto>(caRequest);
        }

        // PUT: api/ApiCustAddr/29485/1086
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{customerId}/{addressId}")]
        public async Task<IActionResult> PutCustomerAddress(int customerId, int addressId, CustomerAddressMergedDto caDto)
        {
            //if (customerId != caDto.CustomerId || addressId != caDto.AddressId)
            //{
            //    return BadRequest();
            //}

            var caRequest = await _context.CustomerAddress.Include(b => b.Address).Include(b => b.Customer).FirstOrDefaultAsync(x => x.CustomerId == customerId && x.AddressId == addressId);

            //Mapping the client data to db-retrieved data to build the caRequest.
            //Reverse mapping to type CustomerAddress, and subtypes Customer and Address. Duplicated properties like CustomerId and AddressId are both updated in type and subtype as well.
            _mapper.Map(caDto, caRequest);

            //_context.Entry(customerAddress).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CustomerAddressExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/ApiCustAddr
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> PostCustomerAddress(CustomerAddress customerAddress)
        {
            _context.CustomerAddress.Add(customerAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerAddressExists(customerAddress.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerAddress", new { id = customerAddress.CustomerId }, customerAddress);
        }

        // DELETE: api/ApiCustAddr/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerAddress>> DeleteCustomerAddress(int id)
        {
            var customerAddress = await _context.CustomerAddress.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddress.Remove(customerAddress);
            await _context.SaveChangesAsync();

            return customerAddress;
        }

        private bool CustomerAddressExists(int id)
        {
            return _context.CustomerAddress.Any(e => e.CustomerId == id);
        }
    }
}
