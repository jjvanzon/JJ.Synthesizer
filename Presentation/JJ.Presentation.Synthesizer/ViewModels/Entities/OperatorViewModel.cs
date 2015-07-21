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
    /// <summary>
    /// NOTE: This is one of the few view models with an inverse property.
    /// OutletViewModel.Operator <=> Operator.Outlets
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public sealed class OperatorViewModel
    {
        public int ID { get; set; }
        public int EntityPositionID { get; set; }

        public string Name { get; set; }
        public string Caption { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public bool IsSelected { get; set; }

        public IList<InletViewModel> Inlets { get; set; }

        /// <summary>
        /// NOTE: This property has an inverse property
        /// Operator.Outlets <=> OutletViewModel.Operator
        /// </summary>
        public IList<OutletViewModel> Outlets { get; set; }

        // TODO: It might make more sense if this is simplu OperatorTypeViewModel.
        /// <summary>
        /// For persistence. Does not need to be displayed.
        /// </summary>
        public int OperatorTypeID { get; set; }

        /// <summary>
        /// Only relevant for Value operators.
        /// </summary>
        public string Value { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
