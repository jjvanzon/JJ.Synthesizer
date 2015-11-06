using JJ.Data.Synthesizer.Helpers;
using System.Diagnostics;

namespace JJ.Data.Synthesizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Inlet
    {
        public virtual int ID { get; set; }

        /// <summary> Optional. Currently (2105-11-05) only relevant for CustomOperators. </summary>
        public virtual string Name { get; set; }

        /// <summary> parent </summary>
        public virtual Operator Operator { get; set; }

        /// <summary> nullable </summary>
        public virtual Outlet InputOutlet { get; set; }

        /// <summary>
        /// TODO: The comment that follows is no longer true.
        /// This sort order is only visual. It is not a reference number.
        /// The ID is the main reference number and the name is an alternative key.
        /// </summary>
        public virtual int ListIndex { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
