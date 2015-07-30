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
        private const string CHILD_DOCUMENT_TAG_PREFIX = "cd ";

        // TODO: Remove outcommented code.

        //private const string CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX = "cd.nx ";

        //public static object GetChildDocumentNodeIndexTag(int childDocumentNodeIndex)
        //{
        //    return String.Format("{0}{1}", CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX, childDocumentNodeIndex);
        //}

        //public static int? TryGetChildDocumentNodeIndex(object tag)
        //{
        //    string tagString = Convert.ToString(tag);

        //    if (String.IsNullOrEmpty(tagString))
        //    {
        //        return null;
        //    }

        //    bool isChildDocumentNodeIndexTag = tagString.StartsWith(CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX);
        //    if (!isChildDocumentNodeIndexTag)
        //    {
        //        return null;
        //    }

        //    string nodeIndexString = tagString.CutLeft(CHILD_DOCUMENT_NODE_INDEX_TAG_PREFIX.Length);

        //    int nodeIndex;
        //    if (!Int32.TryParse(nodeIndexString, out nodeIndex))
        //    {
        //        throw new NotImplementedException();
        //        // TODO: Use proper exception message.
        //        throw new Exception("");
        //    }

        //    return nodeIndex;
        //}

        public static object GetChildDocumentTag(int childDocumentID)
        {
            return String.Format("{0}{1}", CHILD_DOCUMENT_TAG_PREFIX, childDocumentID);
        }

        public static int? TryGetChildDocumentID(object tag)
        {
            string tagString = Convert.ToString(tag);

            if (String.IsNullOrEmpty(tagString))
            {
                return null;
            }

            bool isChildDocumentTag = tagString.StartsWith(CHILD_DOCUMENT_TAG_PREFIX);
            if (!isChildDocumentTag)
            {
                return null;
            }

            string idString = tagString.CutLeft(CHILD_DOCUMENT_TAG_PREFIX.Length);

            int id;
            if (!Int32.TryParse(idString, out id))
            {
                throw new Exception(String.Format("idString '{0}' in tag '{1}' for child document is not a valid ID.", idString, tag));
            }

            return id;
        }
    }
}
