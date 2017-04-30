using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public abstract class ViewModelBase
    {
        public bool Successful { get; set; }
        public IList<MessageDto> ValidationMessages { get; set; }
        public bool Visible { get; set; }
        public int RefreshCounter { get; set; }
    }
}
