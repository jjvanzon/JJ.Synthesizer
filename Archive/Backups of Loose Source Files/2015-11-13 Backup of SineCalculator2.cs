using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class SineCalculator2
    {
        public double Sin(double angleInRadians)
        {
            //angleInRadians = angleInRadians % Maths.TWO_PI - Math.PI;

            //always wrap input angle to -PI..PI
            if (angleInRadians < -3.14159265)
            {
                angleInRadians += 6.28318531;
            }
            else if (angleInRadians > 3.14159265)
            {
                angleInRadians -= 6.28318531;
            }

            //compute sine
            if (angleInRadians < 0)
            {
                double sin = 1.27323954 * angleInRadians + .405284735 * angleInRadians * angleInRadians;
                return sin;
            }
            else
            {
                double sin = 1.27323954 * angleInRadians - 0.405284735 * angleInRadians * angleInRadians;
                return sin;
            }
        }
    }
}