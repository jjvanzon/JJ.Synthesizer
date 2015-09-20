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

        public Add_OperatorWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            var wrapper = _operatorFactory.Add(operandA, operandB);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            var wrapper = _operatorFactory.Adder(operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
        {
            var wrapper = _operatorFactory.Adder(operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.Divide(numerator, denominator, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.Multiply(operandA, operandB, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(Outlet input = null)
        {
            var wrapper = _operatorFactory.PatchInlet(input);

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            var wrapper = _operatorFactory.PatchOutlet(input);

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            var wrapper = _operatorFactory.Power(@base, exponent);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Sine_OperatorWrapper Sine(Outlet volume = null, Outlet pitch = null, Outlet origin = null, Outlet phaseStart = null)
        {
            var wrapper = _operatorFactory.Sine(volume, pitch, origin, phaseStart);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Substract_OperatorWrapper Substract(Outlet operandA = null, Outlet operandB = null)
        {
            var wrapper = _operatorFactory.Substract(operandA, operandB);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public TimeAdd_OperatorWrapper TimeAdd(Outlet signal = null, Outlet timeDifference = null)
        {
            var wrapper = _operatorFactory.TimeAdd(signal, timeDifference);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public TimeDivide_OperatorWrapper TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.TimeDivide(signal, timeDivider, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public TimeMultiply_OperatorWrapper TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.TimeMultiply(signal, timeMultiplier, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            var wrapper = _operatorFactory.TimePower(signal, exponent, origin);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public TimeSubstract_OperatorWrapper TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            var wrapper = _operatorFactory.TimeSubstract(signal, timeDifference);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Value_OperatorWrapper Value(double value = 0)
        {
            var wrapper = _operatorFactory.Value(value);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Curve_OperatorWrapper Curve(Curve curve = null)
        {
            var wrapper = _operatorFactory.Curve(curve);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            var wrapper = _operatorFactory.Sample(sample);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public WhiteNoise_OperatorWrapper WhiteNoise()
        {
            var wrapper = _operatorFactory.WhiteNoise();
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Resample_OperatorWrapper Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            var wrapper = _operatorFactory.Resample(signal, samplingRate);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Custom_OperatorWrapper CustomOperator()
        {
            var wrapper = _operatorFactory.CustomOperator();
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Custom_OperatorWrapper CustomOperator(Document document)
        {
            var wrapper = _operatorFactory.CustomOperator(document);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Custom_OperatorWrapper CustomOperator(Document document, params Outlet[] operands)
        {
            var wrapper = _operatorFactory.CustomOperator(document, operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public Custom_OperatorWrapper CustomOperator(Document document, IList<Outlet> operands)
        {
            var wrapper = _operatorFactory.CustomOperator(document, operands);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        private void TryExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
                operatorTypeEnum == OperatorTypeEnum.PatchOutlet)
            {
                ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(op);
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
