JJ Code Base
============

Larger code base with multiple applications:

- Synthesizer
- Question & Answer
- SaveText prototype app
- Some misc demos


Modularization
--------------

Circle3 was the previous code base, that started being developed around 2010. It was about a quite specific way of programming, not like mainstream .NET development. It had a first-order grouping by functionality.

The way of working was not portable between what I do at home and what I do at the office. My ideas about software architecture have changed.

JJ code base is newer. It started being developed around 2013. Its architecture is more agnostic about specific data models, specific programming models, specific API's and specific functionality. The first order grouping might be by technical aspects such as apps, services, framework, validation, security, etcetera. One architecture might accommodate different types of programming and different types of functionality in an orderly fashion.

I think a first-order grouping by technical aspects instead of by functional aspects may be of help, because technology can change fast, but functionality may change even faster, so for a main outline of things, technical aspects might be more stable as a subdivision, than functional aspects, which may change more frequently, is my opinion.


Setting Up Dev Environment
----------------------

### JJ.Framework

- Pre-release versions of *JJ.Framework* components may be used.
- They might be hosted in *Azure DevOps*: https://dev.azure.com/jjvanzon/JJs%20Software/_packaging?_a=feed&feed=JJs-Pre-Release-Package-Feed
- They may require configuring *NuGet* to connect to an additional package source.
- The following info might be useful for that:
- Name: JJs-Pre-Release-Package-Feed
- Source: https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json

### Internet Information Services (IIS)

- Some web projects might like to run on IIS (Internet Information Services).
- They may not load otherwise.
- Trying to *reload* a web project:
- The Output window in Visual Studio might show an expected web address.
- It is suggested to install IIS.
- Creating a web site.
- That might point its *physical path* to the folder of the csproj that might not load.
- Having a *binding* that might use the  the web address previously found in the Visual Studio output as a *host name*.
- The file C:\Windows\System32\drivers\etc\hosts might need editing.
- Entries like the following might be added:
    ```
    127.0.0.1  dev.questionandanswer.jjvanzon.io
    ::1        dev.questionandanswer.jjvanzon.io
    127.0.0.1  dev.savetextappservice.jjvanzon.io
    ::1        dev.savetextappservice.jjvanzon.io
    127.0.0.1  dev.savetext.jjvanzon.io
    ::1        dev.savetext.jjvanzon.io
    ```

### Databases

#### QuestionAndAnswerDB

- The database backup may be found under "Database\QuestionAndAnswer"
- The file might be unzipped and restored in SQL Server with name DEV_QuestionAndAnswerDB.
- And again with name DEV_QuestionAndAnswerDB_UnitTests
- The database user name "dev" may be expected with password "dev".

#### SaveTextDB

- The folder Database\SaveTextDB may contain a database backup.
- The file might be restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".

#### SynthesizerDB

- Opening the folder Database\SynthesizerDB
- There may be a file that might allow restoring SynthesizerDB.
- Maybe sort the files by name.
- Perhaps look for the a file like "SynthesizerDB.zip".
- Looking for the one last in the list, might be a good idea. That should be the latest database backup.
- The file might be unzipped and then restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".

-----

- In luck, no manual work may be needed after that.
- But it is possible that .sql files are listed after the Synthesizer backup file.
- Those may then be executed one by one, onto the restored SynthesizerDB, to get the database better up-to-date

-----

- Some of the SQL files may indicate "RUN UTILITY".
- That may indicate that the utility "JJ.Utilities.Synthesizer.DataMigration" might be run.
- That utility might be found when opening the Synthesizer.sln.
- It should show a windows that might allow executing a database migration step.
- The "RUN UTILITY" sql file may hav indicated *which step* to run.
- In an unfortunate case, it might be needed, to do a git checkout of a commit somewhere in history in order to run the utility. The commit to check out might be the one closest to the date indicated in the file name of the "RUN UTILITY" sql file.
- Sorry for a possible lack of elegance here.