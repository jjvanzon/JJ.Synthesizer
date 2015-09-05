using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using System;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.Managers
{
    public class PatchManager
    {
        private PatchRepositories _repositories;
        private OperatorFactory _operatorFactory;

        public PatchManager(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _operatorFactory = new OperatorFactory(repositories);
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult AddToPatchRecursive(Operator op, Patch patch)
        {
            if (op == null) throw new NullException(() => op);
            if (patch == null) throw new NullException(() => patch);

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, patch);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            FillInPatchRecursive(op, patch);

            return new VoidResult { Successful = true };
        }

        private void FillInPatchRecursive(Operator op, Patch patch)
        {
            op.LinkTo(patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    FillInPatchRecursive(inlet.InputOutlet.Operator, patch);
                }
            }
        }

        public VoidResult SaveCustomOperator(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => entity.OperatorType, OperatorTypeEnum.CustomOperator);

            ISideEffect sideEffect = new Operator_SideEffect_ApplyUnderlyingDocument(
                entity,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.DocumentRepository,
                _repositories.OperatorTypeRepository,
                _repositories.IDRepository);

            sideEffect.Execute();

            VoidResult result = ValidateNonRecursive(entity);
            return result;
        }

        public VoidResult DeleteWithRelatedEntities(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            bool isMainPatch = patch.Document.MainPatch != null &&
                               (patch.Document.MainPatch == patch ||
                                patch.Document.MainPatch.ID == patch.ID);
            if (isMainPatch)
            {
                var message = new Message { PropertyKey = PropertyNames.Patch, Text = MessageFormatter.CannotDeletePatchBecauseIsMainPatch(patch.Name) };
                return new VoidResult
                {
                    Successful = false,
                    Messages = new Message[] { message }
                };
            }
            else
            {
                patch.DeleteRelatedEntities(_repositories.OperatorRepository, _repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
                patch.UnlinkRelatedEntities();
                _repositories.PatchRepository.Delete(patch);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        public VoidResult Validate(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IValidator validator1 = new PatchValidator_InDocument(patch);
            IValidator validator2 = new PatchValidator_Recursive(patch, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository, new HashSet<object>());

            return new VoidResult
            {
                Messages = Enumerable.Union(validator1.ValidationMessages, validator2.ValidationMessages).ToCanonical(),
                Successful = validator1.IsValid && validator2.IsValid
            };
        }

        public VoidResult ValidateNonRecursive(Operator op)
        {
            IValidator validator = new OperatorValidator_Versatile(op, _repositories.DocumentRepository);

            return new VoidResult
            {
                Messages = validator.ValidationMessages.ToCanonical(),
                Successful = validator.IsValid
            };
        }

        // TODO: These overloads are ugly, e.g. CreateCalculator(true, outlet1)

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(params Outlet[] channelOutlets)
        {
            return CreateCalculator((IList<Outlet>)channelOutlets);
        }

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(bool optimized, params Outlet[] channelOutlets)
        {
            return CreateCalculator((IList<Outlet>)channelOutlets, optimized);
        }

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(IList<Outlet> channelOutlets, bool optimized = true)
        {
            // TODO: Verify channel outlets.

            int assumedSamplingRate = 44100;
            var whiteNoiseCalculator = new WhiteNoiseCalculator(assumedSamplingRate);

            if (optimized)
            {
                return new OptimizedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
            }
            else
            {
                return new InterpretedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
            }
        }

        // Delegation to the OperatorFactory

        public Add_OperatorWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            return _operatorFactory.Add(operandA, operandB);
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            return _operatorFactory.Adder(operands);
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
        {
            return _operatorFactory.Adder(operands);
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            return _operatorFactory.Divide(numerator, denominator, origin);
        }

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            return _operatorFactory.Multiply(operandA, operandB, origin);
        }

        public PatchInlet_OperatorWrapper PatchInlet(Outlet input = null)
        {
            return _operatorFactory.PatchInlet(input);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        { 
            return _operatorFactory.PatchOutlet(input);
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            return _operatorFactory.Power(@base, exponent);
        }

        public Sine_OperatorWrapper Sine(Outlet volume = null, Outlet pitch = null, Outlet level = null, Outlet phaseStart = null)
        {
            return _operatorFactory.Sine(volume, pitch, level, phaseStart);
        }

        public Substract_OperatorWrapper Substract(Outlet operandA = null, Outlet operandB = null)
        {
            return _operatorFactory.Substract(operandA, operandB);
        }

        public TimeAdd_OperatorWrapper TimeAdd(Outlet signal = null, Outlet timeDifference = null)
        {
            return _operatorFactory.TimeAdd(signal, timeDifference);
        }

        public TimeDivide_OperatorWrapper TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            return _operatorFactory.TimeDivide(signal, timeDivider, origin);
        }

        public TimeMultiply_OperatorWrapper TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            return _operatorFactory.TimeMultiply(signal, timeMultiplier, origin);
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            return _operatorFactory.TimePower(signal, exponent, origin);
        }

        public TimeSubstract_OperatorWrapper TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            return _operatorFactory.TimeSubstract(signal, timeDifference);
        }

        public Value_OperatorWrapper Value(double value = 0)
        {
            return _operatorFactory.Value(value);
        }

        public CurveIn_OperatorWrapper CurveIn(Curve curve = null)
        {
            return _operatorFactory.CurveIn(curve);
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            return _operatorFactory.Sample(sample);
        }

        public WhiteNoise_OperatorWrapper WhiteNoise()
        {
            return _operatorFactory.WhiteNoise();
        }

        public Resample_OperatorWrapper Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            return _operatorFactory.Resample(signal, samplingRate);
        }

        public Custom_OperatorWrapper CustomOperator()
        {
            return _operatorFactory.CustomOperator();
        }

        public Custom_OperatorWrapper CustomOperator(Document document)
        {
            return _operatorFactory.CustomOperator(document);
        }

        public Custom_OperatorWrapper CustomOperator(Document document, params Outlet[] operands)
        {
            return _operatorFactory.CustomOperator(document, operands);
        }

        public Custom_OperatorWrapper CustomOperator(Document document, IList<Outlet> operands)
        {
            return _operatorFactory.CustomOperator(document, operands);
        }

        public Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, int inletCountForAdder = 16)
        {
            Operator op = _operatorFactory.Create(operatorTypeEnum, inletCountForAdder);
            return op;
        }

        public Patch CreatePatch(Document document = null, bool mustGenerateName = false)
        {
            var patch = new Patch();
            patch.ID = _repositories.IDRepository.GetID();
            patch.LinkTo(document);
            _repositories.PatchRepository.Insert(patch);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
                sideEffect.Execute();
            }

            return patch;
        }
    }
}