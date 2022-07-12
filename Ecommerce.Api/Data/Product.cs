using System;
using System.Collections.Generic;

namespace Ecommerce.Api.Data
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
    }
}
