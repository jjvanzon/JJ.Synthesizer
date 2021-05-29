using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoDeleteViewModel : UndoItemViewModelBase
    {
        public IList<EntityTypeAndIDViewModel> EntityTypesAndIDs { get; set; }
        public IList<ScreenViewModelBase> States { get; set; }
    }
}
