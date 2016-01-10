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
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
        public Add_OperatorWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Add, inletCount: 2, outletCount: 1);

            var wrapper = new Add_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            wrapper.WrappedOperator.LinkTo(Patch);

            return wrapper;
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            return Adder((IList<Outlet>)operands);
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
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

            var wrapper = new Adder_OperatorWrapper(op);
            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Bundle_OperatorWrapper Bundle(params Outlet[] operands)
        {
            return Bundle((IList<Outlet>)operands);
        }

        public Bundle_OperatorWrapper Bundle(IList<Outlet> operands)
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

            var wrapper = new Bundle_OperatorWrapper(op);
            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Curve_OperatorWrapper Curve(Curve curve = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Curve, inletCount: 0, outletCount: 1);

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository);

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public CustomOperator_OperatorWrapper CustomOperator()
        {
            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            var wrapper = new CustomOperator_OperatorWrapper(op, _repositories.PatchRepository);

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch)
        {
            CustomOperator_OperatorWrapper op = CustomOperator();
            op.UnderlyingPatch = underlyingPatch;

            ISideEffect sideEffect = new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories);
            sideEffect.Execute();

            return op;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (operands == null) throw new NullException(() => operands);

            CustomOperator_OperatorWrapper wrapper = CustomOperator(underlyingPatch);

            SetOperands(wrapper.WrappedOperator, operands);

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return CustomOperator(underlyingPatch, (IList<Outlet>)operands);
        }

        public Delay_OperatorWrapper Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Delay, inletCount: 2, outletCount: 1);

            var wrapper = new Delay_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Divide, inletCount: 3, outletCount: 1);

            var wrapper = new Divide_OperatorWrapper(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Exponent, inletCount: 3, outletCount: 1);

            var wrapper = new Exponent_OperatorWrapper(op)
            {
                Low = low,
                High = high,
                Ratio = ratio
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Loop_OperatorWrapper Loop(
            Outlet signal = null, 
            Outlet attack = null, 
            Outlet start = null, 
            Outlet sustain = null, 
            Outlet end = null, 
            Outlet release = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Loop, inletCount: 6, outletCount: 1);

            var wrapper = new Loop_OperatorWrapper(op)
            {
                Signal = signal,
                Attack = attack,
                Start = start,
                Sustain = sustain,
                End = end,
                Release = release
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Multiply, inletCount: 3, outletCount: 1);

            var wrapper = new Multiply_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB,
                Origin = origin
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Number, inletCount: 0, outletCount: 1);

            var wrapper = new Number_OperatorWrapper(op)
            {
                Number = number
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(InletTypeEnum inletTypeEnum)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.SetInletTypeEnum(inletTypeEnum, _repositories.InletTypeRepository);
            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(InletTypeEnum inletTypeEnum, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            Inlet patchInletInlet = wrapper.Inlet;
            patchInletInlet.SetInletTypeEnum(inletTypeEnum, _repositories.InletTypeRepository);
            patchInletInlet.DefaultValue = defaultValue;

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Name = name;
            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Name = name;
            wrapper.Inlet.DefaultValue = defaultValue;
            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchInlet, inletCount: 1, outletCount: 1);

            var wrapper = new PatchInlet_OperatorWrapper(op)
            {
                // You have to set this property or the wrapper's ListIndex getter would crash.
                ListIndex = 0,
            };

            wrapper.WrappedOperator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(OutletTypeEnum outletTypeEnum, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Result.SetOutletTypeEnum(outletTypeEnum, _repositories.OutletTypeRepository);
            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Name = name;
            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchOutlet, inletCount: 1, outletCount: 1);

            var wrapper = new PatchOutlet_OperatorWrapper(op)
            {
                Input = input,
                // You have to set this property two or the wrapper's ListIndex property getter would crash.
                ListIndex = 0,
            };

            wrapper.WrappedOperator.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            return wrapper;
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Power, inletCount: 2, outletCount: 1);

            var wrapper = new Power_OperatorWrapper(op)
            {
                Base = @base,
                Exponent = exponent
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public SawTooth_OperatorWrapper SawTooth(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SawTooth, inletCount: 2, outletCount: 1);

            var wrapper = new SawTooth_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Resample_OperatorWrapper Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Resample, inletCount: 2, outletCount: 1);

            var wrapper = new Resample_OperatorWrapper(op)
            {
                Signal = signal,
                SamplingRate = samplingRate
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sample, inletCount: 1, outletCount: 1);

            var wrapper = new Sample_OperatorWrapper(op, _repositories.SampleRepository);
            if (sample != null)
            {
                wrapper.SampleID = sample.ID;
            }

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Select_OperatorWrapper Select(Outlet signal = null, Outlet time = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Select, inletCount: 2, outletCount: 1);

            var wrapper = new Select_OperatorWrapper(op)
            {
                Signal = signal,
                Time = time
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Sine_OperatorWrapper Sine(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sine, inletCount: 2, outletCount: 1);

            var wrapper = new Sine_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Subtract_OperatorWrapper Subtract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Subtract, inletCount: 2, outletCount: 1);

            var wrapper = new Subtract_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public SpeedUp_OperatorWrapper SpeedUp(Outlet signal = null, Outlet factor = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SpeedUp, inletCount: 2, outletCount: 1);

            var wrapper = new SpeedUp_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public SlowDown_OperatorWrapper SlowDown(Outlet signal = null, Outlet factor = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SlowDown, inletCount: 2, outletCount: 1);

            var wrapper = new SlowDown_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public SquareWave_OperatorWrapper SquareWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SquareWave, inletCount: 2, outletCount: 1);

            var wrapper = new SquareWave_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.TimePower, inletCount: 3, outletCount: 1);

            var wrapper = new TimePower_OperatorWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Earlier_OperatorWrapper Earlier(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Earlier, inletCount: 2, outletCount: 1);

            var wrapper = new Earlier_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public TriangleWave_OperatorWrapper TriangleWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.TriangleWave, inletCount: 2, outletCount: 1);

            var wrapper = new TriangleWave_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand = null, int outletCount = 1)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Unbundle, inletCount: 1, outletCount: outletCount);

            var wrapper = new Unbundle_OperatorWrapper(op)
            {
                Operand = operand
            };

            wrapper.WrappedOperator.LinkTo(Patch);

            return wrapper;
        }

        public WhiteNoise_OperatorWrapper WhiteNoise()
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.WhiteNoise, inletCount: 0, outletCount: 1);

            var wrapper = new WhiteNoise_OperatorWrapper(op);

            wrapper.WrappedOperator.LinkTo(Patch);
            return wrapper;
        }

        // Helpers

        private void SetOperands(Operator op, IList<Outlet> operands)
        {
            if (op.Inlets.Count != operands.Count) throw new NotEqualException(() => op.Inlets.Count, () => operands.Count);

            for (int i = 0; i < operands.Count; i++)
            {
                // TODO: Use LinkTo.
                op.Inlets[i].InputOutlet = operands[i];
            }
        }

        private void ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            ISideEffect sideEffect2 = new Operator_SideEffect_GeneratePatchInletListIndex(op);
            sideEffect2.Execute();

            ISideEffect sideEffect3 = new Operator_SideEffect_GeneratePatchOutletListIndex(op);
            sideEffect3.Execute();

            ISideEffect sideEffect4 = new Patch_SideEffect_UpdateDependentCustomOperators(op.Patch, _repositories);
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
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.Undefined:
                    case OperatorTypeEnum.Adder:
                    case OperatorTypeEnum.Bundle:
                        continue;

                    case OperatorTypeEnum.CustomOperator:
                        {
                            MethodInfo methodInfo = typeof(PatchManager).GetMethod(operatorTypeEnum.ToString(), Type.EmptyTypes);
                            if (methodInfo == null)
                            {
                                throw new Exception(String.Format("MethodInfo '{0}' not found in type '{1}'.", operatorTypeEnum, typeof(PatchManager).Name));
                            }
                            methodDictionary.Add(operatorTypeEnum, methodInfo);
                            break;
                        }

                    case OperatorTypeEnum.PatchInlet:
                        {
                            string methodName = operatorTypeEnum.ToString();
                            MethodInfo methodInfo = typeof(PatchManager).GetMethod(methodName, Type.EmptyTypes);
                            if (methodInfo == null)
                            {
                                throw new Exception(String.Format("Method '{0}' not found in type '{1}'.", methodName, typeof(PatchManager).Name));
                            }
                            methodDictionary.Add(operatorTypeEnum, methodInfo);
                            break;
                        }

                    case OperatorTypeEnum.PatchOutlet:
                        {
                            string methodName = operatorTypeEnum.ToString();
                            Type[] parameterTypes = new Type[] { typeof(Outlet) };
                            MethodInfo methodInfo = typeof(PatchManager).GetMethod(methodName, parameterTypes);
                            if (methodInfo == null)
                            {
                                throw new Exception(String.Format("Method '{0}' not found in type '{1}'.", methodName, typeof(PatchManager).Name));
                            }
                            methodDictionary.Add(operatorTypeEnum, methodInfo);
                            break;
                        }

                    default:
                        {
                            string methodName = operatorTypeEnum.ToString();
                            MethodInfo methodInfo = typeof(PatchManager).GetMethod(methodName);
                            if (methodInfo == null)
                            {
                                throw new Exception(String.Format("Method '{0}' not found in type '{1}'.", methodName, typeof(PatchManager).Name));
                            }
                            methodDictionary.Add(operatorTypeEnum, methodInfo);
                            break;
                        }
                        break;
                }
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
            Operator op = wrapper.WrappedOperator;
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
