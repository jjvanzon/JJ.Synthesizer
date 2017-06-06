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
        // The following properties are not displayed (as of 2015-12-11)
        // but are there to have all data present in the view model to restore to a valid state of the entity.
        // I need this because of side-effects of changing e.g. a PatchInlet's DefaultValue go off only after changing those,
        // state is not preserved, and the side-effects do not go off upon saving the document (which could be fixed),
        // but side-effects also cannot go off upon PatchDetails.LoseFocus, because there is no way of knowing it should go off.
        // According to the application, state is committed entity state + view model data and it has to be somewhere in there.

        /// <summary> Does not necessarily need to be displayed, but does need to be passed around, so it is present when a new object is saved. </summary>
        public int ListIndex { get; set; }

        /// <summary> Read-only, not necessarily displayed. </summary>
        public double? DefaultValue { get; set; }

        /// <summary> Read-only, nullable, not necessarily displayed. </summary>
        public IDAndName Dimension { get; set; }

        /// <summary> not displayed  </summary>
        public bool IsObsolete { get; set; }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}
