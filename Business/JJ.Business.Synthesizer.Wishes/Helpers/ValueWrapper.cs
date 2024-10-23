using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public class ValueWrapper : ValueOperatorWrapper
    {
        public ValueWrapper(Operator op) : base(op)
        { }

        public ValueWrapper(ValueOperatorWrapper valueOperatorWrapper)
            : base(valueOperatorWrapper.Operator)
        { }

        public static explicit operator double(ValueWrapper wrapper)
        {
            return wrapper.Value;
        }

        // Operator +

        public static double operator +(ValueWrapper a, ValueWrapper b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a.Value + b.Value;
        }
        
        public static double operator +(ValueWrapper a, double b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            return a.Value + b;
        }
        
        public static double operator +(double a, ValueWrapper b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a + b.Value;
        }

        // Operator -
        
        public static double operator -(ValueWrapper a, ValueWrapper b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a.Value - b.Value;
        }
        
        public static double operator -(ValueWrapper a, double b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            return a.Value - b;
        }
        
        public static double operator -(double a, ValueWrapper b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a - b.Value;
        }

        // Operator *
        
        public static double operator *(ValueWrapper a, ValueWrapper b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a.Value * b.Value;
        }
        
        public static double operator *(ValueWrapper a, double b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            return a.Value * b;
        }
        
        public static double operator *(double a, ValueWrapper b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a * b.Value;
        }

        // Operator /
        
        public static double operator /(ValueWrapper a, ValueWrapper b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a.Value / b.Value;
        }
        
        public static double operator /(ValueWrapper a, double b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            return a.Value / b;
        }
        
        public static double operator /(double a, ValueWrapper b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));
            return a / b.Value;
        }
    }
}
