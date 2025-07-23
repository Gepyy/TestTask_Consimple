using System;

namespace TestTask_Consimple.Models
{
    public class PurchaseDto
    {
        public int Number { get; set; }
        public int IDClient { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
    }
} 