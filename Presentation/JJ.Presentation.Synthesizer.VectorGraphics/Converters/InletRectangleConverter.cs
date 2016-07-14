using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using System;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class InletRectangleConverter
    {
        private readonly Dictionary<int, Rectangle> _destInletRectangleDictionary = new Dictionary<int, Rectangle>();
        private readonly HashSet<Rectangle> _destInletRectangleHashSet = new HashSet<Rectangle>();

        private readonly IGesture _dropLineGesture;
        private readonly IGesture _inletToolTipGesture;

        public InletRectangleConverter(DropLineGesture dropLineGesture, ToolTipGesture inletToolTipGesture)
        {
            if (dropLineGesture == null) throw new NullException(() => dropLineGesture);
            if (inletToolTipGesture == null) throw new NullException(() => inletToolTipGesture);

            _dropLineGesture = dropLineGesture;
            _inletToolTipGesture = inletToolTipGesture;
        }

        public IList<Rectangle> ConvertToInletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            IList<InletViewModel> sourceInletViewModelsToConvert = sourceOperatorViewModel.Inlets
                                                                                          .Where(inlet => inlet.Visible)
                                                                                          .ToArray();
            if (sourceInletViewModelsToConvert.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceInletViewModelsToConvert.Count);

            float rowHeight = destOperatorRectangle.Position.Height / 4;
            float heightOverflow = StyleHelper.INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS;
            float inletWidth = destOperatorRectangle.Position.Width / sourceInletViewModelsToConvert.Count;
            float x = 0;

            foreach (InletViewModel sourceInletViewModel in sourceInletViewModelsToConvert)
            {
                if (!sourceInletViewModel.Visible)
                {
                    continue;
                }

                Rectangle destInletRectangle = ConvertToInletRectangle(sourceInletViewModel, destOperatorRectangle);

                destInletRectangle.Position.X = x;
                destInletRectangle.Position.Y = -heightOverflow;
                destInletRectangle.Position.Width = inletWidth;
                destInletRectangle.Position.Height = rowHeight + heightOverflow;

                destInletRectangles.Add(destInletRectangle);

                x += inletWidth;
            }

            return destInletRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToInletRectangle(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            int inletID = sourceInletViewModel.ID;

            Rectangle destInletRectangle;
            if (!_destInletRectangleDictionary.TryGetValue(inletID, out destInletRectangle))
            {
                destInletRectangle = new Rectangle
                {
                    Diagram = destOperatorRectangle.Diagram,
                    Parent = destOperatorRectangle,
                    Tag = VectorGraphicsTagHelper.GetInletTag(inletID)
                };

                _destInletRectangleDictionary.Add(inletID, destInletRectangle);
                _destInletRectangleHashSet.Add(destInletRectangle);
            }

            destInletRectangle.Style.BackStyle = StyleHelper.BackStyleInvisible;
            destInletRectangle.Style.LineStyle = StyleHelper.BorderStyleInvisible;

            destInletRectangle.Gestures.Clear();
            destInletRectangle.Gestures.Add(_dropLineGesture);
            destInletRectangle.Gestures.Add(_inletToolTipGesture);

            return destInletRectangle;
        }

        public void TryRemove(Rectangle destElement)
        {
            if (_destInletRectangleHashSet.Contains(destElement))
            {
                int inletID = VectorGraphicsTagHelper.GetInletID(destElement.Tag);

                _destInletRectangleDictionary.Remove(inletID);
                _destInletRectangleHashSet.Remove(destElement);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }
    }
}