using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Delete : VersatileValidator<Patch>
    {
        private readonly IPatchRepository _patchRepository;

        public PatchValidator_Delete([NotNull] Patch obj, [NotNull] IPatchRepository patchRepository) 
            : base(obj, postponeExecute: true)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            Patch patch = Obj;
            Document document = patch.Document;

            if (document == null)
            {
                return;
            }

            // TODO: Is this SelectMany even needed if you would do this in a PatchValidator?
            bool hasCustomOperators = document.Patches.SelectMany(x => x.EnumerateDependentCustomOperators(_patchRepository)).Any();
            if (hasCustomOperators)
            {
                ValidationMessages.Add(PropertyNames.Patch, MessageFormatter.CannotDeleteBecauseHasReferences());
            }

            // TODO: Be more specific about which custom operators still refer to this document,
            // but beware that just describing the custom operator is not enough,
            // because that is probably the same as th underlying document name anyway.

            //IList<Operator> customOperators = document.EnumerateDependentCustomOperators(_documentRepository).ToArray();
            //foreach (Operator customOperator in customOperators)
            //{
            //    string customOperatordescriptor = ValidationHelper.GetMessagePrefix_ForCustomOperator(customOperator, _documentRepository);

            //    string message = MessageFormatter.CustomOperatorIsDependentOnDocument(customOperatordescriptor, document.Name);
            //    ValidationMessages.Add(PropertyNames.Document, message);
            //}
        }

        //public string GetCustomOperatorDescriptor(Operator entity)
        //{
        //    if (entity == null) throw new NullException(() => entity);

        //    if (!String.IsNullOrEmpty(entity.Name))
        //    {
        //        return entity.Name;
        //    }

        //    var wrapper = new Custom_OperatorWrapper(entity, _documentRepository);
        //    Document underlyingEntity = wrapper.UnderlyingPatch;
        //    string underlyingEntityName = null;
        //    if (underlyingEntity != null)
        //    {
        //        if (!String.IsNullOrEmpty(underlyingEntity.Name))
        //        {
        //            return underlyingEntity.Name;
        //        }
        //    }

        //    string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(entity);
        //    return operatorTypeDisplayName;
        //}
    }
}
