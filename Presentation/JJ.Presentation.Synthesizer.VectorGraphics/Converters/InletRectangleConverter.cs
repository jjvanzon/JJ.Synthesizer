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
using JJ.Presentation.Synthesizer.VectorGraphics.Configuration;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class InletRectangleConverter
    {
        private static bool _toolTipFeatureEnabled = GetToolTipFeatureEnabled();

        private Dictionary<int, Rectangle> _destInletRectangleDictionary = new Dictionary<int, Rectangle>();

        private IGesture _dropLineGesture;
        private IGesture _inletToolTipGesture;

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

            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float rowHeight = destOperatorRectangle.Position.Height / 4;
            float heightOverflow = StyleHelper.INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS;
            float inletWidth = destOperatorRectangle.Position.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;

            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
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
            int id = sourceInletViewModel.ID;

            Rectangle destInletRectangle = TryGetInletRectangle(destOperatorRectangle, id);
            if (destInletRectangle == null)
            {
                destInletRectangle = new Rectangle();
                destInletRectangle.Diagram = destOperatorRectangle.Diagram;
                destInletRectangle.Parent = destOperatorRectangle;
                destInletRectangle.Tag = VectorGraphicsTagHelper.GetInletTag(id);

                _destInletRectangleDictionary.Add(id, destInletRectangle);
            }

            destInletRectangle.Style.BackStyle = StyleHelper.BackStyleInvisible;
            destInletRectangle.Style.LineStyle = StyleHelper.BorderStyleInvisible;

            destInletRectangle.Gestures.Clear();
            destInletRectangle.Gestures.Add(_dropLineGesture);

            if (_toolTipFeatureEnabled)
            {
                destInletRectangle.Gestures.Add(_inletToolTipGesture);
            }

            return destInletRectangle;
        }

        private Rectangle TryGetInletRectangle(Element destParent, int inletID)
        {
            Rectangle destRectangle;
            if (!_destInletRectangleDictionary.TryGetValue(inletID, out destRectangle))
            {
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => VectorGraphicsTagHelper.TryGetInletID(x.Tag) == inletID)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destInletRectangleDictionary.Add(inletID, destRectangle);
                }
            }

            return destRectangle;
        }

        // Helpers

        private const bool DEFAULT_TOOL_TIP_FEATURE_ENABLED = false;

        private static bool GetToolTipFeatureEnabled()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_TOOL_TIP_FEATURE_ENABLED;
            return config.ToolTipFeatureEnabled;
        }
    }
}
