using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal class PersistenceWrapper
    {
        public IOperatorRepository OperatorRepository { get; private set; }
        public IInletRepository InletRepository { get; private set; }
        public IOutletRepository OutletRepository { get; private set; }
        public ICurveInRepository CurveInRepository { get; private set; }
        public IValueOperatorRepository ValueOperatorRepository { get; private set; }
        public ISampleOperatorRepository SampleOperatorRepository { get; private set; }

        public PersistenceWrapper(
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            ICurveInRepository curveInRepository,
            IValueOperatorRepository valueOperatorRepository,
            ISampleOperatorRepository sampleOperatorRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveInRepository == null) throw new NullException(() => curveInRepository);
            if (valueOperatorRepository == null) throw new NullException(() => valueOperatorRepository);
            if (sampleOperatorRepository == null) throw new NullException(() => sampleOperatorRepository);

            OperatorRepository = operatorRepository;
            InletRepository = inletRepository;
            OutletRepository = outletRepository;
            CurveInRepository = curveInRepository;
            ValueOperatorRepository = valueOperatorRepository;
            SampleOperatorRepository = sampleOperatorRepository;
        }

        public void Flush()
        {
            OperatorRepository.Flush();
        }
    }
}