using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    // NameWishes Helper
    
    public static class NameHelper
    {
        /// <inheritdoc cref="docs._membername"/>
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

            title = title.WithShortGuids(4);

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

    // NameWishes SynthWishes
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._fetchname"/>
        public static string FetchName(
            string fallbackName1 = null, string fallbackName2 = null, string fallbackName3 = null, string fallbackName4 = null,
            string explicitName = null, [CallerMemberName] string callerMemberName = null)
        {
            if (!string.IsNullOrWhiteSpace(explicitName))
            {
                return explicitName; // Not sure if it should be prettified too...
            }

            string name = fallbackName1;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName2;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName3;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName4;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = callerMemberName;
            }

            name = PrettifyName(name);
            return name;
        }
    }

    // NameWishes FlowNode
    
    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._names"/>
        public FlowNode SetName(string name = null, string fallbackName = null, [CallerMemberName] string callerMemberName = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = callerMemberName;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = PrettifyName(name);
            }

            return this;
        }

        /// <inheritdoc cref="docs._names"/>
        public string Name
        {
            get => _underlyingOutlet.Operator.Name;
            set => _underlyingOutlet.Operator.Name = value;
        }
        
        public static string FetchName(
            string fallbackName1, string fallbackName2 = null, string fallbackName3 = null, string fallbackName4 = null,
            string explicitName = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.FetchName(fallbackName1, fallbackName2, fallbackName3, fallbackName4, explicitName, callerMemberName);
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
            
            if (string.IsNullOrWhiteSpace(name)) return op;
            
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
