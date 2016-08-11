
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
        [EmailAddress(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        /// <summary>
        /// Avatar of the user
        /// </summary>
        public string Avatar { get; set; }
    }
}
