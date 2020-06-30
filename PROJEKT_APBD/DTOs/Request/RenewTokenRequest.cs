using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.DTOs.Request
{
    public class RenewTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
