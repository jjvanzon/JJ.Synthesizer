using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Custom_OperatorWrapper : OperatorWrapperBase
    {
        private IDocumentRepository _documentRepository;

        public Custom_OperatorWrapper(Operator op, IDocumentRepository documentRepository)
            : base(op)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            Operands = new Custom_OperatorWrapper_Operands(op);
            Inlets = new Custom_OperatorWrapper_Inlets(op);
            Outlets = new Custom_OperatorWrapper_Outlets(op);
        }

        public Custom_OperatorWrapper_Operands Operands { get; private set; }

        public Custom_OperatorWrapper_Inlets Inlets { get; private set; }

        public Custom_OperatorWrapper_Outlets Outlets { get; private set; }

        public int? UnderlyingDocumentID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
        }

        /// <summary> nullable </summary>
        public Document UnderlyingDocument
        {
            get
            {
                int? underlyingDocumentID = UnderlyingDocumentID;
                if (!underlyingDocumentID.HasValue)
                {
                    return null;
                }

                return _documentRepository.TryGet(underlyingDocumentID.Value);
            }
            set
            {
                if (value == null)
                {
                    UnderlyingDocumentID = null;
                    return;
                }

                UnderlyingDocumentID = value.ID;
            }
        }

        //// TODO: These operations must enfore rules and should be integrated in the members above.

        //private void SetUnderlyingDocument(Operator op, Document document)
        //{
        //    if (op == null) throw new NullException(() => op);
        //    if (document == null) throw new NullException(() => document);
        //    if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.CustomOperator);

        //    // What can go wrong? Everything.
        //    throw new NotImplementedException();
        //}

        //private void SetName(Document document, string name)
        //{
        //    if (document == null) throw new NullException(() => document);

        //    //if (document.Name 
        //}

    }
}
