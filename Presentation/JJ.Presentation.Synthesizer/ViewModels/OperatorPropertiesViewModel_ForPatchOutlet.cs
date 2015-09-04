using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForPatchOutlet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
