### Redundant MSBuild Scripting

```xml
    <!--<GenerateDocumentationFile>False</GenerateDocumentationFile>-->

  <TrimmerRootAssembly Include="JJ.Framework.Validation.Legacy" />

  <PropertyGroup>
    <IlcGenerateCompleteTypeMetadata>False</IlcGenerateCompleteTypeMetadata>
    <IlcGenerateStackTraceData>False</IlcGenerateStackTraceData>
  </PropertyGroup>

  <PropertyGroup>
    <!--<GeneratePackageOnBuild>False</GeneratePackageOnBuild>-->
    <!--<PublishProtocol>FileSystem</PublishProtocol>-->
    <!--<_TargetId>Folder</_TargetId>-->
    <PublishDir>bin\$(Configuration)\publish</PublishDir>
    <!--<NuGetAudit>False</NuGetAudit>-->
  </PropertyGroup>

  <!-- Copy Config -->
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="echo copy &quot;$(ProjectDir)app.config&quot; &quot;$(TargetDir)$(AssemblyName).exe.config&quot;&#xD;&#xA;     copy &quot;$(ProjectDir)app.config&quot; &quot;$(TargetDir)$(AssemblyName).exe.config&quot;" />
  </Target>
  <Target Name="PublishConfigjj" AfterTargets="Publish" Condition="'$(PublishDir)'!=''">
    <Exec Command="echo copy &quot;$(ProjectDir)app.config&quot; &quot;$(PublishDir)$(AssemblyName).exe.config&quot;&#xD;&#xA;     copy &quot;$(ProjectDir)app.config&quot; &quot;$(PublishDir)$(AssemblyName).exe.config&quot;" />
  </Target>
```
