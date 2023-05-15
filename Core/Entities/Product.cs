using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity, IMustHaveTenant
    {
        public Product(string name, string description, int rate) {
            Name = name; 
            Description = description; 
            Rate = rate;
        }

        protected Product() { }

        public string Name { get;set; }
        public string Description { get;set; }
        public int Rate { get;set; }
        public string TenantId { get; set; }
    }
}
