Synthesizer Release 0.2
=======================

*2021-05-11*


Introduction
------------

This synthesizer application may not really be finished. But isolating a relatively stable version was desired, before making more changes.


Prerequisites
-------------

- *Microsoft SQL Server* may be required which may be downloaded elsewhere.


Database
--------

- The synthesizer may store things in a database.
- The database backup might be found in the file "SynthesizerDB.0.2.zip".
- The file might be unzipped and restored in SQL Server with name PROD_SynthesizerDB.
- The database user name "prod" may be expected with password "prod".
- If needed, different database name / user name / password might be configured.
- The app.config in of the Windows application may need to be changed in that case.


Windows Application
-------------------

- "JJ.Presentation.Synthesizer.WinForms.0.2.zip" may contain the Windows application files.
- It has no installer.
- It might be unzipped and put somewhere on a computer and then perhaps a shortcut made to it.