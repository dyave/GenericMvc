using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericMvc.Dtos
{
    public class CustomerAddressDto
    {
        public AddressDto Address { get; set; }
        public CustomerDto Customer { get; set; }

        public string AddressType { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
