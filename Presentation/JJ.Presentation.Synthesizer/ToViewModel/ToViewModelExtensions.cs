using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        public static PatchEditViewModel ToPatchEditViewModel(this Operator op, IEntityPositionRepository entityPositionRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            var converter = new EntitiesToDiagramConverter(entityPositionRepository);

            EntitiesToDiagramConverter.Result converterResult = converter.Execute(op);

            var viewModel = new PatchEditViewModel
            {
                Diagram = converterResult.Diagram,
                DragGesture = converterResult.DragGesture,
                DropGesture = converterResult.DropGesture
            };

            return viewModel;
        }
    }
}
