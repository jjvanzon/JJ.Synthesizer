using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Helpers
{
	public static class SampleDataTypeHelper
	{
		public static int SizeOf(SampleDataType sampleDataType)
		{
			if (sampleDataType == null) throw new NullException(() => sampleDataType);

			return SizeOf((SampleDataTypeEnum)sampleDataType.ID);
		}

		public static int SizeOf(SampleDataTypeEnum sampleDataTypeEnum)
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
