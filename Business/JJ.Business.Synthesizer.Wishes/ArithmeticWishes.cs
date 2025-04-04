﻿using JJ.Persistence.Synthesizer;
using System;
// ReSharper disable ExpressionIsAlwaysNull

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FlowNode
    {
        // C# Operators
        
        // Operator +
        
        public static FlowNode operator +(FlowNode a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return a.Plus(b);
        }
        
        public static FlowNode operator +(FlowNode a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return a.Plus(x[b]);
        }

        public static FlowNode operator +(Outlet a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return x[a].Plus(b);
        }
                
        public static FlowNode operator +(FlowNode a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            a = a ?? x[0];
            return a.Plus(b);
        }

        public static FlowNode operator +(double a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '+', b);
            b = b ?? x[0];
            return x[a].Plus(b);
        }

        // Operator -
        
        public static FlowNode operator -(FlowNode a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return a.Minus(b);
        }
        
        public static FlowNode operator -(FlowNode a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return a.Minus(x[b]);
        }
        
        public static FlowNode operator -(Outlet a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x[0];
            b = b ?? x[0];
            return x[a].Minus(b);
        }
        
        public static FlowNode operator -(FlowNode a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '-', b);
            a = a ?? x[0];
            return a.Minus(b);
        }
        
        public static FlowNode operator -(double a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '-',b);
            b = b ?? x[0];
            return x[a].Minus(b);
        }

        // Operator *
        
        public static FlowNode operator *(FlowNode a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x[1];
            b = b ?? x[1];
            return a.Times(b);
        }
        
        public static FlowNode operator *(FlowNode a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x[1];
            b = b ?? x[1];
            return a.Times(x[b]);
        }
        
        public static FlowNode operator *(Outlet a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x[1];
            b = b ?? x[1];
            return x[a].Times(b);
        }
        
        public static FlowNode operator *(FlowNode a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            a = a ?? x[1];
            return a.Times(b);
        }
        
        public static FlowNode operator *(double a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '*', b);
            b = b ?? x[1];
            return x[a].Times(b);
        }

        // Operator /
        
        public static FlowNode operator /(FlowNode a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x[0];
            b = b ?? x[1];
            return a.Divide(b);
        }
        
        public static FlowNode operator /(FlowNode a, Outlet b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x[0];
            b = b ?? x[1];
            return a.Divide(x[b]);
        }
                
        public static FlowNode operator /(Outlet a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x[0];
            b = b ?? x[1];
            return x[a].Divide(b);
        }
        
        public static FlowNode operator /(FlowNode a, double b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            a = a ?? x[0];
            return a.Divide(b);
        }
        
        public static FlowNode operator /(double a, FlowNode b)
        {
            var x = GetSynthWishesOrThrow(a, '/', b);
            b = b ?? x[1];
            return x[a].Divide(b);
        }
    
        // Helpers
                        
        private static SynthWishes GetSynthWishesOrThrow(object a, char op, object b)
        {
            if (a is FlowNode fluentA) return fluentA._synthWishes;
            if (b is FlowNode fluentB) return fluentB._synthWishes;
            throw new Exception(GetNoFlowNodeMessage(a, op, b));
        }

        private static string GetNoFlowNodeMessage(object a, char op, object b)
        {
            string aString = a == null ? "null" : $"{a}";
            string bString = b == null ? "null" : $"{b}";
            string opString = $"{aString} {op} {bString}";
            return $"Cannot evaluate ({opString}). " +
                   "A FlowNode operand is needed " +
                   "for creating new operators.";
        }
    }
}
