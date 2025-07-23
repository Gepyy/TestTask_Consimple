using System;
using System.Collections.Generic;

namespace TestTask_Consimple.Models
{
    public class Client
    {
        public int IDClient { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfReg { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
} 