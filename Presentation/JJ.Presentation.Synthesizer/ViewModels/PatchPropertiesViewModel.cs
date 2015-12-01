using JJ.Business.CanonicalModel;
using System.Collections.Generic;
using System;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary> Leading for saving when it comes to the simple properties. </summary>
    public sealed class PatchPropertiesViewModel
    {
        public int ChildDocumentID { get; set; }
        public int PatchID { get; internal set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public bool CanAddToCurrentPatches { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
