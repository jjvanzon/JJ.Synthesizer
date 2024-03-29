﻿using System.Collections.Generic;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OutletControlPointConverter
    {
        private const float DEFAULT_CONTROL_POINT_DISTANCE = 50;

        private readonly Dictionary<int, Point> _destOutletControlPointDictionary = new Dictionary<int, Point>();
        private readonly HashSet<Point> _destOutletControlPointHashSet = new HashSet<Point>();

        public IList<Point> ConvertToOutletControlPoints(
            IList<Point> sourceOutletPoints,
            IList<OutletViewModel> sourceOutletViewModels)
        {
            if (sourceOutletPoints == null) throw new NullException(() => sourceOutletPoints);
            if (sourceOutletViewModels == null) throw new NullException(() => sourceOutletViewModels);
            if (sourceOutletPoints.Count != sourceOutletViewModels.Count) throw new NotEqualException(() => sourceOutletPoints.Count, () => sourceOutletViewModels.Count);

            int count = sourceOutletPoints.Count;

            var destOutletControlPoints = new List<Point>(count);

            for (int i = 0; i < count; i++)
            {
                Point sourceOutletPoint = sourceOutletPoints[i];
                OutletViewModel sourceOutletViewModel = sourceOutletViewModels[i];

                Point destOutletControlPoint = ConvertPoint(sourceOutletPoint, sourceOutletViewModel);

                destOutletControlPoints.Add(destOutletControlPoint);
            }

            return destOutletControlPoints;
        }

        private Point ConvertPoint(Point sourceOutletPoint, OutletViewModel sourceOutletViewModel)
        {
            if (sourceOutletPoint == null) throw new NullException(() => sourceOutletPoint);
            if (sourceOutletViewModel == null) throw new NullException(() => sourceOutletViewModel);

            int outletID = (int)sourceOutletPoint.Tag;

            if (!_destOutletControlPointDictionary.TryGetValue(outletID, out Point destOutletControlPoint))
            {
                destOutletControlPoint = new Point(sourceOutletPoint)
                {
                    Tag = sourceOutletPoint.Tag,
                    PointStyle = StyleHelper.PointStyleInvisible
                };

                _destOutletControlPointDictionary.Add(outletID, destOutletControlPoint);
                _destOutletControlPointHashSet.Add(destOutletControlPoint);
            }

            // TODO: Make const of 5f.

            float controlPointDistance;
            if (sourceOutletViewModel.AverageConnectionDistance.HasValue)
            {
                controlPointDistance = sourceOutletViewModel.AverageConnectionDistance.Value / 5f;
            }
            else
            {
                controlPointDistance = DEFAULT_CONTROL_POINT_DISTANCE;
            }

            destOutletControlPoint.Position.X = 0;
            destOutletControlPoint.Position.Y = controlPointDistance;

            return destOutletControlPoint;
        }

        public void TryRemove(Point destElement)
        {
            if (!_destOutletControlPointHashSet.Contains(destElement))
            {
                return;
            }

            int outletID = (int)destElement.Tag;

            _destOutletControlPointDictionary.Remove(outletID);
            _destOutletControlPointHashSet.Remove(destElement);

            destElement.Dispose();
        }
    }
}