using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Mathematics;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Helpers.LogHelper;

// ReSharper disable LocalVariableHidesMember
// ReSharper disable AssignmentInsteadOfDiscard
// ReSharper disable ParameterHidesMember
// ReSharper disable RedundantAssignment

namespace JJ.Business.Synthesizer.Wishes
{
    // OperatorWishes SynthWishes

    public partial class SynthWishes
    {
        private void InitializeOperatorWishes()
        {
            _ = new CaptureIndexer(this);
        }

        public FlowNode Fluent(Outlet outlet) => _[outlet];
        
        private bool MathAllowed(params FlowNode[] operands)
            => MathAllowed((IList<FlowNode>)operands);
        
        private bool MathAllowed(IList<FlowNode> operands)
        {
            if (!GetMathBoost) return false;
            
            foreach (var term in operands)
            {
                if (IsTape(term))
                {
                    return false;
                }
            }
            
            return true;
        }
    }

    // OperatorWishes FlowNode

    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class FlowNode
    {
        private readonly SynthWishes _synthWishes;
        private readonly Outlet _underlyingOutlet;

        public FlowNode(SynthWishes synthWishes, Outlet firstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _underlyingOutlet = firstOperand ?? throw new ArgumentNullException(nameof(firstOperand));
            Operands = new FluentOperandList(this);
        }

        private string DebuggerDisplay => GetDebuggerDisplay(this);

        public static implicit operator Outlet(FlowNode flowNode) => flowNode?._underlyingOutlet;

        public Outlet UnderlyingOutlet => _underlyingOutlet;

        public SynthWishes SynthWishes => _synthWishes;
    }

    // Value FlowNode

    public partial class FlowNode
    {
        public static explicit operator double(FlowNode flowNode)
            => flowNode.Value;

        public double Value
        {
            get
            {
                double? constant = AsConst;
                if (constant != null) return constant.Value;

                double calculated = this.Calculate(time: 0);
                return calculated;
            }
        }
    }

    // Add SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(IList<FlowNode> terms)
        {
            if (terms == null) throw new ArgumentNullException(nameof(terms));
            
            var optimizedTerms = terms;
            
            if (MathAllowed(terms))
            {
                // Flatten Nested Sums
                var flattenedTerms = FlattenTerms(terms);
                
                // Consts
                var vars = flattenedTerms.Where(x => x.IsVar).ToArray();
                var consts = flattenedTerms.Where(x => x.IsConst).ToArray();
                double constant = consts.Sum(x => x.Value);
                
                optimizedTerms = vars.ToList();
                if (constant != 0) // Skip Identity 0
                {
                    optimizedTerms.Add(_[constant]);
                }
                
                LogAdditionOptimizations(terms, flattenedTerms, optimizedTerms, consts, constant);
            }
            
            switch (optimizedTerms.Count)
            {
                case 0:
                    return _[0];

                case 1:
                    // Return single term
                    return optimizedTerms[0];

                case 2:
                    // Simple Add for 2 Operands
                    return _[_operatorFactory.Add(optimizedTerms[0], optimizedTerms[1])];

                default:
                    // Make Normal Adder
                    return _[_operatorFactory.Adder(optimizedTerms.Select(x => x.UnderlyingOutlet).ToArray())];
            }
        }
        
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(params FlowNode[] operands) => Add((IList<FlowNode>)operands);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(double a, double b) => Add(_[a], _[b]);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(FlowNode a, double b) => Add(a, _[b]);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(double a, FlowNode b) => Add(_[a], b);

        /// <inheritdoc cref="docs._flattentermswithsumoradd"/>
        [UsedImplicitly]
        private IList<FlowNode> FlattenTerms(FlowNode sumOrAdd)
        {
            if (sumOrAdd == null) throw new ArgumentNullException(nameof(sumOrAdd));

            if (sumOrAdd.IsAdd)
            {
                return FlattenTerms(sumOrAdd.A, sumOrAdd.B);
            }

            if (sumOrAdd.IsAdder)
            {
                return FlattenTerms(sumOrAdd.Operands);
            }

            throw new Exception("sumOrAdd is not a Sum / Adder or Add operator.");
        }

        private IList<FlowNode> FlattenTerms(params FlowNode[] operands)
            => FlattenTerms((IList<FlowNode>)operands);

