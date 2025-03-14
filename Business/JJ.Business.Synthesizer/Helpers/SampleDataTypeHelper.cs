﻿using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
                case SampleDataTypeEnum.Float32:
                    return 4;

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
    }
}
