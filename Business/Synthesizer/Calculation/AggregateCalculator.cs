using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Exceptions;

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

            double[] remaingItems = items.Skip(1).ToArray();

            return ClosestUnsafe(input, items[0], remaingItems, remaingItems.Length);
        }

        /// <summary> Slower than the other overload. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExp(double input, IList<double> items)
        {
            if (items == null) throw new NullException(() => items);
            if (items.Count < 1) throw new NullException(() => items);

            double[] remaingItems = items.Skip(1).ToArray();

            return ClosestExpUnsafe(input, items[0], remaingItems, remaingItems.Length);
        }

        /// <summary> Null-checks a.o. omitted for performance. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestUnsafe(double input, double firstItem, double[] remainingItems, int remainingItemsCount)
        {
            double smallestDistance = Geometry.AbsoluteDistance(input, firstItem);
            double closestItem = firstItem;

            for (int i = 0; i < remainingItemsCount; i++)
            {
                double item = remainingItems[i];

                double distance = Geometry.AbsoluteDistance(input, item);

                // ReSharper disable once InvertIf
                if (smallestDistance > distance)
                {
                    smallestDistance = distance;
                    closestItem = item;
                }
            }

            return closestItem;
        }

        /// <summary> Null-checks a.o. omitted for performance. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExpUnsafe(double input, double firstItem, double[] remainingItems, int remainingItemsCount)
        {
            double logInput = Math.Log(input);

            double smallestDistance = Geometry.AbsoluteDistance(logInput, Math.Log(firstItem));
            double closestItem = firstItem;

            for (int i = 0; i < remainingItemsCount; i++)
            {
                double item = remainingItems[i];

                double distance = Geometry.AbsoluteDistance(logInput, Math.Log(item));

                // ReSharper disable once InvertIf
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

            return distance1 < distance2 ? item1 : item2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestExp(double input, double item1, double item2)
        {
            double logInput = Math.Log(input);

            double distance1 = Geometry.AbsoluteDistance(logInput, Math.Log(item1));
            double distance2 = Geometry.AbsoluteDistance(logInput, Math.Log(item2));

            return distance1 < distance2 ? item1 : item2;
        }
    }
}