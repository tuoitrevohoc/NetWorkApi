using NetWorkApi.Core.Validations;
using System.Collections.Generic;

namespace NetWorkApi.Core.Model
{

    /// <summary>
    /// 
    /// </summary>
    public class ColumnData
    {

        /// <summary>
        /// Name of the columns
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// list of validations apply to this column
        /// </summary>
        public IEnumerable<DataValidation> Validations { get; set; }
    }
}
