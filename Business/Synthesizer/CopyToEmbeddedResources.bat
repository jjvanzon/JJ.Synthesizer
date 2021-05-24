prompt @

echo EXECUTING CopyToEmbeddedResources.bat

cd /D "%~dp0"

rem /R = Overwrites read-only files.
rem /K = Copies attributes. Normal Xcopy will reset read-only attributes.
rem /Y = Suppresses prompting to confirm you want to overwrite an existing destination file.
xcopy "Calculation\SineCalculator.cs" "EmbeddedResources\" /R /K /Y
xcopy "Calculation\Arrays\*.cs" "EmbeddedResources\" /R /K /Y
xcopy "Calculation\BiQuadFilterWithoutFields.cs" "EmbeddedResources\" /R /K /Y
xcopy "Calculation\Patches\PatchCalculatorHelper.cs" "EmbeddedResources\" /R /K /Y
rem xcopy "CopiedCode\FromFramework\Geometry.cs" "EmbeddedResources\" /R /K /Y
xcopy "..\..\..\JJ.Framework\Framework\Mathematics\Geometry.cs" "EmbeddedResources\" /R /K /Y
rem xcopy "CopiedCode\FromFramework\Interpolator.cs" "EmbeddedResources\" /R /K /Y
xcopy "..\..\..\JJ.Framework\Framework\Mathematics\Interpolator.cs" "EmbeddedResources\" /R /K /Y
rem xcopy "CopiedCode\FromFramework\MathHelper.cs" "EmbeddedResources\" /R /K /Y
xcopy "..\..\..\JJ.Framework\Framework\Mathematics\MathHelper.cs" "EmbeddedResources\" /R /K /Y
xcopy "Helpers\CalculationHelper.cs" "EmbeddedResources\" /R /K /Y


pause