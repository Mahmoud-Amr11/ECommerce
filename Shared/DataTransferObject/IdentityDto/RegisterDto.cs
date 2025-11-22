using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.IdentityDto
{
    public class RegisterDto {
        [EmailAddress]
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string UserName{ get; set; }
        public string DisplayName{ get; set; }
        [Phone]
        public string PhoneNumer{ get; set; }


    }
}
