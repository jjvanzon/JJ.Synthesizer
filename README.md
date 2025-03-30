JJ.Synthesizer
==============

Release 0.250 - "Additive Engine"
---------------------------------

*2015-03-10*

This release introduces the foundational __Additive Engine__, laying the groundwork for future synthesizer development. While there is no user interface or real-time audio playback yet, core functionality is implemented and validated through test programs in the form of unit tests.

### Main Features

- __Operators__: Manages how data or signals are processed within the synthesizer, enabling various operations for sound generation.
- __Curves__: Modulation curves that evolve over time, shaping sound dynamically.
- __Samples__: Supports audio samples for playback and manipulation of pre-recorded sounds.
- __Audio File Output__: Generates and exports synthesized sound as files.
- __WAV Header__: Adds the file header needed to make audio output playable in standard media players.
- __WAV Header Detection__: Detects whether input files are standard WAV format before attempting to read them.

### Supported Operators

This version of the engine supports various types of synthesis and signal manipulation, including:

- __Sines__ (pure tone waveforms, the simplest sound wave)
- __Samples / Audio Files__ (pre-recorded sound manipulation)
- __Additive Synthesis__ (combines multiple sine waves or samples to create complex sounds)
- __Oscillators__ (periodic waveforms, modulating pitch, volume, and other sound aspects)
- __Delays__ (time-shifting effects for echo and delay)
- __Detune__ (slight pitch adjustments for sound enrichment)
- __Speed Control__ (altering playback speed, affecting pitch and duration)
- __Curves/Envelopes__ (modulating and shapes sound over time)
- __Volume & Pitch Control__ (including envelopes for dynamic adjustments)
- __Basic Arithmetic__ (mathematical operations for sound manipulation)
- __FM Synthesis__ (theoretical support for frequency modulation synthesis, not fully tested)

*Note:* Due to a limitation in this version, pitch envelopes do not work with freely drawn curves. However, pitch envelopes might work by using continuous math functions.

### Interpolation Options

The engine supports two interpolation methods for __Curves__, __Samples__, and __Audio Files__:

- __Blocky__ (step-wise interpolation)
- __Linear__ (smooth transition between data points)

### Samples

Supports the following sample formats:

- **8-bit** and **16-bit** audio
- **Mono** and **Stereo** channels
- **WAV** and **RAW** file formats

The engine also automatically detects and parses WAV headers when loading samples, simplifying the handling of audio files.