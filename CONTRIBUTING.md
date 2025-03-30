Contributing to JJ.Synthesizer
==============================


🏛 Reference Architecture
--------------------------

The patterns and practices in this project closely match the following guidelines for structuring software: [JJ's Software Architecture](https://github.com/jjvanzon/JJs-Reference-Architecture)


⚙ Dev Env
-----------

### 🖥 Visual Studio

This project was developed using C# in Visual Studio. You can download that from Microsoft.

### 🕘 Old Commits

Having gone through many migration scenarios, older commits may have some quirks to them as you try to check those out, in particular:

### ⛓️ Broken References

__Option 1: NuGet Packages (Recommended)__

Reference the NuGet version of these dependencies.

__Option 2: Clone [JJ.Framework](https://github.com/jjvanzon/JJ.Framework) & [JJ.Canonical](https://github.com/jjvanzon/JJ.Canonical) repos__

Place their folders alongside the `JJ.Synthesizer` folder.


__Option 3: Add [JJ.Framework](https://github.com/jjvanzon/JJ.Framework) & [JJ.Canonical](https://github.com/jjvanzon/JJ.Canonical) sub-modules__


For commits that expect a git submodule inside `JJ.Synthesizer`, manually re-add the git submodule and go find docs to configure this.

__💡 Tip__

For most use cases, stick with NuGet to keep things simple.