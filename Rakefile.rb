require 'bundler/setup'

require 'albacore'
require 'albacore/tasks/versionizer'

package_dir = 'out/pkg'

Albacore::Tasks::Versionizer.new :versioning

desc 'restore all nugets as per the packages.config files'
nugets_restore :restore do |p|
  p.out = './packages'
  p.exe = '.nuget/NuGet.exe'
end

task :patch_assembly_info => [:versioning] do
	FileList.new("**/*.csproj").each{ |p|
		project = Pathname.new p
		file_name = File.join(project.dirname.to_s, 'Properties/AssemblyInfo.cs')
		text = File.read(file_name)
	  	text  = text.gsub(/AssemblyVersion.+$/, "AssemblyVersion(\"#{ENV["FORMAL_VERSION"]}\")]")
		text  = text.gsub(/AssemblyFileVersion.+$/, "AssemblyFileVersion(\"#{ENV["FORMAL_VERSION"]}\")]")
		File.open(file_name, "w") {|file| file.puts text}
	}
end

desc 'Perform full build'
build :build => [:versioning, :restore, :patch_assembly_info] do |b|
  b.sln = 'MiniWebDeploy.sln'
  b.prop 'Configuration', 'Release'
end

desc 'ILMerge the exe and its dependencies'
task :ilmerge => [:build] do
	cmd = 'packages\ilmerge.2.13.0307\ilmerge.exe /target:winexe ' 
	cmd =  cmd + '/out:out\MiniWebDeploy.exe '
	cmd =  cmd + '"src\MiniWebDeploy.Deployer\bin\Release\MiniWebDeploy.Deployer.exe" '
	cmd =  cmd + '"src\MiniWebDeploy\bin\Release\MiniWebDeploy.dll" '
	cmd =  cmd + '"packages\Microsoft.Web.Administration.7.0.0.0\lib\net20\Microsoft.Web.Administration.dll"'
	sh cmd
end

directory package_dir

desc 'package nugets - finds all projects and package them'
nugets_pack :create_nugets => [package_dir, :versioning, :build, :ilmerge] do |p|
  p.files   = FileList['src/**/MiniWebDeploy.csproj']
  p.out     = package_dir
  p.configuration = 'Release'
  p.exe     = '.nuget/NuGet.exe'
  p.with_metadata do |m|
    m.description = 'A really simple website deployment tool providing an easy to understand and discoverable wrapper around the Microsoft.Web.Administration assembly and additional helpers for common deployment tasks.'
    m.authors = 'Jaimal Chohan'
    m.version = ENV['NUGET_VERSION']
    m.license_url = 'https://github.com/jaimalchohan/mini-web-deploy/blob/master/LICENSE.txt'
    m.project_url = 'https://github.com/jaimalchohan/mini-web-deploy'
  end
  p.with_package do |p|
    p.add_file '../../out/MiniWebDeploy.exe', 'tools'
  end
end

task :default => :create_nugets
