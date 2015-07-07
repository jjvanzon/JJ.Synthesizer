using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public sealed class InletViewModel
    {
        public InletKeysViewModel Keys { get; set; }

        public string Name { get; set; }

        /// <summary> nullable </summary>
        public OutletViewModel InputOutlet { get; set; }

        // TODO: Remove outcommented code.
        //[Obsolete("Use Keys instead.", true)]
        //public int ID { get; set; }

        //[Obsolete("Use Keys instead.", true)]
        //public int ListIndex { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
