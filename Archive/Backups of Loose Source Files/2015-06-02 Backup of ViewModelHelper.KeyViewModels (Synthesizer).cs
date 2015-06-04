using JJ.Presentation.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static SampleKeyViewModel CreateSampleKeyViewModel(
            int id,
            int documentID,
            int listIndex,
            ChildDocumentTypeEnum childDocumentTypeEnum,
            int? childDocumentListIndex)
        {
            return new SampleKeyViewModel
            {
                ID = id,
                DocumentID = documentID,
                ChildDocumentTypeEnum = childDocumentTypeEnum,
                ChildDocumentListIndex = childDocumentListIndex,
                ListIndex = listIndex
            };
        }
    }
}
