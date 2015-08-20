using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel
    {
        // Properties put directly here, instead of entity view model,
        // because entity view model is too elaborate.
        // TODO: Low priority: Make a separation between a simpler OperatorViewModel and a more elaborate one?

        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary> not editable </summary>
        public OperatorTypeViewModel OperatorType { get; set; }

        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
