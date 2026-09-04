using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class PingAnalysisResult
    {
        public int online { get; set; }
        public int offline { get; set; }
        public int unknown { get; set; }
        public int total { get; set; }
    }
}
