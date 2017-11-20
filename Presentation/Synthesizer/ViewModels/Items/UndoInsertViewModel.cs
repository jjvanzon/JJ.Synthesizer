using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoInsertViewModel : UndoItemViewModelBase
    {
        public IList<ViewModelBase> NewStates { get; set; }
    }
}
