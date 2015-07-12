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
        // TODO: It seems this method should be called, but it no longer is. If so, call this method in the appropriate place again.
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
