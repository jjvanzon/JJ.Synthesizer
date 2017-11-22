using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
	internal class PositionHelper
	{
		public static float CalculateY(float totalHeight, float rowCount, float rowIndexFrom)
		{
			if (rowCount <= 0) throw new LessThanOrEqualException(() => rowCount, 0);

			float rowHeight = totalHeight / rowCount;
			float y = rowHeight * rowIndexFrom;
			return y;
		}

		public static float CalculateHeight(float totalHeight, float rowCount, float rowIndexFrom, float rowIndexTill)
		{
			if (rowCount <= 0) throw new LessThanOrEqualException(() => rowCount, 0);

			float rowHeight = totalHeight / rowCount;
			float y = rowHeight * (rowIndexTill - rowIndexFrom + 1);
			return y;
		}
	}
}
