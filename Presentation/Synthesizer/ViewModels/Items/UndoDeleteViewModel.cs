using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoDeleteViewModel : UndoItemViewModelBase
    {
        public IList<ViewModelBase> DeletedStates { get; set; }
    }
}
