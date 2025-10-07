using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    [Owned]
    public class Addess
    {


        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public int BuildingNumber { get; set; }
        
    }
}
