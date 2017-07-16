using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
#pragma warning disable IDE0003

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sample_OperatorWrapper : OperatorWrapper
    {
        private readonly ISampleRepository _sampleRepository;

        public Sample_OperatorWrapper(Operator op, ISampleRepository sampleRepository)
            : base(op)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
        }

        public int? SampleID
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, nameof(SampleID));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(SampleID), value);
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
