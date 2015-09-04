using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary> Leading for saving when it comes to the simple properties and the MainPatch. </summary>
    public sealed class ChildDocumentPropertiesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IDAndName ChildDocumentType { get; set; }
        public IList<IDAndName> ChildDocumentTypeLookup { get; set; }
        public IDAndName MainPatch { get; set; }
        public IList<IDAndName> MainPatchLookup { get; set; }

        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
