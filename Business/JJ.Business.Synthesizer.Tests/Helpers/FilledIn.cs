using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class FilledInExtensions
    {
        public static bool IsNully <T>(this NullyPair<T> nullyPair) where T : struct => FilledInHelper.IsNully(nullyPair);
        public static bool FilledIn<T>(this NullyPair<T> nullyPair) where T : struct => FilledInHelper.FilledIn(nullyPair);
    }
    
    internal class FilledInHelper
    {
        public static bool IsNully <T>(NullyPair<T> nullyPair) where T : struct => !FilledIn(nullyPair);
        public static bool Has     <T>(NullyPair<T> nullyPair) where T : struct => FilledIn(nullyPair);
        public static bool FilledIn<T>(NullyPair<T> nullyPair) where T : struct 
            => nullyPair != null && !nullyPair.Equals(default(NullyPair<T>)) && !nullyPair.Nully.Equals(default(T));
    }
}
