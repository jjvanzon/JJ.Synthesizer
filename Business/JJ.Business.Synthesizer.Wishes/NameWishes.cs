using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Framework.Existence.Core;
using JJ.Framework.Text.Core;
using static System.Environment;
using static System.IO.Path;
using static System.String;
using static JJ.Framework.Existence.Core.FilledInHelper;
using static JJ.Framework.IO.Core.FileHelperCore;

namespace JJ.Business.Synthesizer.Wishes
{
    // NameWishes Helper
    
    public static class NameWishes
    {
        // ResolveName

        /// <inheritdoc cref="_resolvename"/>
        public static string ResolveName(
            object nameSource1 = null, object nameSource2 = null, object nameSource3 = null, object nameSource4 = null,
            object nameSource5 = null, object nameSource6 = null, object nameSource7 = null, object nameSource8 = null,
            [CallerMemberName] string callerMemberName = null)
            => ResolveName(null, nameSource1, nameSource2, nameSource3, nameSource4, nameSource5, nameSource6, nameSource7, nameSource8, callerMemberName);

        /// <inheritdoc cref="_resolvename"/>
        public static string ResolveName(
            IList<int> ids,
            object nameSource1 = null, object nameSource2 = null, object nameSource3 = null, object nameSource4 = null,
            object nameSource5 = null, object nameSource6 = null, object nameSource7 = null, object nameSource8 = null,
            [CallerMemberName] string callerMemberName = null)
        {
            string name = TryGetName(
                nameSource1, nameSource2, nameSource3, nameSource4, 
                nameSource5, nameSource6, nameSource7, nameSource8,
                callerMemberName);

            name = PrettifyName(name);
            
            if (Has(ids))
            {
                string idDescriptor = ids.IDDescriptor();
                if (!name.EndsWith("(" + idDescriptor + ")")) // Prevent duplicate mentions of the ID.
                {
                    name += " " + idDescriptor;
                }
            }
            
            return name;
        }
        
        private static string TryGetName(params object[] nameSources) => TryGetName((IList<object>)nameSources);

        private static string TryGetName(IList<object> nameSources)
        {
            if (nameSources == null) throw new ArgumentNullException(nameof(nameSources));
            return nameSources.Select(TryGetName).FirstOrDefault(FilledIn);
        }
                
        private static string TryGetName(object nameSource)
        {
            switch (nameSource)
            {
                case null: return null;
                case string str: return str;
                case Outlet outlet: return outlet.GetName();
                case IEnumerable<object> coll: return TryGetName(coll.ToArray());
                case Tape tape: return tape.GetName();
                case Delegate d: return d.Method.Name;
                case AudioFileOutput audioFileOutput: return TryGetName(audioFileOutput.FilePath, audioFileOutput.Name);
                case FlowNode flowNode: return flowNode.Name;
                case Sample sample: return TryGetName(sample.Location, sample.Name);
                case Buff buff: return TryGetName(buff.FilePath, buff.UnderlyingAudioFileOutput);
                default: throw new Exception($"Unsupported {nameof(nameSource)} type: {nameSource.GetType()}.");
            }
        }
        
        // ResolveFileExtension
        
        public static string ResolveFileExtension(
            AudioFileFormatEnum audioFormat = default, 
            params string[] filePaths)
            => ResolveFileExtension(default, audioFormat, filePaths);
        
        public static string ResolveFileExtension(
            string fileExtension, AudioFileFormatEnum audioFileFormat = default, 
            params string[] filePaths)
        {
            if (Has(fileExtension)) return fileExtension;
            if (Has(audioFileFormat)) return audioFileFormat.FileExtension();
            
            foreach (string filePath in filePaths)
            {
                string value = TryGetName(filePath);
                value = SanitizeFilePath(value);
                value = GetExtension(value, ConfigResolver.Static.GetFileExtensionMaxLength);
                if (Has(value))
                {
                    return value;
                } 
            }
            
            var exceptionInfo = new
            {
                fileExtension, 
                audioFileFormat, 
                filePaths, 
                filePath1 = filePaths.ElementAtOrDefault(0),
                filePath2 = filePaths.ElementAtOrDefault(1),
                filePath3 = filePaths.ElementAtOrDefault(2)
            };

            throw new Exception($"Could not resolve file extension from {exceptionInfo}.");
        }
        
        // ResolveFilePath

