using JJ.Presentation.Synthesizer.Helpers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    /// <summary>
    /// NOTE: This is one of the few view models with an inverse property.
    /// OutletViewModel.Operator <=> Operator.Outlets
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public sealed class OutletViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary> Does not necessarily need to be displayed, but does need to be passed around, so it is present when a new object is saved. </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// NOTE: This property has an inverse property
        /// OutletViewModel.Operator <=> Operator.Outlets
        /// </summary>
        public OperatorViewModel Operator { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
