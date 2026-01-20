using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Request
{
    public class PagintedResponse<T>
    {
        public int TotalCount { get; set; }
        public int page { get; set; }
        public int Litmit { get; set; }

        public List<T> Data { get; set; }
    }
}
