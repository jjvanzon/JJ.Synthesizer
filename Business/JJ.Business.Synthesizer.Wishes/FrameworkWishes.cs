using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Collection_Wishes;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Mathematics_Copied;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Copied;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    namespace JJ_Framework_Collection_Wishes
    {
        public static class CollectionExtensionWishes
        { 
            internal static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
            {
                if (timeSpans == null) throw new ArgumentNullException(nameof(timeSpans));
                return timeSpans.Aggregate((x, y) => x + y);
            }
            
            internal static TimeSpan Sum<T>(this IEnumerable<T> source, Func<T, TimeSpan> selector)
            {
                return source.Select(selector).Sum();
            }

            internal static bool Contains(this IList<string> source, string match, bool ignoreCase = false)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));

                StringComparison stringComparison = ignoreCase.ToStringComparison();

                return source.Any(x => (x ?? "").Equals(match, stringComparison));
            }
            
            /// <inheritdoc cref="docs._onebecomestwo" />
            internal static IList<T> OneBecomesTwo<T>(this IList<T> list)
            {
                if (list == null) throw new ArgumentNullException(nameof(list));
                if (list.Count == 1) list = new List<T> { list[0], list[0] };
                return list;
            }
            
            /// <inheritdoc cref="docs._onebecomestwo" />
            internal static T[] OneBecomesTwo<T>(this T[] list) => OneBecomesTwo((IList<T>)list).ToArray();
            
            /// <inheritdoc cref="docs._frameworkwishproduct" />
            internal static double Product(this IEnumerable<double> collection)
            {
                if (collection == null) throw new ArgumentNullException(nameof(collection));
                
                var array = collection as double[] ?? collection.ToArray();
                
                if (!array.Any()) return 1;
                
                double product = array.FirstOrDefault();
                
                foreach (double value in array.Skip(1))
                {
                    product *= value;
                }
                
                return product;
            }
            
            public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
            {
                int i = 0;
                foreach (var x in enumerable)
                {
                    action(x, i++);
                }
            }

        }
    }

    namespace JJ_Framework_Collections_Copied
    {
        internal static class CollectionsExtensions_Copied
        {
            /// <inheritdoc cref="docs._frameworkwishproduct" />
            public static double Product<TSource>(this IEnumerable<TSource> collection, Func<TSource, double> selector)
                => collection.Select(selector).Product();
        
            /// <summary> AddRange is a member of List&lt;T&gt;. Here is an overload for HashSet&lt;T&gt;. </summary>
            public static void AddRange<T>(this HashSet<T> dest, IEnumerable<T> source)
            {
                if (dest == null) throw new ArgumentNullException(nameof(dest));
                if (source == null) throw new ArgumentNullException(nameof(source));

                foreach (T item in source)
                {
                    dest.Add(item);
                }
            }
        }
    }
    
    namespace JJ_Framework_Common_Wishes
    {
        internal static class EnvironmentHelperWishes
        {
            public static bool EnvironmentVariableIsDefined(string environmentVariableName, string environmentVariableValue)
                => String.Equals(GetEnvironmentVariable(environmentVariableName), environmentVariableValue, StringComparison.OrdinalIgnoreCase);
        }

        internal static class FilledInExtensionWishes
        {
            public static bool FilledIn(this string value)                 => FilledInWishes.FilledIn(value, false);
            public static bool FilledIn(this string value, bool trimSpace) => FilledInWishes.FilledIn(value, trimSpace);
            public static bool FilledIn<T>(this T[] arr)                   => FilledInWishes.FilledIn(arr);
            public static bool FilledIn<T>(this IList<T> coll)             => FilledInWishes.FilledIn(coll);
            public static bool FilledIn<T>(this T value)                   => FilledInWishes.FilledIn(value);
            public static bool FilledIn<T>(this T? value) where T : struct => FilledInWishes.FilledIn(value);

            public static bool Is(this string value, string comparison, bool ignoreCase = true) => FilledInWishes.Is(value, comparison, ignoreCase);
        }
        
        internal static class FilledInWishes
        {
            public static bool FilledIn(string value)                 => FilledIn(value, false);
            public static bool FilledIn(string value, bool trimSpace) => trimSpace ? !string.IsNullOrWhiteSpace(value): !string.IsNullOrEmpty(value);
            public static bool FilledIn<T>(T[] arr)                   => arr != null && arr.Length > 0;
            public static bool FilledIn<T>(IList<T> coll)             => coll != null && coll.Count > 0;
            public static bool FilledIn<T>(T value)                   => !Equals(value, default(T));
            public static bool FilledIn<T>(T? value) where T : struct => !Equals(value, default(T?)) && !Equals(value, default(T));
            
            public static bool Has(string value)                      => FilledIn(value);
            public static bool Has(string value, bool trimSpace)      => FilledIn(value, trimSpace);
            public static bool Has<T>(T[] arr)                        => FilledIn(arr);
            public static bool Has<T>(IList<T> coll)                  => FilledIn(coll);
            public static bool Has<T>(T value)                        => FilledIn(value);
            public static bool Has<T>(T? value) where T : struct      => FilledIn(value);
            
            public static bool Is(string value, string comparison, bool ignoreCase = false) => string.Equals(value, comparison, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }
    }
    
    /// <inheritdoc cref="_trygetsection"/>
    namespace JJ_Framework_Configuration_Wishes
    {
        internal static class ConfigurationManagerWishes
        {
            /// <inheritdoc cref="_trygetsection"/>
            public static T TryGetSection<T>()
                where T: class, new()
            {
                T config = null;

                try
                {
                    config = CustomConfigurationManager.GetSection<T>();
                }
                catch (Exception ex)
                {
                    // Allow 'Not Found' Exception
                    string configSectionName = NameWishes.GetAssemblyName<T>().ToLower();
                    string allowedMessage = $"Configuration section '{configSectionName}' not found.";
                    bool messageIsAllowed = string.Equals(ex.Message, allowedMessage);
                    bool messageIsAllowed2 = string.Equals(ex.InnerException?.Message, allowedMessage);
                    bool mustThrow = !messageIsAllowed && !messageIsAllowed2;
                    
                    if (mustThrow)
                    {
                        throw;
                    }
                }

                return config;
            }
        }
    }
    
    namespace JJ_Framework_IO_Wishes
    {
        internal static class FileWishes
        {
            
            private const int DEFAULT_MAX_EXTENSION_LENGTH = 8;
            
            /// <summary>
            /// If the originalFilePath already exists,
            /// a higher and higher number is inserted into the file name 
            /// until a file name is encountered that does not exist.
            /// Then that file path is returned.
            /// </summary>
            /// <param name="originalFilePath">
            /// The path to a file name, that does not yet have a number in it.
            /// </param>
            public static string GetNumberedFilePath(
                string originalFilePath,
                string numberPrefix = " (",
                string numberFormatString = "#",
                string numberSuffix = ")",
                bool mustNumberFirstFile = false,
                int maxExtensionLength = DEFAULT_MAX_EXTENSION_LENGTH)
            {
                (string filePathFirstPart, int number, string filePathLastPart) =
                    GetNumberedFilePathParts(originalFilePath, numberPrefix, numberSuffix, mustNumberFirstFile, maxExtensionLength);
            
                if (!mustNumberFirstFile && !File.Exists(originalFilePath))
                {
                    return originalFilePath;
                }
                
                string filePath;
                do
                {
                    filePath = $"{filePathFirstPart}{number.ToString(numberFormatString)}{filePathLastPart}";
                    number++;
                }
                while (File.Exists(filePath));
                
                return filePath;
            }
            
            private static readonly Mutex _createSafeFileStreamMutex = CreateMutex();
            private static Mutex CreateMutex()
                => new Mutex(false, "Global\\SynthWishes_CreateSafeFileStreamMutex_7f64fd76542045bb98c2e28a44d2df25");

            /// <summary>
            /// If the originalFilePath already exists,
            /// a higher and higher number is inserted into the file name 
            /// until a file name is encountered that does not exist.
            /// Then a file stream is returned for writing, so that
            /// the file immediately locks.
            /// Be sure to dispose the stream when you're done,
            /// so the file lock is released.
            /// </summary>
            /// <param name="originalFilePath">
            /// The absolute path to a file name, that does not yet have a number in it.
            /// </param>
            public static (string filePath, FileStream) CreateSafeFileStream(
                string originalFilePath,
                string numberPrefix = " (",
                string numberFormatString = "#",
                string numberSuffix = ")",
                bool mustNumberFirstFile = false,
                int maxExtensionLength = 8)
            {
                (string filePathFirstPart, int number, string filePathLastPart) =
                    GetNumberedFilePathParts(originalFilePath, numberPrefix, numberSuffix, mustNumberFirstFile, maxExtensionLength);
                
                _createSafeFileStreamMutex.WaitOne();
                try
                {
                    string filePath = originalFilePath;
                    
                    if (mustNumberFirstFile || File.Exists(filePath))
                    {
                        do
                        {
                            filePath = $"{filePathFirstPart}{number.ToString(numberFormatString)}{filePathLastPart}";
                            number++;
                        }
                        while (File.Exists(filePath));
                    }
                    
                    return (filePath, new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read));
                }
                finally
                {
                    _createSafeFileStreamMutex.ReleaseMutex();
                }
            }
            
            /// <summary>
            /// Splits the original file path into three parts: the first part of the file path, 
            /// the initial number to be used for numbering, and the last part of the file path.
            /// This method is used to generate a new file path by inserting a number into the file name 
            /// if the original file path already exists.
            /// </summary>
            /// <param name="originalFilePath">The path to a file name that does not yet have a number in it.</param>
            /// <param name="numberPrefix">The prefix to be used before the number in the file name.</param>
            /// <param name="numberSuffix">The suffix to be used after the number in the file name.</param>
            /// <param name="mustNumberFirstFile">
            /// A boolean indicating whether the first file should be numbered. 
            /// If true, numbering starts from 1; otherwise, it starts from 2.
            /// </param>
            /// <returns>
            /// A tuple containing three parts: 
            /// - The first part of the file path, which includes the directory and the file name up to the number prefix.
            /// - The initial number to be used for numbering.
            /// - The last part of the file path, which includes the number suffix and the file extension.
            /// </returns>
            public static (string filePathFirstPart, int number, string filePathLastPart) 
                GetNumberedFilePathParts(
                    string originalFilePath, 
                    string numberPrefix = " (", 
                    string numberSuffix = ")", 
                    bool mustNumberFirstFile = false,
                    int maxExtensionLength = DEFAULT_MAX_EXTENSION_LENGTH)
            {
                if (string.IsNullOrEmpty(originalFilePath)) throw new Exception("originalFilePath is null or empty.");
                
                string folderPath = Path.GetDirectoryName(originalFilePath)?.TrimEnd('\\'); // Remove slash from root (e.g. @"C:\")
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFilePath);
                string fileExtension = GetExtension(originalFilePath, maxExtensionLength);
                string separator = !string.IsNullOrEmpty(folderPath) ? "\\" : "";
                
                string filePathFirstPart = $"{folderPath}{separator}{fileNameWithoutExtension}{numberPrefix}";
                int number = mustNumberFirstFile ? 1 : 2;
                string filePathLastPart = $"{numberSuffix}{fileExtension}";
                return (filePathFirstPart, number, filePathLastPart);
            }
        
            public static string SanitizeFilePath(string filePath, string badCharReplacement = "-")
            {
                // Crashing a sanitize on an empty string seems a bit harsh.
                if (string.IsNullOrWhiteSpace(filePath)) return filePath;

                var forbiddenCharacters = Path.GetInvalidFileNameChars().ToHashSet();
                
                // Allow slash and colon (but not wildcards)
                forbiddenCharacters.Remove('\\');
                forbiddenCharacters.Remove('/');
                forbiddenCharacters.Remove(':');
                
                string sanitizedFilePath = new string(
                    filePath.SelectMany(chr => forbiddenCharacters.Contains(chr) ? badCharReplacement : $"{chr}")
                            .ToArray());
                
                sanitizedFilePath = sanitizedFilePath.Trim(badCharReplacement);
                
                return sanitizedFilePath;
            }

            public static string GetFileNameWithoutExtension(string filePath, int maxExtensionLength)
            {
                if (!Has(filePath)) return filePath;
                string extension = Path.GetExtension(filePath);
                if (extension.Length > maxExtensionLength) return filePath;
                string fileNameWithoutExtension = filePath.CutRight(extension);
                return fileNameWithoutExtension;
            }
            
            public static string GetExtension(string filePath, int maxExtensionLength)
            {
                if (!Has(filePath)) return filePath;
                string extension = Path.GetExtension(filePath);
                if (extension.Length > maxExtensionLength) return "";
                return extension;
            }
            
            public static bool HasExtension(string filePath, int maxExtensionLength)
            {
                if (!Has(filePath)) return false;
                string extension = GetExtension(filePath, maxExtensionLength);
                if (extension.Length > maxExtensionLength) return false;
                return true;
            }
            
            /// <summary>
            /// If the file actually exists, true is returned.
            /// If it exists as a directory, false is returned.
            /// If the value contains invalid path characters, false is returned.
            /// Otherwise, it returns true if the path has an extension.
            /// </summary>
            public static bool IsFile(string path, int maxExtensionLength = DEFAULT_MAX_EXTENSION_LENGTH)
            {
                if (!Has(path)) return false;
                if (File.Exists(path)) return true;
                if (Directory.Exists(path)) return false;
                if (path.Contains(Path.GetInvalidPathChars())) return false;
                string extension = Path.GetExtension(path);
                if (string.IsNullOrEmpty(extension)) return false;
                return extension.Length <= maxExtensionLength;
            }
        }
    }
    
    namespace JJ_Framework_Mathematics_Wishes
    {
        public static class RandomizerWishes
        {
            public static T GetRandomItem<T>(params T[] collection) 
                => Randomizer_Copied.GetRandomItem(collection);
        }
    }
    
    namespace JJ_Framework_Mathematics_Copied
    {
        public static class Randomizer_Copied
        {
            private static readonly Random _random = CreateRandom();

            private static Random CreateRandom()
            {
                int randomSeed = Guid.NewGuid().GetHashCode();

                var random = new Random(randomSeed);

                return random;
            }

            /// <summary>
            /// Gets a random Int32 between Int32.MinValue and Int32.MaxValue - 1.
            /// </summary>
            public static int GetInt32() => GetInt32(int.MinValue, int.MaxValue - 1);

            /// <summary>
            /// Gets a random Int32 between 0 and the specified value.
            /// max must at most be Int32.MaxValue - 1 or an overflow exception could occur.
            /// </summary>
            public static int GetInt32(int max) => GetInt32(0, max);

            /// <summary>
            /// Gets a random Int32 between between a minimum and a maximum.
            /// Both the minimum and the maximum are included.
            /// max must at most be Int32.MaxValue - 1 or an overflow exception could occur.
            /// </summary>
            public static int GetInt32(int min, int max)
            {
                checked
                {
                    int result = _random.Next(min, max + 1);
                    return result;
                }
            }

            public static T GetRandomItem<T>(IEnumerable<T> collection)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                int count = collection.Count();
                if (count == 0)
                {
                    throw new Exception("collection.Count() == 0");
                }

                int index = GetInt32(count - 1);
                // ReSharper disable once PossibleMultipleEnumeration
                return collection.ElementAt(index);
            }

            public static T TryGetRandomItem<T>(IEnumerable<T> collection)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                int count = collection.Count();
                if (count == 0)
                {
                    // Unfortunately, you cannot create overloads that return T? for structs and null for classes.
                    // This is not currently possible in C#. I think they're working on it.
                    return default;
                }

                int index = GetInt32(count - 1);
                // ReSharper disable once PossibleMultipleEnumeration
                return collection.ElementAt(index);
            }

            /// <summary> Returns a random number between 0.0 and 1.0. </summary>
            public static double GetDouble() => _random.NextDouble();

            /// <summary> Returns a random number between 0.0 and 1.0. </summary>
            public static float GetSingle() => (float)_random.NextDouble();

            /// <param name="min">inclusive</param>
            /// <param name="max">exclusive</param>
            public static double GetDouble(double min, double max)
            {
                double between0And1 = GetDouble();
                double range = max - min;
                double value = min + between0And1 * range;
                return value;
            }

            /// <param name="min">inclusive</param>
            /// <param name="max">exclusive</param>
            public static float GetSingle(float min, float max)
            {
                float between0And1 = GetSingle();
                float range = max - min;
                float  value = min + between0And1 * range;
                return value;
            }
         }
    }
    
    namespace JJ_Framework_Reflection_Copied
    {
        internal static class ReflectioHelper_Copied
        {
            public static bool IsProperty(this MethodBase method)
            {
                if (method == null) throw new ArgumentNullException(nameof(method));

                bool isProperty = method.Name.StartsWith("get_") ||
                                  method.Name.StartsWith("set_");

                return isProperty;
            }
        }
    }
    
    namespace JJ_Framework_Testing_Wishes
    {
        internal static class TestWishes
        {
            // ReSharper disable AssignNullToNotNullAttribute
            public static bool CurrentTestIsInCategory(string category)
            {
                var methodQuery = new StackTrace().GetFrames().Select(x => x.GetMethod());
                
                var attributeQuery
                    = methodQuery.SelectMany(method => method.GetCustomAttributes()
                                                             .Union(method.DeclaringType?.GetCustomAttributes()));
                var categoryQuery
                    = attributeQuery.Where(attr => attr.GetType().Name == "TestCategoryAttribute")
                                    .Select(attr => attr.GetType().GetProperty("TestCategories")?.GetValue(attr))
                                    .OfType<IEnumerable<string>>()
                                    .SelectMany(x => x);
                
                bool isInCategory = categoryQuery.Any(x => String.Equals(x, category, StringComparison.OrdinalIgnoreCase));
                
                return isInCategory;
            }
        }
    }
        
    namespace JJ_Framework_Text_Wishes
    {
        internal static class StringExtensionWishes
        { 
            public static bool StartsWithBlankLine(this string text) => StringWishes.StartsWithBlankLine(text);
            public static bool EndsWithBlankLine(this string text) => StringWishes.EndsWithBlankLine(text);
        }
        
        internal static class StringWishes
        { 
            public static int CountLines(this string str)
            {
                // Less efficient:
                //int count = str.Trim().Split(NewLine).Length;
                //int count = 1 + str.Count(c => c == '\n');

                if (str == null) return 0;
                    
                int count = 1; // Start with 1 to account for the first line
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '\n') // Platform safe for '\n' or "\r\n".
                    {
                        count++;
                    }
                }

                if (str.EndsWith(NewLine))
                {
                    count--;
                }

                return count;
            }

            public static bool Contains(this string str, string substring, bool ignoreCase = false)
            {
                if (str == null) throw new ArgumentNullException(nameof(str));
                return str.IndexOf(substring, ToStringComparison(ignoreCase)) >= 0;
            }

            public static bool Contains(this string str, string[] words, bool ignoreCase = false)
            {
                if (str == null) throw new ArgumentNullException(nameof(str));
                return words.Any(x => str.IndexOf(x, ToStringComparison(ignoreCase)) >= 0);
            }

            public static bool Contains(this string str, char[] chars)
            {
                if (str == null) throw new ArgumentNullException(nameof(str));
                return chars.Any(str.Contains);
            }

            public static StringComparison ToStringComparison(this bool ignoreCase) 
                => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            public static string PrettyDuration(double? durationInSeconds)
            {
                if (!durationInSeconds.HasValue) return default;
                return PrettyDuration(durationInSeconds.Value);
            }
            
            public static string PrettyDuration(double durationInSeconds) => PrettyTimeSpan(TimeSpan.FromSeconds(durationInSeconds));
            
            public static string PrettyTimeSpan(this TimeSpan timeSpan)
            {
                double totalNanoseconds = timeSpan.TotalMilliseconds * 1000;
        
                if (timeSpan.TotalDays >= 1) return $"{timeSpan.TotalDays:0.00} d";
                if (timeSpan.TotalHours >= 1) return $"{timeSpan.TotalHours:0.00} h";
                if (timeSpan.TotalMinutes >= 1) return $"{timeSpan.TotalMinutes:0.00} min";
                if (timeSpan.TotalSeconds >= 1) return $"{timeSpan.TotalSeconds:0.00} s";
                if (timeSpan.TotalMilliseconds >= 1) return $"{timeSpan.TotalMilliseconds:0.00} ms";
        
                return $"{totalNanoseconds:0.00} ns";
            }
            
            public static string PrettyByteCount(long byteCount)
            {
                const double kB = 1024;
                const double MB = kB * 1024;
                const double GB = MB * 1024;

                if (byteCount <= 5 * kB) return $"{byteCount} bytes";
                if (byteCount <= 5 * MB) return $"{byteCount / kB:0} kB";
                if (byteCount <= 5 * GB) return $"{byteCount / MB:0} MB";
                
                return $"{byteCount / GB:0} GB";
            }
            
            public static string WithShortGuids(this string input, int length)
            {
                // Regular expression to match GUID-like sequences with or without dashes
                var guidPattern = new Regex(@"\b[a-fA-F0-9]{4,32}\b(-?[a-fA-F0-9]{4,32})*\b", RegexOptions.IgnoreCase);

                // Replace each matched GUID-like sequence with a truncated version
                string output = guidPattern.Replace(input, match =>
                {
                    // Remove dashes from the matched sequence
                    string guid = match.Value.Replace("-", "");

                    // Shorten the GUID to the desired length, ensuring it doesn't exceed the original length
                    return guid.Substring(0, Math.Min(length, guid.Length));
                });

                return output;
            }
            
            public static string PrettyTime() => PrettyTime(DateTime.Now);
            
            public static string PrettyTime(DateTime dateTime) => $"{dateTime:HH:mm:ss.fff}";
            
            public static string Trim(this string text, string trim)
            {
                if (text == null) throw new ArgumentNullException(nameof(text));
                return text.TrimStart(trim).TrimEnd(trim);
            }

            /// <summary>
            /// Determines whether the given string ends with a punctuation character,
            /// optionally ignoring trailing whitespace.
            /// This method is helpful when building strings with multiple optional elements,
            /// ensuring proper punctuation is applied only when necessary.
            /// 
            /// If the string is <c>null</c> or empty, it is treated as the beginning of a line,
            /// where no punctuation is required. In this case, the method returns <c>true</c>,
            /// indicating no additional punctuation is needed.
            /// </summary>
            /// <param name="text">
            /// The string to evaluate. This parameter can be <c>null</c> or empty.
            /// </param>
            /// <param name="ignoreWhiteSpace">
            /// If set to <c>true</c>, trailing whitespace in the string is ignored before evaluating for punctuation.
            /// Defaults to <c>true</c>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the string ends with a punctuation character,
            /// or if the string is <c>null</c> or empty (considered as starting a new line).<br/>
            /// <c>false</c> if the string does not end with a punctuation character
            /// after accounting for <paramref name="ignoreWhiteSpace"/>.
            /// </returns>
            public static bool EndsWithPunctuation(this string text, bool ignoreWhiteSpace = true)
            {
                if (ignoreWhiteSpace) text = text?.TrimEnd();
                
                if (!FilledIn(text)) 
                { 
                    // Start of string is good enough for punctuation.
                    return true;
                }
                
                // ReSharper disable once PossibleNullReferenceException
                return char.IsPunctuation(text[text.Length - 1]);
            }
            
            public static bool StartsWithBlankLine(string text)
            {
                if (!Has(text)) return true;
                
                for (int i = 0; i < text.Length; i++)
                {
                    char chr = text[i];
                    
                    bool isWhiteSpace = char.IsWhiteSpace(chr);
                    if (!isWhiteSpace) return false;
                    
                    bool isNewLine = chr == '\n';
                    if (isNewLine) return true;
                    
                    bool isLastChar = i == text.Length - 1;
                    if (isLastChar) return false;
                }
                
                return false;
            }
                
            public static bool EndsWithBlankLine(string text)
            {
                if (!Has(text)) return true;
                
                for (int i = text.Length - 1; i >= 0; i--)
                {
                    char chr = text[i];
                    
                    bool isWhiteSpace = char.IsWhiteSpace(chr);
                    if (!isWhiteSpace) return false;
                    
                    bool isNewLine = chr == '\n';
                    if (isNewLine) return true;
                    
                    bool isFirstChar = i == 0;
                    if (isFirstChar) return false;
                }
                
                return false;
            }
        }
    }
    
    namespace JJ_Framework_Text_Copied
    {
        internal class StringBuilderWithIndentation_AdaptedFromFramework
        {
            public StringBuilderWithIndentation_AdaptedFromFramework()
                : this("  ")
            { }

            public StringBuilderWithIndentation_AdaptedFromFramework(string tabString)
                : this(tabString, NewLine)
            { }

            public StringBuilderWithIndentation_AdaptedFromFramework(string tabString , string enter)
            {
                _tabString = tabString;
                _enter = enter;
            }
            
            private readonly string _tabString;
            private readonly string _enter;
            
            private readonly StringBuilder _sb = new StringBuilder();

            public override string ToString() => _sb.ToString();
            public void Indent() => IndentLevel++;
            public void Unindent() => IndentLevel--;
            public void AppendEnter() => Append(_enter);
            public void AppendLine() => AppendLine("");
            public void Append(object x) => _sb.Append(x);

            public void AppendLine(string value)
            {
                AppendTabs();

                _sb.Append(value);

                _sb.Append(_enter);
            }

            public void AppendFormat(string format, params object[] args) => _sb.AppendFormat(format, args);

            private int _indentLevel;
            public int IndentLevel
            {
                get => _indentLevel;
                set
                {
                    if (value < 0) throw new Exception("value cannot be less than 0.");
                    _indentLevel = value;
                }
            }

            public void AppendTabs()
            {
                string tabs = GetTabs();
                _sb.Append(tabs);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string GetTabs()
            {
                string tabs = _tabString?.Repeat(_indentLevel);
                return tabs;
            }
        }

        internal static class StringExtensions_Copied
        {
            /// <summary>
            /// Will trim off repetitions of the same value from the given string.
            /// These are variations of the standard .NET methods that instead of just taking char[] can take a string or a length.
            /// </summary>
            public static string TrimEnd(this string input, string end)
            {
                if (string.IsNullOrEmpty(end)) throw new Exception($"{nameof(end)} is null or empty.");

                string temp = input;

                while (temp.EndsWith(end))
                {
                    temp = temp.TrimEnd(end.Length);
                }

                return temp;
            }
            
            /// <summary>
            /// Will trim off repetitions of the same value from the given string.
            /// These are variations of the standard .NET methods that instead of just taking char[] can take a string or a length.
            /// </summary>
            public static string TrimEnd(this string input, int length) => input.Left(input.Length - length);
            
            /// <summary>
            /// Will trim off repetitions of the same value from the given string.
            /// These are variations of the standard .NET methods that instead of just taking char[] can take a string or a length.
            /// </summary>
            public static string TrimStart(this string input, string start)
            {
                if (string.IsNullOrEmpty(start)) throw new Exception($"{nameof(start)} is null or empty.");

                string temp = input;

                while (temp.StartsWith(start))
                {
                    temp = temp.TrimStart(start.Length);
                }

                return temp;
            }

            /// <summary>
            /// Will trim off repetitions of the same value from the given string.
            /// These are variations of the standard .NET methods that instead of just taking char[] can take a string or a length.
            /// </summary>
            public static string TrimStart(this string input, int length) => input.Right(input.Length - length);
            
            /// <summary>
            /// Repeat a string a number of times, returning a single string.
            /// </summary>
            public static string Repeat(this string stringToRepeat, int repeatCount)
            {
                if (stringToRepeat == null) throw new ArgumentNullException(nameof(stringToRepeat));

                char[] sourceChars = stringToRepeat.ToCharArray();
                int sourceLength = sourceChars.Length;

                int destLength = sourceLength * repeatCount;
                var destChars = new char[destLength];

                for (var i = 0; i < destLength; i += sourceLength)
                {
                    Array.Copy(sourceChars, 0, destChars, i, sourceLength);
                }

                var destString = new string(destChars);
                return destString;
            }
        
            /// <summary>
            /// Takes the part of a string until the specified delimiter. Excludes the delimiter itself.
            /// </summary>
            public static string TakeEndUntil(this string input, string until)
            {
                if (until == null) throw new ArgumentNullException(nameof(until));
                int index = input.LastIndexOf(until, StringComparison.Ordinal);
                if (index == -1) return "";
                int length = input.Length - index - 1;
                string output = input.Right(length);
                return output;
            }
            
            /// <summary>
            /// Returns the left part of a string.
            /// Can return less characters than the length provided if string is shorter.
            /// </summary>
            public static string TakeStart(this string input, int length)
            {
                if (length > input.Length) length = input.Length;

                return input.Left(length);
            }
        }
    }
}
