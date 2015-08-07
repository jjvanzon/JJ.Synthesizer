using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_IsCircular : FluentValidator<Operator>
    {
        private IDocumentRepository _documentRepository;

        public OperatorValidator_IsCircular(Operator op, IDocumentRepository documentRepository)
            : base(op, postponeExecute: true)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            if (op.IsCircular())
            {
                ValidationMessages.Add(() => op, MessageFormatter.OperatorIsCircularWithName(op.Name));
            }

            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                if (op.IsCircularCustomOperatorDocumentReference(_documentRepository))
                {
                    ValidationMessages.Add(() => op, MessageFormatter.CustomOperatorDocumentReferenceIsCircular_WithName(op.Name));
                }
            }
        }
    }
}
