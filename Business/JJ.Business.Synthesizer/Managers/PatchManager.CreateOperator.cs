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
using JJ.Business.CanonicalModel;

namespace JJ.Business.Synthesizer.Managers
{
    public partial class PatchManager
    {
        public OperatorWrapper_Add Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Add, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

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
                inlet.Name = String.Format("{0}{1}", PropertyNames.Operand, i + 1);
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.Name = PropertyNames.Result;
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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Curve, 0,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Curve(op, _repositories.CurveRepository);

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

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
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.Name = patchInlet.Name;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);
            }

            IList<Operator> patchOutlets = underlyingDocument.MainPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            foreach (Operator patchOutlet in patchOutlets)
            {
                var outlet = new Outlet();
                outlet.ID = _repositories.IDRepository.GetID();
                outlet.Name = patchOutlet.Name;
                outlet.LinkTo(op);
                _repositories.OutletRepository.Insert(outlet);
            }

            var wrapper = new OperatorWrapper_CustomOperator(op, _repositories.DocumentRepository);

            wrapper.UnderlyingDocument = underlyingDocument;

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

        public OperatorWrapper_Delay Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Delay, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Divide, 3,
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Exponent, 3,
                PropertyNames.Low, PropertyNames.High, PropertyNames.Ratio,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Loop, 6,
                PropertyNames.Signal, 
                PropertyNames.Attack, 
                PropertyNames.Start, 
                PropertyNames.Sustain, 
                PropertyNames.End,
                PropertyNames.Release,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Multiply, 3,
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Number, 0,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Number(op)
            {
                Number = number
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_PatchInlet PatchInlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PatchInlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_PatchInlet(op)
            {
                Input = input,
                SortOrder = 0 // You have to set this or the wrapper's SortOrder getter would crash.
            };

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_PatchOutlet PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PatchOutlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_PatchOutlet(op)
            {
                Input = input,
                SortOrder = 0 // You have to set this or the wrapper's SortOrder getter would crash.
            };

            wrapper.Operator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.Operator);

            return wrapper;
        }

        public OperatorWrapper_Power Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Power, 2,
                PropertyNames.Base, PropertyNames.Exponent,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Power(op)
            {
                Base = @base,
                Exponent = exponent
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SawTooth SawTooth(Outlet pitch = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SawTooth, 2,
                PropertyNames.Pitch, PropertyNames.PhaseShift,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_SawTooth(op)
            {
                Pitch = pitch,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Resample Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Resample, 2,
                PropertyNames.Signal, PropertyNames.SamplingRate,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Sample, 0,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Select, 2,
                PropertyNames.Signal, PropertyNames.Time,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Select(op)
            {
                Signal = signal,
                Time = time
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Sine Sine(Outlet volume = null, Outlet pitch = null, Outlet origin = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Origin, PropertyNames.PhaseShift,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Sine(op)
            {
                Volume = volume,
                Pitch = pitch,
                Origin = origin,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Substract Substract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Substract, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_Substract(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SpeedUp SpeedUp(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SpeedUp, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin,
                PropertyNames.Result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SlowDown, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_SlowDown(op)
            {
                Signal = signal,
                TimeMultiplier = timeMultiplier,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_SquareWave SquareWave(Outlet pitch = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SquareWave, 2,
                PropertyNames.Pitch, PropertyNames.PhaseShift,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_SquareWave(op)
            {
                Pitch = pitch,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.TimePower, 3,
                PropertyNames.Signal, PropertyNames.Exponent, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_TimePower(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.TimeSubstract, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_TimeSubstract(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_TriangleWave TriangleWave(Outlet pitch = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.TriangleWave, 2,
                PropertyNames.Pitch, PropertyNames.PhaseShift,
                PropertyNames.Result);

            var wrapper = new OperatorWrapper_TriangleWave(op)
            {
                Pitch = pitch,
                PhaseShift = phaseShift
            };

            wrapper.Operator.LinkTo(Patch);
            return wrapper;
        }

        public OperatorWrapper_Unbundle Unbundle(Outlet operand = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Unbundle, 1,
                PropertyNames.Operand);

            var wrapper = new OperatorWrapper_Unbundle(op)
            {
                Operand = operand
            };

            wrapper.Operator.LinkTo(Patch);

            return wrapper;
        }

        public OperatorWrapper_WhiteNoise WhiteNoise()
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.WhiteNoise, 0,
                PropertyNames.Result);

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

        private Operator CreateOperatorBase(OperatorTypeEnum operatorTypeEnum, int inletCount, params string[] inletAndOutletNames)
        {
            if (inletCount > inletAndOutletNames.Length) throw new GreaterThanException(() => inletCount, () => inletAndOutletNames.Length);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            int sortOrder = 1;
            foreach (string inletName in inletAndOutletNames.Take(inletCount))
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.Name = inletName;
                inlet.SortOrder = sortOrder++;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);
            }

            sortOrder = 1;
            foreach (string outletName in inletAndOutletNames.Skip(inletCount))
            {
                var outlet = new Outlet();
                outlet.ID = _repositories.IDRepository.GetID();
                outlet.Name = outletName;
                outlet.SortOrder = sortOrder++;
                outlet.LinkTo(op);
                _repositories.OutletRepository.Insert(outlet);
            }

            return op;
        }
    }
}
