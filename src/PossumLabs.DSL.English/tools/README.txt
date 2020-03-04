Welcome to Possum Labs DSL

Build to finish the intallation and sample files will be created.

Then set appsettings.json, Entities\User.json and Entities\Existing.json to copy always

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


Visit DSL.PossumLabs.com for examples and documentation. 