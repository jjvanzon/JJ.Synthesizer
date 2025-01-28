using System;
using System.Reflection;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class FileWishesAccessor
    {
        private static readonly Accessor _accessor = CreateAccessor();
        
        private static Accessor CreateAccessor()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes";
            Type     type     = assembly.GetType(typeName, true);
            var      accessor = new Accessor(type);
            return accessor;
        }
        
        private static int DEFAULT_MAX_EXTENSION_LENGTH => (int)_accessor.GetFieldValue(MemberName());
        
        public static (string filePathFirstPart, int number, string filePathLastPart)
            GetNumberedFilePathParts(
                string originalFilePath,
                string numberPrefix = " (",
                string numberSuffix = ")",
                bool mustNumberFirstFile = false,
                int maxExtensionLength = default)
        {
            if (maxExtensionLength == default) maxExtensionLength = DEFAULT_MAX_EXTENSION_LENGTH;
            
            return ((string filePathFirstPart, int number, string filePathLastPart))
                _accessor.InvokeMethod(MemberName(), originalFilePath, numberPrefix, numberSuffix, mustNumberFirstFile, maxExtensionLength);
        }
    }
}
