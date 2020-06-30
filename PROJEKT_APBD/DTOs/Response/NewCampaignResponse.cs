using PROJEKT_APBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.DTOs.Response
{
    public class NewCampaignResponse
    {
        public Campaign Campaign { get; set; }
        public Banner BannerFirst { get; set; }
        public Banner BannerSecond { get; set; }
    }
}
