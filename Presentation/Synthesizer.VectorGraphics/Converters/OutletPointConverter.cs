﻿using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OutletPointConverter
    {
        private readonly Dictionary<int, Point> _destOutletPointDictionary = new Dictionary<int, Point>();
        private readonly HashSet<Point> _destOutletPointHashSet = new HashSet<Point>();

        public IList<Point> ConvertToOutletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            IList<OutletViewModel> sourceOutletViewModelsToConvert =
                sourceOperatorViewModel.Outlets
                                       .Where(outlet => outlet.Visible)
                                       .ToArray();

            if (sourceOutletViewModelsToConvert.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destOutletPoints = new List<Point>(sourceOutletViewModelsToConvert.Count);

            float outletWidth = destOperatorRectangle.Position.Width / sourceOutletViewModelsToConvert.Count;
            float x = 0;

            foreach (OutletViewModel sourceOutletViewModel in sourceOutletViewModelsToConvert)
            {
                Point destOutletPoint = ConvertToOutletPoint(sourceOutletViewModel, destOperatorRectangle);

                destOutletPoint.Position.X = x + outletWidth / 2f;
                destOutletPoint.Position.Y = destOperatorRectangle.Position.Height;

                destOutletPoints.Add(destOutletPoint);

                x += outletWidth;
            }

            return destOutletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToOutletPoint(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
        {
            int outletID = sourceOutletViewModel.ID;

            if (!_destOutletPointDictionary.TryGetValue(outletID, out Point destOutletPoint))
            {
                destOutletPoint = new Point(destOperatorRectangle)
                {
                    Tag = outletID
                };

                _destOutletPointDictionary.Add(outletID, destOutletPoint);
                _destOutletPointHashSet.Add(destOutletPoint);
            }

            if (sourceOutletViewModel.HasWarningAppearance)
            {
                destOutletPoint.PointStyle = StyleHelper.PointStyleWarning;
            }
            else
            {
                destOutletPoint.PointStyle = StyleHelper.PointStyle;
            }

            return destOutletPoint;
        }

        public void TryRemove(Point destElement)
        {
            if (!_destOutletPointHashSet.Contains(destElement))
            {
                return;
            }

            int outletID = (int)destElement.Tag;

            _destOutletPointDictionary.Remove(outletID);
            _destOutletPointHashSet.Add(destElement);

            destElement.Dispose();
        }
    }
}