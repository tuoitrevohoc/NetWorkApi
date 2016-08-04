
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
        public string Email { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Avatar of the user
        /// </summary>
        public string Avatar { get; set; }
    }
}
