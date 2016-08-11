using System;

namespace NetWorkApi.Core.Validations
{

    /// <summary>
    /// Range validation class
    /// </summary>
    public class RangeValidation: DataValidation
    {

        /// <summary>
        /// Min value
        /// </summary>
        public object MinValue { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        public object MaxValue { get; set; }

        /// <summary>
        /// Create a range validation with min & max value
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="message">The error message when error failed</param>
        public RangeValidation(object min, object max, string message): base(message)
        {
            MinValue = min;
            MaxValue = max;
        }
    }
}
