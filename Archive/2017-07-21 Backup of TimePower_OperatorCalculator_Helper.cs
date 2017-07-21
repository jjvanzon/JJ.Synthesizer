//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal static class TimePower_OperatorCalculator_Helper
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static double GetTransformedPosition(double position, double exponent)
//        {
//            // IMPORTANT: 

//            // To increase time in the output, you have to decrease time of the input. 
//            // That is why the reciprocal of the exponent is used.

//            // Furthermore, you can not use a fractional exponent on a negative number.
//            // Time can be negative, that is why the sign is taken off the time 
//            // before taking the power and then added to it again after taking the power.

//            // (time: -4, exponent: 2) => -1 * Pow(4, 1/2)

//            double positionAbs;
//            if (position >= 0.0)
//            {
//                positionAbs = position;
//            }
//            else
//            {
//                positionAbs = -position;
//            }

//            double transformedPosition = Math.Pow(positionAbs, 1.0 / exponent);

//            if (position < 0.0)
//            {
//                transformedPosition = -transformedPosition;
//            }

//            return transformedPosition;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static double GetTransformedPosition(double position, double exponent, double origin)
//        {
//            // IMPORTANT: 

//            // To increase time in the output, you have to decrease time of the input. 
//            // That is why the reciprocal of the exponent is used.

//            // Furthermore, you can not use a fractional exponent on a negative number.
//            // Time can be negative, that is why the sign is taken off the time 
//            // before taking the power and then added to it again after taking the power.

//            double distanceFromOrigin = position - origin;

//            double distanceFromOriginAbs;
//            if (distanceFromOrigin >= 0.0)
//            {
//                distanceFromOriginAbs = distanceFromOrigin;
//            }
//            else
//            {
//                distanceFromOriginAbs = -distanceFromOrigin;
//            }

//            double transformedPosition = Math.Pow(distanceFromOriginAbs, 1.0 / exponent);

//            if (distanceFromOrigin < 0.0)
//            {
//                transformedPosition = -transformedPosition;
//            }

//            return transformedPosition;
//        }

//    }
//}
