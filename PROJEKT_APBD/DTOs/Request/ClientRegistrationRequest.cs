using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.DTOs.Request
{
    public class ClientRegistrationRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        [MinLength(8)]
        public string Password { get; set; }

    }
}