        private IList<FlowNode> FlattenTerms(IList<FlowNode> operands)
        {
            return operands.SelectMany(x =>
            {
                if ((x.IsAdder || x.IsAdd))
                {
                    return FlattenTerms(x.Operands);
                }
                else
                {
                    // Wrap the single operand in a list
                    return new List<FlowNode> { x };
                }
            }).ToList();
        }

        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(params FlowNode[] operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(IList<FlowNode> operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(double a, double b) => Add(a, b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(FlowNode a, double b) => Add(a, b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(double a, FlowNode b) => Add(a, b);
    }

    // Add FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(IList<FlowNode> operands) => _synthWishes.Add(new[] { this }.Concat(operands).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(params FlowNode[] operands) => _synthWishes.Add(new[] { this }.Concat(operands).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(FlowNode b) => _synthWishes.Add(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Add(double b) => _synthWishes.Add(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(params FlowNode[] operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(IList<FlowNode> operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(FlowNode b) => Add(b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Plus(double b) => Add(b);
    }

    // Multiply SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(FlowNode a, FlowNode b)
        {
            a = a ?? _[1];
            b = b ?? _[1];

            if (!MathAllowed(a, b))
            {
                return _[_operatorFactory.Multiply(a, b)];
            }

            // Reverse operands increasing likelihood to have a 0-valued (volume) curve first.
            (a, b) = (b, a);

            // Flatten Nested Products
            var flattenedFactors = FlattenFactors(a, b);

            return Multiply(flattenedFactors);
        }

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(IList<FlowNode> factors)
        {
            var optimizedFactors = factors;
            
            if (MathAllowed(factors))
            {
                // Consts
                var vars = factors.Where(x => x.IsVar).ToArray();
                var consts = factors.Where(x => x.IsConst).ToArray();
                double constant = consts.Product(x => x.Value);
                
                optimizedFactors = new List<FlowNode>(vars);
                if (constant != 1) // Skip Identity 1
                {
                    optimizedFactors.Add(_[constant]);
                }
                
                LogMultiplicationOptimizations(factors, optimizedFactors, consts, constant);
            }
            
            switch (optimizedFactors.Count)
            {
                case 0:
                    // Return identity 1
                    return _[1];

                case 1:
                    // Return single number
                    return optimizedFactors[0];

                case 2:
                    // Simple Multiply for 2 Operands
                    var multiply = _[_operatorFactory.Multiply(optimizedFactors[0], optimizedFactors[1])];

                    // Fancy math optimization for stuff like:
                    // ((sin * 0.8) + 1) * 2000 => sin * (0.8 * 2000) + (1 * 2000)
                    multiply = TryOptimizeMultiplicationByDistributionOverAddition(multiply);

                    return multiply;

                default:
                    // Re-nest remaining factors
                    return _[NestMultiplications(optimizedFactors)];
            }
        }

        /// <param name="h">Is a multiplication operation.</param>
        private FlowNode TryOptimizeMultiplicationByDistributionOverAddition(FlowNode h)
        {
            // Example:
            // ((sin * 0.8) + 1) * 2000 => sin * (0.8 * 2000) + (1 * 2000)
            //
            // This kind of pattern occurs quite frequently, so it might be worth optimizing.
            // It'll eliminate the outer multiplication when we pre-compute the constants.
            //
            // More generally:
            //
            // ((x * a) + b) * c => x * (a * c) + (b * c)
            //
            // (Where a, b and c are constants.)
            //
            // If I can find the operations and the constants,
            // I can transform the expression.
            //
            // Source formula:
            // ((x * a) + b) * c
            //
            // With function names f, g, and h:
            // h[g[f[x * a] + b] * c]
            //
            // f is a multiplication
            // g is an addition
            // h is a multiplication again
            //
            // one of f's factors is variable.
            // the others are all constant.
            //
            // I have to analyse from outward to inward.

            // Ensure h is a multiplication.
            // h[g * c]
            if (h.IsMultiply)
            {
                // Grab the operands
                var g = h.A;
                var c = h.B;

                // And we want g to be an addition.
                // g[f + b] * c

                // Switch if switched
                if (g.IsConst && c.IsAdd) (g, c) = (c, g);

                // So ensure g is addition and c constant.
                if (g.IsAdd && c.IsConst)
                {
                    // Grab its operands
                    var f = g.A;
                    var b = g.B;

                    // Now we want f to be a multiplication again.
                    // f[x * a] + b

                    // Switch if switched
                    if (f.IsConst && b.IsMultiply) (f, b) = (b, f);

                    // Yeah, ensure f is multiply and b is const.
                    if (f.IsMultiply && b.IsConst)
                    {
                        // Grab its operands
                        var x = f.A;
                        var a = f.B;

                        // Switch if switched
                        if (x.IsConst) (x, a) = (a, x);

                        // A also has to be a constant
                        if (x.IsVar && a.IsConst)
                        {
                            // We found the structure!
                            // h[g[f[x * a] + b] * c]

                            // We now have all our elements resting in variables,
                            // so we can do our calculation:
                            // ((x * a) + b) * c => x * (a * c) + (b * c)

                            var l = x * (a.Value * c.Value) + (b.Value * c.Value);
                            LogDistributeMultiplyOverAddition(h, l);
                            return l;
                        }
                    }
                }
            }

            return h;
        }

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(FlowNode a, double b) => Multiply(a, _[b]);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(double a, FlowNode b) => Multiply(_[a], b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(params FlowNode[] factors) => Multiply((IList<FlowNode>)factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(FlowNode a, FlowNode b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(FlowNode a, double b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(double a, FlowNode b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(IList<FlowNode> factors) => Multiply(factors);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(params FlowNode[] factors) => Multiply((IList<FlowNode>)factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Volume(FlowNode a, FlowNode b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Volume(FlowNode a, double b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Volume(double a, FlowNode b) => Multiply(a, b);

        /// <inheritdoc cref="docs._flattenfactorswithmultiplyoutlet"/>
        [UsedImplicitly]
        private IList<FlowNode> FlattenFactors(FlowNode multiplyOutlet)
        {
            if (multiplyOutlet == null) throw new ArgumentNullException(nameof(multiplyOutlet));

            if (!multiplyOutlet.IsMultiply)
            {
                throw new Exception($"{nameof(multiplyOutlet)} parameter is not a Multiply operator.");
            }

            return FlattenFactors(multiplyOutlet.A, multiplyOutlet.B);
        }

        private IList<FlowNode> FlattenFactors(params FlowNode[] operands)
            => FlattenFactors((IList<FlowNode>)operands);

        private IList<FlowNode> FlattenFactors(IList<FlowNode> operands)
        {
            return operands.SelectMany(x =>
            {
                if (x.IsMultiply)
                {
                    return FlattenFactors(x.A, x.B);
                }
                else
                {
                    // Wrap the single operand in a list
                    return new List<FlowNode> { x };
                }
            }).ToList();
        }

        private FlowNode NestMultiplications(IList<FlowNode> flattenedFactors)
        {
            // Base case: If there's only one factor, return it
            // Also stops the recursion
            if (flattenedFactors.Count == 1)
            {
                return flattenedFactors[0];
            }

            // Recursive case: Nest the first factor with the result of nesting the rest
            var firstFactor = flattenedFactors[0];
            var remainingFactors = flattenedFactors.Skip(1).ToList();

            // Recursively nest the remaining factors and multiply with the first
            return _[_operatorFactory.Multiply(firstFactor, NestMultiplications(remainingFactors))];
        }
    }

    // Multiply FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(FlowNode b) => _synthWishes.Multiply(this, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Multiply(double b) => _synthWishes.Multiply(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Multiply(IList<FlowNode> factors) => _synthWishes.Multiply(new[] { this }.Concat(factors).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Multiply(params FlowNode[] factors) => _synthWishes.Multiply(new[] { this }.Concat(factors).ToArray());

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(FlowNode b) => Multiply(b);
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Times(double b) => Multiply(b);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Times(IList<FlowNode> factors) => Multiply(factors);
        /// <inheritdoc cref="docs._add"/>
        public FlowNode Times(params FlowNode[] factors) => Multiply(factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Volume(FlowNode b) => Multiply(b).SetName();
        /// <inheritdoc cref="docs._multiply"/>
        public FlowNode Volume(double b) => Multiply(b).SetName();
    }
    
    // Subtract SynthWishes
    
    public partial class SynthWishes
    {
        public FlowNode Subtract(FlowNode a, FlowNode b)
        {
            a = a ?? _[0];
            b = b ?? _[0];
            
            if (MathAllowed(a, b))
            {
                double? constA = a.AsConst;
                double? constB = b.AsConst;
                
                if (constA.HasValue & constB.HasValue) // Compute constant
                {
                    var result = _[constA.Value - constB.Value];
                    LogComputeConstant(a, "-", b, result);
                    return result;
                }
                else if (constB.HasValue && constB.Value == 0) // a - 0 = a
                {
                    LogIdentityOperation(a, "-", b);
                    return a;
                }
            }
            
            return _[_operatorFactory.Substract(a, b)];
        }
        
        public FlowNode Subtract(FlowNode a, double b) => Subtract(a, _[b]);
        public FlowNode Subtract(double a, FlowNode b) => Subtract(_[a], b);
        public FlowNode Minus(FlowNode a, FlowNode b) => Subtract(a, b);
        public FlowNode Minus(FlowNode a, double b) => Subtract(a, b);
        public FlowNode Minus(double a, FlowNode b) => Subtract(a, b);
    }
    
    // Subtract FlowNode
    
    public partial class FlowNode
    {
        public FlowNode Subtract(FlowNode b) => _synthWishes.Subtract(this, b);
        public FlowNode Subtract(double b) => _synthWishes.Subtract(this, b);
        public FlowNode Minus(FlowNode b) => Subtract(b);
        public FlowNode Minus(double b) => Subtract(b);
    }

    // Divide SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Divide(FlowNode a, FlowNode b)
        {
            a = a ?? _[0];
            b = b ?? _[1];
            
            if (MathAllowed(a, b))
            {
                double? constA = a.AsConst;
                double? constB = b.AsConst;
                
                if (constA.HasValue && constB.HasValue)
                {
                    // Compute constant
                    var result = _[constA.Value / constB.Value];
                    LogComputeConstant(a, "/", b, result);
                    return result;
                }
                else if (constB == 1) // a / 1 = a
                {
                    // Identity 1
                    LogIdentityOperation(a, "/", b);
                    return _[a];
                }
                else if (constB.HasValue) // a / 4 = a * 1/4 = faster
                {
                    // Replace division by multiplication
                    double fraction = 1 / constB.Value;
                    var result = Multiply(a, fraction);
                    LogDivisionByMultiplication(a, b, result);
                    return result;
                }
            }
            
            return _[_operatorFactory.Divide(a, b)];
        }

        public FlowNode Divide(FlowNode a, double b) => Divide(a, _[b]);
        public FlowNode Divide(double a, FlowNode b) => Divide(_[a], b);
    }

    // Divide FlowNode

    public partial class FlowNode
    {
        public FlowNode Divide(FlowNode b) => _synthWishes.Divide(this, b);
        public FlowNode Divide(double b) => _synthWishes.Divide(this, b);
    }

    // Power SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Power(FlowNode @base, FlowNode exponent)
        {
            @base = @base ?? _[1];
            exponent = exponent ?? _[1];
            
            if (MathAllowed(@base, exponent))
            {
                
                double? baseConst = @base.AsConst;
                double? exponentConst = exponent.AsConst;
                
                if (baseConst.HasValue && exponentConst.HasValue)
                {
                    // Compute constant if both are constants
                    var result = _[Math.Pow(@base.Value, exponent.Value)];
                    LogComputeConstant(@base, "^", exponent, result);
                    return result;
                }
                else if (baseConst == 1)
                {
                    // 1^x is always 1
                    var result = _[1];
                    LogAlwaysOneOptimization(@base, "^", exponent);
                    return result;
                }
                else if (exponentConst == 0)
                {
                    // x^0 is always 1
                    var result = _[1];
                    LogAlwaysOneOptimization(@base, "^", exponent);
                    return result;
                }
                else if (exponentConst == 1)
                {
                    // x^1 is x (identity)
                    LogIdentityOperation(@base, "^", exponent);
                    return @base;
                }
            }
            
            // Use operator factory only when no constant optimizations apply
            return _[_operatorFactory.Power(@base, exponent)];
        }
        
        public FlowNode Power(FlowNode @base, double exponent) => Power(@base, _[exponent]);
        public FlowNode Power(double @base, FlowNode exponent) => Power(_[@base], exponent);
    }

    // Power FlowNode

    public partial class FlowNode
    {
        public FlowNode Power(FlowNode exponent) => _synthWishes.Power(this, exponent);
        public FlowNode Power(double exponent) => _synthWishes.Power(this, exponent);
    }

    // Sine SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._sine" />
        public FlowNode Sine(FlowNode pitch = null) => _[_operatorFactory.Sine(_[1], pitch ?? _[1])];
        /// <inheritdoc cref="docs._sine" />
        public FlowNode Sine(double pitch) => Sine(_[pitch]);
    }

    // Sine FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._sine" />
        public FlowNode Sine => _synthWishes.Sine(this);
    }

    // Delay SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Delay(FlowNode signal, FlowNode delay)
        {
            signal = signal ?? _[0];
            delay = delay ?? _[0];

            if (MathAllowed(signal, delay))
            {
                if (signal.IsConst)
                {
                    LogInvariance(signal, "time", "+", delay);
                    return signal;
                }
                else if (delay.AsConst == 0)
                {
                    LogIdentityOperation(signal, "time", "+", delay);
                    return signal;
                }
            }
            
            return _[_operatorFactory.TimeAdd(signal, delay)];
        }

        public FlowNode Delay(FlowNode signal, double delay) => Delay(signal, _[delay]);
    }

    // Delay FlowNode

    public partial class FlowNode
    {
        public FlowNode Delay(FlowNode delay) => _synthWishes.Delay(this, delay);
        public FlowNode Delay(double delay) => _synthWishes.Delay(this, delay);
    }

    // Skip SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Skip(FlowNode signal, FlowNode skip)
        {
            signal = signal ?? _[0];
            skip = skip ?? _[0];

            if (MathAllowed(signal, skip))
            {
                if (signal.IsConst)
                {
                    LogInvariance(signal, "time", "-", skip);
                    return signal;
                }
                else if (skip.AsConst == 0)
                {
                    LogIdentityOperation(signal, "time", "-", skip);
                    return signal;
                }
            }
            
            return _[_operatorFactory.TimeSubstract(signal, skip)];
        }

        public FlowNode Skip(FlowNode signal, double skip) => Skip(signal, _[skip]);
    }

    // Skip FlowNode

    public partial class FlowNode
    {
        public FlowNode Skip(FlowNode skip) => _synthWishes.Skip(this, skip);
        public FlowNode Skip(double skip) => _synthWishes.Skip(this, skip);
    }

    // Stretch SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Stretch(FlowNode signal, FlowNode timeScale)
        {
            signal = signal ?? _[0];
            timeScale = timeScale ?? _[1];
            
            if (MathAllowed(signal, timeScale))
            {
                double? signalConst = signal.AsConst;
                double? timeScaleConst = timeScale.AsConst;
                
                if (signalConst.HasValue)
                {
                    // If signal is constant, stretching time does nothing.
                    LogInvariance(signal, "time", "*", timeScale);
                    return signal;
                }
                else if (timeScaleConst == 1)
                {
                    // Return signal directly if multiplier is 1 (no change in timing)
                    LogIdentityOperation(signal, "time", "*", timeScale);
                    return signal;
                }
                // Outcommented, to have code coverage for TimeMultiply.
                //else if (timeScaleConst.HasValue)
                //{
                //    // SpeedUp slightly faster, because it does a * instead of a / internally.
                //    return SpeedUp(signal, _[1 / timeScaleConst.Value]);
                //}
            }
            
            // Apply TimeMultiply only when the time scale actually modifies timing.
            return _[_operatorFactory.TimeMultiply(signal, timeScale)];
        }
        
        public FlowNode Stretch(FlowNode signal, double timeScale) => Stretch(signal, _[timeScale]);
    }

    // Stretch FlowNode

    public partial class FlowNode
    {
        public FlowNode Stretch(FlowNode timeScale) => _synthWishes.Stretch(this, timeScale);
        public FlowNode Stretch(double timeScale) => _synthWishes.Stretch(this, timeScale);
    }

    // SpeedUp SynthWishes

    public partial class SynthWishes
    {
        public FlowNode SpeedUp(FlowNode signal, FlowNode factor)
        {
            signal = signal ?? _[0];
            factor = factor ?? _[1];

            if (MathAllowed(signal, factor))
            {
                if (signal.IsConst)
                {
                    // If signal is constant, stretching time does nothing.
                    LogInvariance(signal, "time", "/", factor);
                    return signal;
                }
                else if (factor.AsConst == 1)
                {
                    // Return signal directly if multiplier is 1 (no change in timing)
                    LogIdentityOperation(signal, "time", "/", factor);
                    return signal;
                }
            }
            
            return _[_operatorFactory.TimeDivide(signal, factor)];
        }

        public FlowNode SpeedUp(FlowNode signal, double factor) => SpeedUp(signal, _[factor]);
    }

    // SpeedUp FlowNode

    public partial class FlowNode
    {
        public FlowNode SpeedUp(FlowNode factor) => _synthWishes.SpeedUp(this, factor);
        public FlowNode SpeedUp(double factor) => _synthWishes.SpeedUp(this, factor);
    }

    // TimePower SynthWishes

    public partial class SynthWishes
    {
        public FlowNode TimePower(FlowNode signal, FlowNode exponent)
        {
            signal = signal ?? _[0];
            exponent = exponent ?? _[1];

            double? signalConst = signal.AsConst;
            double? exponentConst = exponent.AsConst;
            
            if (MathAllowed(signal, exponent))
            {
                if (signalConst.HasValue)
                {
                    // If time is constant, the power operation has no effect on timing
                    LogInvariance(signal, "time", "^", exponent);
                    return signal;
                }
                else if (exponentConst == 1)
                {
                    // Identity case: time raised to the power of 1 keeps timing unchanged
                    LogIdentityOperation(signal, "time", "^", exponent);
                    return signal;
                }
                else if (exponentConst == 0)
                {
                    // When time is raised to the power of 0, timing is fixed at t=1
                    LogAlwaysOneOptimization(signal, "time", "^", exponent);
                    return _[signal.Calculate(time: 1)];
                }
            }
            
            // Apply TimePower when exponent meaningfully transforms timing
            return _[_operatorFactory.TimePower(signal, exponent)];
        }

        public FlowNode TimePower(FlowNode signal, double exponent) => TimePower(signal, _[exponent]);
    }

    // TimePower FlowNode

    public partial class FlowNode
    {
        public FlowNode TimePower(FlowNode exponent) => _synthWishes.TimePower(this, exponent);
        public FlowNode TimePower(double exponent) => _synthWishes.TimePower(this, exponent);
    }
    
    // StrikeNote SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._default" />
        public FlowNode StrikeNote(FlowNode sound, FlowNode delay = default, FlowNode volume = default)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst != 0;
            bool volumeFilledIn = volume != null && volume.AsConst != 1;

            if (delayFilledIn) sound = Delay(sound, delay);
            if (volumeFilledIn) sound = Multiply(sound, volume);

            return sound.SetName();
        }

        /// <inheritdoc cref="docs._default" />
        public FlowNode StrikeNote(FlowNode sound, FlowNode delay, double volume) => StrikeNote(sound, delay, _[volume]);
        /// <inheritdoc cref="docs._default" />
        public FlowNode StrikeNote(FlowNode sound, double delay, FlowNode volume = default) => StrikeNote(sound, _[delay], volume);
        /// <inheritdoc cref="docs._default" />
        public FlowNode StrikeNote(FlowNode sound, double delay, double volume) => StrikeNote(sound, _[delay], _[volume]);
    }

