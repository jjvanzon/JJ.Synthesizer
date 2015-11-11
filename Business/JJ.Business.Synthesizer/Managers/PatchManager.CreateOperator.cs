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
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        public OperatorWrapper_Add Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Add, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Add(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            wrapper.Operator.LinkTo(Patch);

            return wrapper;
        }

        public OperatorWrapper_Adder Adder(params Outlet[] operands)
        {
            return Adder((IList<Outlet>)operands);
        }

        public OperatorWrapper_Adder Adder(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.Adder, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            var wrapper = new OperatorWrapper_Adder(op);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Bundle Bundle(params Outlet[] operands)
        {
            return Bundle((IList<Outlet>)operands);
        }

        public OperatorWrapper_Bundle Bundle(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.Bundle, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            var wrapper = new OperatorWrapper_Bundle(op);
            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Curve Curve(Curve curve = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Curve, inletCount: 0, outletCount: 1);

            var wrapper = new OperatorWrapper_Curve(op, _repositories.CurveRepository);

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        /// <param name="underlyingDocument">The Document to base the CustomOperator on.</param>
        public OperatorWrapper_CustomOperator CustomOperator(Document underlyingDocument, params Outlet[] operands)
        {
            return CustomOperator(underlyingDocument, (IList<Outlet>)operands);
        }

        /// <param name="underlyingDocument">The Document to base the CustomOperator on.</param>
        public OperatorWrapper_CustomOperator CustomOperator(Document underlyingDocument, IList<Outlet> operands)
        {
            if (underlyingDocument == null) throw new NullException(() => underlyingDocument);
            if (operands == null) throw new NullException(() => operands);

            OperatorWrapper_CustomOperator wrapper = CustomOperator(underlyingDocument);

            SetOperands(wrapper.Operator, operands);

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator()
        {
            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            var wrapper = new OperatorWrapper_CustomOperator(op, _repositories.DocumentRepository);

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_CustomOperator CustomOperator(Document underlyingDocument)
        {
            if (underlyingDocument == null) throw new NullException(() => underlyingDocument);
            if (underlyingDocument.MainPatch == null) throw new NullException(() => underlyingDocument.MainPatch);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            IList<Operator> patchInlets = underlyingDocument.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);
            foreach (Operator patchInlet in patchInlets)
            {
                var patchInletWrapper = new OperatorWrapper_PatchInlet(patchInlet);
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.Name = patchInlet.Name;
                inlet.ListIndex = patchInletWrapper.ListIndex;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);
            }

            IList<Operator> patchOutlets = underlyingDocument.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            foreach (Operator patchOutlet in patchOutlets)
            {
                var patchOutletWrapper = new OperatorWrapper_PatchOutlet(patchOutlet);
                var outlet = new Outlet();
                outlet.ID = _repositories.IDRepository.GetID();
                outlet.Name = patchOutlet.Name;
                outlet.ListIndex = patchOutletWrapper.ListIndex;
                outlet.LinkTo(op);
                _repositories.OutletRepository.Insert(outlet);
            }

            var wrapper = new OperatorWrapper_CustomOperator(op, _repositories.DocumentRepository);

            wrapper.UnderlyingDocument = underlyingDocument;

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Delay Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Delay, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Delay(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Divide Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Divide, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_Divide(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Exponent Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Exponent, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_Exponent(op)
            {
                Low = low,
                High = high,
                Ratio = ratio
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Loop Loop(
            Outlet signal = null, 
            Outlet attack = null, 
            Outlet start = null, 
            Outlet sustain = null, 
            Outlet end = null, 
            Outlet release = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Loop, inletCount: 6, outletCount: 1);

            var wrapper = new OperatorWrapper_Loop(op)
            {
                Signal = signal,
                Attack = attack,
                Start = start,
                Sustain = sustain,
                End = end,
                Release = release
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Multiply Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Multiply, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_Multiply(op)
            {
                OperandA = operandA,
                OperandB = operandB,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Number Number(double number = 0)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Number, inletCount: 0, outletCount: 1);

            var wrapper = new OperatorWrapper_Number(op)
            {
                Number = number
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_PatchInlet PatchInlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchInlet, inletCount: 1, outletCount: 1);

            var wrapper = new OperatorWrapper_PatchInlet(op)
            {
                Input = input,
                ListIndex = 0 // You have to set this or the wrapper's ListIndex getter would crash.
            };

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_PatchOutlet PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchOutlet, inletCount: 1, outletCount: 1);

            var wrapper = new OperatorWrapper_PatchOutlet(op)
            {
                Input = input,
                ListIndex = 0 // You have to set this or the wrapper's ListIndex getter would crash.
            };

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_Power Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Power, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Power(op)
            {
                Base = @base,
                Exponent = exponent
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SawTooth SawTooth(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SawTooth, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_SawTooth(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Resample Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Resample, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Resample(op)
            {
                Signal = signal,
                SamplingRate = samplingRate
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Sample Sample(Sample sample = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sample, inletCount: 1, outletCount: 1);

            var wrapper = new OperatorWrapper_Sample(op, _repositories.SampleRepository);
            if (sample != null)
            {
                wrapper.SampleID = sample.ID;
            }

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Select Select(Outlet signal = null, Outlet time = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Select, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Select(op)
            {
                Signal = signal,
                Time = time
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Sine Sine(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sine, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Sine(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Subtract Subtract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Subtract, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Subtract(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SpeedUp SpeedUp(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SpeedUp, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_SpeedUp(op)
            {
                Signal = signal,
                TimeDivider = timeDivider,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SlowDown SlowDown(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SlowDown, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_SlowDown(op)
            {
                Signal = signal,
                TimeMultiplier = timeMultiplier,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SquareWave SquareWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SquareWave, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_SquareWave(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.TimePower, inletCount: 3, outletCount: 1);

            var wrapper = new OperatorWrapper_TimePower(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Earlier Earlier(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Earlier, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_Earlier(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TriangleWave TriangleWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.TriangleWave, inletCount: 2, outletCount: 1);

            var wrapper = new OperatorWrapper_TriangleWave(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Unbundle Unbundle(Outlet operand = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Unbundle, inletCount: 1, outletCount: 1);

            var wrapper = new OperatorWrapper_Unbundle(op)
            {
                Operand = operand
            };

            wrapper.Operator.LinkTo(Patch);

            return wrapper;
        }

        public OperatorWrapper_WhiteNoise WhiteNoise()
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.WhiteNoise, inletCount: 0, outletCount: 1);

            var wrapper = new OperatorWrapper_WhiteNoise(op);

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        // Helpers

        private void SetOperands(Operator op, IList<Outlet> operands)
        {
            if (op.Inlets.Count != operands.Count) throw new NotEqualException(() => op.Inlets.Count, () => operands.Count);

            for (int i = 0; i < operands.Count; i++)
            {
                op.Inlets[i].InputOutlet = operands[i];
            }
        }

        private void ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            ISideEffect sideEffect1 = new Operator_SideEffect_GenerateName(op);
            sideEffect1.Execute();

            ISideEffect sideEffect2 = new Operator_SideEffect_GeneratePatchInletListIndex(op);
            sideEffect2.Execute();

            ISideEffect sideEffect3 = new Operator_SideEffect_GeneratePatchOutletListIndex(op);
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

        // Generic methods for operator creation

        private static Dictionary<OperatorTypeEnum, MethodInfo> _creationMethodDictionary = CreateCreationMethodDictionary();

        private static Dictionary<OperatorTypeEnum, MethodInfo> CreateCreationMethodDictionary()
        {
            OperatorTypeEnum[] enumMembers = (OperatorTypeEnum[])Enum.GetValues(typeof(OperatorTypeEnum));

            var methodDictionary = new Dictionary<OperatorTypeEnum, MethodInfo>(enumMembers.Length);

            foreach (OperatorTypeEnum operatorTypeEnum in enumMembers)
            {
                if (operatorTypeEnum == OperatorTypeEnum.Undefined ||
                    operatorTypeEnum == OperatorTypeEnum.Adder ||
                    operatorTypeEnum == OperatorTypeEnum.Bundle)
                {
                    continue;
                }

                MethodInfo methodInfo;
                if (operatorTypeEnum == OperatorTypeEnum.CustomOperator)
                {
                    methodInfo = typeof(PatchManager).GetMethod(operatorTypeEnum.ToString(), Type.EmptyTypes);
                }
                else
                {
                    methodInfo = typeof(PatchManager).GetMethod(operatorTypeEnum.ToString());
                }
                methodDictionary.Add(operatorTypeEnum, methodInfo);
            }

            return methodDictionary;
        }

        /// <param name="inletCount">
        /// Applies to operators with a variable amount of inlets, such as the Adder operator and the Bundle operator.
        /// </param>
        public Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, int inletCount = 16)
        {
            // Handle operators for which we do not use a generic instantiation.
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Adder:
                    return Adder(new Outlet[inletCount]);

                case OperatorTypeEnum.Bundle:
                    return Bundle(new Outlet[inletCount]);
            }

            MethodInfo methodInfo;

            if (!_creationMethodDictionary.TryGetValue(operatorTypeEnum, out methodInfo))
            {
                throw new ValueNotSupportedException(operatorTypeEnum);
            }

            object[] nullParameters = new object[methodInfo.GetParameters().Length];
            OperatorWrapperBase wrapper = (OperatorWrapperBase)methodInfo.Invoke(this, nullParameters);
            Operator op = wrapper.Operator;
            op.LinkTo(Patch);

            return op;
        }

        private Operator CreateOperatorBase(OperatorTypeEnum operatorTypeEnum, int inletCount, int outletCount)
        {
            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories.OperatorTypeRepository);

            // TODO: This code line was just added on the fly, but really the whole code file must be checked,
            // so that the operators are always linked to the (nullable) Patch.
            op.LinkTo(Patch);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < inletCount; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);
            }

            for (int i = 0; i < outletCount; i++)
            {
                var outlet = new Outlet();
                outlet.ID = _repositories.IDRepository.GetID();
                outlet.ListIndex = i;
                outlet.LinkTo(op);
                _repositories.OutletRepository.Insert(outlet);
            }

            return op;
        }
    }
}
