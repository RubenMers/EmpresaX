using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public List<Object> Objects { get; set; }

        public string ErrorMessage { get; set; }

        public Object Object { get; set; }

        public Exception Ex { get; set; }

        public bool Correct { get; set; }
    }
}
