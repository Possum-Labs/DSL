Welcome to Possum Labs DSL

1. Build the project to finish the intallation; sample files will be created.

2. Then set "appsettings.json", "Entities\User.json" and "Entities\Existing.json" to copy always

This should look as follows in the project file

  <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="Entities\User.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="Entities\Existing.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>

3. Add the following packages for MsTest execution "SpecFlow.MsTest" and "SpecFlow.Tools.MsBuild.Generation"

This should look as follows in the project file

    <PackageReference Include="SpecFlow.MsTest" Version="3.1.82" />
    <PackageReference Include="SpecFlow.Tools.MsBuild.Generation" Version="3.1.82" />

4. Run tests

Visit DSL.PossumLabs.com for examples and documentation. 