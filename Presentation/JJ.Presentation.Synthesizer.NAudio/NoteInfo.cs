using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class NoteInfo
    {
        public int NoteNumber { get; set; }
        public int ListIndex { get; set; }
        public double StartTime { get; set; }
        public double ReleaseTime { get; set; }
        public double EndTime { get; set; }
    }
}
