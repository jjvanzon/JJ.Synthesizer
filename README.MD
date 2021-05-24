JJ Code Base
============

*Jan Joost van Zon, 2013-09-09*

Introduction
------------

JJ is the new code base.
The previous code base I began 3 years ago.

My ideas about architecture have changed.

The old code base is about a specific way of programming, not like mainstream .NET development.
The way of working is not portable between what I do at home and what I do at the office.

The new architecture is more agnostic about specific data models, specific programming models, specific API's and specific functionality.
The first order grouping will be by technical aspects such as apps, services, framework, validation, security, etcetera,
unlike the old code base which basically had a first-order grouping by functionality.

I feel that one architecture should accommodate many types of programming and many types of functionality in an orderly fashion.

Setting Up Dev Environment
----------------------

### JJ.Framework

- The solutions may only build if you clone another repository too: JJ.Framework. ([https://github.com/jjvanzon/JJ.Framework](https://github.com/jjvanzon/JJ.Framework))
- It might be cloned to a folder 'JJ.Framework' that is right next to your 'JJ' repository clone.
- First JJ.Framework.sln may be built or at least NuGet-restored.
- Then one of the solutions in this repository might build.

### IIS (Internet Information Services)

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
