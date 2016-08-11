using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWorkApi.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class PagingData<Entity>
    {

        /// <summary>
        /// number of items 
        /// </summary>
        public int Count { get; set;}

        /// <summary>
        /// the data 
        /// </summary>
        public IEnumerable<Entity> Data { get; set; }

        /// <summary>
        /// Get pagination data
        /// </summary>
        /// <param name="count"></param>
        /// <param name="data"></param>
        public PagingData(int count, IEnumerable<Entity> data)
        {
            Count = count;
            Data = data;
        }
    }
}
