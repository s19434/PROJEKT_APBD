using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Models
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }

        public int Name { get; set; }
        public decimal Price { get; set; }
        public int? IdCampaign { get; set; }
        public decimal Area { get; set; }

        public Campaign Campaign { get; set; }


    }
}
