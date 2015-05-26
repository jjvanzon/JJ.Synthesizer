using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class ChildDocumentViewModel
    {
        public IDNameAndListIndexViewModel Document { get; set; }

        public SampleListViewModel SampleList { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public CurveListViewModel CurveList { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }

        public PatchListViewModel PatchList { get; set; }
        public IList<PatchDetailsViewModel> PatchDetailsList { get; set; }
    }
}
