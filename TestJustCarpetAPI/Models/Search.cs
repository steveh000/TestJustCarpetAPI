using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJustCarpetAPI.Models
{
    public class Search
    {
        public bool Pets { get; set; }
        public CarpetStyle Style { get; set; }
        public int Budget { get; set; }
        public bool Hardwearing { get; set; }
        public bool SkipSearchParameters { get; set; }
    }
}
