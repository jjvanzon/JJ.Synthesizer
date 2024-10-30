using System.Runtime.CompilerServices;
using JJ.Framework.Common;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        /// <summary> Returns the current method name or current property name. </summary>
        public static string MemberName([CallerMemberName] string calledMemberName = null) 
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() => typeof(TType).Assembly.GetName().Name;
            
        public static string GetPrettyTitle(string uglyName)
        {
            string title = PrettifyName(uglyName);

            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }

        public static string PrettifyName(string uglyName)
        {
            string name;
            if (string.IsNullOrWhiteSpace(uglyName))
            {
                name = "Untitled";
            }
            else
            {
                name = uglyName.CutLeft("get_")
                               .CutLeft("set_")
                               .CutRightUntil(".") // Removing file extension
                               .CutRight(".")
                               .Replace("RunTest", "")
                               .Replace("Test", "")
                               .Replace("_", " ")
                               .RemoveExcessiveWhiteSpace();
            }

            return name;
        }
    }
}