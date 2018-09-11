prompt @

echo EXECUTING CopyFrameworkCode.bat

cd /D "%~dp0"

rem /R = Overwrites read-only files.
rem /K = Copies attributes. Normal Xcopy will reset read-only attributes.
rem /Y = Suppresses prompting to confirm you want to overwrite an existing destination file.
xcopy "..\..\Framework\Mathematics\*.cs" "CopiedCode\FromFramework\" /R /K /Y
xcopy "..\..\Framework\Collections\*.cs" "CopiedCode\FromFramework\" /R /K /Y

attrib -r "CopiedCode\FromFramework\*.cs";

pause