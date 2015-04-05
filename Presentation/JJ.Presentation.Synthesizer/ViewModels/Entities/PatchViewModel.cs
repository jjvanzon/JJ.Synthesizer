using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public class PatchViewModel
    {
        public int ID { get; set; }
        public string PatchName { get; set; }
        public IList<OperatorViewModel> Operators { get; set; }
    }
}
