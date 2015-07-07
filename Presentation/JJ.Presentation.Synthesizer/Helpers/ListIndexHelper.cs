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
        public static void RenumberListIndexes(IList<CurveListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<PatchListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.PatchListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<AudioFileOutputListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentPropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentViewModel> list, int startIndex = 0 )
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<AudioFileOutputPropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Entity.Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<CurveDetailsViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Curve.Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<PatchDetailsViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Entity.Keys.PatchListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<SamplePropertiesViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Entity.Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<SampleListItemViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberListIndexes(IList<ChildDocumentTreeNodeViewModel> list, int startIndex = 0)
        {
            if (list == null) throw new NullException(() => list);
            if (startIndex < 0) throw new LessThanException(() => startIndex, 0);

            for (int i = startIndex; i < list.Count; i++)
            {
                list[i].Keys.ListIndex = i;
            }
        }

        public static void RenumberNodeIndexes(DocumentTreeViewModel tree)
        {
            if (tree == null) throw new NullException(() => tree);

            int i = 0;

            foreach (ChildDocumentTreeNodeViewModel childDocumentTreeNodeViewModel in tree.Instruments)
            {
                childDocumentTreeNodeViewModel.Keys.NodeIndex = i++;
            }

            foreach (ChildDocumentTreeNodeViewModel childDocumentTreeNodeViewModel in tree.Effects)
            {
                childDocumentTreeNodeViewModel.Keys.NodeIndex = i++;
            }
        }
    }
}
