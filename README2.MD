JJ.Synthesizer Details
======================

*2022-11-18, JJ van Zon*

<h2>Contents</h2>

- [Bugs](#bugs)
    - [Bug #1: Ticks](#bug-1-ticks)
    - [Bug #2: Note Delays](#bug-2-note-delays)
    - [Bugs in the Main Flow](#bugs-in-the-main-flow)
    - [Workaround](#workaround)
    - [Hunch](#hunch)
- [Technical Wishlist](#technical-wishlist)
    - [Automated Testing](#automated-testing)

Bugs
----

When I dropped this project out of my hands, a few bugs remained. Unfortunately I was not able to resolve these bugs yet. More shortcomings might be there, but *these* might make it musically unusable:

- Ticks when letting go of the keyboard key.
- Delay on the notes after hitting the keyboard.

The sound was quite beautiful before this point, so it is a bit of a shame it got buggy like this.

It appeared to happen after creating a much faster calculation engine, but thus introducing breaking issues. (The faster calculation engine got at 42 note polyphony on a laptop. The slower calculation only 3 note polyphony.)

My health prevented me from bringing the program back into to the light.

### Bug #1: Ticks

Ticks when letting go of the keyboard key.

The problem seems to be the so called Release Time does not work. Release Time is the time after hitting the note, that the note keeps playing to fade out or something like that. The problem seems to do with a bug in the graph transformations in the code. Transformations around the Time variable seem to be applied in the wrong order, leading to 0 Release Time, so instant stopping of the sound, producing quite an ugly tick. Unfortunately, I was unable to hack my own algorithm anymore, because I was dealing with chronic fatigue and health problems. I am really hoping this 100 code line algorithm is the problem, and can be fixed by a mind able to hack what the issue is. Perhaps making automated tests with expected outcomes around this algorithm, might pinpoint (and make debuggable) what is going on, and what is wrong and hopefully: the solution.

### Bug #2: Note Delays

Delay on the notes after hitting the keyboard.

The delay was not there at one point. And then it just seemed to all of a sudden appear. I am suspecting a subtle threading issue. I think another thread might not read the most current value of something, but a copy of the value for 'performance' purposes. Or perhaps a locking issue having a less subtle effect than anticipated. When I debugged, it seemed to stop at the breakpoint immediately after hitting the note, and when taking a few debugger steps, the sound of the new note seemed to play quite soon / instantly, so I am not sure where the delay is precisely coming from. I think with checking out older commits, until the problem disappears, may pinpoint the problem and which code changes caused it.

*Remark from 2018-02-03:*  
TFS changeset 2151 was the version from before letting MidiInputProcessor use MidiMappingCalculator. Tones started to have a delay at some point. Trying to find the point at which the tones didn't have a delay yet to try to find the cause more easily.

### Bugs in the Main Flow

Bugs in the main flow of the application might get some more priority (new document, create patch, add operators, edit curves, play sound, etc.)

### Workaround

A workaround might be to go back in time, getting old commits, and producing a release version of it, without the ticks and delays, but perhaps a slower calculation speed. So that at least there would be this nice intermediate result again.

### Hunch

One hypothesis for the note delays is, that value changes are not being yielded over from the Dictionary to the fields / AH! stack variables, until the next buffer of sound is calculated. Hmm... using stack variables was one of the things that made it quite a lot faster.

Maybe invalidate the buffer as soon as possible somehow? A new buffer then might be calculated, but at least the existing buffer is not held on to so thrifty. And MIDI value changes are not so frequent? A bit iffy all of that. Hmm... may cause some slow down, but that might be something to see in practice.

In all honesty, I never really understood why there wasn't a delay


Technical Wishlist
------------------

### Automated Testing

*There is a good reason for the lack of automated testing, but it is also a bad reason.*

The reasons is, that it was supposed to be a hobby project, and to give me some fun out of it. Lots of automated testing was not fun in my view. It may have resulted in a lack of stability and downfall of some forms of synthesis down to unusability. Now the lack of tests might be the part not fun about it! Automated testing may both prevent and fix this. Steps have been made, but more would be welcome. It may be a lot of work, and put a lot of other things on hold, but I think it may be the best way to go for this project:

More automated testing.