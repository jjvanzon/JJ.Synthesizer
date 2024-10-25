using JJ.Persistence.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FluentOutlet
    {
        // C# Operators
        
        // Operator +
        
        public static FluentOutlet operator +(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
        
        public static FluentOutlet operator +(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }
                
        public static FluentOutlet operator +(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Plus(b);
        }

        public static FluentOutlet operator +(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[0];
            return x._[a].Plus(b);
        }

        // Operator -
        
        public static FluentOutlet operator -(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Minus(b);
        }
        
        public static FluentOutlet operator -(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[0];
            return x._[a].Minus(b);
        }

        // Operator *
        
        public static FluentOutlet operator *(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            b = b ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[1];
            return x._[a].Times(b);
        }
        
        public static FluentOutlet operator *(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[1];
            return x._[a].Times(b);
        }

        // Operator /
        
        public static FluentOutlet operator /(FluentOutlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
                
        public static FluentOutlet operator /(Outlet a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(FluentOutlet a, double b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            a = a ?? x._[0];
            return x._[a].Divide(b);
        }
        
        public static FluentOutlet operator /(double a, FluentOutlet b)
        {
            var x = GetSynthWishesOrThrow(a, b);
            b = b ?? x._[1];
            return x._[a].Divide(b);
        }
    
        // Helpers

        // ReSharper disable UnusedParameter.Local

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, FluentOutlet b) 
            => a?._synthWishes ?? b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, double b) 
            => a?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(double a, FluentOutlet b) 
            => b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(FluentOutlet a, Outlet b) 
            => a?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private static SynthWishes GetSynthWishesOrThrow(Outlet a, FluentOutlet b) 
            => b?._synthWishes ?? throw new Exception(noSynthWishesMessage);

        private const string noSynthWishesMessage = 
            "Cannot perform math operator when no Outlet is involved "+
            "to provide the Context in which to create more Outlets.";
    }
}
