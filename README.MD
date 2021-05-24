JJ Code Base
============

Introduction
------------

Larger code base with multiple applications:

- Synthesizer
- Question & Answer
- SaveText 
- Some misc demos


Modularization
--------------

Circle3 was the previous code base, that started being developed around 2010. It was about a quite specific way of programming, not like mainstream .NET development. It had a first-order grouping by functionality.

The way of working was not portable between what I do at home and what I do at the office. My ideas about architecture have changed.

JJ code base is newer. It started being developed around 2013.

This newer architecture is more agnostic about specific data models, specific programming models, specific API's and specific functionality. The first order grouping might be by technical aspects such as apps, services, framework, validation, security, etcetera. I feel that one architecture should accommodate many types of programming and many types of functionality in an orderly fashion.

I think a first-order grouping by technical aspects instead of functional aspects may help, because technology may change fast, but functionality may change faster, so for a main outline of things, technical aspects might be more stable as a subdivision, than functional aspects, which may change more frequently.


Setting Up Dev Environment
----------------------

### JJ.Framework

- Pre-release versions of *JJ.Framework* components may be used.
- They might be hosted in *Azure DevOps*: https://dev.azure.com/jjvanzon/JJs%20Software/_packaging?_a=feed&feed=JJs-Pre-Release-Package-Feed
- They may require configuring *NuGet* to connect to an additional package source.
- The following info might be useful for that:
- *Name:* JJs-Pre-Release-Package-Feed
- *Source:* https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json

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
    127.0.0.1	dev.questionandanswer.jjvanzon.io
    ::1		    dev.questionandanswer.jjvanzon.io
    127.0.0.1	dev.savetextappservice.jjvanzon.io
    ::1		    dev.savetextappservice.jjvanzon.io
    127.0.0.1	dev.savetext.jjvanzon.io
    ::1		    dev.savetext.jjvanzon.io
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
