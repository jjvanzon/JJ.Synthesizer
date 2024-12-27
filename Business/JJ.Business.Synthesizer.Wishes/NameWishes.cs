using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.IO.Path;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // NameWishes Helper
    
    public static class NameWishes
    {
        // ResolveName
        
        /// <inheritdoc cref="docs._resolvename"/>
        public static string ResolveName(
            object nameSource1 = null, object nameSource2 = null, object nameSource3 = null, object nameSource4 = null,
            object nameSource5 = null, object nameSource6 = null, object nameSource7 = null, object nameSource8 = null,
            object explicitNameSource = null, [CallerMemberName] string callerMemberName = null)
        {
            string explicitName = TryGetName(explicitNameSource);
            if (!IsNullOrWhiteSpace(explicitName))
            {
                // Don't prettify. Used for explicit file names.
                return explicitName;
            }
            
            string name = TryGetName(
                nameSource1, nameSource2, nameSource3, nameSource4, 
                nameSource5, nameSource6, nameSource7, nameSource8,
                callerMemberName);

            name = PrettifyName(name);
            
            return name;
        }
        
        private static string TryGetName(params object[] nameSources)
            => TryGetName((IList<object>)nameSources);

        private static string TryGetName(IList<object> nameSources)
        {
            if (nameSources == null) throw new ArgumentNullException(nameof(nameSources));
            return nameSources.Select(TryGetName).FirstOrDefault(x => !IsNullOrWhiteSpace(x));
        }
                
        private static string TryGetName(object nameSource)
        {
            switch (nameSource)
            {
                case null: 
                    return null;

                case string str: 
                    return str;
                
                case Outlet outlet: 
                    return outlet.Operator.Name;
                
                case FlowNode flowNode: 
                    return flowNode.Name;
                
                case Tape tape:
                    return tape.GetName();
                
                case Buff buff:
                    return TryGetName(buff.FilePath, buff.UnderlyingAudioFileOutput);
                
                case AudioFileOutput audioFileOutput:
                    return TryGetName(audioFileOutput.FilePath, audioFileOutput.Name);
                
                case Sample sample:
                    return TryGetName(sample.Location, sample.Name);
                                    
                case Delegate d: 
                    return d.Method.Name;

                case IEnumerable<string> strings:
                    return strings.FirstOrDefault(FilledIn);

                case IEnumerable<FlowNode> flowNodes:
                    return TryGetName(flowNodes.Select(x => x?.Name));

                case IEnumerable<object> coll:
                    return TryGetName(coll.ToArray());

                default: 
                    throw new Exception($"Unsupported {nameof(nameSource)} type: {nameSource.GetType()}.");
            }
        }
        
        // ResolveFileExtension
        
        public static string ResolveFileExtension(
            string fileExtension, AudioFileFormatEnum audioFileFormat = default, 
            params object[] filePathSources)
        {
            if (Has(fileExtension))
            {
                return fileExtension;
            }
                        
            if (Has(audioFileFormat))
            {
                return audioFileFormat.FileExtension();
            }

            foreach (object filePathSource in filePathSources)
            {
                string value = TryGetName(filePathSource);
                value = SanitizeFilePath(value);
                value = GetExtension(value, ConfigWishes.Default.GetFileExtensionMaxLength);
                if (Has(value))
                {
                    return value;
                } 
            }
            
            var exceptionInfo = new
            {
                fileExtension, 
                audioFileFormat, 
                filePathSources, 
                filePathSource1 = filePathSources.ElementAtOrDefault(0),
                filePathSource2 = filePathSources.ElementAtOrDefault(1),
                filePathSource3 = filePathSources.ElementAtOrDefault(2)
            };

            throw new Exception($"Could not resolve file extension from {exceptionInfo}.");
        }
        
        // ResolveFilePath

        public static string ResolveFilePath(
            AudioFileFormatEnum audioFormat, 
            object filePathSource1 = null,
            object filePathSource2 = null,
            object filePathSource3 = null,
            object filePathSource4 = null,
            object filePathSource5 = null,
            object filePathSource6 = null,
            [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(default, audioFormat, default, filePathSource1, filePathSource2, filePathSource3, filePathSource4, filePathSource5, filePathSource6, callerMemberName);
        
        public static string ResolveFilePath(
            string fileExtension, 
            object filePathSource1 = null, 
            object filePathSource2 = null, 
            object filePathSource3 = null, 
            object filePathSource4 = null, 
            object filePathSource5 = null,
            object filePathSource6 = null,
            [CallerMemberName] string callerMemberName = null)
            => ResolveFilePath(fileExtension, default, default, filePathSource1, filePathSource2, filePathSource3, filePathSource4, filePathSource5, filePathSource6, callerMemberName);
        
        public static string ResolveFilePath(
            string fileExtension,
            AudioFileFormatEnum audioFormat,
            IList<int> ids,
            params object[] filePathSources)
        {
            string resolvedFileExtension = ResolveFileExtension(fileExtension, audioFormat, filePathSources);
            string resolvedName = ResolveName(filePathSources, explicitNameSource: filePathSources.ElementAtOrDefault(0));
            
            if (Has(ids))
            {
                string idDescriptor = IDDescriptor(ids);
                // Prevent duplicate mentions of the ID.
                if (!resolvedName.EndsWith("(" + idDescriptor + ")"))
                {
                    resolvedName += " " + idDescriptor;
                }
            }

            string resolvedFilePath = ReformatFilePath(resolvedName, resolvedFileExtension);
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
            
            // Find the full folder path
            string folderPath = GetDirectoryName(sanitizedFilePath);
            string absoluteFolder = IsNullOrWhiteSpace(folderPath) 
                ? Directory.GetCurrentDirectory() 
                : GetFullPath(folderPath);
            
            // Replace file extension
            string fileNameWithoutExtension = GetFileNameWithoutExtension(sanitizedFilePath, ConfigWishes.Default.GetFileExtensionMaxLength);
            string audioFormatExtension = newFileExtension;
            string fileName = fileNameWithoutExtension + audioFormatExtension;

            // Combine folder path and new file name
            return Combine(absoluteFolder, fileName);
        }

        // Helpers
        
        internal static bool NameIsOperatorTypeName(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return NameIsOperatorTypeName(op.Name, op.OperatorTypeName);
        }
        
        internal static bool NameIsOperatorTypeName(string name, string operatorTypeName)
        {
            if (IsNullOrWhiteSpace(name)) return false;

            if (string.Equals(name, operatorTypeName))
            {
                return true;
            }
            
            string operatorTypeDisplayName = PropertyDisplayNames.ResourceManager.GetString(operatorTypeName);
            if (string.Equals(name, operatorTypeDisplayName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        
        public static string PrettifyName(string uglyName)
        {
            string prettyName = uglyName;
            
            if (IsFile(prettyName, ConfigWishes.Default.GetFileExtensionMaxLength))
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
        
        public static string PrettyTitle(string uglyName)
        {
            string title = PrettifyName(uglyName);

            if (IsNullOrWhiteSpace(title))
            {
                title = "Untitled";
            }

            title = title.WithShortGuids(4);

            string dashes = "".PadRight(title.Length, '-');

            return title + NewLine + dashes;
        }
    
        /// <inheritdoc cref="docs._membername"/>
        public static string MemberName([CallerMemberName] string calledMemberName = null)
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() 
            => typeof(TType).Assembly.GetName().Name;
    }

    // NameWishes FlowNode
    
    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._names"/>
        public FlowNode SetName(object nameSource = null, object fallBackNameSource = null, [CallerMemberName] string callerMemberName = null)
        {
            Name = ResolveName(nameSource, fallBackNameSource, callerMemberName);
            return this;
        }

        /// <inheritdoc cref="docs._names"/>
        public string Name
        {
            get => !NameIsOperatorTypeName(_underlyingOutlet.Operator) ? _underlyingOutlet.Operator.Name : default;
            set => _underlyingOutlet.Operator.Name = value;
        }
    }

    // NameWishes Entity Extensions
    
    public static class NameWishesEntityExtensions
    {
        /// <inheritdoc cref="docs._names"/>
        public static Curve SetName(this Curve entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        // NameWishes Samples / AudioFileOutput

        /// <inheritdoc cref="docs._names"/>
        public static Sample SetName(this Sample entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        /// <inheritdoc cref="docs._names"/>
        public static AudioFileOutput SetName(this AudioFileOutput entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        // NameWishes Operators

        /// <inheritdoc cref="docs._names"/>
        public static Outlet SetName(this Outlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Operator.SetName(name);
            return entity;
        }

        /// <inheritdoc cref="docs._names"/>
        public static Operator SetName(this Operator op, string name)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            
            if (IsNullOrWhiteSpace(name)) return op;
            
            op.Name = name;

            if (op.AsCurveIn?.Curve != null)
            {
                op.AsCurveIn.Curve.Name = name;
            }

            if (op.AsSampleOperator?.Sample != null)
            {
                op.AsSampleOperator.Sample.Name = name;
            }

            return op;
        }
    }
}
