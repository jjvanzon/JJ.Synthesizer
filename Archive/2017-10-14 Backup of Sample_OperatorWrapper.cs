//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Exceptions;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//// ReSharper disable ArrangeThisQualifier
//#pragma warning disable IDE0003 // Remove qualification

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Sample_OperatorWrapper : OperatorWrapper
//    {
//        private readonly ISampleRepository _sampleRepository;

//        public Sample_OperatorWrapper(Operator op, ISampleRepository sampleRepository)
//            : base(op)
//        {
//            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
//        }

//        /// <summary> nullable </summary>
//        public Sample Sample => _sampleRepository.TryGetByOperatorID(WrappedOperator.ID);

//        /// <summary> nullable </summary>
//        public byte[] SampleBytes
//        {
//            get
//            {
//                Sample sample = Sample;
//                return sample == null ? null : _sampleRepository.GetBytes(sample.ID);
//            }
//        }

//        /// <summary> not nullable </summary>
//        public SampleInfo SampleInfo => new SampleInfo
//        {
//            Sample = this.Sample,
//            Bytes = this.SampleBytes
//        };
//    }
//}
