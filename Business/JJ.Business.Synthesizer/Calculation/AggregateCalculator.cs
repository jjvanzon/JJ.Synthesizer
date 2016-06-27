using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Copies.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal static class AggregateCalculator
    {
        /// <summary> Slower than the other overload. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Closest(double input, IList<double> items)
        {
            if (items == null) throw new NullException(() => items);
            if (items.Count < 1) throw new NullException(() => items);

            return Closest(input, items[0], items.Skip(1).ToArray());
        }

        /// <summary> Slower than the other overload. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExp(double input, IList<double> items)
        {
            if (items == null) throw new NullException(() => items);
            if (items.Count < 1) throw new NullException(() => items);

            return ClosestExp(input, items[0], items.Skip(1).ToArray());
        }

        /// <summary> Null-checks omitted for performance. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Closest(double input, double firstItem, double[] remainingItems)
        {
            double smallestDistance = Geometry.AbsoluteDistance(input, firstItem);
            double closestItem = firstItem;

            for (int i = 0; i < remainingItems.Length; i++)
            {
                double item = remainingItems[i];

                double distance = Geometry.AbsoluteDistance(input, item);

                if (smallestDistance > distance)
                {
                    smallestDistance = distance;
                    closestItem = item;
                }
            }

            return closestItem;
        }

        /// <summary> Null-checks omitted for performance. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExp(double input, double firstItem, double[] remainingItems)
        {
            double logInput = Math.Log(input);

            double smallestDistance = Geometry.AbsoluteDistance(logInput, Math.Log(firstItem));
            double closestItem = firstItem;

            for (int i = 0; i < remainingItems.Length; i++)
            {
                double item = remainingItems[i];

                double distance = Geometry.AbsoluteDistance(logInput, Math.Log(item));

                if (smallestDistance > distance)
                {
                    smallestDistance = distance;
                    closestItem = item;
                }
            }

            return closestItem;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Closest(double input, double item1, double item2)
        {
            double distance1 = Geometry.AbsoluteDistance(input, item1);
            double distance2 = Geometry.AbsoluteDistance(input, item2);

            if (distance1 < distance2)
            {
                return item1;
            }
            else
            {
                return item2;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExp(double input, double item1, double item2)
        {
            double logInput = Math.Log(input);

            double distance1 = Geometry.AbsoluteDistance(logInput, Math.Log(item1));
            double distance2 = Geometry.AbsoluteDistance(logInput, Math.Log(item2));

            if (distance1 < distance2)
            {
                return item1;
            }
            else
            {
                return item2;
            }
        }
    }
}