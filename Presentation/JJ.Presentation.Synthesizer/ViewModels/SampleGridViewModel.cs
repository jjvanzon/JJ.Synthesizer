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
    public sealed class SampleGridViewModel
    {
        public int DocumentID { get; set; }

        /// <summary> We need this property, because ChildDocumentViewModel and DocumentViewModel have no mutual type. </summary>
        public int? ChildDocumentTypeID { get; set; }

        public bool Visible { get; set; }
        public IList<SampleListItemViewModel> List { get; set; }
    }
}
