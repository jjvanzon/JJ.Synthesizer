prompt @

cd /D "%~dp0"

rem /R = Overwrites read-only files.
rem /K = Copies attributes. Normal Xcopy will reset read-only attributes.
rem /Y = Suppresses prompting to confirm you want to overwrite an existing destination file.

xcopy "..\..\Framework\Mathematics\*.cs" "CopiedCode\FromFramework\" /R /K /Y

xcopy "..\..\Framework\Collections\CollectionHelper.cs" "CopiedCode\FromFramework\" /R /K /Y

pause