        public static string ResolveFilePath(
            AudioFileFormatEnum audioFormat, 
            string filePath1 = null,
            string filePath2 = null,
            string filePath3 = null,
            string filePath4 = null,
            string filePath5 = null,
            string filePath6 = null,
            [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(default, audioFormat, default, filePath1, filePath2, filePath3, filePath4, filePath5, filePath6, callerMemberName);
        
        public static string ResolveFilePath(
            string fileExtension, 
            string filePath1 = null, 
            string filePath2 = null, 
            string filePath3 = null, 
            string filePath4 = null, 
            string filePath5 = null,
            string filePath6 = null,
            [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(fileExtension, default, default, filePath1, filePath2, filePath3, filePath4, filePath5, filePath6, callerMemberName);
        
        public static string ResolveFilePath(
            string fileExtension,
            AudioFileFormatEnum audioFormat,
            params string[] filePaths)
        {
            string resolvedExtension = ResolveFileExtension(fileExtension, audioFormat, filePaths);
            string minusExtension = (filePaths.FirstOrDefault(FilledIn) ?? "").CutRight(resolvedExtension);
            string resolvedFilePath = ReformatFilePath(minusExtension, resolvedExtension);
            return resolvedFilePath;
        }

        /// <summary>
        /// Sanitizes any invalid characters from the file path.
        /// Replaces the file extension with the current AudioFormat.
        /// Fills up to the full path, in case it is a relative folder.
        /// Or if there is no folder at all, the current directory is used.
        /// </summary>
        private static string ReformatFilePath(string filePath, string newFileExtension)
        {
            // Sanitize file path
            string sanitizedFilePath = SanitizeFilePath(filePath, badCharReplacement: " ");
            string sanitizedFileExtension = SanitizeFilePath(newFileExtension, badCharReplacement: " ");
            
            // Find the full folder path
            string folderPath = GetDirectoryName(sanitizedFilePath);
            string absoluteFolder = IsNullOrWhiteSpace(folderPath) 
                ? Directory.GetCurrentDirectory() 
                : GetFullPath(folderPath);
            
            // Replace file extension
            string fileNameWithoutExtension = GetFileNameWithoutExtension(sanitizedFilePath, ConfigResolver.Static.GetFileExtensionMaxLength);
            string audioFormatExtension = sanitizedFileExtension;
            string fileName = fileNameWithoutExtension + audioFormatExtension;

            // Combine folder path and new file name
            return Combine(absoluteFolder, fileName);
        }

        // For Entities
        
        /// <inheritdoc cref="_operatorgetname" />
        public static string GetName(Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetName(entity.Operator);
        }

        /// <inheritdoc cref="_operatorgetname" />
        public static string GetName(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (op.NameIsOperatorTypeName()) return default;
            return op.Name;
        }

        /// <inheritdoc cref="_names"/>
        public static Curve SetName(Curve entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        /// <inheritdoc cref="_names"/>
        public static Sample SetName(Sample entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        /// <inheritdoc cref="_names"/>
        public static AudioFileOutput SetName(AudioFileOutput entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }
        
        /// <inheritdoc cref="_names"/>
        public static Outlet SetName(Outlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Operator.SetName(name);
            return entity;
        }

        /// <inheritdoc cref="_names"/>
        public static Operator SetName(Operator op, string name)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            
            if (name.IsNully()) return op;
            op.Name = name;
            if (op.AsCurveIn?.Curve != null) op.AsCurveIn.Curve.Name = name;
            if (op.AsSampleOperator?.Sample != null) op.AsSampleOperator.Sample.Name = name;
            
            return op;
        }
        
        public static string OperatorTypeName(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return op.OperatorTypeName;
        }

        public static string OperatorTypeName(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return OperatorTypeName(outlet.Operator);
        }
        
        public static string OperatorTypeName(FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.OperatorTypeName;
        }

        public static string ToDisplayName(string operatorTypeName) =>  OperatorTypeDisplayName(operatorTypeName);

        public static string OperatorTypeDisplayName(string operatorTypeName) => PropertyDisplayNames.ResourceManager.GetString(operatorTypeName);

        public static string OperatorTypeDisplayName(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return OperatorTypeDisplayName(op.OperatorTypeName);
        }
        
        public static string OperatorTypeDisplayName(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return OperatorTypeName(outlet.Operator);
        }
        
        public static string OperatorTypeDisplayName(FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return OperatorTypeName(flowNode.UnderlyingOperator);
        }

        // Helpers
        
        internal static bool NameIsOperatorTypeName(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return NameIsOperatorTypeName(op.Name, op.OperatorTypeName);
        }
        
        internal static bool NameIsOperatorTypeName(string name, string operatorTypeName)
        {
            if (!Has(name)) return false;
            if (operatorTypeName.Is(name)) return true;
            if (PropertyDisplayNames.ResourceManager.GetString(operatorTypeName).Is(name)) return true;
            return false;
        }
        
        public static string PrettifyName(string uglyName)
        {
            string prettyName = uglyName;
            
            if (IsFile(prettyName, ConfigResolver.Static.GetFileExtensionMaxLength))
            {
                prettyName = GetFileNameWithoutExtension(GetFileName(uglyName));
            }
            
            prettyName = (prettyName ?? "").CutLeft("get_")
                                           .CutLeft("set_")
                                           .Replace("RunTest", "")
                                           .Replace("Test", "")
                                           .Replace("_", " ")
                                           .RemoveExcessiveWhiteSpace();
            return prettyName;
        }
        
        public static string PrettyTitle(string uglyName, char underlineChar = '-')
        {
            string title = PrettifyName(uglyName);

            if (IsNullOrWhiteSpace(title))
            {
                title = "Untitled";
            }

            title = title.WithShortGuids(4);

            string dashes = "".PadRight(title.Length, underlineChar);

            return title + NewLine + dashes;
        }
    
        // TODO: Use the one from JJ.Framework.Common.Core.
        /// <inheritdoc cref="_membername"/>
        public static string Name([CallerMemberName] string name = null)
            => name.CutLeft("get_").CutLeft("set_");
    }

    // NameWishes Entity Extensions
    
    public static class NameExtensionWishes
    {
        /// <inheritdoc cref="_operatorgetname" />
        public static string GetName(this Outlet outlet) => NameWishes.GetName(outlet);
        /// <inheritdoc cref="_operatorgetname" />
        public static string GetName(this Operator op) => NameWishes.GetName(op);
        /// <inheritdoc cref="_names"/>
        public static Curve SetName(this Curve entity, string name) => NameWishes.SetName(entity, name);
        /// <inheritdoc cref="_names"/>
        public static Sample SetName(this Sample entity, string name) => NameWishes.SetName(entity, name);
        /// <inheritdoc cref="_names"/>
        public static AudioFileOutput SetName(this AudioFileOutput entity, string name) => NameWishes.SetName(entity, name);
        /// <inheritdoc cref="_names"/>
        public static Outlet SetName(this Outlet entity, string name) => NameWishes.SetName(entity, name);
        /// <inheritdoc cref="_names"/>
        public static Operator SetName(this Operator op, string name) => NameWishes.SetName(op, name);
        public static string OperatorTypeName(this Operator op) => NameWishes.OperatorTypeName(op);
        public static string OperatorTypeName(this Outlet outlet) => NameWishes.OperatorTypeName(outlet);
        public static string OperatorTypeName(this FlowNode flowNode) => NameWishes.OperatorTypeName(flowNode);
        public static string ToDisplayName(this string operatorTypeName) => NameWishes.ToDisplayName(operatorTypeName);
        public static string OperatorTypeDisplayName(this string operatorTypeName) => NameWishes.OperatorTypeDisplayName(operatorTypeName);
        public static string OperatorTypeDisplayName(this Operator op) => NameWishes.OperatorTypeDisplayName(op);
        public static string OperatorTypeDisplayName(this Outlet outlet) => NameWishes.OperatorTypeDisplayName(outlet);
        public static string OperatorTypeDisplayName(this FlowNode flowNode) => NameWishes.OperatorTypeDisplayName(flowNode);

        // Helpers
        
        internal static bool NameIsOperatorTypeName(this Operator op) => NameWishes.NameIsOperatorTypeName(op);
        public static string PrettifyName(this string uglyName) => NameWishes.PrettifyName(uglyName);
        public static string PrettyTitle(this string uglyName, char underlineChar = '-')
            => NameWishes.PrettyTitle(uglyName, underlineChar);
    }

    // NameWishes FlowNode
    
    public partial class FlowNode
    {
        /// <inheritdoc cref="_names"/>
        public FlowNode SetName(object nameSource = null, object fallBackNameSource = null, [CallerMemberName] string callerMemberName = null)
        {
            Name = ResolveName(nameSource, fallBackNameSource, callerMemberName);
            return this;
        }

        /// <inheritdoc cref="_names"/>
        public string Name
        {
            get => _underlyingOutlet.GetName();
            set => _underlyingOutlet.SetName(value);
        }

        public string OperatorTypeName => NameWishes.OperatorTypeName(this);
        public string OperatorTypeDisplayName => NameWishes.OperatorTypeDisplayName(this);
    }
}
