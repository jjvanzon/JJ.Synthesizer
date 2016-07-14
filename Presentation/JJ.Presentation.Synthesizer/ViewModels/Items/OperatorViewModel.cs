using JJ.Presentation.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
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

        public string Caption { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public bool IsSelected { get; set; }
        public DimensionViewModel Dimension { get; set; }
        public StyleGradeEnum StyleGrade { get; set; }

        public IList<InletViewModel> Inlets { get; set; }

        /// <summary>
        /// NOTE: This property has an inverse property
        /// Operator.Outlets <=> OutletViewModel.Operator
        /// </summary>
        public IList<OutletViewModel> Outlets { get; set; }

        /// <summary> persistence. Does not need to be displayed. </summary>
        public IDAndName OperatorType { get; set; }

        public bool IsOwned { get; set; }

        private string DebuggerDisplay
        {
            get { return DebugHelper.GetDebuggerDisplay(this); }
        }
    }
}
