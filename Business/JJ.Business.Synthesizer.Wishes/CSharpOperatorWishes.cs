using JJ.Persistence.Synthesizer;
using System;
// ReSharper disable ExpressionIsAlwaysNull

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FluentOutlet
    {
        // C# Operators
        
        // Operator +
        
        public static FluentOutlet operator +(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
        
        public static FluentOutlet operator +(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
                
        public static FluentOutlet operator +(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        // Operator -
        
        public static FluentOutlet operator -(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '-',b);
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }

        // Operator *
        
        public static FluentOutlet operator *(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            b = b ?? x._[1];
            return x._[a].Times(b);
        }

        // Operator /
        
        public static FluentOutlet operator /(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
                
        public static FluentOutlet operator /(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x._[0];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
    
        // Helpers
                        
        private static SynthWishes GetSynthWishesOrThrow(object a, char op, object b)
        {
            if (a is FluentOutlet fluentA) return fluentA.x;
            if (b is FluentOutlet fluentB) return fluentB.x;
            throw new Exception(GetNoFluentOutletMessage(a, op, b));
        }

        private static string GetNoFluentOutletMessage(object a, char op, object b)
        {
            string aString = a == null ? "null" : $"{a}";
            string bString = b == null ? "null" : $"{b}";
            string opString = $"{aString} {op} {bString}";
            return $"Cannot evaluate ({opString}). " +
                   "A FluentOutlet operand is needed " +
                   "for creating new operators.";
        }
    }
}
