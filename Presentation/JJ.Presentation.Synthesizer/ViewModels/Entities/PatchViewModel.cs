using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class PatchViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<OperatorViewModel> Operators { get; set; }
    }
}
