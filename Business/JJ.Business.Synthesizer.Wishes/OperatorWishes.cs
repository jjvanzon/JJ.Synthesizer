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
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;
using static JJ.Business.Synthesizer.Wishes.LogHelper;

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

        public FluentOutlet Fluent(Outlet outlet) => _[outlet];
    }

    // OperatorWishes FluentOutlet

    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class FluentOutlet
    {
        private readonly SynthWishes _synthWishes;
        private readonly Outlet _wrappedOutlet;

        public FluentOutlet(SynthWishes synthWishes, Outlet firstOperand)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _wrappedOutlet = firstOperand ?? throw new ArgumentNullException(nameof(firstOperand));
            Operands = new FluentOperandList(this);
        }

        private string DebuggerDisplay => GetDebuggerDisplay(this);

        public static implicit operator Outlet(FluentOutlet fluentOutlet) => fluentOutlet?._wrappedOutlet;

        public Outlet WrappedOutlet => _wrappedOutlet;
    }

    // Value FluentOutlet

    public partial class FluentOutlet
    {
        public static explicit operator double(FluentOutlet fluentOutlet)
            => fluentOutlet.Value;

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
        public FluentOutlet Add(IList<FluentOutlet> terms)
        {
            if (terms == null) throw new ArgumentNullException(nameof(terms));

            // Flatten Nested Sums
            IList<FluentOutlet> flattenedTerms = FlattenTerms(terms);

            // Consts
            IList<FluentOutlet> vars = flattenedTerms.Where(x => x.IsVar).ToArray();
            IList<FluentOutlet> consts = flattenedTerms.Where(x => x.IsConst).ToArray();
            double constant = consts.Sum(x => x.Value);

            IList<FluentOutlet> optimizedTerms = vars.ToList();
            if (constant != 0) // Skip Identity 0
            {
                optimizedTerms.Add(_[constant]);
            }

            LogAdditionOptimizations(terms, flattenedTerms, optimizedTerms, consts, constant);

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
                    return _[_operatorFactory.Adder(optimizedTerms.Select(x => x.WrappedOutlet).ToArray())];
            }
        }

        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(params FluentOutlet[] operands) => Add((IList<FluentOutlet>)operands);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(double a, double b) => Add(_[a], _[b]);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(FluentOutlet a, double b) => Add(a, _[b]);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(double a, FluentOutlet b) => Add(_[a], b);

        /// <inheritdoc cref="docs._flattentermswithsumoradd"/>
        [UsedImplicitly]
        private IList<FluentOutlet> FlattenTerms(FluentOutlet sumOrAdd)
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

        private IList<FluentOutlet> FlattenTerms(params FluentOutlet[] operands)
            => FlattenTerms((IList<FluentOutlet>)operands);

        private IList<FluentOutlet> FlattenTerms(IList<FluentOutlet> operands)
        {
            return operands.SelectMany(x =>
            {
                if (x.IsAdder || x.IsAdd)
                {
                    return FlattenTerms(x.Operands);
                }
                else
                {
                    // Wrap the single operand in a list
                    return new List<FluentOutlet> { x };
                }
            }).ToList();
        }

        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(params FluentOutlet[] operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(IList<FluentOutlet> operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(double a, double b) => Add(a, b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(FluentOutlet a, double b) => Add(a, b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(double a, FluentOutlet b) => Add(a, b);
    }

    // Add FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(IList<FluentOutlet> operands) => _synthWishes.Add(new[] { this }.Concat(operands).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(params FluentOutlet[] operands) => _synthWishes.Add(new[] { this }.Concat(operands).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(FluentOutlet b) => _synthWishes.Add(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Add(double b) => _synthWishes.Add(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(params FluentOutlet[] operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(IList<FluentOutlet> operands) => Add(operands);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(FluentOutlet b) => Add(b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Plus(double b) => Add(b);
    }
    
    // Subtract SynthWishes
    
    public partial class SynthWishes
    {
        public FluentOutlet Subtract(FluentOutlet a, FluentOutlet b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            
            double? constA = a.AsConst;
            double? constB = b.AsConst;
            
            if (constA.HasValue & constB.HasValue) // Compute constant
            {
                var result = _[constA.Value - constB.Value];
                LogPreComputeConstant(a, "-", b, result);
                return result;
            }
            else if (constB.HasValue && constB.Value == 0) // a - 0 = a
            {
                LogIdentityOperation(a, "-", b);
                return a;
            }
            else
            {
                return _[_operatorFactory.Substract(a, b)];
            }
        }
        
        public FluentOutlet Subtract(FluentOutlet a, double b) => Subtract(a, _[b]);
        public FluentOutlet Subtract(double a, FluentOutlet b) => Subtract(_[a], b);
        public FluentOutlet Minus(FluentOutlet a, FluentOutlet b) => Subtract(a, b);
        public FluentOutlet Minus(FluentOutlet a, double b) => Subtract(a, b);
        public FluentOutlet Minus(double a, FluentOutlet b) => Subtract(a, b);
    }
    
    // Subtract FluentOutlet
    
    public partial class FluentOutlet
    {
        public FluentOutlet Subtract(FluentOutlet b) => _synthWishes.Subtract(this, b);
        public FluentOutlet Subtract(double b) => _synthWishes.Subtract(this, b);
        public FluentOutlet Minus(FluentOutlet b) => Subtract(b);
        public FluentOutlet Minus(double b) => Subtract(b);
    }

    // Multiply SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(FluentOutlet a, FluentOutlet b)
        {
            a = a ?? _[1];
            b = b ?? _[1];

            // Reverse operands increasing likelihood to have a 0-valued (volume) curve first.
            (a, b) = (b, a);

            // Flatten Nested Products
            var flattenedFactors = FlattenFactors(a, b);

            return Multiply(flattenedFactors);
        }

        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(IList<FluentOutlet> factors)
        {
            // Consts
            var vars = factors.Where(x => x.IsVar).ToArray();
            var consts = factors.Where(x => x.IsConst).ToArray();
            double constant = consts.Product(x => x.Value);

            var optimizedFactors = new List<FluentOutlet>(vars);
            if (constant != 1) // Skip Identity 1
            {
                optimizedFactors.Add(_[constant]);
            }
            
            LogMultiplicationOptimizations(factors, optimizedFactors, consts, constant);

            switch (optimizedFactors.Count)
            {
                case 0:
                    // Return identity 1
                    return _[1];

                case 1:
                    // Return single number
                    return _[optimizedFactors[0]];

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
        private FluentOutlet TryOptimizeMultiplicationByDistributionOverAddition(FluentOutlet h)
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
        public FluentOutlet Multiply(FluentOutlet a, double b) => Multiply(a, _[b]);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(double a, FluentOutlet b) => Multiply(_[a], b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(params FluentOutlet[] factors) => Multiply((IList<FluentOutlet>)factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(FluentOutlet a, FluentOutlet b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(FluentOutlet a, double b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(double a, FluentOutlet b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(IList<FluentOutlet> factors) => Multiply(factors);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(params FluentOutlet[] factors) => Multiply((IList<FluentOutlet>)factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Volume(FluentOutlet a, FluentOutlet b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Volume(FluentOutlet a, double b) => Multiply(a, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Volume(double a, FluentOutlet b) => Multiply(a, b);

        /// <inheritdoc cref="docs._flattenfactorswithmultiplyoutlet"/>
        [UsedImplicitly]
        private IList<FluentOutlet> FlattenFactors(FluentOutlet multiplyOutlet)
        {
            if (multiplyOutlet == null) throw new ArgumentNullException(nameof(multiplyOutlet));

            if (!multiplyOutlet.IsMultiply)
            {
                throw new Exception($"{nameof(multiplyOutlet)} parameter is not a Multiply operator.");
            }

            return FlattenFactors(multiplyOutlet.A, multiplyOutlet.B);
        }

        private IList<FluentOutlet> FlattenFactors(params FluentOutlet[] operands)
            => FlattenFactors((IList<FluentOutlet>)operands);

        private IList<FluentOutlet> FlattenFactors(IList<FluentOutlet> operands)
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
                    return new List<FluentOutlet> { x };
                }
            }).ToList();
        }

        private FluentOutlet NestMultiplications(IList<FluentOutlet> flattenedFactors)
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

    // Multiply FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(FluentOutlet b) => _synthWishes.Multiply(this, b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Multiply(double b) => _synthWishes.Multiply(this, b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Multiply(IList<FluentOutlet> factors) => _synthWishes.Multiply(new[] { this }.Concat(factors).ToArray());
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Multiply(params FluentOutlet[] factors) => _synthWishes.Multiply(new[] { this }.Concat(factors).ToArray());

        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(FluentOutlet b) => Multiply(b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Times(double b) => Multiply(b);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Times(IList<FluentOutlet> factors) => Multiply(factors);
        /// <inheritdoc cref="docs._add"/>
        public FluentOutlet Times(params FluentOutlet[] factors) => Multiply(factors);

        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Volume(FluentOutlet b) => Multiply(b);
        /// <inheritdoc cref="docs._multiply"/>
        public FluentOutlet Volume(double b) => Multiply(b);
    }

    // Divide SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Divide(FluentOutlet a, FluentOutlet b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            double? constA = a.AsConst;
            double? constB = b.AsConst;

            if (constA.HasValue && constB.HasValue)
            {
                // Compute constant
                var result = _[constA.Value / constB.Value];
                LogPreComputeConstant(a, "/", b, result);
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

            return _[_operatorFactory.Divide(a, b)];
        }

        public FluentOutlet Divide(FluentOutlet a, double b) => Divide(a, _[b]);
        public FluentOutlet Divide(double a, FluentOutlet b) => Divide(_[a], b);
    }

    // Divide FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Divide(FluentOutlet b) => _synthWishes.Divide(this, b);
        public FluentOutlet Divide(double b) => _synthWishes.Divide(this, b);
    }

    // Power SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Power(FluentOutlet @base, FluentOutlet exponent)
        {
            @base = @base ?? _[1];
            exponent = exponent ?? _[1];

            double? baseConst = @base.AsConst;
            double? exponentConst = exponent.AsConst;

            if (baseConst.HasValue && exponentConst.HasValue)
            {
                // Compute constant if both are constants
                var result = _[Math.Pow(@base.Value, exponent.Value)];
                LogPreComputeConstant(@base, "^", exponent, result);
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
            else
            {
                // Use operator factory only when no constant optimizations apply
                return _[_operatorFactory.Power(@base, exponent)];
            }
        }

        public FluentOutlet Power(FluentOutlet @base, double exponent) => Power(@base, _[exponent]);
        public FluentOutlet Power(double @base, FluentOutlet exponent) => Power(_[@base], exponent);
    }

    // Power FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Power(FluentOutlet exponent) => _synthWishes.Power(this, exponent);
        public FluentOutlet Power(double exponent) => _synthWishes.Power(this, exponent);
    }

    // Sine SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._sine" />
        public FluentOutlet Sine(FluentOutlet pitch = null) => _[_operatorFactory.Sine(_[1], pitch ?? _[1])];
        /// <inheritdoc cref="docs._sine" />
        public FluentOutlet Sine(double pitch) => Sine(_[pitch]);
    }

    // Sine FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._sine" />
        public FluentOutlet Sine => _synthWishes.Sine(this);
    }

    // Delay SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Delay(FluentOutlet signal, FluentOutlet delay)
        {
            signal = signal ?? _[0];
            delay = delay ?? _[0];

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
            else
            {
                return _[_operatorFactory.TimeAdd(signal, delay)];
            }
        }

        public FluentOutlet Delay(FluentOutlet signal, double delay) => Delay(signal, _[delay]);
    }

    // Delay FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Delay(FluentOutlet delay) => _synthWishes.Delay(this, delay);
        public FluentOutlet Delay(double delay) => _synthWishes.Delay(this, delay);
    }

    // Skip SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Skip(FluentOutlet signal, FluentOutlet skip)
        {
            signal = signal ?? _[0];
            skip = skip ?? _[0];

            if (signal.IsConst)
            {
                return signal;
            }
            else if (skip.AsConst == 0)
            {
                return signal;
            }
            else
            {
                return _[_operatorFactory.TimeSubstract(signal, skip)];
            }
        }

        public FluentOutlet Skip(FluentOutlet signal, double skip) => Skip(signal, _[skip]);
    }

    // Skip FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Skip(FluentOutlet skip) => _synthWishes.Skip(this, skip);
        public FluentOutlet Skip(double skip) => _synthWishes.Skip(this, skip);
    }

    // Stretch SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Stretch(FluentOutlet signal, FluentOutlet timeScale)
        {
            signal = signal ?? _[0];
            timeScale = timeScale ?? _[1];

            double? signalConst = signal.AsConst;
            double? timeScaleConst = timeScale.AsConst;

            if (signalConst.HasValue)
            {
                // If signal is constant, stretching time does nothing.
                return signal;
            }
            else if (timeScaleConst == 1)
            {
                // Return signal directly if multiplier is 1 (no change in timing)
                return signal;
            }
            // Outcommented, to have code coverage for TimeMultiply.
            //else if (timeScaleConst.HasValue)
            //{
            //    // SpeedUp slightly faster, because it does a * instead of a / internally.
            //    return SpeedUp(signal, _[1 / timeScaleConst.Value]);
            //}
            else
            {
                // Apply TimeMultiply only when the time scale actually modifies timing.
                return _[_operatorFactory.TimeMultiply(signal, timeScale)];
            }
        }

        public FluentOutlet Stretch(FluentOutlet signal, double timeScale) => Stretch(signal, _[timeScale]);
    }

    // Stretch FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Stretch(FluentOutlet timeScale) => _synthWishes.Stretch(this, timeScale);
        public FluentOutlet Stretch(double timeScale) => _synthWishes.Stretch(this, timeScale);
    }

    // SpeedUp SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet SpeedUp(FluentOutlet signal, FluentOutlet factor)
        {
            signal = signal ?? _[0];
            factor = factor ?? _[1];

            if (signal.IsConst)
            {
                // If signal is constant, stretching time does nothing.
                return signal;
            }
            else if (factor.AsConst == 1)
            {
                // Return signal directly if multiplier is 1 (no change in timing)
                return signal;
            }
            else
            {
                return _[_operatorFactory.TimeDivide(signal, factor)];
            }
        }

        public FluentOutlet SpeedUp(FluentOutlet signal, double factor) => SpeedUp(signal, _[factor]);
    }

    // SpeedUp FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet SpeedUp(FluentOutlet factor) => _synthWishes.SpeedUp(this, factor);
        public FluentOutlet SpeedUp(double factor) => _synthWishes.SpeedUp(this, factor);
    }

    // TimePower SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet TimePower(FluentOutlet signal, FluentOutlet exponent)
        {
            signal = signal ?? _[0];
            exponent = exponent ?? _[1];

            double? signalConst = signal.AsConst;
            double? exponentConst = exponent.AsConst;

            if (signalConst.HasValue)
            {
                // If time is constant, the power operation has no effect on timing
                return signal;
            }
            else if (exponentConst == 1)
            {
                // Identity case: time raised to the power of 1 keeps timing unchanged
                return signal;
            }
            else if (exponentConst == 0)
            {
                // When time is raised to the power of 0, timing is fixed at t=1
                return _[signal.Calculate(time: 1)];
            }
            else
            {
                // Apply TimePower when exponent meaningfully transforms timing
                return _[_operatorFactory.TimePower(signal, exponent)];
            }
        }

        public FluentOutlet TimePower(FluentOutlet signal, double exponent) => TimePower(signal, _[exponent]);
    }

    // TimePower FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet TimePower(FluentOutlet exponent) => _synthWishes.TimePower(this, exponent);
        public FluentOutlet TimePower(double exponent) => _synthWishes.TimePower(this, exponent);
    }
    // StrikeNote SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._default" />
        public FluentOutlet StrikeNote(FluentOutlet sound, FluentOutlet delay = default, FluentOutlet volume = default)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst != 0;
            bool volumeFilledIn = volume != null && volume.AsConst != 1;

            if (delayFilledIn) sound = Delay(sound, delay);
            if (volumeFilledIn) sound = Multiply(sound, volume);

            return sound.SetName();
        }

        /// <inheritdoc cref="docs._default" />
        public FluentOutlet StrikeNote(FluentOutlet sound, FluentOutlet delay, double volume) => StrikeNote(sound, delay, _[volume]);
        /// <inheritdoc cref="docs._default" />
        public FluentOutlet StrikeNote(FluentOutlet sound, double delay, FluentOutlet volume = default) => StrikeNote(sound, _[delay], volume);
        /// <inheritdoc cref="docs._default" />
        public FluentOutlet StrikeNote(FluentOutlet sound, double delay, double volume) => StrikeNote(sound, _[delay], _[volume]);
    }

    // StrikeNote FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet StrikeNote(FluentOutlet delay = null, FluentOutlet volume = default) => _synthWishes.StrikeNote(this, delay, volume);
        public FluentOutlet StrikeNote(FluentOutlet delay, double volume) => _synthWishes.StrikeNote(this, delay, volume);
        public FluentOutlet StrikeNote(double delay, FluentOutlet volume = default) => _synthWishes.StrikeNote(this, delay, volume);
        public FluentOutlet StrikeNote(double delay, double volume) => _synthWishes.StrikeNote(this, delay, volume);
    }

    // Tremolo SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet sound, (FluentOutlet speed, FluentOutlet depth) tremolo = default)
        {
            var speed = tremolo.speed ?? _[8];
            var depth = tremolo.depth ?? _[0.33];
            var modulated = sound * (1 + Sine(speed) * depth);
            return modulated.SetName();
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet sound, (FluentOutlet speed, double depth) tremolo)
        {
            var depth = tremolo.depth == default ? default : _[tremolo.depth];
            return Tremolo(sound, (tremolo.speed, depth));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet sound, (double speed, FluentOutlet depth) tremolo)
        {
            var speed = tremolo.speed == default ? default : _[tremolo.speed];
            return Tremolo(sound, (speed, tremolo.depth));
        }

        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet sound, (double speed, double depth) tremolo)
        {
            var speed = tremolo.speed == default ? default : _[tremolo.speed];
            var depth = tremolo.depth == default ? default : _[tremolo.depth];
            return Tremolo(sound, (speed, depth));
        }
    }

    // Tremolo FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet speed = default, FluentOutlet depth = default) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(double speed, FluentOutlet depth = default) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(FluentOutlet speed, double depth) => _synthWishes.Tremolo(this, (speed, depth));
        /// <inheritdoc cref="docs._tremolo" />
        public FluentOutlet Tremolo(double speed, double depth) => _synthWishes.Tremolo(this, (speed, depth));
    }

    // Vibrato SynthWishes 

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet freq, (FluentOutlet speed, FluentOutlet depth) vibrato = default)
        {
            var speed = vibrato.speed ?? _[5.5];
            var depth = vibrato.depth ?? _[0.0005];
            var modulated = freq * (1 + Sine(speed) * depth);
            return modulated.SetName();
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet freq, (FluentOutlet speed, double depth) vibrato)
        {
            var depth = vibrato.depth == default ? default : _[vibrato.depth];
            return VibratoOverPitch(freq, (vibrato.speed, depth));
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet freq, (double speed, FluentOutlet depth) vibrato)
        {
            var speed = vibrato.speed == default ? default : _[vibrato.speed];
            return VibratoOverPitch(freq, (speed, vibrato.depth));
        }

        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet freq, (double speed, double depth) vibrato)
        {
            var speed = vibrato.speed == default ? default : _[vibrato.speed];
            var depth = vibrato.depth == default ? default : _[vibrato.depth];
            return VibratoOverPitch(freq, (speed, depth));
        }
    }

    // Vibrato FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet speed = default, FluentOutlet depth = default) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(double speed, FluentOutlet depth = default) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(FluentOutlet speed, double depth) => _synthWishes.VibratoOverPitch(this, (speed, depth));
        /// <inheritdoc cref="docs._vibrato" />
        public FluentOutlet VibratoOverPitch(double speed, double depth) => _synthWishes.VibratoOverPitch(this, (speed, depth));
    }

    // Panning SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._panning" />
        public FluentOutlet Panning(FluentOutlet sound, FluentOutlet panning)
        {
            ChannelEnum channel = Channel;

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
                case ChannelEnum.Single: return _[sound];
                case ChannelEnum.Left: return Multiply(sound, Subtract(_[1], panning)).SetName();
                case ChannelEnum.Right: return Multiply(sound, panning).SetName();

                default: throw new ValueNotSupportedException(channel);
            }
        }

        /// <inheritdoc cref="docs._panning" />
        public FluentOutlet Panning(FluentOutlet sound, double panning)
        {
            ChannelEnum channel = Channel;

            if (panning < 0) panning = 0;
            if (panning > 1) panning = 1;

            switch (channel)
            {
                case ChannelEnum.Single: return _[sound];
                case ChannelEnum.Left: return (sound * _[1 - panning]).SetName();
                case ChannelEnum.Right: return (sound * _[panning]).SetName();

                default: throw new ValueNotSupportedException(channel);
            }
        }
    }

    // Panning Fluent FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._panning" />
        public FluentOutlet Panning(FluentOutlet panning) => _synthWishes.Panning(this, panning);
        /// <inheritdoc cref="docs._panning" />
        public FluentOutlet Panning(double panning) => _synthWishes.Panning(this, panning);
    }

    // Panbrello SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(FluentOutlet sound, (FluentOutlet speed, FluentOutlet depth) panbrello = default)
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
        public FluentOutlet Panbrello(
            FluentOutlet sound, (FluentOutlet speed, double depth) panbrello)
        {
            var depth = panbrello.depth == default ? default : _[panbrello.depth];
            return Panbrello(sound, (panbrello.speed, depth));
        }

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(
            FluentOutlet sound, (double speed, FluentOutlet depth) panbrello)
        {
            var speed = panbrello.speed == default ? default : _[panbrello.speed];
            return Panbrello(sound, (speed, panbrello.depth));
        }

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(
            FluentOutlet sound, (double speed, double depth) panbrello)
        {
            var speed = panbrello.speed == default ? default : _[panbrello.speed];
            var depth = panbrello.depth == default ? default : _[panbrello.depth];
            return Panbrello(sound, (speed, depth));
        }
    }

    // Panbrello Fluent FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(FluentOutlet speed = default, FluentOutlet depth = default)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(FluentOutlet speed, double depth)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(double speed, FluentOutlet depth)
            => _synthWishes.Panbrello(this, (speed, depth));

        /// <inheritdoc cref="docs._panbrello" />
        public FluentOutlet Panbrello(double speed, double depth)
            => _synthWishes.Panbrello(this, (speed, depth));
    }

    // PitchPan SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._pitchpan" />
        public FluentOutlet PitchPan(
            FluentOutlet actualFrequency, FluentOutlet centerFrequency,
            FluentOutlet referenceFrequency, FluentOutlet referencePanning)
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
        public FluentOutlet PitchPan(
            FluentOutlet actualFrequency, double centerFrequency,
            double referenceFrequency, double referencePanning)
            => PitchPan(actualFrequency, _[centerFrequency], _[referenceFrequency], _[referencePanning]);
    }

    // PitchPan FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._pitchpan" />
        public FluentOutlet PitchPan(FluentOutlet centerFrequency, FluentOutlet referenceFrequency, FluentOutlet referencePanning)
            => _synthWishes.PitchPan(this, centerFrequency, referenceFrequency, referencePanning);

        /// <inheritdoc cref="docs._pitchpan" />
        public FluentOutlet PitchPan(double centerFrequency, double referenceFrequency, double referencePanning)
            => _synthWishes.PitchPan(this, centerFrequency, referenceFrequency, referencePanning);
    }

    // Echo SynthWishes

    public partial class SynthWishes
    {
        public FluentOutlet Echo(FluentOutlet signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, _[magnitude], _[delay], callerMemberName);

        public FluentOutlet Echo(FluentOutlet signal, int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, magnitude, _[delay], callerMemberName);

        public FluentOutlet Echo(FluentOutlet signal, int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => Echo(signal, count, _[magnitude], delay, callerMemberName);

        public FluentOutlet Echo(
            FluentOutlet signal, int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default,
            [CallerMemberName] string callerMemberName = null)
            => EchoTape(signal, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, _[magnitude], _[delay], callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet signal, int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, magnitude, _[delay], callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet signal, int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => EchoAdditive(signal, count, _[magnitude], delay, callerMemberName);

        public FluentOutlet EchoAdditive(
            FluentOutlet signal, int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];

            IList<FluentOutlet> repeats = new List<FluentOutlet>(count);

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
        public FluentOutlet EchoFeedBack(FluentOutlet signal, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, _[magnitude], _[delay], callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(FluentOutlet signal, int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, magnitude, _[delay], callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(FluentOutlet signal, int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => EchoFeedBack(signal, count, _[magnitude], delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(
            FluentOutlet signal, int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default,
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

        public FluentOutlet EchoParallel(
            FluentOutlet signal, int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            // Fetch (user-chosen) name before anything else does.
            string name = FetchName(callerMemberName);

            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];

            var echoTasks = new Func<FluentOutlet>[count];

            for (int i = 0; i < count; i++)
            {
                var quieter = signal * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                echoTasks[i] = () => shifted;

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            string originalName = GetName;
            try
            {
                WithName(name);

                return ParallelAdd(echoTasks);
            }
            finally
            {
                WithName(originalName);
            }
        }

        public FluentOutlet EchoDuration(int count = 4, FluentOutlet delay = default)
        {
            delay = delay ?? _[0.25];
            var echoDuration = (count - 1) * delay;
            return echoDuration.SetName();
        }
    }

    // Echo FluentOutlet

    public partial class FluentOutlet
    {
        public FluentOutlet Echo(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet Echo(int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet Echo(int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet Echo(int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Echo(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoAdditive(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoAdditive(int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoAdditive(int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoAdditive(int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoAdditive(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(int count, double magnitude, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        /// <inheritdoc cref="docs._echofeedback"/>
        public FluentOutlet EchoFeedBack(int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoFeedBack(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet EchoDuration(int count = 4, FluentOutlet delay = default)
            => _synthWishes.EchoDuration(count, delay);

        public FluentOutlet EchoParallel(int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoParallel(this, count, magnitude, delay, callerMemberName);
    }

    internal static class LogHelper
    {
        public static void LogPreComputeConstant(FluentOutlet a, string mathSymbol, FluentOutlet b, FluentOutlet result, [CallerMemberName] string callerMemberName = null)
            => Console.WriteLine($"{PrettyTime()} Compute const : {Stringify(callerMemberName, a, mathSymbol, b)} = {Stringify(result)}");

        //public static void LogPreComputeConstants(IList<FluentOutlet> constants, string mathSymbol, double result) 
        //    => Console.WriteLine($"{PrettyTime()} Compute const : {Stringify(mathSymbol, constants)} = {result}");

        public static void LogIdentityOperation(FluentOutlet a, string mathSymbol, FluentOutlet identityValue, [CallerMemberName] string callerMemberName = null)
            => Console.WriteLine($"{PrettyTime()} Identity op : {Stringify(callerMemberName, a, mathSymbol, identityValue)} = {Stringify(a)}");
        
        public static void LogIdentityOperation(FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform, [CallerMemberName] string callerMemberName = null)
            => Console.WriteLine($"{PrettyTime()} Identity op ({dimension}) : {Stringify(callerMemberName, signal, dimension, mathSymbol, transform)} = {Stringify(signal)}");
        
        public static void LogAlwaysOneOptimization(FluentOutlet a, string mathSymbol, FluentOutlet b, [CallerMemberName] string callerMemberName = null)
            => Console.WriteLine($"{PrettyTime()} Always 1 : {Stringify(callerMemberName, a, mathSymbol, b)} = 1");
        
        public static void LogInvariance(FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform, [CallerMemberName] string callerMemberName = null)
            => Console.WriteLine($"{PrettyTime()} Invariance ({dimension}) : {Stringify(callerMemberName, signal, dimension, mathSymbol, transform)} = {Stringify(signal)}");

        public static void LogAdditionOptimizations(
            IList<FluentOutlet> terms, IList<FluentOutlet> flattenedTerms, IList<FluentOutlet> optimizedTerms,
            IList<FluentOutlet> consts, double constant, [CallerMemberName] string callerMemberName = null)
        {
            string opName = callerMemberName;
            string symbol = "+";
            
            bool wasFlattened = terms.Count != flattenedTerms.Count;
            if (wasFlattened)
            {
                Console.WriteLine($"{PrettyTime()} Flatten {symbol} : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, flattenedTerms)}");
            }

            bool hasConst0 = consts.Count >= 1 && constant == 0;
            if (hasConst0)
            {
                Console.WriteLine($"{PrettyTime()} Eliminate 0 : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }

            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine($"{PrettyTime()} Compute const : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool noTermsLeft = terms.Count != 0 && optimizedTerms.Count == 0;
            if (noTermsLeft)
            {
                Console.WriteLine($"{PrettyTime()} 0 terms remain : {Stringify(opName, symbol, terms)} => 0");
            }
            
            bool oneTermLeft = optimizedTerms.Count == 1;
            if (oneTermLeft)
            {
                Console.WriteLine($"{PrettyTime()} Eliminate {symbol} : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(symbol, optimizedTerms)}");
            }
        }
        
        public static void LogMultiplicationOptimizations(
            IList<FluentOutlet> factors, IList<FluentOutlet> optimizedFactors,
            IList<FluentOutlet> consts, double constant, [CallerMemberName] string callerMemberName = null)
        {
            string opName = callerMemberName;
            string symbol = "*";

            bool hasConst1 = consts.Count >= 1 && constant == 1;
            if (hasConst1)
            {
                Console.WriteLine($"{PrettyTime()} Eliminate 1 : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }

            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine($"{PrettyTime()} Compute const : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }

            bool noFactorsLeft = factors.Count != 0 && optimizedFactors.Count == 0;
            if (noFactorsLeft)
            {
                Console.WriteLine($"{PrettyTime()} 0 factors remain: {Stringify(opName, symbol, factors)} => 1");
            }
            
            bool oneFactorLeft = optimizedFactors.Count == 1;
            if (oneFactorLeft)
            {
                Console.WriteLine($"{PrettyTime()} Eliminate {symbol} : {Stringify(opName, symbol, optimizedFactors)} => {Stringify(symbol, optimizedFactors)}");
            }
        }

        public static void LogDivisionByMultiplication(FluentOutlet a, FluentOutlet b, FluentOutlet result)
            => Console.WriteLine($"{PrettyTime()} / by * : {Stringify(a)} / {Stringify(b)} = {Stringify(result)}");

        public static void LogDistributeMultiplyOverAddition(FluentOutlet formulaBefore, FluentOutlet formulaAfter)
            => Console.WriteLine($"{PrettyTime()} Distribute * over + : {Stringify(formulaBefore)} => {Stringify(formulaAfter)}");
        
        public static string Stringify(string opName, FluentOutlet a, string mathSymbol, FluentOutlet b)
            => Stringify(opName, mathSymbol, a, b);
        
        public static string Stringify(string opName, string mathSymbol, params FluentOutlet[] operands)
            => Stringify(opName, mathSymbol, (IList<FluentOutlet>)operands);
        
        public static string Stringify(string opName, string mathSymbol, IList<FluentOutlet> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        public static string Stringify(FluentOutlet a, string mathSymbol, FluentOutlet b)
            => Stringify(mathSymbol, a, b);
        
        public static string Stringify(string mathSymbol, params FluentOutlet[] operands)
            => Stringify(mathSymbol, (IList<FluentOutlet>)operands);
        
        public static string Stringify(string mathSymbol, IList<FluentOutlet> operands)
            => string.Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        public static string Stringify(FluentOutlet operand)
            => operand.Stringify(true);
        
        public static string Stringify(string opName, FluentOutlet signal, string dimension, string mathSymbol, FluentOutlet transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
    }
}