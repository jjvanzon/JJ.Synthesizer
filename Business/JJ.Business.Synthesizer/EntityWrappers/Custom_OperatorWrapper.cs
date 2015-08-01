using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        /// <summary>
        /// Executes a loop, so prevent calling it multiple times.
        /// </summary>
        public IList<Outlet> Operands
        {
            get
            {
                IList<Outlet> operands = new Outlet[_operator.Inlets.Count];
                for (int i = 0; i < _operator.Inlets.Count; i++)
                {
                    operands[i] = _operator.Inlets[i].InputOutlet;
                }
                return operands;
            }
        }


        public int? DocumentID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
        }

        /// <summary> nullable </summary>
        public Document Document
        {
            get
            {
                int? documentID = DocumentID;
                if (!documentID.HasValue)
                {
                    return null;
                }

                return _documentRepository.TryGet(documentID.Value);
            }
            set
            {
                if (value == null)
                {
                    DocumentID = null;
                    return;
                }

                DocumentID = value.ID;
            }
        }
    }
}
