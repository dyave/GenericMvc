using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericMvc.Dtos
{
    public class CustomerAddressDto
    {
        public CustomerDto CustomerDto { get; set; }
        public AddressDto AddressDto { get; set; }

        public string AddressType { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
