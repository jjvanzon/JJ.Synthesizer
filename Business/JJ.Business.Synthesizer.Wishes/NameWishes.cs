using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    // NameWishes Helper
    
    public static class NameHelper
    {
        /// <inheritdoc cref="docs._fetchname"/>
        public static string FetchName(
            object nameSource1 = null, object nameSource2 = null, object nameSource3 = null, object nameSource4 = null,
            object nameSource5 = null, object nameSource6 = null, object nameSource7 = null, object nameSource8 = null,
            string explicitName = null, [CallerMemberName] string callerMemberName = null)
        {
            if (!IsNullOrWhiteSpace(explicitName))
            {
                return explicitName; // Not sure if it should be prettified too...
            }
            
            string name = TryGetName(nameSource1, nameSource2, nameSource3, nameSource4, nameSource5, nameSource6, nameSource7, nameSource8, callerMemberName);

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
                case string str: 
                    return str;
                
                case IEnumerable<string> strings: 
                    return strings.FirstOrDefault(x => !IsNullOrWhiteSpace(x));                    
                
                case FlowNode flowNode: 
                    return flowNode.Name;
                
                case IEnumerable<FlowNode> flowNodes: 
                    return TryGetName(flowNodes.Select(x => x?.Name));
                
                case Delegate d: 
                    return d.Method.Name;
                    
                case Buff buff:
                    return TryGetName(buff.FilePath, buff.UnderlyingAudioFileOutput);
                
                case AudioFileOutput audioFileOutput:
                    return TryGetName(audioFileOutput.FilePath, audioFileOutput.Name);
                    //return TryGetName(audioFileOutput.Name, audioFileOutput.FilePath);
                
                case Sample sample:
                    return TryGetName(sample.Location, sample.Name);
                    //return TryGetName(sample.Name, sample.Location);

                case null: 
                    return null;
                
                default: 
                    throw new Exception($"Unsupported {nameof(nameSource)} type: {nameSource.GetType()}.");
            }
        }
        
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
            
            if (IsFile(prettyName))
            {
                prettyName = Path.GetFileNameWithoutExtension(Path.GetFileName(uglyName));
            }
            
            prettyName = (prettyName ?? "").CutLeft("get_")
                               .CutLeft("set_")
                               .Replace("RunTest", "")
                               .Replace("Test", "")
                               .Replace("_", " ")
                               .RemoveExcessiveWhiteSpace();
            return prettyName;
        }

        public static string GetPrettyTitle(string uglyName)
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
        public FlowNode SetName(string name = null, string fallbackName = null, [CallerMemberName] string callerMemberName = null)
        {
            Name = FetchName(name, fallbackName, callerMemberName);
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
