
namespace NetWorkApi.Core.Validations
{

    /// <summary>
    /// 
    /// </summary>
    public class StringLengthValidation: DataValidation
    {

        /// <summary>
        /// Min value
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        public int MaxLength { get; set; }


        /// <summary>
        /// Create a range validation with min & max value
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="message">The error message when error failed</param>
        public StringLengthValidation(int min, int max, string message): base(message)
        {
            MinLength = min;
            MaxLength = max;
        }
    }
}
