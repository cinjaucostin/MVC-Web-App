using System;
using System.Collections.Generic;

namespace MVCpractice.Models
{
    public partial class AvailableProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Package { get; set; }
        public bool IsDiscontinued { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}
