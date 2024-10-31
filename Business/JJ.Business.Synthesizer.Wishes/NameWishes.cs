using System;
using System.Runtime.CompilerServices;
using System.Threading;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Environment;

// ReSharper disable NotResolvedInText

namespace JJ.Business.Synthesizer.Wishes
{
    // NameWishes Helper
    
    public static class NameHelper
    {
        /// <inheritdoc cref="_membername"/>
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

        public string Name
        {
            get => _name.Value;
            private set => _name.Value = value;
        }

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

            Name = name;
            return this;
        }

        /// <inheritdoc cref="_fetchname"/>
        public string FetchName(string fallbackName1 = null, string fallbackName2 = null, string explicitName = null, [CallerMemberName] string callerMemberName = null)
        {
            if (!string.IsNullOrWhiteSpace(explicitName))
            {
                // Not sure if it should be prettified too...
                return explicitName;
            }

            string name = Name;
            Name = null;

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
                name = callerMemberName;
            }

            name = NameHelper.PrettifyName(name);
            return name;
        }
    }

    // NameWishes FluentOutlet
    
    public partial class FluentOutlet
    { 
        public FluentOutlet WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return this;
            
            _thisOutlet.WithName(name);

            return this;
        }
    }

    // NameWishes Extensions
    
    public static class NameExtensionWishes
    {
        // NameWishes Curves

        public static Curve WithName(this Curve entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }
                
        public static CurveInWrapper WithName(this CurveInWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");
            
            wrapper.Operator().WithName(name);
            wrapper.Curve.WithName(name);

            return wrapper;
        }

        // NameWishes Samples / AudioFileOutput

        public static Sample WithName(this Sample entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        public static AudioFileOutput WithName(this AudioFileOutput entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        public static SampleOperatorWrapper WithName(this SampleOperatorWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");
            
            wrapper.Operator().WithName(name);
            wrapper.Sample.WithName(name);
            
            return wrapper;
        }

        // NameWishes Operators

        public static Outlet WithName(this Outlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Operator.WithName(name);
            return entity;
        }

        public static Operator WithName(this Operator op, string name)
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

        public static Inlet WithName(this Inlet entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Input.WithName(name);
            return entity;
        }
        
        public static OperatorWrapperBase WithName(this OperatorWrapperBase wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.WithName(name);
            return wrapper;
        }
    }
}
