using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericMvc.Dtos
{
    public class ProductDiffDto
    {
        public int ProductId { get; set; }//x
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }//x
        public string ProductColor { get; set; }
        public decimal ProductStandardCost { get; set; }
        public decimal ProductListPrice { get; set; }
    }
}
