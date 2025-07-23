namespace TestTask_Consimple.Models
{
    public class PurchaseItemDto
    {
        public int ID { get; set; }
        public int PurchaseNumber { get; set; }
        public int IDProduct { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
} 