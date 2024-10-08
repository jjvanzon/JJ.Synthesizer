*JJ van Zon, 2022*

🎶 JJ.Synthesizer 🎹
=====================

<h2>📔 Contents</h2>

- [⏯ Video](#-video)
- [🖼 Screen Shots](#-screen-shots)
- [📢 Intro](#-intro)
- [🐞 Bugs](#-bugs)
- [👨‍💻 Technical Wishlist](#-technical-wishlist)
- [🏛 Reference Architecture](#-reference-architecture)
- [⚙ Dev Env](#-dev-env)
    - [🖥 Visual Studio](#-visual-studio)
    - [💽 Database](#-database)
    - [📦 JJ.Framework](#-jjframework)
    - [🧱 JJ.Canonical](#-jjcanonical)
- [🕘 Checking Out Old Commits](#-checking-out-old-commits)


⏯ Video
---------

<a href="https://www.youtube.com/watch?v=keeSetBu9A0&start=16" target="_blank" rel="noopener noreferrer"><img src="https://img.youtube.com/vi/keeSetBu9A0/0.jpg" alt="Demo Video"/></a>


🖼 Screen Shots
---------------

![](Resources/jj-synthesizer-overview.png)

![](Resources/jj-synthesizer-midi-mapping.png)


📢 Intro 
---------

`JJ.Synthesizer` is software for making music.

The purpose is to connect a MIDI keyboard to it, and the program generates the sound for the notes you play.

It has been a pet project of mine for a few years between about 2015 and 2018. It has been in the ice box for a few years now, because of my health issues. I am now trying to get it to the public domain at least.

It supports certain types of synthesis, like additive, subtractive, sampling, ADSR, noise generator, filters, and effects like compression and envelope following.

[Install](https://github.com/jjvanzon/JJ.Synthesizer/releases/) | Requires SQL Server


🐞 Bugs
--------

When I dropped this project out of my hands, a few [bugs](README2.MD#bugs) remained. Unfortunately I was not able to resolve these bugs yet. More shortcomings might be there, but *these* might make it musically less usable:

- [Ticks when letting go of the keyboard key.](README2.MD#bug-1-ticks)
- [Delay on the notes after hitting the keyboard.](README2.MD#bug-2-note-delays)

Also [bugs in the main flow](README2.MD#bugs-in-the-main-flow) of the application might get priority.


👨‍💻 Technical Wishlist
----------------------

Not entirely proud of the many other little bugs that remained. Solution:  
[More Automated Testing?](README2.MD#technical-wishlist)


🏛 Reference Architecture
--------------------------

The patterns and practices in this project closely match the following guidelines for structuring software: [JJ's Reference Architecture](https://github.com/jjvanzon/JJs-Reference-Architecture)


⚙ Dev Env
-----------

Here you may find some pointers how to set up a developent environment to build and run `JJ.Synthesizer`.

### 🖥 Visual Studio

This project was developed using C# in Visual Studio. You might download that elsewhere.

### 💽 Database

- Open the folder `Database\SynthesizerDB`.
- There you might find the file `SynthesizerDB.zip`.
- Look for the one last in the list sorted by name. That should be the latest database backup.
- The file can be unzipped and then restored in SQL Server with name `DEV_SaveTextDB`.
- The database user name `dev` might be expected with password `dev`.
- Remove that user from the database, and then add that user again on the server level, giving rights for reading and writing to the database.

-----

- In luck, no manual work after that may be needed.
- But it is possible that .sql files are listed after the `Synthesizer` backup file.
- Those may then be executed one by one, onto the restored `SynthesizerDB`, to get the database more up-to-date.

-----

- Some of the SQL files may indicate `RUN UTILITY`.
- That indicates that the utility `JJ.Utilities.Synthesizer.DataMigration` might be run.
- That utility would be found when opening the `Synthesizer.sln`.
- Running it should show a window that allows executing a database migration step.
- The `RUN UTILITY` sql should have indicated *which step* to run.
- In an unfortunate case, it might be needed, to do a git checkout of a commit somewhere in history in order to run the utility. The commit to check out might be the one closest to the date indicated in the file name of the `RUN UTILITY` sql file.
- Sorry for a possible lack of elegance here.

### 📦 JJ.Framework

- Pre-release versions of `JJ.Framework` components may be used.
- Those are currently hosted in *Azure DevOps*:
- <https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json>
- Name: `JJs-Pre-Release-Package-Feed`
- It might require configuring `NuGet` in your dev environment to connect to this additional package source.

### 🧱 JJ.Canonical

- `JJ.Data.Canonical` and `JJ.Business.Canonical` can also be found right next to the `JJ.Framework` packages in `JJs-Pre-Release-Package-Feed`.


🕘 Checking Out Old Commits
----------------------------

`JJ.Synthesizer` was once part of a larger code base. It was extracted to become a new git repository with history in tact. Some quirks when checking out older history items, may have to do with that. The following may only be relevant when getting older commits from history (from before 2022-11-18).

- Commit messages:
    - May mention projects not part of the `JJ.Synthesizer` repo.
- References to other non-existent projects:
    - To fix this, try referencing the NuGet version of these dependencies.
- Broken project references to `JJ.Framework` / `JJ.Canonical`:
    - `JJ.Framework` / `JJ.Canonical` projects are no longer in the same code base.
    - You could try referencing the NuGet version of these dependencies.
    - But another solution may be to clone the [JJ.Framework](https://github.com/jjvanzon/JJ.Framework) repository and put the `JJ.Framework` folder right next to the `JJ.Synthesizer` folder. This may fix the broken links.
    - Same for the [JJ.Canonical](https://github.com/jjvanzon/JJ.Canonical) repository.
    - But sometimes the paths are still incorrect and may need to be modified (with Notepad)?
    - But then you could use a specific commit from `JJ.Framework` or `JJ.Canonical`'s history.
