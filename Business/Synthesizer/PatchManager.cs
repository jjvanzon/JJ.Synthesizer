using GeneratedCSharp;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Roslyn;
using JJ.Business.Synthesizer.Roslyn.Calculation;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Business.Synthesizer.Validation.Patches;
using JJ.Business.Synthesizer.Visitors;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer
{
    /// <summary>
    /// Manages a Patch and its Operators.
    /// You can supply a patch, Create a new one using the CreatePatch method
    /// or omit the Patch to only call methods that do not require it.
    /// </summary>
    public class PatchManager
    {
        private static readonly CalculationMethodEnum _calculationMethodEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().CalculationMethod;

        private readonly RepositoryWrapper _repositories;

        // Constructors

        public PatchManager(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        // Create

        /// <param name="document">Nullable. Used e.g. to generate a unique name for a Patch.</param>
        public Patch CreatePatch(Document document = null)
        {
            var patch = new Patch { ID = _repositories.IDRepository.GetID() };
            _repositories.PatchRepository.Insert(patch);

            patch.LinkTo(document);

            new Patch_SideEffect_GenerateName(patch).Execute();

            return patch; 
        }

        public Inlet CreateInlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var inlet = new Inlet { ID = _repositories.IDRepository.GetID() };
            _repositories.InletRepository.Insert(inlet);

            inlet.LinkTo(op);

            return inlet;
        }

        public Outlet CreateOutlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var outlet = new Outlet { ID = _repositories.IDRepository.GetID() };
            _repositories.OutletRepository.Insert(outlet);

            outlet.LinkTo(op);

            return outlet;
        }

        // Save

        public VoidResult SavePatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            // TODO: At one time, it said Patch.Operators collection was changed. That is what the ToArray is for. 
            // I still do not know why the collection was changed, so that must be investigated.
            // (It was when SaveOperator was called instead of ExecuteSideEffects.)

            foreach (Operator op in patch.Operators.ToArray())
            {
                new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();
            }

            VoidResult result = ValidatePatchWithRelatedEntities(patch);
            if (!result.Successful)
            {
                return result;
            }

            new Patch_SideEffect_UpdateDerivedOperators(patch, _repositories).Execute();

            return result;
        }

        /// <summary>
        /// Related operators will also be added to the operator's patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult SaveOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            if (op.Patch == null) throw new NullException(() => op.Patch);

            VoidResult result1 = AddToPatchRecursive(op, op.Patch);
            if (!result1.Successful)
            {
                return result1;
            }

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();
            new Operator_SideEffect_UpdateDerivedOperators(op, _repositories).Execute();

            // Validate the whole patch, because side-effect can affect the whole patch.
            VoidResult result2 = ValidatePatchWithRelatedEntities(op.Patch);

            return result2;
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        private VoidResult AddToPatchRecursive(Operator op, Patch patch)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_IsOfSamePatchOrPatchIsNull_Recursive(op, patch, _repositories.CurveRepository);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }

            AddToPatchRecursive_WithoutValidation(op, patch);

            return new VoidResult { Successful = true };
        }

        private void AddToPatchRecursive_WithoutValidation(Operator op, Patch patch)
        {
            op.LinkTo(patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    AddToPatchRecursive_WithoutValidation(inlet.InputOutlet.Operator, patch);
                }
            }
        }

        // Delete

        public VoidResult DeletePatchWithRelatedEntities(int patchID)
        {
            Patch patch = _repositories.PatchRepository.Get(patchID);
            return DeletePatchWithRelatedEntities(patch);
        }

        public VoidResult DeletePatchWithRelatedEntities(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IValidator validator = new PatchValidator_Delete(patch, _repositories.CurveRepository);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }

            patch.DeleteRelatedEntities(_repositories);
            patch.UnlinkRelatedEntities();
            _repositories.PatchRepository.Delete(patch);

            return new VoidResult { Successful = true };
        }

        /// <summary>
        /// Deletes the operator, its inlets and outlets
        /// and connections to its inlets and outlets.
        /// Also applies changes to underlying documents to dependent CustomOperators.
        /// Also cleans up obsolete inlets and outlets from custom operators.
        /// </summary>
        public void DeleteOperatorWithRelatedEntities(int id)
        {
            Operator op = _repositories.OperatorRepository.Get(id);
            DeleteOperatorWithRelatedEntities(op);
        }

        /// <summary>
        /// Deletes the operator, its inlets and outlets
        /// and connections to its inlets and outlets.
        /// Also applies changes to underlying documents to derived operators.
        /// Also cleans up obsolete inlets and outlets from operators.
        /// </summary>
        public void DeleteOperatorWithRelatedEntities(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            if (op.Patch == null) throw new NullException(() => op.Patch);

            // Get this before deleting and unlinking things.
            IList<Operator> connectedOperators = op.GetConnectedOperators();
            Patch patch = op.Patch;

            op.UnlinkRelatedEntities();
            op.DeleteRelatedEntities(_repositories);
            _repositories.OperatorRepository.Delete(op);

            new Patch_SideEffect_UpdateDerivedOperators(patch, _repositories).Execute();

            // Clean up obsolete inlets and outlets when the last connection to it is gone.
            foreach (Operator connectedOperator in connectedOperators)
            {
                new Operator_SideEffect_ApplyUnderlyingPatch(connectedOperator, _repositories).Execute();
            }
        }

        public void DeleteInlet(int id)
        {
            Inlet entity = _repositories.InletRepository.Get(id);
            DeleteInlet(entity);
        }

        public void DeleteInlet(Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.InletRepository.Delete(entity);

            // TODO:
            // In theory, if obsolete outlets were connected to this inlet we delete here,
            // we could clean up those obsolete outlets. Just like in DeleteOperator.
        }

        public void DeleteOutlet(int id)
        {
            Outlet entity = _repositories.OutletRepository.Get(id);
            DeleteOutlet(entity);
        }

        public void DeleteOutlet(Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.OutletRepository.Delete(entity);

            // TODO:
            // In theory if an outlet is connected to obsolete inlets,
            // it might be possible to clean up those obsolete inlets here. Just like in DeleteOperator.
        }

        // Validate (Private)

        private VoidResult ValidatePatchWithRelatedEntities(Patch patch)
        {
            IValidator validator = new PatchValidator_WithRelatedEntities(
                patch,
                _repositories.CurveRepository,
                _repositories.SampleRepository, new HashSet<object>());

            return validator.ToResult();
        }

        // Misc

        /// <summary> Validates for instance that no operator connections are lost. </summary>
        public VoidResult SetOperatorInletCount(Operator op, int inletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetInletCount(op, inletCount);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }

            IList<Inlet> sortedInlets = op.Inlets.Sort().ToArray();

            // Create additional inlets
            for (int i = sortedInlets.Count; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
                // This should be just enough to let Operator_SideEffect_ApplyUnderlyingPatch do the rest of the properties.
                inlet.IsRepeating = true;
            }

            // Delete excessive inlets
            for (int i = sortedInlets.Count - 1; i >= inletCount; i--)
            {
                Inlet inlet = sortedInlets[i];
                DeleteInlet(inlet);
            }

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

            return new VoidResult { Successful = true };
        }

        /// <summary> Validates for instance that no operator connections are lost. </summary>
        public VoidResult SetOperatorOutletCount(Operator op, int outletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetOutletCount(op, outletCount);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }

            IList<Outlet> sortedOutlets = op.Outlets.Sort().ToArray();

            // Create additional outlets
            for (int i = sortedOutlets.Count; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
                // This should be just enough to let Operator_SideEffect_ApplyUnderlyingPatch do the rest of the properties.
                outlet.IsRepeating = true;
            }

            // Delete excessive outlets
            for (int i = sortedOutlets.Count - 1; i >= outletCount; i--)
            {
                Outlet outlet = sortedOutlets[i];
                DeleteOutlet(outlet);
            }

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

            return new VoidResult { Successful = true };
        }

        /// <summary>
        /// Does work, that is shared for creating multiple calculators, only once.
        /// In particular in compiled mode, this means it compiles the calculation only once.
        /// Note that you are still going to have to call it once for each channel, unfortunately,
        /// due to the inherent behavior of sample mixing inside the PatchCalculators.
        /// </summary>
        public IList<IPatchCalculator> CreateCalculators(
            int calculatorCount,
            Outlet outlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache)
        {
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.Roslyn:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                {
                    IOperatorDto dto = new OperatorEntityToDtoVisitor(
                            calculatorCache,
                            _repositories.CurveRepository,
                            _repositories.SampleRepository,
                            _repositories.SpeakerSetupRepository).Execute(outlet);

                    dto = new OperatorDtoPreProcessingExecutor(samplingRate, channelCount).Execute(dto);

                    ActivationInfo activationInfo = new OperatorDtoCompiler().CompileToPatchCalculatorActivationInfo(dto, samplingRate, channelCount, channelIndex);

                    IList<IPatchCalculator> patchCalculators = CollectionHelper.Repeat(
                        calculatorCount,
                        () => (IPatchCalculator)Activator.CreateInstance(activationInfo.Type, activationInfo.Args)).ToArray();

                    return patchCalculators;
                }
                default:
                {
                    IList<IPatchCalculator> patchCalculators =
                        CollectionHelper.Repeat(calculatorCount, () => CreateCalculator(outlet, samplingRate, channelCount, channelIndex, calculatorCache))
                                        .ToArray();

                    return patchCalculators;
                }
            }
        }

        public IPatchCalculator CreateCalculator(
            Outlet outlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache)
        {
            IPatchCalculator patchCalculator;
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.CalculatorClasses:
                    patchCalculator = new SingleChannelPatchCalculator(
                        outlet,
                        samplingRate,
                        channelCount,
                        channelIndex,
                        calculatorCache,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _repositories.SpeakerSetupRepository);
                    break;

                case CalculationMethodEnum.Roslyn:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                    IOperatorDto dto = new OperatorEntityToDtoVisitor(
                        calculatorCache,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _repositories.SpeakerSetupRepository).Execute(outlet);

                    dto = new OperatorDtoPreProcessingExecutor(samplingRate, channelCount).Execute(dto);
                    patchCalculator = new OperatorDtoCompiler().CompileToPatchCalculator(dto, samplingRate, channelCount, channelIndex);

                    break;

                case CalculationMethodEnum.HardCoded:
                    return new HardCodedPatchCalculator(samplingRate, channelCount, channelIndex, null, null);

                case CalculationMethodEnum.ExampleGeneratedCode:
                    return new GeneratedPatchCalculator(samplingRate, channelCount, channelIndex, new Dictionary<string, ArrayDto>());

                default:
                    throw new ValueNotSupportedException(_calculationMethodEnum);
            }

            return patchCalculator;
        }

        public void DeleteOwnedNumberOperators(int id)
        {
            Operator op = _repositories.OperatorRepository.Get(id);
            DeleteOwnedNumberOperators(op);
        }

        /// <summary> If ownerOperator is the sole referrer to a number operator, the number operator will be deleted. </summary>
        public void DeleteOwnedNumberOperators(Operator ownerOperator)
        {
            if (ownerOperator == null) throw new NullException(() => ownerOperator);

            IEnumerable<Operator> inputNumberOperators =
                ownerOperator.Inlets.Select(x => x.InputOutlet?.Operator)
                                    .Where(x => x != null)
                                    .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Number);

            foreach (Operator numberOperator in inputNumberOperators.ToArray())
            {
                Outlet numberOutlet = numberOperator.Outlets.Single();
                bool isOwned = numberOutlet.ConnectedInlets.Count == 1;
                if (isOwned)
                {
                    DeleteOperatorWithRelatedEntities(numberOperator);
                }
            }
        }
    }
}
