using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.DTOs.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
