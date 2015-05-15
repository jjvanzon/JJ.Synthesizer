using JJ.Business.CanonicalModel;
using JJ.Framework.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class EffectListViewModel
    {
        public int ParentDocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}
