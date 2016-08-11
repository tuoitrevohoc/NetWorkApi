
namespace NetWorkApi.Core.Validations
{

    /// <summary>
    /// Data validation
    /// </summary>
    public class RegularExpressionValidation: DataValidation
    {

        /// <summary>
        /// The regular expression
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public RegularExpressionValidation(string expression, string message)
            : base(message)
        {
            Expression = expression;
        }
    }
}
