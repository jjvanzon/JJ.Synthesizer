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
    public sealed class OperatorViewModel
    {
        public int ID { get; set; }
        public int ListIndex { get; set; }

        [Obsolete("Use ListIndex instead.")]
        public Guid TemporaryID { get; set; }

        public string Name { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public bool IsSelected { get; set; }

        public IList<InletViewModel> Inlets { get; set; }
        public IList<OutletViewModel> Outlets { get; set; }

        /// <summary>
        /// For persistence. Does not need to be displayed.
        /// </summary>
        public int OperatorTypeID { get; set; }

        [Obsolete("Use OperatorTypeID instead.", true)]
        /// <summary>
        /// For persistence. Does not need to be displayed.
        /// </summary>
        public string OperatorTypeName { get; set; }
    }
}
