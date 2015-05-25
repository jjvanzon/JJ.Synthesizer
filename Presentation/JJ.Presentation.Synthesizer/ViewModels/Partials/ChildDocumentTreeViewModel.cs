using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ChildDocumentTreeViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsExpanded { get; set; }
        public int TemporaryID { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel PatchesNode { get; set; }
    }
}
