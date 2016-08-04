
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
        public int Id { get; set; }
    }
}