    // StrikeNote FlowNode

    public partial class FlowNode
    {
        public FlowNode StrikeNote(FlowNode delay = null, FlowNode volume = default) => _synthWishes.StrikeNote(this, delay, volume);
        public FlowNode StrikeNote(FlowNode delay, double volume) => _synthWishes.StrikeNote(this, delay, volume);
        public FlowNode StrikeNote(double delay, FlowNode volume = default) => _synthWishes.StrikeNote(this, delay, volume);
        public FlowNode StrikeNote(double delay, double volume) => _synthWishes.StrikeNote(this, delay, volume);
    }

    // Tremolo SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode sound, (FlowNode speed, FlowNode depth) tremolo = default)
        {
            var speed = tremolo.speed ?? _[8];
            var depth = tremolo.depth ?? _[0.33];
            var modulated = sound * (1 + Sine(speed) * depth);
            return modulated.SetName();
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode sound, (FlowNode speed, double depth) tremolo)
        {
            var depth = tremolo.depth == default ? default : _[tremolo.depth];
            return Tremolo(sound, (tremolo.speed, depth));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode sound, (double speed, FlowNode depth) tremolo)
        {
            var speed = tremolo.speed == default ? default : _[tremolo.speed];
            return Tremolo(sound, (speed, tremolo.depth));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode sound, (double speed, double depth) tremolo)
        {
            var speed = tremolo.speed == default ? default : _[tremolo.speed];
            var depth = tremolo.depth == default ? default : _[tremolo.depth];
            return Tremolo(sound, (speed, depth));
        }
    }

    // Tremolo FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode speed = default, FlowNode depth = default) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(double speed, FlowNode depth = default) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(FlowNode speed, double depth) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FlowNode Tremolo(double speed, double depth) => _synthWishes.Tremolo(this, (speed, depth));
    }

    // Vibrato SynthWishes 

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoOverPitch(FlowNode freq, (FlowNode speed, FlowNode depth) vibrato = default)
        {
            var speed = vibrato.speed ?? _[5.5];
            var depth = vibrato.depth ?? _[0.0005];
            var modulated = freq * (1 + Sine(speed) * depth);
            return modulated.SetName();
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoOverPitch(FlowNode freq, (FlowNode speed, double depth) vibrato)
        {
            var depth = vibrato.depth == default ? default : _[vibrato.depth];
            return VibratoOverPitch(freq, (vibrato.speed, depth));
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoOverPitch(FlowNode freq, (double speed, FlowNode depth) vibrato)
        {
            var speed = vibrato.speed == default ? default : _[vibrato.speed];
            return VibratoOverPitch(freq, (speed, vibrato.depth));
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoOverPitch(FlowNode freq, (double speed, double depth) vibrato)
        {
            var speed = vibrato.speed == default ? default : _[vibrato.speed];
            var depth = vibrato.depth == default ? default : _[vibrato.depth];
            return VibratoOverPitch(freq, (speed, depth));
        }
    }

    // Vibrato FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoFreq(FlowNode speed = default, FlowNode depth = default) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoFreq(double speed, FlowNode depth = default) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoFreq(FlowNode speed, double depth) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FlowNode VibratoFreq(double speed, double depth) => _synthWishes.VibratoOverPitch(this, (speed, depth));
    }

    // Panning SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._panning" />
        public FlowNode Panning(FlowNode sound, FlowNode panning)
        {
            ChannelEnum channel = GetChannel;

            // Some optimization in case of a constant value
            {
                double? constPanning = panning.AsConst;
                if (constPanning != null)
                {
                    return Panning(sound, constPanning.Value);
                }
            }

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left: return Multiply(sound, Subtract(_[1], panning)).SetName();
                case ChannelEnum.Right: return Multiply(sound, panning).SetName();

                default: throw new ValueNotSupportedException(channel);
            }
        }

        /// <inheritdoc cref="docs._panning" />
        public FlowNode Panning(FlowNode sound, double panning)
        {
            ChannelEnum channel = GetChannel;

            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case ChannelEnum.Single: return sound;
                case ChannelEnum.Left: return (sound * _[1 - panning]).SetName();
                case ChannelEnum.Right: return (sound * _[panning]).SetName();

                default: throw new ValueNotSupportedException(channel);
            }
        }
    }

    // Panning Fluent FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._panning" />
        public FlowNode Panning(FlowNode panning) => _synthWishes.Panning(this, panning);
        /// <inheritdoc cref="docs._panning" />
        public FlowNode Panning(double panning) => _synthWishes.Panning(this, panning);
    }

    // Panbrello SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(FlowNode sound, (FlowNode speed, FlowNode depth) panbrello = default)
        {
            panbrello.speed = panbrello.speed ?? _[1];
            panbrello.depth = panbrello.depth ?? _[1];

            // 0.5 is in the middle. 0 is left, 1 is right.
            var sine = Sine(panbrello.speed) * panbrello.depth; // [-1,+1]
            var halfSine = 0.5 * sine; // [-0.5,+0.5]
            var zeroToOne = 0.5 + halfSine; // [0,1]

            return Panning(sound, zeroToOne).SetName();
        }

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(
            FlowNode sound, (FlowNode speed, double depth) panbrello)
        {
            var depth = panbrello.depth == default ? default : _[panbrello.depth];
            return Panbrello(sound, (panbrello.speed, depth));
        }

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(
            FlowNode sound, (double speed, FlowNode depth) panbrello)
        {
            var speed = panbrello.speed == default ? default : _[panbrello.speed];
            return Panbrello(sound, (speed, panbrello.depth));
        }

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(
            FlowNode sound, (double speed, double depth) panbrello)
        {
            var speed = panbrello.speed == default ? default : _[panbrello.speed];
            var depth = panbrello.depth == default ? default : _[panbrello.depth];
            return Panbrello(sound, (speed, depth));
        }
    }

    // Panbrello Fluent FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(FlowNode speed = default, FlowNode depth = default)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(FlowNode speed, double depth)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(double speed, FlowNode depth)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FlowNode Panbrello(double speed, double depth)
            => _synthWishes.Panbrello(this, (speed, depth));
    }

    // PitchPan SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._pitchpan" />
        public FlowNode PitchPan(
            FlowNode actualFrequency, FlowNode centerFrequency,
            FlowNode referenceFrequency, FlowNode referencePanning)
        {
            // Some optimization in case of constants, because things are currently so slow.
            {
                double? constActualFrequency = actualFrequency?.AsConst;
                double? constCenterFrequency = centerFrequency?.AsConst;
                double? constReferenceFrequency = referenceFrequency?.AsConst;
                double? constReferencePanning = referencePanning?.AsConst;

                if (constActualFrequency != null &&
                    constCenterFrequency != null &&
                    constReferenceFrequency != null &&
                    constReferencePanning != null)
                {
                    double pitchPan = PitchPan(constActualFrequency.Value, constCenterFrequency.Value,
                                               constReferenceFrequency.Value, constReferencePanning.Value);
                    return _[pitchPan].SetName();
                }
            }

            // Defaults
            centerFrequency = centerFrequency ?? A4;
            referenceFrequency = referenceFrequency ?? E4;
            referencePanning = referencePanning ?? _[0.6];

            var centerPanning = _[0.5];

            // Calculate intervals relative to the center frequency
            var referenceInterval = Divide(referenceFrequency, centerFrequency);
            var actualInterval = Divide(actualFrequency, centerFrequency);

            var factor = actualInterval * referenceInterval;

            // Calculate panning deviation
            //var newPanningDeviation = Multiply(x, x.Substract(referencePanning, centerPanning), factor);
            // AI's correction:
            var newPanningDeviation = (referencePanning - centerPanning) * (factor - 1);
            var newPanning = centerPanning + newPanningDeviation;

            return newPanning.SetName();
        }

        /// <inheritdoc cref="docs._pitchpan" />
        public double PitchPan(
            double actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
        {
            // Defaults
            if (centerFrequency == default) centerFrequency = Notes.A4;
            if (referenceFrequency == default) referenceFrequency = Notes.E4;
            if (referencePanning == default) referencePanning = 0.6;

            double centerPanning = 0.5;

            // Calculate intervals relative to the center frequency
            double referenceInterval = referenceFrequency / centerFrequency;
            double actualInterval = actualFrequency / centerFrequency;

            double factor = actualInterval * referenceInterval;

            // Calculate panning deviation
            //double newPanningDeviation = (referencePanning - centerPanning) * factor;
            // AI's correction:
            double newPanningDeviation = (referencePanning - centerPanning) * (factor - 1);
            double newPanning = centerPanning + newPanningDeviation;

            return newPanning;
        }

        /// <inheritdoc cref="docs._pitchpan" />
        public FlowNode PitchPan(
            FlowNode actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
            => PitchPan(actualFrequency, _[centerFrequency], _[referenceFrequency], _[referencePanning]);
    }

    // PitchPan FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._pitchpan" />
        public FlowNode PitchPan(FlowNode centerFrequency, FlowNode referenceFrequency, FlowNode referencePanning)
            => _synthWishes.PitchPan(this, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public FlowNode PitchPan(double centerFrequency, double referenceFrequency, double referencePanning)
            => _synthWishes.PitchPan(this, centerFrequency, referenceFrequency, referencePanning);
    }

    // Echo SynthWishes

    public partial class SynthWishes
    {
        public FlowNode Echo(FlowNode signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, _[magnitude], _[delay], callerMemberName);

        public FlowNode Echo(FlowNode signal, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, magnitude, _[delay], callerMemberName);

        public FlowNode Echo(FlowNode signal, int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, _[magnitude], delay, callerMemberName);

        public FlowNode Echo(
            FlowNode signal, int count, FlowNode magnitude = default, FlowNode delay = default,
            [CallerMemberName] string callerMemberName = null)
            => EchoTape(signal, count, magnitude, delay);

        public FlowNode EchoAdditive(FlowNode signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, _[magnitude], _[delay], callerMemberName);

        public FlowNode EchoAdditive(FlowNode signal, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, magnitude, _[delay], callerMemberName);

        public FlowNode EchoAdditive(FlowNode signal, int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, _[magnitude], delay, callerMemberName);

        public FlowNode EchoAdditive(
            FlowNode signal, int count = 4, FlowNode magnitude = default, FlowNode delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];

            IList<FlowNode> repeats = new List<FlowNode>(count);

            for (int i = 0; i < count; i++)
            {
                var quieter = signal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                repeats.Add(shifted);

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            return Add(repeats).SetName(callerMemberName);
        }

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(FlowNode signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, _[magnitude], _[delay], callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(FlowNode signal, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, magnitude, _[delay], callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(FlowNode signal, int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, _[magnitude], delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(
            FlowNode signal, int count = 4, FlowNode magnitude = default, FlowNode delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var cumulativeSignal = signal;
            var cumulativeMagnitude = magnitude;
            var cumulativeDelay = delay;

            int loopCount = Maths.Log(count, 2);

            for (int i = 0; i < loopCount; i++)
            {
                var quieter = cumulativeSignal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                cumulativeSignal += shifted;

                cumulativeMagnitude *= cumulativeMagnitude;
                cumulativeDelay += cumulativeDelay;
            }

            return cumulativeSignal.SetName(callerMemberName);
        }

        public FlowNode EchoParallel(
            FlowNode signal, int count = 4, FlowNode magnitude = default, FlowNode delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];

            var repeats = new FlowNode[count];

            for (int i = 0; i < count; i++)
            {
                var quieter = signal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);
                var taped = shifted.Tape();
                
                repeats[i] = taped;

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            return Add(repeats);
        }
        
        public FlowNode EchoTape(
            FlowNode signal, int count = 4, FlowNode magnitude = default, FlowNode delay = default)
        {
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];
            
            var tape = Tape(signal);
            
            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];
            
            var repeats = new List<FlowNode>(count);
            
            for (int i = 0; i < count; i++)
            {
                var quieter = tape * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);
                
                repeats.Add(shifted);
                
                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }
            
            return Add(repeats).SetName();
        }

        public FlowNode EchoDuration(int count = 4, FlowNode delay = default)
        {
            delay = delay ?? _[0.25];
            var echoDuration = (count - 1) * delay;
            return echoDuration.SetName();
        }
        
        public SynthWishes AddEchoDuration(int count = 4, FlowNode delay = default) 
            => AddAudioLength(EchoDuration(count, delay));
    }

    // Echo FlowNode

    public partial class FlowNode
    {
        public FlowNode Echo(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FlowNode Echo(int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FlowNode Echo(int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FlowNode Echo(int count, FlowNode magnitude = default, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FlowNode EchoAdditive(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FlowNode EchoAdditive(int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FlowNode EchoAdditive(int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FlowNode EchoAdditive(int count = 4, FlowNode magnitude = default, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(int count, double magnitude, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FlowNode EchoFeedBack(int count = 4, FlowNode magnitude = default, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        public FlowNode EchoParallel(int count = 4, FlowNode magnitude = default, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoParallel(this, count, magnitude, delay, callerMemberName);
        
        public FlowNode EchoTape(int count = 4, FlowNode magnitude = default, FlowNode delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoTape(this, count, magnitude, delay);
        
        public FlowNode EchoDuration(int count = 4, FlowNode delay = default)
            => _synthWishes.EchoDuration(count, delay);
        
        public FlowNode AddEchoDuration(int count = 4, FlowNode delay = default)
        {
            _synthWishes.AddEchoDuration(count, delay);
            return this;
        }
    }
}