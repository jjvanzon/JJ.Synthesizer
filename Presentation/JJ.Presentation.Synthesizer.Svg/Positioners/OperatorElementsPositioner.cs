using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Positioners
{
    /// <summary>
    /// Calculates the rectangles for the regions in the SVG model for an operator 
    /// that have different gestures associated with them
    /// </summary>
    internal static class OperatorElementsPositioner
    {
        public class Result
        {
            public IList<Rectangle> InletRectangles { get; set; }
            public IList<Rectangle> OutletRectangles { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
        }

        private const float ROW_COUNT = 3f;

        public static Result Execute(Rectangle parentRectangle, int inletCount, int outletCount)
        {
            if (parentRectangle == null) throw new NullException(() => parentRectangle);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);
            if (outletCount < 0) throw new LessThanException(() => outletCount, 0);

            float rowHeight = parentRectangle.Height / ROW_COUNT;

            float x;

            IList<Rectangle> inletRectangles = new Rectangle[inletCount];
            IList<Point> inletPoints = new Point[inletCount];
            if (inletCount > 0)
            {
                x = 0;
                float inletWidth = parentRectangle.Width / inletCount;
                for (int i = 0; i < inletCount; i++)
                {
                    inletRectangles[i] = new Rectangle
                    {
                        X = x,
                        Y = 0,
                        Width = inletWidth,
                        Height = rowHeight
                    };

                    inletPoints[i] = new Point
                    {
                        X = x + inletWidth / 2f,
                        Y = 0
                    };

                    x += inletWidth;
                }
            }

            IList<Rectangle> outletRectangles = new Rectangle[outletCount];
            IList<Point> outletPoints = new Point[outletCount];
            if (outletCount > 0)
            {
                x = 0;
                float y = 2f * rowHeight;
                float outletWidth = parentRectangle.Width / outletCount;
                for (int i = 0; i < outletCount; i++)
                {
                    outletRectangles[i] = new Rectangle
                    {
                        X = x,
                        Y = y,
                        Width = outletWidth,
                        Height = rowHeight
                    };

                    outletPoints[i] = new Point
                    {
                        X = x + outletWidth / 2f,
                        Y = parentRectangle.Height
                    };

                    x += outletWidth;
                }
            }

            return new Result 
            {
                InletRectangles = inletRectangles,
                InletPoints = inletPoints,
                OutletRectangles = outletRectangles,
                OutletPoints = outletPoints
            };
        }
    }
}
