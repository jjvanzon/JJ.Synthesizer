using JJ.Data.Canonical;
using JJ.Framework.Presentation;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentGridViewModel : ViewModelBase
    {
        public IList<IDAndName> List { get; set; }
    }
}
