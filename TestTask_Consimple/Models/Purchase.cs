using System;
using System.Collections.Generic;

namespace TestTask_Consimple.Models
{
    public class Purchase
    {
        public int Number { get; set; }
        public int IDClient { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }

        public Client Client { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
} 