using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ListIndexHelper
    {
        public static void RenumberListIndexes(IList<IDNameAndListIndexViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<AudioFileOutputListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentPropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Document.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentViewModel> list, int startIndex = 0 )
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Document.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<AudioFileOutputPropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].AudioFileOutput.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<CurveDetailsViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Curve.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<PatchDetailsViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Patch.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<SamplePropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Sample.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<SampleListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].ListIndex = i;
            }
        }
    }
}
