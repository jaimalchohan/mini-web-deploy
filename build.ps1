properties {
    $base_dir  = resolve-path .
    $version = "0.8.0"
    $sln_file = "$base_dir\MiniWebDeploy.sln" 
    $package_dir = "$base_dir\out\pkg"
    $nuspec = "MiniWebDeploy.nuspec"
    $nuspec_working_path = "package_dir\$nuspec"
}

task default -depends Pack

task RestorePackages {
     & ".\.nuget\nuget.exe" restore -PackagesDirectory .\packages
}

task PatchAssemblyInfo {
    (Get-ChildItem -Path $base_dir\src -Filter AssemblyInfo.cs -Recurse) | 
        Foreach-Object {
            (Get-Content $_.FullName) | 
                Foreach-Object {
                    $_ -replace 'AssemblyVersion.+$',"AssemblyVersion(`"$version`")]" `
                       -replace 'AssemblyFileVersion.+$',"AssemblyFileVersion(`"$version`")]"
                }  | 
                Out-File $_.FullName
        }
}

task Build -depends RestorePackages, PatchAssemblyInfo {
   Exec { msbuild  "$sln_file" /p:Configuration=Release }
}

task Merge -depends Build {
    
    New-Item -ItemType Directory -Force -Path out

    & packages\ilmerge.2.13.0307\ilmerge.exe /target:winexe `
        src\MiniWebDeploy.Deployer\bin\Release\MiniWebDeploy.Deployer.exe `
        src\MiniWebDeploy\bin\Release\MiniWebDeploy.dll `
        packages\Microsoft.Web.Administration.7.0.0.0\lib\net20\Microsoft.Web.Administration.dll `
        /out:out\MiniWebDeploy.exe

    if ($lastExitCode -ne 0) {
        throw "Error: Failed to merge assemblies!"
    }
}

task Pack -depends Merge {
    New-Item -ItemType Directory -Force -Path $package_dir
    iex ".\.nuget\nuget.exe pack $base_dir\src\MiniWebDeploy\MiniWebDeploy.csproj -OutputDirectory $package_dir -Properties Configuration=Release -Version $version"
}

task Clean { 
  remove-item -force -recurse out -ErrorAction SilentlyContinue 
} 