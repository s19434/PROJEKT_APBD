using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.DTOs.Request
{
    public class NewCampaignRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public int IdCampaign { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        public decimal PricePerSquareMeter { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public int? FromIdBuilding { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        public int? ToIdBuilding { get; set; }
    }
}
