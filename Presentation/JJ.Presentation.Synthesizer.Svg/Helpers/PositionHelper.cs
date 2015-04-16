using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Helpers
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
