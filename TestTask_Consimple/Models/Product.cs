using System.Collections.Generic;

namespace TestTask_Consimple.Models
{
    public class Product
    {
        public int IDProduct { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
} 