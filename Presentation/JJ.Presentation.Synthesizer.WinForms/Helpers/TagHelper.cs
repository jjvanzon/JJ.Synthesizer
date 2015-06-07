using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class TagHelper
    {
        // The the shorter the prefixes, the faster it will be.
        private const string CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX = "cd.nx ";
        private const string CHILD_DOCUMENT_TAG_PREFIX = "cd ";

        public static object GetChildDocumentNodeIndexTag(int childDocumentNodeIndex)
        {
            return String.Format("{0}{1}", CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX, childDocumentNodeIndex);
        }

        public static int? TryGetChildDocumentNodeIndex(object tag)
        {
            string tagString = Convert.ToString(tag);

            if (String.IsNullOrEmpty(tagString))
            {
                return null;
            }

            bool isChildDocumentNodeIndexTag = tagString.StartsWith(CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX);
            if (!isChildDocumentNodeIndexTag)
            {
                return null;
            }

            string nodeIndexString = tagString.CutLeft(CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX.Length);

            int nodeIndex;
            if (!Int32.TryParse(nodeIndexString, out nodeIndex))
            {
                throw new NotImplementedException();
                // TODO: Use proper exception message.
                throw new Exception("");
            }

            return nodeIndex;
        }

        public static object GetChildDocumentTag(ChildDocumentTypeEnum childDocumentTypeEnum, int childDocumentListIndex)
        {
            return String.Format("{0}{1}[{2}]", CHILD_DOCUMENT_TAG_PREFIX, childDocumentTypeEnum, childDocumentListIndex);
        }

        public static void TryGetChildDocumentKey(object tag, out ChildDocumentTypeEnum? childDocumentTypeEnum, out int? childDocumentListIndex)
        {
            childDocumentTypeEnum = null;
            childDocumentListIndex = null;

            string tagString = Convert.ToString(tag);

            if (String.IsNullOrEmpty(tagString))
            {
                return;
            }

            bool isChildDocumentTag = tagString.StartsWith(CHILD_DOCUMENT_TAG_PREFIX);
            if (!isChildDocumentTag)
            {
                return;
            }

            string keyString = tagString.CutLeft(CHILD_DOCUMENT_TAG_PREFIX.Length);
            string[] split = keyString.Split('[');
            if (split.Length != 2)
            {
                throw new NotImplementedException();
                // TODO: Add good exception message.
                throw new Exception();
            }

            string childDocumentTypeEnumString = split[0];
            string childDocumentIndexString = split[1];

            ChildDocumentTypeEnum childDocumentTypeEnumParsed;
            if (!Enum.TryParse<ChildDocumentTypeEnum>(childDocumentTypeEnumString, out childDocumentTypeEnumParsed))
            {
                throw new NotImplementedException();
                // TODO: Add good exception message.
                throw new Exception();
            }

            if (!childDocumentIndexString.EndsWith("]"))
            {
                throw new NotImplementedException();
                // TODO: Add good exception message.
                throw new Exception();
            }
            childDocumentIndexString = childDocumentIndexString.CutRight(1);
            int childDocumentIndexParsed;
            if (!Int32.TryParse(childDocumentIndexString, out childDocumentIndexParsed))
            {
                throw new NotImplementedException();
                // TODO: Add good exception message.
                throw new Exception();
            }

            childDocumentTypeEnum = childDocumentTypeEnumParsed;
            childDocumentListIndex = childDocumentIndexParsed;
        }
    }
}
