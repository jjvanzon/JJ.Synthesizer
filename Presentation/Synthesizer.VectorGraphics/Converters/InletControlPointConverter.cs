using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class InletControlPointConverter
	{
		private const float DEFAULT_CONTROL_POINT_DISTANCE = 50;

		private readonly Dictionary<int, Point> _destInletControlPointDictionary = new Dictionary<int, Point>();
		private readonly HashSet<Point> _destInletControlPointHashSet = new HashSet<Point>();

		public IList<Point> ConvertToInletControlPoints(
			IList<Point> sourceInletPoints,
			IList<InletViewModel> sourceInletViewModels)
		{
			if (sourceInletPoints == null) throw new NullException(() => sourceInletPoints);
			if (sourceInletViewModels == null) throw new NullException(() => sourceInletViewModels);
			if (sourceInletPoints.Count != sourceInletViewModels.Count) throw new NotEqualException(() => sourceInletPoints.Count, () => sourceInletViewModels.Count);

			int count = sourceInletPoints.Count;

			var destInletControlPoints = new List<Point>(count);

			for (int i = 0; i < count; i++)
			{
				Point sourceInletPoint = sourceInletPoints[i];
				InletViewModel sourceInletViewModel = sourceInletViewModels[i];

				Point destInletControlPoint = ConvertPoint(sourceInletPoint, sourceInletViewModel);

				destInletControlPoints.Add(destInletControlPoint);
			}

			return destInletControlPoints;
		}

		private Point ConvertPoint(Point sourceInletPoint, InletViewModel sourceInletViewModel)
		{
			if (sourceInletPoint == null) throw new NullException(() => sourceInletPoint);
			if (sourceInletViewModel == null) throw new NullException(() => sourceInletViewModel);

			int inletID = VectorGraphicsTagHelper.GetInletID(sourceInletPoint.Tag);

			Point destInletControlPoint;
			if (!_destInletControlPointDictionary.TryGetValue(inletID, out destInletControlPoint))
			{
				destInletControlPoint = new Point
				{
					Diagram = sourceInletPoint.Diagram,
					Parent = sourceInletPoint,
					PointStyle = StyleHelper.PointStyleInvisible,
					Tag = sourceInletPoint.Tag
				};

				_destInletControlPointDictionary.Add(inletID, destInletControlPoint);
				_destInletControlPointHashSet.Add(destInletControlPoint);
			}

			// TODO: Make const of 5f.

			float controlPointDistance;
			if (sourceInletViewModel.ConnectionDistance.HasValue)
			{
				controlPointDistance = sourceInletViewModel.ConnectionDistance.Value / 5f;
			}
			else
			{
				controlPointDistance = DEFAULT_CONTROL_POINT_DISTANCE;
			}

			destInletControlPoint.Position.X = 0;
			destInletControlPoint.Position.Y = -controlPointDistance;

			return destInletControlPoint;
		}

		public void TryRemove(Point destElement)
		{
			if (_destInletControlPointHashSet.Contains(destElement))
			{
				int inletID = VectorGraphicsTagHelper.GetInletID(destElement.Tag);

				_destInletControlPointDictionary.Remove(inletID);
				_destInletControlPointHashSet.Remove(destElement);

				destElement.Children.Clear();
				destElement.Parent = null;
				destElement.Diagram = null;
			}
		}
	}
}