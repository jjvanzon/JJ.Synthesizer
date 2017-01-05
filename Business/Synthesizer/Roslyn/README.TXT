2017-01-05

This folder will at one point become its own assembly, 
but that requires the different calculations to have exactly the same interfacing 
and that would require a ton of refactoring.
I want to see the calculations from SynthesizerPrototype up and running in the main project
as fast as possible.

When moving it to a separate .Roslyn assembly, the following references should also be removed:
- Microsoft.CodeAnalysis
- Microsoft.CodeAnalysis.CSharp
- System.Collections.Immutable
- System.Reflection.Metadata