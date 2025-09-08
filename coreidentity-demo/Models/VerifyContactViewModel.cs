using System.ComponentModel.DataAnnotations;

namespace coreidentity_demo.Models
{
    public class VerifyContactViewModel
    {
        [Required(ErrorMessage = "Please enter the code sent to your email or phone")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter your contact (email or phone)")]
        public string Contact { get; set; }

        public bool IsEmailVerification { get; set; }
    }
}
