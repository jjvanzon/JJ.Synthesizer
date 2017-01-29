namespace JJ.Business.Synthesizer.Helpers
{
    public static class NameHelper
    {
        /// <summary>
        /// Formats the name into a form, that is case-insensitive,
        /// null / "" tollerant, whitespace tollerant, etc.
        /// </summary>
        public static string ToCanonical(string name)
        {
            name = name ?? "";
            name = name.Trim();
            name = name.ToLower();

            // TODO: Accent-insensitivity?
            // TODO: Remove second spaces?

            return name;
        }

        /// <summary>
        /// Checks whether the name is filled in,
        /// taking whitespace into account, etc.
        /// </summary>
        public static bool IsFilledIn(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// Compares two names case-insensitively, null / "" tollerant,
        /// white space tollerant, etc.
        /// Note that this executes NameHelper.ToCanonical twice.
        /// Explicitly calling ToCanonical and String.Equals yourself
        /// might perform better.
        /// </summary>
        public static bool AreEqual(string name1, string name2)
        {
            string canonicalName1 = ToCanonical(name1);
            string canonicalName2 = ToCanonical(name2);
            bool areEqual = string.Equals(canonicalName1, canonicalName2);
            return areEqual;
        }
    }
}
