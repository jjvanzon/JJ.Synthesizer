//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Business;
//using JJ.Framework.Common;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//    internal abstract class Generic_SideEffect_GenerateNameBase<TEntity> : ISideEffect
//    {
//        private readonly Document _document;
//        private readonly TEntity _entity;

//        public Generic_SideEffect_GenerateNameBase(Document document, TEntity entity)
//        {
//            if (document == null) throw new NullException(() => document);
//            if (entity == null) throw new NullException(() => entity);

//            _document = document;
//            _entity = entity;
//        }

//        public void Execute()
//        {
//            HashSet<string> existingNamesLowerCase = _document.EnumerateSelfAndParentAndTheirChildren()
//                                                              .SelectMany(x => x.Curves)
//                                                              .Select(x => x.Name?.ToLower())
//                                                              .ToHashSet();
//            string suggestedName = BlaDoIt(existingNamesLowerCase);

//            _entity.Name = suggestedName;
//        }
//    }
//}
