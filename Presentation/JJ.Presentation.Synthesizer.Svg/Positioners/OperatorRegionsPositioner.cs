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
    internal static class OperatorRegionsPositioner
    {
        public class Result
        {
            public IList<Rectangle> InletRectangles { get; private set; }
            public IList<Rectangle> OutletRectangles { get; private set; }

            public Result(IList<Rectangle> inletRectangles, IList<Rectangle> outletRectangles)
            {
                InletRectangles = inletRectangles;
                OutletRectangles = outletRectangles;
            }
        }

        public static Result Execute(Rectangle parentRectangle, int inletCount, int outletCount)
        {
            if (parentRectangle == null) throw new NullException(() => parentRectangle);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);
            if (outletCount < 0) throw new LessThanException(() => outletCount, 0);

            float rowHeight = parentRectangle.Height / 3f;

            float x;

            IList<Rectangle> inletRectangles;
            if (inletCount > 0)
            {
                x = 0;
                float inletWidth = parentRectangle.Width / inletCount;
                inletRectangles = new Rectangle[inletCount];
                for (int i = 0; i < inletCount; i++)
                {
                    inletRectangles[i] = new Rectangle
                    {
                        X = x,
                        Y = 0,
                        Width = inletWidth,
                        Height = rowHeight
                    };

                    x += inletWidth;
                }
            }
            else
            {
                inletRectangles = new Rectangle[0];
            }

            IList<Rectangle> outletRectangles;
            if (outletCount > 0)
            {
                x = 0;
                float y = 2f * rowHeight;
                float outletWidth = parentRectangle.Width / outletCount;
                outletRectangles = new Rectangle[outletCount];
                for (int i = 0; i < outletCount; i++)
                {
                    outletRectangles[i] = new Rectangle
                    {
                        X = x,
                        Y = y,
                        Width = outletWidth,
                        Height = rowHeight
                    };

                    x += outletWidth;
                }
            }
            else
            {
                outletRectangles = new Rectangle[0];
            }

            return new Result(inletRectangles, outletRectangles);
        }
    }
}
