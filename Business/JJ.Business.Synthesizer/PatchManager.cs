using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Business;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.Operators;

namespace JJ.Business.Synthesizer
{
    /// <summary>
    /// Manages a Patch and its Operators.
    /// You can supply a patch, Create a new one using the Create method
    /// or omit the Patch to only call methods that do not require it.
    /// </summary>
    public partial class PatchManager
    {
        private PatchRepositories _repositories;

        /// <summary> nullable </summary>
        public Patch Patch { get; set; }

        // Constructors

        public PatchManager(Patch patch, PatchRepositories repositories)
            : this(repositories)
        {
            if (patch == null) throw new NullException(() => patch);

            Patch = patch;
        }

        public PatchManager(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);
            _repositories = repositories;
        }

        // Create

        /// <summary> Use the Patch property after calling this method. </summary>
        /// <param name="document">Nullable. Used e.g. to generate a unique name for a Patch.</param>
        /// <param name="mustGenerateName">Only possible if you also pass a document.</param>
        public void CreatePatch(Document document = null, bool mustGenerateName = false)
        {
            Patch = new Patch();
            Patch.ID = _repositories.IDRepository.GetID();
            _repositories.PatchRepository.Insert(Patch);

            Patch.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(Patch);
                sideEffect.Execute();
            }
        }

        public Inlet CreateInlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var inlet = new Inlet();
            inlet.ID = _repositories.IDRepository.GetID();
            _repositories.InletRepository.Insert(inlet);

            inlet.LinkTo(op);

            return inlet;
        }

        public Outlet CreateOutlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            _repositories.OutletRepository.Insert(outlet);

            outlet.LinkTo(op);

            return outlet;
        }

        // Save

        public VoidResult SavePatch()
        {
            AssertPatch();

            // TODO: At one time, it said Patch.Operators collection was changed. That is what the ToArray is for. 
            // I still do not know why the collection was changed, so that must be investigated. (It was when SaveOperator was called instead of EecuteSideEffects)
            foreach (Operator op in Patch.Operators.ToArray())
            {
                ExecuteSideEffects(op);
            }

            VoidResult result = ValidatePatchRecursive();
            if (!result.Successful)
            {
                return result;
            }

            ISideEffect sideEffect = new Patch_SideEffect_UpdateDependentCustomOperators(Patch, _repositories);
            sideEffect.Execute();

            return result;
        }

        /// <summary>
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult SaveOperator(Operator op)
        {
            AssertPatch();

            if (op == null) throw new NullException(() => op);

            VoidResult result = AddToPatchRecursive(op);
            if (!result.Successful)
            {
                return result;
            }

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                    return SaveOperator_PatchInlet(op);

                case OperatorTypeEnum.PatchOutlet:
                    return SaveOperator_PatchOutlet(op);

                case OperatorTypeEnum.CustomOperator:
                    return SaveOperator_Custom(op);

                default:
                    return SaveOperator_Other(op);
            }
        }

        private VoidResult SaveOperator_PatchInlet(Operator op)
        {
            ExecuteSideEffects(op);

            // Side-effect can affect whole patch,
            // but also there are unique validations over e.g. ListIndexes of multiple PatchInlet Operators.
            // That is why the whole patch is validated.

            Document rootDocument = op.Patch.Document.GetRootDocument();
            VoidResult result = ValidatePatchRecursive();

            return result;
        }

        private VoidResult SaveOperator_PatchOutlet(Operator op)
        {
            ExecuteSideEffects(op);

            // Side-effect can affect whole patch,
            // but also there are unique validations over e.g. ListIndexes of multiple PatchInlet Operators.
            // That is why the whole patch is validated.

            Document rootDocument = op.Patch.Document.GetRootDocument();
            VoidResult result = ValidatePatchRecursive();

            return result;
        }

        private VoidResult SaveOperator_Custom(Operator op)
        {
            ExecuteSideEffects(op);

            Document rootDocument = op.Patch.Document.GetRootDocument();
            VoidResult result = ValidateOperatorNonRecursive(op);

            return result;
        }

        private VoidResult SaveOperator_Other(Operator op)
        {
            ExecuteSideEffects(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            return result;
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        private VoidResult AddToPatchRecursive(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, Patch);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            AddToPatchRecursive_WithoutValidation(op);

            return new VoidResult { Successful = true };
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

        public VoidResult DeleteWithRelatedEntities()
        {
            AssertPatch();

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
        public void DeleteOperator(int id)
        {
            Operator op = _repositories.OperatorRepository.Get(id);
            DeleteOperator(op);
        }

        /// <summary>
        /// Deletes the operator, its inlets and outlets
        /// and connections to its inlets and outlets.
        /// Also applies changes to underlying documents to dependent CustomOperators.
        /// Also cleans up obsolete inlets and outlets from custom operators.
        /// </summary>
        public void DeleteOperator(Operator op)
        {
            AssertPatch();

            if (op == null) throw new NullException(() => op);
            if (op.Patch != Patch) throw new NotEqualException(() => op.Patch, Patch);

            IList<Operator> connectedCustomOperators =
                Enumerable.Union(
                    op.Inlets.Where(x => x.InputOutlet != null).Select(x => x.InputOutlet.Operator),
                    op.Outlets.SelectMany(x => x.ConnectedInlets).Select(x => x.Operator))
                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
                .ToArray();

            op.UnlinkRelatedEntities();
            op.DeleteRelatedEntities(_repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
            _repositories.OperatorRepository.Delete(op);

            if (Patch.Document != null)
            {
                ISideEffect sideEffect = new Patch_SideEffect_UpdateDependentCustomOperators(Patch, _repositories);
                sideEffect.Execute();

                // Clean up obsolete inlets and outlets.
                // (Inlets and outlets that do not exist anymore in a CustomOperator's UnderlyingPatch
                //  are kept alive by the system until it has no connections anymore, so that a user's does not lose data.)
                foreach (Operator connectedCustomOperator in connectedCustomOperators)
                {
                    ISideEffect sideEffect2 = new Operator_SideEffect_ApplyUnderlyingPatch(connectedCustomOperator, _repositories);
                    sideEffect2.Execute();
                }
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
        }

        // Validate (Private)

        private VoidResult ValidatePatchRecursive()
        {
            // TODO: 'Recursive' suggests that you would also validate the UnderlyingPatches of CustomOperators.
            // Make this distinctly clear. Perhaps use the term 'WithRelatedEntities' if that is clearer.        
            var validators = new List<IValidator>
            {
                new PatchValidator_UniqueName(Patch),
                new PatchValidator_Recursive(
                    Patch,
                    _repositories.CurveRepository,
                    _repositories.SampleRepository,
                    _repositories.PatchRepository, new HashSet<object>())
            };

            if (Patch.Document != null)
            {
                validators.Add(new PatchValidator_InDocument(Patch));
            }

            var result = new VoidResult
            {
                Successful = validators.All(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        private VoidResult ValidateOperatorNonRecursive(Operator op)
        {
            IValidator validator = new Versatile_OperatorValidator(op, _repositories.PatchRepository);

            return new VoidResult
            {
                Messages = validator.ValidationMessages.ToCanonical(),
                Successful = validator.IsValid
            };
        }

        // ExecuteSideEffects (Private)

        private void ExecuteSideEffects(Operator op)
        {
            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                    ExecuteSideEffects_ForPatchInlet(op);
                    break;

                case OperatorTypeEnum.PatchOutlet:
                    ExecuteSideEffects_ForPatchOutlet(op);
                    break;

                case OperatorTypeEnum.CustomOperator:
                    ExecuteSideEffects_ForCustomOperator(op);
                    break;
            }
        }

        private void ExecuteSideEffects_ForPatchInlet(Operator op)
        {
            ISideEffect sideEffect = new Patch_SideEffect_UpdateDependentCustomOperators(op.Patch, _repositories);
            sideEffect.Execute();
        }

        private void ExecuteSideEffects_ForPatchOutlet(Operator op)
        {
            ISideEffect sideEffect = new Patch_SideEffect_UpdateDependentCustomOperators(op.Patch, _repositories);
            sideEffect.Execute();
        }

        private void ExecuteSideEffects_ForCustomOperator(Operator op)
        {
            ISideEffect sideEffect = new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories);
            sideEffect.Execute();
        }

        // Misc

        /// <summary> Validates for instance that no operator connections are lost. </summary>
        public VoidResult SetOperatorInletCount(Operator op, int inletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetInletCount(op, inletCount);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Messages = validator.ValidationMessages.ToCanonical(),
                    Successful = false
                };
            }

            IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();

            // Create additional inlets
            for (int i = sortedInlets.Count; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
            }

            // Delete excessive inlets
            for (int i = sortedInlets.Count - 1; i >= inletCount; i--)
            {
                Inlet inlet = sortedInlets[i];
                DeleteInlet(inlet);
            }

            return new VoidResult
            {
                Successful = true
            };
        }

        public VoidResult SetOperatorOutletCount(Operator op, int outletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetOutletCount(op, outletCount);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Messages = validator.ValidationMessages.ToCanonical(),
                    Successful = false
                };
            }

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();

            // Create additional outlets
            for (int i = sortedOutlets.Count; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
            }

            // Delete excessive outlets
            for (int i = sortedOutlets.Count - 1; i >= outletCount; i--)
            {
                Outlet outlet = sortedOutlets[i];
                DeleteOutlet(outlet);
            }

            return new VoidResult
            {
                Successful = true
            };
        }

        public IPatchCalculator CreateCalculator(
            Outlet outlet,
            CalculatorCache calculatorCache,
            bool mustSubstituteSineForUnfilledInSignalPatchInlets = true)
        {
            if (mustSubstituteSineForUnfilledInSignalPatchInlets)
            {
                SubstituteSineForUnfilledInSignalPatchInlets();
            }

            IPatchCalculator calculator = new OptimizedPatchCalculator(
                outlet,
                calculatorCache,
                _repositories.CurveRepository,
                _repositories.SampleRepository,
                _repositories.PatchRepository,
                _repositories.SpeakerSetupRepository);

            return calculator;
        }

        private void SubstituteSineForUnfilledInSignalPatchInlets()
        {
            AssertPatch();

            IList<PatchInlet_OperatorWrapper> patchInletWrappers = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                                        .Where(x => x.Inlet.GetDimensionEnum() == DimensionEnum.Signal &&
                                                                                    x.Inlet.InputOutlet == null &&
                                                                                    !x.Inlet.DefaultValue.HasValue)
                                                                        .ToArray();
            Outlet sineOutlet = Sine(Number(440));

            foreach (PatchInlet_OperatorWrapper patchInletWrapper in patchInletWrappers)
            {
                patchInletWrapper.Input = sineOutlet;
            }
        }

        private void AssertPatch()
        {
            if (Patch == null) throw new NullException(() => Patch);
        }
    }
}
