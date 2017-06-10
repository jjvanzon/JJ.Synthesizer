using System;
using System.Collections.Generic;
using System.Linq;
using GeneratedCSharp;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Validation.Patches;
using JJ.Framework.Collections;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;
using JJ.Business.Synthesizer.Roslyn;
using JJ.Business.Synthesizer.Roslyn.Calculation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Configuration;

namespace JJ.Business.Synthesizer
{
    /// <summary>
    /// Manages a Patch and its Operators.
    /// You can supply a patch, Create a new one using the CreatePatch method
    /// or omit the Patch to only call methods that do not require it.
    /// </summary>
    public partial class PatchManager
    {
        private static readonly double _secondsBetweenApplyFilterVariables = CustomConfigurationManager.GetSection<ConfigurationSection>().SecondsBetweenApplyFilterVariables;
        private static readonly CalculationMethodEnum _calculationMethodEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().CalculationMethod;

        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        /// <summary> nullable </summary>
        public Patch Patch { get; set; }

        public int? PatchID
        {
            get => Patch?.ID;
            set
            {
                if (!value.HasValue)
                {
                    Patch = null;
                }
                else
                {
                    Patch = _repositories.PatchRepository.Get(value.Value);
                }
            }
        }

        // Constructors

        public PatchManager(Patch patch, RepositoryWrapper repositories)
            : this(repositories)
        {
            Patch = patch ?? throw new NullException(() => patch);
        }

        public PatchManager([NotNull] RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
        }

        // Create

        /// <summary> Use the Patch property after calling this method. </summary>
        /// <param name="document">Nullable. Used e.g. to generate a unique name for a Patch.</param>
        public void CreatePatch(Document document = null)
        {
            Patch = new Patch { ID = _repositories.IDRepository.GetID() };
            _repositories.PatchRepository.Insert(Patch);

            Patch.LinkTo(document);

            new Patch_SideEffect_GenerateName(Patch).Execute();
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

        public VoidResultDto SavePatch()
        {
            AssertPatchNotNull();

            // TODO: At one time, it said Patch.Operators collection was changed. That is what the ToArray is for. 
            // I still do not know why the collection was changed, so that must be investigated.
            // (It was when SaveOperator was called instead of ExecuteSideEffects.)

            foreach (Operator op in Patch.Operators.ToArray())
            {
                new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();
            }

            VoidResultDto result = ValidatePatchWithRelatedEntities();
            if (!result.Successful)
            {
                return result;
            }

            new Patch_SideEffect_UpdateDependentCustomOperators(Patch, _repositories).Execute();

            return result;
        }

        /// <summary>
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResultDto SaveOperator(Operator op)
        {
            AssertPatchNotNull();

            if (op == null) throw new NullException(() => op);

            VoidResultDto result1 = AddToPatchRecursive(op);
            if (!result1.Successful)
            {
                return result1;
            }

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();
            new Operator_SideEffect_UpdateDependentCustomOperators(op, _repositories).Execute();

            // Validate the whole patch, because side-effect can affect the whole patch.
            // But also there are unique validations over e.g. ListIndexes of multiple PatchInlet Operators.
            VoidResultDto result2 = ValidatePatchWithRelatedEntities();

            return result2;
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        private VoidResultDto AddToPatchRecursive(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, Patch, _repositories.SampleRepository, _repositories.CurveRepository);
            if (!validator.IsValid)
            {
                return validator.ToCanonical();
            }

            AddToPatchRecursive_WithoutValidation(op);

            return new VoidResultDto { Successful = true };
        }

        private void AddToPatchRecursive_WithoutValidation(Operator op)
        {
            op.LinkTo(Patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    AddToPatchRecursive_WithoutValidation(inlet.InputOutlet.Operator);
                }
            }
        }

        // Delete

        public VoidResult DeletePatchWithRelatedEntities()
        {
            AssertPatchNotNull();

            IValidator validator = new PatchValidator_Delete(Patch);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }

