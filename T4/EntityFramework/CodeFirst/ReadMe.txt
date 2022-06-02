1) Copy Template.tt into your csproj and rename to reflect the target database name (JobSystem.tt)
2) Configure the settings in the Template.tt file.  Depending on the location of your Template.tt file, you may have to adjust the include paths.
i.e. <#@ include file="..\..\..\..\T4\EntityFramework\01. CodeFirst\ClientDelivery.EF.Core.ttinclude" #>