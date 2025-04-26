using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchiaSharp.Core.Models.Searchia
{
    public class ApiGenericModel<T> where T : class
    {
        public string statusType { get; set; }
        public object details { get; set; }
        public T entity { get; set; }
        public string path { get; set; }
    }
}
