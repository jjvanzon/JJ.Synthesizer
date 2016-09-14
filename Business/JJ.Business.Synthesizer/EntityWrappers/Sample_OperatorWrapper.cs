using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sample_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int FREQUENCY_INDEX = 0;

        private readonly ISampleRepository _sampleRepository;

        public Sample_OperatorWrapper(Operator op, ISampleRepository sampleRepository)
            : base(op)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;
        }

        public Outlet Frequency
        {
            get { return FrequencyInlet.InputOutlet; }
            set { FrequencyInlet.LinkTo(value); }
        }

        public Inlet FrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, FREQUENCY_INDEX);

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

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Frequency);
            return name;
        }
    }
}
