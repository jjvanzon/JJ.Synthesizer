using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Sample : OperatorWrapperBase
    {
        private ISampleRepository _sampleRepository;

        public OperatorWrapper_Sample(Operator op, ISampleRepository sampleRepository)
            : base(op)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;
        }

        public int? SampleID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
        }

        /// <summary> nullable </summary>
        public Sample Sample
        {
            get
            {
                int? sampleID = SampleID;
                if (!sampleID.HasValue)
                {
                    return null;
                }

                return _sampleRepository.TryGet(sampleID.Value);
            }
            set
            {
                if (value == null)
                {
                    SampleID = null;
                    return;
                }

                SampleID = value.ID;
            }
        }

        /// <summary> nullable </summary>
        public byte[] SampleBytes
        {
            get
            {
                int? sampleID = SampleID;
                if (!sampleID.HasValue)
                {
                    return null;
                }

                return _sampleRepository.TryGetBytes(sampleID.Value);
            }
        }

        /// <summary> not nullable </summary>
        public SampleInfo SampleInfo
        {
            get
            {
                return new SampleInfo
                {
                    Sample = this.Sample,
                    Bytes = this.SampleBytes
                };
            }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Sample wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
