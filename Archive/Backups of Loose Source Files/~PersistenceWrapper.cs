//using JJ.Framework.Reflection.Exceptions;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Presentation.Synthesizer.WinForms.Helpers
//{
//    internal class PersistenceWrapper
//    {
//        public IPatchRepository PatchRepository { get; private set; }
//        public IOperatorRepository OperatorRepository { get; private set; }
//        public IOperatorTypeRepository OperatorTypeRepository { get; private set; }
//        public IInletRepository InletRepository { get; private set; }
//        public IOutletRepository OutletRepository { get; private set; }
//        public ICurveRepository CurveRepository { get; private set; }
//        public ISampleRepository SampleRepository { get; private set; }

//        public PersistenceWrapper(
//            IPatchRepository patchRepository,
//            IOperatorRepository operatorRepository,
//            IOperatorTypeRepository operatorTypeRepository,
//            IInletRepository inletRepository,
//            IOutletRepository outletRepository,
//            ICurveRepository curveRepository,
//            ISampleRepository sampleRepository)
//        {
//            if (patchRepository == null) throw new NullException(() => patchRepository);
//            if (operatorRepository == null) throw new NullException(() => operatorRepository);
//            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
//            if (inletRepository == null) throw new NullException(() => inletRepository);
//            if (outletRepository == null) throw new NullException(() => outletRepository);
//            if (curveRepository == null) throw new NullException(() => curveRepository);
//            if (sampleRepository == null) throw new NullException(() => sampleRepository);

//            PatchRepository = patchRepository;
//            OperatorRepository = operatorRepository;
//            OperatorTypeRepository = operatorTypeRepository;
//            InletRepository = inletRepository;
//            OutletRepository = outletRepository;
//            CurveRepository = curveRepository;
//            SampleRepository = sampleRepository;
//        }

//        public void Flush()
//        {
//            OperatorRepository.Flush();
//        }

//        public IEntityPositionRepository EntityPositionRepository { get; set; }
//    }
//}