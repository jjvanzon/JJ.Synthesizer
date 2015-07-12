using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Keys;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SampleListViewModel
    {
        public int RootDocumentID { get; set; }
        public int? ChildDocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<SampleListItemViewModel> List { get; set; }
    }
}
