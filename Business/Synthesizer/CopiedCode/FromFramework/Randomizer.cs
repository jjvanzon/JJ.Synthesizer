using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Framework.Mathematics
{
	public static class Randomizer
	{
		private static readonly Random _random = CreateRandom();

		private static Random CreateRandom()
		{
			int randomSeed = Guid.NewGuid().GetHashCode();

			var random = new Random(randomSeed);

			return random;
		}

		/// <summary>
		/// Gets a random Int32 between Int32.MinValue and Int32.MaxValue - 1.
		/// </summary>
		public static int GetInt32() => GetInt32(int.MinValue, int.MaxValue - 1);

	    /// <summary>
		/// Gets a random Int32 between 0 and the specified value.
		/// max must at most be Int32.MaxValue - 1 or an overflow exception could occur.
		/// </summary>
		public static int GetInt32(int max) => GetInt32(0, max);

	    /// <summary>
		/// Gets a random Int32 between between a minimum and a maximum.
		/// Both the minimum and the maximum are included.
		/// max must at most be Int32.MaxValue - 1 or an overflow exception could occur.
		/// </summary>
		public static int GetInt32(int min, int max)
		{
			checked
			{
				int result = _random.Next(min, max + 1);
				return result;
			}
		}

		public static T GetRandomItem<T>(IEnumerable<T> collection)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			int count = collection.Count();
			if (count == 0)
			{
				throw new CollectionEmptyException(() => collection);
			}

			int index = GetInt32(count - 1);
			// ReSharper disable once PossibleMultipleEnumeration
			return collection.ElementAt(index);
		}

		public static T TryGetRandomItem<T>(IEnumerable<T> collection)
		{
			// ReSharper disable once PossibleMultipleEnumeration
			int count = collection.Count();
			if (count == 0)
			{
				// Unfortunately, you cannot create overloads that return T? for structs and null for classes.
				// This is not currently possible in C#. I think they're working on it.
				return default;
			}

			int index = GetInt32(count - 1);
			// ReSharper disable once PossibleMultipleEnumeration
			return collection.ElementAt(index);
		}

		/// <summary> Returns a random number between 0.0 and 1.0. </summary>
		public static double GetDouble() => _random.NextDouble();

		/// <summary> Returns a random number between 0.0 and 1.0. </summary>
		public static float GetSingle() => (float)_random.NextDouble();

		/// <param name="min">inclusive</param>
		/// <param name="max">exclusive</param>
		public static double GetDouble(double min, double max)
		{
			double between0And1 = GetDouble();
			double range = max - min;
			double value = min + between0And1 * range;
			return value;
		}

		/// <param name="min">inclusive</param>
		/// <param name="max">exclusive</param>
		public static float GetSingle(float min, float max)
		{
			float between0And1 = GetSingle();
			float range = max - min;
			float  value = min + between0And1 * range;
			return value;
		}
	}
}
