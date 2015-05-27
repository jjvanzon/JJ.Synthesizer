using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    /// <summary>
    /// NOTE: This is one of the few view models with an inverse property.
    /// OutletViewModel.Operator <=> Operator.Outlets
    /// </summary>
    public sealed class OutletViewModel
    {
        public int ID { get; set; }
        public int ListIndex { get; set; }

        [Obsolete("Use ListIndex instead.")]
        public Guid TemporaryID { get; set; }

        public string Name { get; set; }

        public OperatorViewModel Operator { get; set; }
    }
}
