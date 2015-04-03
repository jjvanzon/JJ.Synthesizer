using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class PatchEditViewModel
    {
        //public IList<OperatorViewModel> Operators { get; set; }
        public Diagram Diagram { get; set; }
        public DragDropGesture DragGesture { get; set; }
        public DragDropGesture DropGesture { get; set; }
    }
}
