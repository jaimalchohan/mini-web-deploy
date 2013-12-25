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

directory package_dir

desc 'package nugets - finds all projects and package them'
nugets_pack :create_nugets => [package_dir, :versioning, :build] do |p|
  p.files   = FileList['src/**/MiniWebDeploy.csproj']
  p.out     = package_dir
  p.configuration = 'Release'
  p.exe     = '.nuget/NuGet.exe'
  p.with_metadata do |m|
    m.description = 'An easy to use web deployment tool'
    m.authors = 'Jaimal Chohan'
    m.version = ENV['NUGET_VERSION']
  end
  p.with_package do |p|
    p.add_file '../MiniWebDeploy.Deployer/bin/Release/MiniWebDeploy.Deployer.exe', 'lib'
  end
end

task :default => :create_nugets
