using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    internal class EntityTypeAndIDViewModel
    {
        public int EntityID { get; set; }
        public EntityTypeEnum EntityTypeEnum { get; set; }

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}