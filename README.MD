JJ.Synthesizer
==============

*2022-11-18, JJ van Zon*

Introduction
------------

JJ.Synthesizer is software for making music.

The purpose is to connect a MIDI keyboard to it, and play while the program generates the sound for the notes you play. It supports certain types of synthesis, like additive, subtractive, ADSR, noise generator, filters, and effect like compression, envelope following...

It has been a pet project of mine for a few years between about 2015 and 2018. It has been in the ice box for a few years now, because I had health issues. I am now trying to get it to the public domain.

Prerequisites
-------------

- SQL Server

Setting Up Dev Environment
--------------------------

### Database

- Open the folder Database\SynthesizerDB
- You might a file there like "SynthesizerDB.zip".
- You could looki for the one last in the list sorted by name. That should be the latest database backup.
- The file can be unzipped and then restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".

-----

- In luck, no manual work after that may be needed.
- But it is possible that .sql files are listed after the Synthesizer backup file.
- Those may then be executed one by one, onto the restored SynthesizerDB, to get the database more up-to-date.

-----

- Some of the SQL files may indicate "RUN UTILITY".
- That indicates that the utility "JJ.Utilities.Synthesizer.DataMigration" might be run.
- That utility would be found when opening the Synthesizer.sln.
- Running it should show a window that allows executing a database migration step.
- The "RUN UTILITY" sql should have indicated *which step* to run.
- In an unfortunate case, it might be needed, to do a git checkout of a commit somewhere in history in order to run the utility. The commit to check out might be the one closest to the date indicated in the file name of the "RUN UTILITY" sql file.
- Sorry for a possible lack of elegance here.

### JJ.Framework

- Pre-release versions of *JJ.Framework* components may be used.
- Those are currently hosted in *Azure DevOps*:
    - https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json
    - Name: JJs-Pre-Release-Package-Feed
- It might require configuring *NuGet* in your dev environment to connect to this additional package source.


Bugs
----

When I dropped this project out of my hands, a few bugs remained. Unfortunately I was not able to resolve these bugs yet. More shortcomings might be there, but *these* might unfortunately make it musically unusable:

- Ticks when letting go of the keyboard key.
- Delay on the notes after hitting the keyboard.

The sound was quite beautiful before this point, so it is a bit of a shame it got buggy like this.

It appeared to happen after creating a much faster calculation engine, but thus introducing breaking issues. (The faster calculation engine got at 42 note polyphony on a laptop. The slower calculation only 3 note polyphony.)

My health prevented me from bringing the program back into to the light.

### Workaround

A workaround might be to go back in time, getting old commits, and producing a release version of it, without the ticks and delays, but perhaps a slower calculation speed. So that at least there would be this nice intermediate result again.

### Bug #1: Ticks

Ticks when letting go of the keyboard key.

The problem seems to be the so called Release Time does not work. Release Time is the time after the note, that the note keeps playing to fade out or something like that. The problem seems to do with a bug in the graph transformations in the code. Transformations around the Time variable seem to be applied in the wrong order, leading to 0 Release Time, so instant stopping of the sound, producing quite an ugly tick. Unfortunately, I was unable to hack my own algorithm anymore, because I was dealing with chronic fatigue and health issues. I am really hoping this 100 code line algorithm is the problem, and can be fixed by a mind able to hack what the issue is. Perhaps making automated tests with expected outcomes around this algorithm, might pinpoint and make debuggable what is going on, and what is wrong and hopefully lead to the solution.

### Bug #2: Note Delays

Delay on the notes after hitting the keyboard.

The delay was not there at one point. And then it just seemed to all of a sudden appear. I am suspecting a subtle threading issue. I think another thread might not read the most current value of something, but a copy of the value for 'performance' purposes. Or perhaps a locking issue having a less subtle effect than anticipated. When I debugged, it seemed to stop at the breakpoint immediately after the note, and when taking a few debugger steps, the sound of the new note seemed generated quite soon / instantly, so I am not sure where the delay is precisely coming from. I think with checking out older commits, until the problem disappears, may pinpoint the problem and which code changes caused it.

Technical Wishlist
------------------

### Automated Testing

*There is a good reason for the lack of automated testing, but it is also a bad reason.*

The reasons is that it was supposed to be a hobby project, and to give me some fun out of it. Lots of automated testing was not fun in my view. It may have resulted in a lack of stability and downfall of some forms of synthesis down to unusability. Now the lack of tests is the part not fun about it! Automated testing may both prevent and fix this. Steps have been made, but more would be welcome. It may be a lot of work, and put a lot of other things on hold, but I think it may be the best way to go:

More automated testing.


Quirks Checking Out Old Commits
-------------------------------

...

