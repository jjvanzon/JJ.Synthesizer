﻿using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Document_SideEffect_ApplyExternalUnderlyingPatches : ISideEffect
    {
        private readonly Document _document;
        private readonly RepositoryWrapper _repositories;

        public Document_SideEffect_ApplyExternalUnderlyingPatches(
            [NotNull] Document document,
            [NotNull] RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _document = document ?? throw new NullException(() => document);
        }

        public void Execute()
        {
            IEnumerable<Operator> operators = _document.Patches
                                                       .SelectMany(x => x.Operators)
                                                       .Where(
                                                           x => x.UnderlyingPatch != null &&
                                                                x.UnderlyingPatch.Document.ID != _document.ID);
            foreach (Operator op in operators)
            {
                new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();
            }
        }
    }
}