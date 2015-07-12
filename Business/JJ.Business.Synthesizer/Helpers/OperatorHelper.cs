using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class OperatorHelper
    {
        /// <summary>
        /// This method is extracted from the OperatorFactory with the idea that the OperatorWrappers
        /// would also be capable of creating their own underlying operators. However that has not been implemented (yet).
        /// </summary>
        public static Operator CreateOperator(
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository, 
            IInletRepository inletRepository, IOutletRepository outletRepository, IIDRepository idRepository,
            OperatorTypeEnum operatorTypeEnum, string name, int inletCount, params string[] inletAndOutletNames)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);
            if (inletAndOutletNames == null) throw new NullException(() => inletAndOutletNames);

            if (inletCount > inletAndOutletNames.Length) throw new GreaterThanException(() => inletCount, () => inletAndOutletNames.Length);

            Operator op = operatorRepository.Create();
            op.ID = idRepository.GetID();
            op.Name = name;
            op.SetOperatorTypeEnum(operatorTypeEnum, operatorTypeRepository);

            foreach (string inletName in inletAndOutletNames.Take(inletCount))
            {
                Inlet inlet = inletRepository.Create();
                inlet.ID = idRepository.GetID();
                inlet.Name = inletName;
                inlet.LinkTo(op);
            }

            foreach (string outletName in inletAndOutletNames.Skip(inletCount))
            {
                Outlet outlet = outletRepository.Create();
                outlet.ID = idRepository.GetID();
                outlet.Name = outletName;
                outlet.LinkTo(op);
            }

            return op;
        }
    }
}