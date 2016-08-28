using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class DimensionViewModel
    {
        /// <summary> not editable </summary>
        public object Identifier { get; set; }
        /// <summary> not editable </summary>
        public string Name { get; set; }
        public bool Visible { get; set; }
    }
}
