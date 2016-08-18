using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static IList<PatchGridViewModel> CreatePatchGridViewModelList(
            IList<Document> grouplessChildDocuments,
            IList<ChildDocumentGroupDto> childDocumentGroupDtos,
            int rootDocumentID)
        {
            if (grouplessChildDocuments == null) throw new NullException(() => grouplessChildDocuments);
            if (childDocumentGroupDtos == null) throw new NullException(() => childDocumentGroupDtos);

            var list = new List<PatchGridViewModel>();

            list.Add(grouplessChildDocuments.ToPatchGridViewModel(rootDocumentID, null));
            list.AddRange(childDocumentGroupDtos.Select(x => x.Documents.ToPatchGridViewModel(rootDocumentID, x.GroupName)));

            return list;
        }
    }
}
