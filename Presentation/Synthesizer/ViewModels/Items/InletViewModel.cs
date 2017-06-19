using System.Diagnostics;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public sealed class InletViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }

        /// <summary> Some inlets that have to be stored, might not have to be shown. </summary>
        public bool Visible { get; set; }

        /// <summary> nullable </summary>
        public OutletViewModel InputOutlet { get; set; }

        public float? ConnectionDistance { get; set; }
        public bool HasWarningAppearance { get; set; }

        // NOTE:
        // The following properties are not displayed,
        // but are there to have all data present in the view model to restore to a valid state of the entity.

        /// <summary> not displayed </summary>
        public IDAndName Dimension { get; set; }
        /// <summary> not displayed </summary>
        public int Position { get; set; }
        /// <summary> not displayed </summary>
        public double? DefaultValue { get; set; }
        /// <summary> not displayed  </summary>
        public bool IsObsolete { get; set; }
        /// <summary> not displayed  </summary>
        public bool WarnIfEmpty { get; set; }
        /// <summary> not displayed  </summary>
        public bool NameOrDimensionHidden { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
