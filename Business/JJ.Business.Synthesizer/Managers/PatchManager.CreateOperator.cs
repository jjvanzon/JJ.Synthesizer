using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        public Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, int inletCountForAdder = 16)
        {
            Operator op = _operatorFactory.CreateOperatorPolymorphic(operatorTypeEnum, inletCountForAdder);
            op.LinkTo(Patch);

            TryExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(op);

            return op;
        }

        public OperatorWrapper_Add Add(Outlet operandA = null, Outlet operandB = null)
        {
            var wrapper = _operatorFactory.Add(operandA, operandB);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Adder Adder(params Outlet[] operands)
        {
            var wrapper = _operatorFactory.Adder(operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Adder Adder(IList<Outlet> operands)
        {
            var wrapper = _operatorFactory.Adder(operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Divide Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.Divide(numerator, denominator, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Multiply Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.Multiply(operandA, operandB, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_PatchInlet PatchInlet(Outlet input = null)
        {
            var wrapper = _operatorFactory.PatchInlet(input);

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_PatchOutlet PatchOutlet(Outlet input = null)
        {
            var wrapper = _operatorFactory.PatchOutlet(input);

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_Power Power(Outlet @base = null, Outlet exponent = null)
        {
            var wrapper = _operatorFactory.Power(@base, exponent);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Sine Sine(Outlet volume = null, Outlet pitch = null, Outlet origin = null, Outlet phaseStart = null)
        {
            var wrapper = _operatorFactory.Sine(volume, pitch, origin, phaseStart);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Substract Substract(Outlet operandA = null, Outlet operandB = null)
        {
            var wrapper = _operatorFactory.Substract(operandA, operandB);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Delay Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            var wrapper = _operatorFactory.Delay(signal, timeDifference);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SpeedUp SpeedUp(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.SpeedUp(signal, timeDivider, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SlowDown SlowDown(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.SlowDown(signal, timeMultiplier, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.TimePower(signal, exponent, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            var wrapper = _operatorFactory.TimeSubstract(signal, timeDifference);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Number Number(double number = 0)
        {
            var wrapper = _operatorFactory.Number(number);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Curve Curve(Curve curve = null)
        {
            var wrapper = _operatorFactory.Curve(curve);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Sample Sample(Sample sample = null)
        {
            var wrapper = _operatorFactory.Sample(sample);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_WhiteNoise WhiteNoise()
        {
            var wrapper = _operatorFactory.WhiteNoise();
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Resample Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            var wrapper = _operatorFactory.Resample(signal, samplingRate);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator()
        {
            var wrapper = _operatorFactory.CustomOperator();
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator(Document document)
        {
            var wrapper = _operatorFactory.CustomOperator(document);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator(Document document, params Outlet[] operands)
        {
            var wrapper = _operatorFactory.CustomOperator(document, operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator(Document document, IList<Outlet> operands)
        {
            var wrapper = _operatorFactory.CustomOperator(document, operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        private void TryExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            switch (op.GetOperatorTypeEnum())
            {
                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                    ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(op);
                    break;
            }
        }

        private void ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            ISideEffect sideEffect1 = new Operator_SideEffect_GenerateName(op);
            sideEffect1.Execute();

            ISideEffect sideEffect2 = new Operator_SideEffect_GeneratePatchInletSortOrder(op);
            sideEffect2.Execute();

            ISideEffect sideEffect3 = new Operator_SideEffect_GeneratePatchOutletSortOrder(op);
            sideEffect3.Execute();

            ISideEffect sideEffect4 = new Document_SideEffect_UpdateDependentCustomOperators(
                op.Patch.Document,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.DocumentRepository,
                _repositories.OperatorTypeRepository,
                _repositories.IDRepository);

            sideEffect4.Execute();
        }
    }
}
