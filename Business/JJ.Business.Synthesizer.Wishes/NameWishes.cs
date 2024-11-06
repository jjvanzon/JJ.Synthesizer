using System;
using System.Runtime.CompilerServices;
using System.Threading;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;

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
        private readonly ThreadLocal<string> _name = new ThreadLocal<string>();

        /// <inheritdoc cref="docs._names"/>
        public string GetName => _name.Value;

        /// <inheritdoc cref="docs._names"/>
        public SynthWishes WithName(string uglyName = null, string fallbackName = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = uglyName;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = callerMemberName;
            }

            _name.Value = name;
            return this;
        }

        /// <inheritdoc cref="docs._fetchname"/>
        public string FetchName(
            string fallbackName1 = null, string fallbackName2 = null, 
            string fallbackName3 = null, string explicitName = null,
            [CallerMemberName] string callerMemberName = null)
        {
            if (!string.IsNullOrWhiteSpace(explicitName))
            {
                // Not sure if it should be prettified too...
                return explicitName;
            }

            string name = GetName;
            _name.Value = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = fallbackName1;
            }

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
                name = callerMemberName;
            }

            name = NameHelper.PrettifyName(name);
            return name;
        }
    }

    // NameWishes FluentOutlet
    
    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._names"/>
        public FluentOutlet SetName([CallerMemberName] string name = null)
        {
            if (string.IsNullOrWhiteSpace(name)) return this;
            
            _wrappedOutlet.SetName(name);

            return this;
        }
    
        /// <inheritdoc cref="docs._names"/>
        public FluentOutlet WithName(string uglyName = null, string fallbackName = null, [CallerMemberName] string callerMemberName = null)
        {
            _synthWishes.WithName(uglyName, fallbackName, callerMemberName);
            return this;
        }

        public string Name => _wrappedOutlet.Operator.Name;

        public FluentOutlet AppendName(string nameSuffix = null, [CallerMemberName] string callerMemberName = null)
        {
            string suffix = nameSuffix;
            if (string.IsNullOrWhiteSpace(suffix))
            {
                suffix = callerMemberName;
            }
            SetName($"{Name}{suffix}");

            return this;
        }
    }

    // NameWishes Extensions
    
    public static class NameExtensionWishes
    {
        /// <inheritdoc cref="docs._names"/>
        public static string Name(this FluentOutlet fluentOutlet)
        {
            if (fluentOutlet == null) throw new ArgumentNullException(nameof(fluentOutlet));
            return fluentOutlet.Name;
        }

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
