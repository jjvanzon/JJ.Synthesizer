using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class SampleDataTypeExtensions
	{
		public static int SizeOf(this SampleDataType sampleDataType)
		{
			if (sampleDataType == null) throw new NullException(() => sampleDataType);

			return SizeOf((SampleDataTypeEnum)sampleDataType.ID);
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public static int SizeOf(this SampleDataTypeEnum sampleDataTypeEnum)
		{
			switch (sampleDataTypeEnum)
			{
				case SampleDataTypeEnum.Byte:
					return 1;

				case SampleDataTypeEnum.Int16:
					return 2;

				default:
					throw new ValueNotSupportedException(sampleDataTypeEnum);
			}
		}
	}
}
