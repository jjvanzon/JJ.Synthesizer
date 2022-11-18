Synthesizer Release 0.2
=======================

*2021-05-11*


Introduction
------------

`JJ.Synthesizer` is software for making music.

The purpose is to connect a MIDI keyboard to it, and play while the program generates the sound for the notes you play. It supports certain types of synthesis, like additive, subtractive, sampling, ADSR, noise generator, filters, and effects like compression and envelope following.

This synthesizer application may not really be finished. But isolating a relatively stable version was desired, before making more changes.


Prerequisites
-------------

- *Microsoft SQL Server* is required which may be downloaded elsewhere.


Database
--------

- The synthesizer stores things in a database.
- The database backup can be found in the file "SynthesizerDB.0.2.zip".
- The file might be unzipped and restored in SQL Server with name PROD_SynthesizerDB.
- The database user name "prod" may be expected with password "prod".
- If needed, different database name / user name / password might be configured.
- The app.config in of the Windows application may need to be changed in that case.


Windows Application
-------------------

- "JJ.Presentation.Synthesizer.WinForms.0.2.zip" contains the Windows application files.
- It has no installer.
- It can be unzipped and put somewhere on a computer and then perhaps a shortcut made to it.