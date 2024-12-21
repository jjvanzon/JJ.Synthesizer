using System;
using System.Reflection;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class JJFrameworkIOWishesAccessor
    {
        private static readonly Accessor _accessor = CreateAccessor();
        
        private static Accessor CreateAccessor()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes";
            Type     type     = assembly.GetType(typeName, true);
            var      accessor = new Accessor(type);
            return accessor;
        }
        
        public static (string filePathFirstPart, int number, string filePathLastPart)
            GetNumberedFilePathParts(
                string originalFilePath,
                string numberPrefix = " (",
                string numberSuffix = ")",
                bool mustNumberFirstFile = false)
        {
            return ((string filePathFirstPart, int number, string filePathLastPart))
                _accessor.InvokeMethod(NameWishes.MemberName(), originalFilePath, numberPrefix, numberSuffix, mustNumberFirstFile);
        }
    }
}
