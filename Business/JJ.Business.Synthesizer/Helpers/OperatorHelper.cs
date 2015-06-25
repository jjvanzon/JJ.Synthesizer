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
        public static Operator CreateOperator(
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository, IInletRepository inletRepository, IOutletRepository outletRepository,
            OperatorTypeEnum operatorTypeEnum, string name, int inletCount, params string[] inletAndOutletNames)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (inletAndOutletNames == null) throw new NullException(() => inletAndOutletNames);

            if (inletCount > inletAndOutletNames.Length) throw new GreaterThanException(() => inletCount, () => inletAndOutletNames.Length);

            Operator op = operatorRepository.Create();
            op.Name = name;
            op.SetOperatorTypeEnum(operatorTypeEnum, operatorTypeRepository);

            foreach (string inletName in inletAndOutletNames.Take(inletCount))
            {
                Inlet inlet = inletRepository.Create();
                inlet.Name = inletName;
                inlet.LinkTo(op);
            }

            foreach (string outletName in inletAndOutletNames.Skip(inletCount))
            {
                Outlet outlet = outletRepository.Create();
                outlet.Name = outletName;
                outlet.LinkTo(op);
            }

            return op;
        }
    }
}