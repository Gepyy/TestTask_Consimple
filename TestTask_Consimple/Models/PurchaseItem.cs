namespace TestTask_Consimple.Models
{
    public class PurchaseItem
    {
        public int ID { get; set; }
        public int PurchaseNumber { get; set; }
        public int IDProduct { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }

        public Purchase Purchase { get; set; }
        public Product Product { get; set; }
    }
} 