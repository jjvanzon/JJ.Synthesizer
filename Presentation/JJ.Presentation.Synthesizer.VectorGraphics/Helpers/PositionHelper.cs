using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal class PositionHelper
    {
        /// <summary>
        /// Tells us how much an inlet or outlet's clickable rectangle
        /// should overflow the operator rectangle's boundaries.
        /// </summary>
        public const float INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS = 10;

        public const float MINIMUM_INLET_OR_OUTLET_WIDTH_IN_PIXELS = 12;

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
