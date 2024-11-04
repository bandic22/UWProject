using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWProject.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }

        public Item(string id, string name, decimal value, int quantity)
        {
            Id = id;
            Name = name;
            Value = value;
            Quantity = quantity;
        }
    }
}
