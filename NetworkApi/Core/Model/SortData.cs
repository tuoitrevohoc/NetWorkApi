using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWorkApi.Core.Model
{

    /// <summary>
    /// Sort Data
    /// </summary>
    public class SortData
    {

        /// <summary>
        /// The column name
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Is Is Decending
        /// </summary>
        public bool IsDescending  { get; set; }

    }
}
