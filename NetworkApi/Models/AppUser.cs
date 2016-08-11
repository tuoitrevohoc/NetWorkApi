
using System.ComponentModel.DataAnnotations;

namespace NetWorkApi.Models
{

    /// <summary>
    /// Application user
    /// </summary>
    public class AppUser: AppModel
    {

        /// <summary>
        /// Name of the user
        /// </summary>
        [Display(Name ="Email Address", Description = "Email address of the user")]
        [EmailAddress(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        [Display(Name = "Full Name", Description = "Fullname of user")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        /// <summary>
        /// Avatar of the user
        /// </summary>
        [Display(Name = "Avatar of the user")]
        public string Avatar { get; set; }

    }
}
