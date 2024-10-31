// Moved

using System.Runtime.CompilerServices;
using JJ.Framework.Common;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        /// <inheritdoc cref="_membername"/>
        public static string MemberName([CallerMemberName] string calledMemberName = null)
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() 
            => typeof(TType).Assembly.GetName().Name;

        public static string GetPrettyTitle(string uglyName)
        {
            string title = PrettifyName(uglyName);

            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Untitled";
            }

            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }

        public static string PrettifyName(string uglyName)
            => (uglyName ?? "").CutLeft("get_")
                               .CutLeft("set_")
                               .CutRightUntil(".") // Removing file extension
                               .CutRight(".")
                               .Replace("RunTest", "")
                               .Replace("Test", "")
                               .Replace("_", " ")
                               .RemoveExcessiveWhiteSpace();
    }
}