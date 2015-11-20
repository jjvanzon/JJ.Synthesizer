using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForPatchOutlet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary> Note that this is 1-based, while the value stored in the entity model is 0-based. </summary>
        public int Number { get; set; }
        public IDAndName OutletType { get; set; }
        public IList<IDAndName> OutletTypeLookup { get; set; }
        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
