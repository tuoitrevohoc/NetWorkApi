

namespace NetWorkApi.Core.Validations
{
    /// <summary>
    /// Validation class
    /// </summary>
    public abstract class DataValidation
    {

        /// <summary>
        /// Get type of the validator
        /// </summary>
        public string Type
        {
            get
            {
                return GetType().Name;
            }
        }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Message on error
        /// </summary>
        /// <param name="message"></param>
        public DataValidation(string message)
        {
            ErrorMessage = message;
        }
    }
}
