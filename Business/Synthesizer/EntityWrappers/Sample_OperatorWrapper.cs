using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sample_OperatorWrapper : OperatorWrapperBase_WithFrequency
    {
        private readonly ISampleRepository _sampleRepository;

        public Sample_OperatorWrapper(Operator op, ISampleRepository sampleRepository)
            : base(op)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;
        }

        public int? SampleID
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.SampleID); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.SampleID, value); }
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

                return _sampleRepository.Get(sampleID.Value);
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
        public SampleInfo SampleInfo => new SampleInfo
        {
            // ReSharper disable once ArrangeThisQualifier
            Sample = this.Sample,
            // ReSharper disable once ArrangeThisQualifier
            Bytes = this.SampleBytes
        };
    }
}
