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

desc 'Perform full build'
build :build => [:versioning, :restore] do |b|
  b.sln = 'MiniWebDeploy.sln'
  b.prop 'Configuration', 'Release'
end

directory package_dir

desc 'package nugets - finds all projects and package them'
nugets_pack :create_nugets => [package_dir, :versioning, :build] do |p|
  p.files   = FileList['src/**/MiniWebDeploy.csproj']
  p.out     = package_dir
  p.exe     = '.nuget/NuGet.exe'
  p.with_metadata do |m|
    m.description = 'A cool nuget'
    m.authors = 'Henrik'
    m.version = ENV['NUGET_VERSION']
  end
  p.with_package do |p|
    p.add_file '../MiniWebDeploy.Deployer/bin/Release/MiniWebDeploy.Deployer.exe', 'lib'
  end
end

task :default => :create_nugets