            Patch.DeleteRelatedEntities(_repositories.OperatorRepository, _repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
            Patch.UnlinkRelatedEntities();
            _repositories.PatchRepository.Delete(Patch);

            return new VoidResult
            {
                Successful = true
            };
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
        /// Also applies changes to underlying documents to dependent CustomOperators.
        /// Also cleans up obsolete inlets and outlets from custom operators.
        /// </summary>
        public void DeleteOperatorWithRelatedEntities(Operator op)
        {
            AssertPatchNotNull();

            if (op == null) throw new NullException(() => op);
            if (op.Patch != Patch) throw new NotEqualException(() => op.Patch, () => Patch);

            // Get this list before deleting and unlinking things.
            IList<Operator> connectedCustomOperators =
                op.GetConnectedOperators()
                  .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
                  .ToArray();

            op.UnlinkRelatedEntities();
            op.DeleteRelatedEntities(_repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
            _repositories.OperatorRepository.Delete(op);

            new Patch_SideEffect_UpdateDependentCustomOperators(Patch, _repositories).Execute();

            // Clean up obsolete inlets and outlets.
            // (Inlets and outlets that do not exist anymore in a CustomOperator's UnderlyingPatch
            //  are kept alive by the system until it has no connections anymore, so that a user's does not lose data.)

            foreach (Operator connectedCustomOperator in connectedCustomOperators)
            {
                new Operator_SideEffect_ApplyUnderlyingPatch(connectedCustomOperator, _repositories).Execute();
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

        private VoidResultDto ValidatePatchWithRelatedEntities()
        {
            IValidator validator = new PatchValidator_WithRelatedEntities(
                Patch,
                _repositories.CurveRepository,
                _repositories.SampleRepository, new HashSet<object>());

            VoidResultDto result = validator.ToCanonical();

            return result;
        }

        private VoidResultDto ValidateOperatorNonRecursive(Operator op)
        {
            IValidator validator = new Versatile_OperatorValidator(op);

            return new VoidResultDto
            {
                Messages = validator.ValidationMessages.ToCanonical(),
                Successful = validator.IsValid
            };
        }

        // Grouping

        public IList<DocumentReferencePatchGroupDto> GetDocumentReferencePatchGroupDtos_IncludingGrouplessIfAny([NotNull] IList<DocumentReference> documentReferences, bool mustIncludeHidden)
        {
            if (documentReferences == null) throw new NullException(() => documentReferences);

            var dtos = new List<DocumentReferencePatchGroupDto>();

            IEnumerable<DocumentReference> lowerDocumentReferences = documentReferences.Where(x => x.LowerDocument != null);
            foreach (DocumentReference lowerDocumentReference in lowerDocumentReferences)
            {
                var dto = new DocumentReferencePatchGroupDto
                {
                    LowerDocumentReference = lowerDocumentReference,
                    Groups = new List<PatchGroupDto>()
                };
                
                IList<PatchGroupDto> patchGroupDtos = GetPatchGroupDtos_IncludingGrouplessIfAny(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden);
                dto.Groups.AddRange(patchGroupDtos);

                dtos.Add(dto);
            }

            return dtos;
        }

        public IList<PatchGroupDto> GetPatchGroupDtos_IncludingGrouplessIfAny(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            var dtos = new List<PatchGroupDto>();

            IList<Patch> grouplessPatches = GetGrouplessPatches(patchesInDocument, mustIncludeHidden);
            dtos.Add(new PatchGroupDto { Patches = grouplessPatches });

            dtos.AddRange(GetPatchGroupDtos_ExcludingGroupless(patchesInDocument, mustIncludeHidden));

            return dtos;
        }

        public IList<PatchGroupDto> GetPatchGroupDtos_ExcludingGroupless(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            // TODO: Low Priority:
            // This reuses the logic in the other methods, so there can be no inconsistencies,
            // but it would be faster to put all the code here.
            // Perhaps you can group in one method and delegate the rest of the methods to
            // the grouping method.

            var dtos = new List<PatchGroupDto>();

            IList<string> groupNames = GetPatchGroupNames(patchesInDocument, mustIncludeHidden);

            foreach (string groupName in groupNames)
            {
                string canonicalGroupName = NameHelper.ToCanonical(groupName);

                IList<Patch> patchesInGroup = GetPatchesInGroup(patchesInDocument, groupName, mustIncludeHidden);

                dtos.Add(
                    new PatchGroupDto
                    {
                        FriendlyGroupName = groupName,
                        CanonicalGroupName = canonicalGroupName,
                        Patches = patchesInGroup
                    });
            }

            return dtos;
        }

        public IList<string> GetPatchGroupNames(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            IList<string> groupNames = patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
                                                        .Where(x => NameHelper.IsFilledIn(x.GroupName))
                                                        .Distinct(x => NameHelper.ToCanonical(x.GroupName))
                                                        .Select(x => x.GroupName)
                                                        .ToList();
            return groupNames;
        }

        public IList<Patch> GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(IList<Patch> patchesInDocument, string groupName, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            if (string.IsNullOrWhiteSpace(groupName))
            {
                return GetGrouplessPatches(patchesInDocument, mustIncludeHidden);
            }
            else
            {
                return GetPatchesInGroup(patchesInDocument, groupName, mustIncludeHidden);
            }
        }

        public IList<Patch> GetGrouplessPatches(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            IList<Patch> list = patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
                                                 .Where(x => string.IsNullOrWhiteSpace(x.GroupName))
                                                 .ToArray();

            return list;
        }

        public IList<Patch> GetPatchesInGroup(IList<Patch> patchesInDocument, string groupName, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);
            if (string.IsNullOrWhiteSpace(groupName)) throw new NullOrWhiteSpaceException(() => groupName);

            string canonicalGroupName = NameHelper.ToCanonical(groupName);

            IList<Patch> patchesInGroup =
                patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
                                 .Where(x => NameHelper.IsFilledIn(x.GroupName))
                                 .Where(x => string.Equals(NameHelper.ToCanonical(x.GroupName), canonicalGroupName))
                                 .ToArray();

            return patchesInGroup;
        }

        // Misc

        /// <summary> Validates for instance that no operator connections are lost. </summary>
        public VoidResultDto SetOperatorInletCount(Operator op, int inletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetInletCount(op, inletCount);
            if (!validator.IsValid)
            {
                return validator.ToCanonical();
            }

            IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();

            // Create additional inlets
            for (int i = sortedInlets.Count; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
                inlet.ListIndex = i;
            }

            // Delete excessive inlets
            for (int i = sortedInlets.Count - 1; i >= inletCount; i--)
            {
                Inlet inlet = sortedInlets[i];
                DeleteInlet(inlet);
            }

            return new VoidResultDto
            {
                Successful = true
            };
        }

        public VoidResultDto SetOperatorOutletCount(Operator op, int outletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetOutletCount(op, outletCount);
            if (!validator.IsValid)
            {
                return validator.ToCanonical();
            }

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();

            // Create additional outlets
            for (int i = sortedOutlets.Count; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
                outlet.ListIndex = i;
            }

            // Delete excessive outlets
            for (int i = sortedOutlets.Count - 1; i >= outletCount; i--)
            {
                Outlet outlet = sortedOutlets[i];
                DeleteOutlet(outlet);
            }

            return new VoidResultDto
            {
                Successful = true
            };
        }

        /// <summary>
        /// Does work, shared for creating multiple calculators only once.
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
            CalculatorCache calculatorCache,
            bool mustSubstituteSineForUnfilledInSignalPatchInlets = true)
        {
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.Roslyn:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                {
                    var entityToDtoVisitor = new OperatorEntityToDtoVisitor(
                        calculatorCache,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _repositories.SpeakerSetupRepository);
                    IOperatorDto dto = entityToDtoVisitor.Execute(outlet);

                    var preProcessingVisitor = new OperatorDtoPreProcessingExecutor(samplingRate, channelCount);
                    dto = preProcessingVisitor.Execute(dto);

                    var compiler = new OperatorDtoCompiler();
                    ActivationInfo activationInfo = compiler.CompileToPatchCalculatorActivationInfo(dto, samplingRate, channelCount, channelIndex);

                    IList<IPatchCalculator> patchCalculators = CollectionHelper.Repeat(
                        calculatorCount,
                        () => (IPatchCalculator)Activator.CreateInstance(activationInfo.Type, activationInfo.Args)).ToArray();

                    return patchCalculators;
                }
                default:
                {
                    IList<IPatchCalculator> patchCalculators =
                        CollectionHelper.Repeat(
                                            calculatorCount,
                                            () =>
                                                CreateCalculator(
                                                    outlet,
                                                    samplingRate,
                                                    channelCount,
                                                    channelIndex,
                                                    calculatorCache,
                                                    mustSubstituteSineForUnfilledInSignalPatchInlets))
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
            CalculatorCache calculatorCache,
            bool mustSubstituteSineForUnfilledInSignalPatchInlets = true)
        {
            if (mustSubstituteSineForUnfilledInSignalPatchInlets)
            {
                SubstituteSineForUnfilledInSoundPatchInlets();
            }

            IPatchCalculator patchCalculator;
            switch (_calculationMethodEnum)
            {
                case CalculationMethodEnum.EntityThruDtoToCalculator:
                case CalculationMethodEnum.EntityToCalculatorDirectly:
                    patchCalculator = new SingleChannelPatchCalculator(
                        outlet,
                        samplingRate,
                        channelCount,
                        channelIndex,
                        calculatorCache,
                        _secondsBetweenApplyFilterVariables,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _repositories.SpeakerSetupRepository);
                    break;

                case CalculationMethodEnum.Roslyn:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithNormalAndOutParameters:
                case CalculationMethodEnum.Roslyn_WithUninlining_WithRefParameters:
                    var entityToDtoVisitor = new OperatorEntityToDtoVisitor(
                        calculatorCache,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _repositories.SpeakerSetupRepository);
                    IOperatorDto dto = entityToDtoVisitor.Execute(outlet);

                    var preProcessingVisitor = new OperatorDtoPreProcessingExecutor(samplingRate, channelCount);
                    dto = preProcessingVisitor.Execute(dto);

                    var compiler = new OperatorDtoCompiler();
                    patchCalculator = compiler.CompileToPatchCalculator(dto, samplingRate, channelCount, channelIndex);

                    break;

                case CalculationMethodEnum.HardCoded:
                    return new HardCodedPatchCalculator(samplingRate, channelCount, channelIndex, null, null);

                case CalculationMethodEnum.ExampleGeneratedCode:
                    return new GeneratedPatchCalculator(samplingRate, channelCount, channelIndex, new Dictionary<string, double[]>(), new Dictionary<string, double>());

                default:
                    throw new ValueNotSupportedException(_calculationMethodEnum);
            }

            return patchCalculator;
        }

        public void CreateNumbersForEmptyInletsWithDefaultValues(
            Operator op,
            float estimatedOperatorWidth,
            float operatorHeight,
            EntityPositionManager entityPositionManager)
        {
            if (op == null) throw new NullException(() => op);
            if (op.Patch != Patch) throw new NotEqualException(() => op.Patch, () => Patch);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(op.ID);

            int inletCount = op.Inlets.Count;
            float spacingX = operatorHeight / 2f;
            float spacingY = operatorHeight;
            float fullWidth = estimatedOperatorWidth * inletCount + spacingX * (inletCount - 1);
            float left = entityPosition.X - fullWidth / 2f;
            float x = left + estimatedOperatorWidth / 2f; // Coordinates are the centers.
            float y = entityPosition.Y - operatorHeight - spacingY;
            float stepX = estimatedOperatorWidth + spacingX;

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet == null)
                {
                    if (inlet.DefaultValue.HasValue)
                    {
                        var number = Number(inlet.DefaultValue.Value);

                        inlet.LinkTo(number.NumberOutlet);

                        EntityPosition numberEntityPosition = entityPositionManager.GetOrCreateOperatorPosition(number.WrappedOperator.ID);
                        numberEntityPosition.X = x;
                        numberEntityPosition.Y = y;
                    }
                }

                x += stepX;
            }
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

            foreach (Operator numberOperator in inputNumberOperators)
            {
                Outlet numberOutlet = numberOperator.Outlets.Single();
                bool isOwned = numberOutlet.ConnectedInlets.Count == 1;
                if (isOwned)
                {
                    DeleteOperatorWithRelatedEntities(numberOperator);
                }
            }
        }

        private void SubstituteSineForUnfilledInSoundPatchInlets()
        {
            AssertPatchNotNull();

            IList<PatchInlet_OperatorWrapper> patchInletWrappers = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                                        .Where(x => x.Inlet.GetDimensionEnum() == DimensionEnum.Sound &&
                                                                                    !x.Inlet.DefaultValue.HasValue &&
                                                                                    x.Input == null)
                                                                        .ToArray();
            // ReSharper disable once InvertIf
            if (patchInletWrappers.Count != 0)
            {
                Outlet sineOutlet = Sine(Number(440));

                foreach (PatchInlet_OperatorWrapper patchInletWrapper in patchInletWrappers)
                {
                    patchInletWrapper.Input = sineOutlet;
                }
            }
        }

        private void AssertPatchNotNull()
        {
            if (Patch == null) throw new NullException(() => Patch);
        }
    }
}
