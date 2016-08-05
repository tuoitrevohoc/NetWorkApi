
using System.ComponentModel.DataAnnotations;

namespace NetWorkApi.Models
{

    /// <summary>
    /// Model class
    /// </summary>
    public class AppModel
    {

        /// <summary>
        /// Id of the model
        /// </summary>
        [Key]
        public string Id { get; set; }
    }
}
