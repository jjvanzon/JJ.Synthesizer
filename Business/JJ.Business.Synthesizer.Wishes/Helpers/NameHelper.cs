using System.Runtime.CompilerServices;
using JJ.Framework.Common;
using static System.Environment;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        /// <summary> Returns the current method name or current property name. </summary>
        public static string Name([CallerMemberName] string calledMemberName = null) 
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() => typeof(TType).Assembly.GetName().Name;
            
        public static string GetPrettyTitle(string fileName)
        {
            string title = GetPrettyName(fileName);

            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }

        public static string GetPrettyName(string fileName)
        {
            string name;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                name = "Untitled";
            }
            else
            {
                name = fileName.CutRightUntil(".") // Removing file extension
                                .CutRight(".")
                                .Replace("RunTest", "")
                                .Replace("Test", "")
                                .Replace("_", " ")
                                .Trim();
            }

            return name;
        }
    }
}