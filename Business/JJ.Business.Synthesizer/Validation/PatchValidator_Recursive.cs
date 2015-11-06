using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Validation
{
    public class PatchValidator_Recursive : FluentValidator<Patch>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;
        private HashSet<object> _alreadyDone;

        public PatchValidator_Recursive(
            Patch obj, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository, 
            HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            ValidatePatchInletNamesAreUnique();
            ValidatePatchInletListIndexesAreUnique();
            ValidatePatchOutletNamesAreUnique();
            ValidatePatchOutletListIndexesAreUnique();

            foreach (Operator op in Object.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository, _documentRepository);
                
                Execute(new OperatorValidator_IsCircular(op, _documentRepository), messagePrefix);
                Execute(new OperatorValidator_Recursive(op, _curveRepository, _sampleRepository, _documentRepository, _alreadyDone), messagePrefix);
            }
        }

        private void ValidatePatchInletNamesAreUnique()
        {
            IList<string> names = Object.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                        .Where(x => !String.IsNullOrEmpty(x.Name))
                                        .Select(x => x.Name)
                                        .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchInlet, Messages.InletNamesAreNotUnique);
            }
        }

        private void ValidatePatchInletListIndexesAreUnique()
        {
            IList<int> listIndexes = Object.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                           .Select(x => new OperatorWrapper_PatchInlet(x))
                                           .Select(x => x.ListIndex)
                                           .ToArray();

            bool listIndexesAreUnique = listIndexes.Distinct().Count() == listIndexes.Count;
            if (!listIndexesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchInlet, Messages.InletListIndexesAreNotUnique);
            }
        }

        private void ValidatePatchOutletNamesAreUnique()
        {
            IList<string> names = Object.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                        .Where(x => !String.IsNullOrEmpty(x.Name))
                                        .Select(x => x.Name)
                                        .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletNamesAreNotUnique);
            }
        }

        private void ValidatePatchOutletListIndexesAreUnique()
        {
            IList<int> listIndexes = Object.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                           .Select(x => new OperatorWrapper_PatchOutlet(x))
                                           .Select(x => x.ListIndex)
                                           .ToArray();

            bool listIndexesAreUnique = listIndexes.Distinct().Count() == listIndexes.Count;
            if (!listIndexesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletListIndexesAreNotUnique);
            }
        }
    }
}
