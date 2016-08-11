using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWorkApi.Core.Model
{
    /// <summary>
    /// Filter data
    /// </summary>
    public class FilterData
    {

        /// <summary>
        /// field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Text expression
        /// </summary>
        public string Text { get; set; }
    }
}
