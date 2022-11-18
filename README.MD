JJ.Synthesizer
==============

Setting Up Dev Environment
--------------------------

### JJ.Framework

- Pre-release versions of *JJ.Framework* components may be used.
- Those are currently hosted in *Azure DevOps*: https://dev.azure.com/jjvanzon/JJs-Software/_packaging?_a=feed&feed=JJs-Pre-Release-Package-Feed
- It might require configuring *NuGet* in your dev environment to connect to an additional package source.
- The following info may be useful for that:
- Name: JJs-Pre-Release-Package-Feed
- Source: https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json

### Databases

- Open the folder Database\SynthesizerDB
- You might a file there like "SynthesizerDB.zip".
- Looking for the one last in the list sorted by name, might be a good idea. That should be the latest database backup.
- The file can be unzipped and then restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".

-----

- In luck, no manual work may be needed after that.
- But it is possible that .sql files are listed after the Synthesizer backup file.
- Those may then be executed one by one, onto the restored SynthesizerDB, to get the database up-to-date

-----

- Some of the SQL files may indicate "RUN UTILITY".
- That indicates that the utility "JJ.Utilities.Synthesizer.DataMigration" might be run.
- That utility would be found when opening the Synthesizer.sln.
- Running it should show a windows that allows executing a database migration step.
- The "RUN UTILITY" sql should have indicated *which step* to run.
- In an unfortunate case, it might be needed, to do a git checkout of a commit somewhere in history in order to run the utility. The commit to check out might be the one closest to the date indicated in the file name of the "RUN UTILITY" sql file.
- Sorry for a possible lack of elegance here.