using JJ.Presentation.Synthesizer.Helpers;
using System.Diagnostics;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    /// <summary>
    /// NOTE: This is one of the few view models with an inverse property.
    /// OutletViewModel.Operator &lt;=&gt; Operator.Outlets
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public sealed class OutletViewModel
    {
        public int ID { get; set; }
        public string Caption { get; set; }

        /// <summary> Some inlets that have to be stored, might not have to be shown. </summary>
        public bool Visible { get; set; }

        public bool HasWarningAppearance { get; set; }
        public float? AverageConnectionDistance { get; set; }

        /// <summary>
        /// NOTE: This property has an inverse property
        /// OutletViewModel.Operator &lt;=&gt; Operator.Outlets
        /// </summary>
        public OperatorViewModel Operator { get; set; }

        // NOTE:
        // The following properties are not displayed,
        // but are there to have all data present in the view model to restore to a valid state of the entity.

        /// <summary> not displayed </summary>
        public string Name { get; set; }
        /// <summary> not displayed  </summary>
        public IDAndName Dimension { get; set; }
        /// <summary> not displayed  </summary>
        public int Position { get; set; }
        /// <summary> not displayed  </summary>
        public bool IsObsolete { get; set; }
        /// <summary> not displayed  </summary>
        public bool NameOrDimensionHidden { get; set; }
        /// <summary> not displayed  </summary>
        public bool IsRepeating { get; set; }
        /// <summary> not displayed  </summary>
        public int? RepetitionPosition { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
