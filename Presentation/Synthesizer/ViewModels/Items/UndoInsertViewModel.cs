using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    internal class UndoInsertViewModel : UndoItemViewModelBase
    {
        public int EntityID { get; set; }
        public EntityTypeEnum EntityTypeEnum { get; set; }
        public IList<ViewModelBase> States { get; set; }
    }
}
