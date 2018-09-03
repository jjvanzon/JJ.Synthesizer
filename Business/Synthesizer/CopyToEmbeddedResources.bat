prompt @

echo EXECUTING CopyToEmbeddedResources.bat

cd /D "%~dp0"

rem /R = Overwrites read-only files.
rem /K = Copies attributes. Normal Xcopy will reset read-only attributes.
rem /Y = Suppresses prompting to confirm you want to overwrite an existing destination file.
xcopy "Calculation\SineCalculator.cs" "EmbeddedResources\" /R /K /Y
xcopy "Calculation\Arrays\*.cs" "EmbeddedResources\" /R /K /Y

pause