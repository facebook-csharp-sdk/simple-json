require File.join(File.dirname(__FILE__), 'build/albacore/albacore.rb')

PROJECT_NAME      = "Simple Json"
PROJECT_NAME_SAFE = "SimpleJson"
LOG               = false                # TODO: enable albacore logging from ENV
ENV['NIGHTLY']    = 'false'

build_config = nil
nuspec_config = nil

task :configure do
   # do configuration stuffs here
   Albacore.configure do |config|
       config.log_level = :verbose if LOG
   end
   
   root_path    = "#{File.dirname(__FILE__)}/"
   base_version = 0
   
   File.open("#{root_path}VERSION",'r') do |f|
       base_version = f.gets.chomp
   end
   
   build_config = {
       :log    => LOG,
       :paths  => {
            :root    => root_path,
            :src     => "#{root_path}src/",
            :output  => "#{root_path}bin/",
            :build   => "#{root_path}build/",
            :dist    => "#{root_path}dist/",
            :working => "#{root_path}working/",
            :doc     => "#{root_path}doc/",
            :packages=> '',
            :nuget   => '',
            :nunit   => {
                :x86_console_path => ''
            }
       },
       :version => {
            :base => "#{base_version}",
            :full => "#{base_version}",
            :long => "#{base_version}"
        },
       :vcs => { # version control system
            :name         => '',        # version control name
            :rev_id       => '',        # revision number
            :short_rev_id => ''         # short revision id
        },        
       :ci => {
            :build_number_param_name => 'BUILD_NUMBER',
            :is_nightly              => true,
            :build_number            => 0
        },
       :configuration => :Release,
       :sln => '',
	   :nuspec => {
            :authors                 => "Jim Zimmerman, Nathan Totten, Prabir Shrestha"
       }
   }

   nuspec_config = {
        "SimpleJson" => {
            :description => "Super lightweight Json library for .NET"
        }
	}
   
   build_config[:paths][:packages]  = "#{build_config[:paths][:src]}packages/"
   build_config[:paths][:nunit][:x86_console_path]  = "#{build_config[:paths][:packages]}NUnit.2.5.10.11092/Tools/nunit-console-x86.exe"
   build_config[:paths][:nuget]  = "#{build_config[:paths][:packages]}NuGet.CommandLine.1.3.20425.372/Tools/NuGet.exe"
   
   build_config[:sln]        = "#{build_config[:paths][:src]}SimpleJson.sln"
   
   begin
        build_config[:vcs][:rev_id] = `hg id -i`.chomp.chop # remove the +
        build_config[:vcs][:name] = 'hg'
        build_config[:vcs][:short_rev_id] = build_config[:vcs][:rev_id]
    rescue
    end
   
   if(build_config[:vcs][:rev_id].length==0) then
       # if mercurial fails try git
       begin
           build_config[:vcs][:rev_id]    = `git log -1 --pretty=format:%H`.chomp
            build_config[:vcs][:name] = 'git'
           build_config[:vcs][:short_rev_id] = build_config[:vcs][:rev_id][0..7]
       rescue
       end
   end
   
   build_config[:ci][:is_nightly]   = ENV['NIGHTLY'].nil? ? true : ENV['NIGHTLY'].match(/(true|1)$/i) != nil
   build_config[:ci][:build_number] = ENV[build_config[:ci][:build_number_param_name]] || 0
   
   build_config[:version][:full] = "#{build_config[:version][:base]}.#{build_config[:ci][:build_number]}"
   
    if(build_config[:ci][:is_nightly])
        build_config[:version][:long] = "#{build_config[:version][:full]}-nightly-#{build_config[:vcs][:short_rev_id]}"
    else
        build_config[:version][:long] = "#{build_config[:version][:full]}-#{build_config[:vcs][:short_rev_id]}"        
    end
   
    puts build_config if build_config[:log]
    puts
    puts  "     Project Name: #{PROJECT_NAME}"
    puts  "Safe Project Name: #{PROJECT_NAME_SAFE}"
    puts  "          Version: #{build_config[:version][:full]} (#{build_config[:version][:long]})"
    puts  "     Base Version: #{build_config[:version][:base]}"
    print "  CI Build Number: #{build_config[:ci][:build_number]}"
    print " (not running under CI mode)" if build_config[:ci][:build_number] == 0
    puts
    puts  "        Root Path: #{build_config[:paths][:root]}"
    puts
    puts  "              VCS: #{build_config[:vcs][:name]}"
    print "      Revision ID: #{build_config[:vcs][:rev_id]}"
    print "  (#{build_config[:vcs][:short_rev_id]})" if build_config[:vcs][:name] == 'git'
    puts    
    puts   
end

Rake::Task["configure"].invoke

task :default => [:build,:tests,:powershell,:nuget]

desc "Build"
msbuild :build => [:clean] do |msb|
   msb.properties :configuration => build_config[:configuration]
   msb.solution = build_config[:sln]
   msb.targets :Build
end

msbuild :clean_build do |msb|
   msb.properties :configuration => build_config[:configuration]
   msb.solution = build_config[:sln]
   msb.targets :Clean
end

desc "Clean All"
task :clean => [:clean_build] do
   FileUtils.rm_rf build_config[:paths][:output]
   FileUtils.rm_rf build_config[:paths][:working]
   FileUtils.rm_rf build_config[:paths][:dist]
end

nunit :tests => [:build] do |nunit|
    nunit.command    = build_config[:paths][:nunit][:x86_console_path]
    nunit.assemblies = ["#{build_config[:paths][:src]}SimpleJson.Tests/bin/Release/SimpleJson.Tests.dll"]
    nunit.options = ["/xml=#{build_config[:paths][:output]}/Tests.nunit.xml"]
end

directory "#{build_config[:paths][:working]}"
directory "#{build_config[:paths][:working]}NuGet/"
directory "#{build_config[:paths][:dist]}"
directory "#{build_config[:paths][:dist]}NuGet"

task :nuspec => ["#{build_config[:paths][:working]}"] do
    rm_rf "#{build_config[:paths][:working]}NuGet/"
    mkdir "#{build_config[:paths][:working]}NuGet/"
    
	 Dir.entries(base_dir = "#{build_config[:paths][:build]}NuGet/").each do |name|
        path = "#{base_dir}#{name}/"
        dest_path = "#{build_config[:paths][:working]}NuGet/#{name}/"
        if name == '.' or name == '..' then
            next
        end
        FileUtils.cp_r path, dest_path
        
        nuspec do |nuspec|
            config = nuspec_config[name]
            nuspec.id = name
            nuspec.version = "#{build_config[:version][:full]}"
            nuspec.authors = "#{build_config[:nuspec][:authors]}"
            nuspec.description = config[:description]
            nuspec.language = "en-US"
            nuspec.licenseUrl = "http://simplejson.codeplex.com/license"
            nuspec.requireLicenseAcceptance = "false"
            nuspec.projectUrl = "http://simplejson.codeplex.com"
            nuspec.tags = "json"
            nuspec.output_file = "#{dest_path}/#{name}.nuspec"
        
            if !config[:dependencies].nil? then
                config[:dependencies].each do |d|
                    nuspec.dependency d[:id], d[:version]
                end
            end
        end
    end

	mkdir "#{build_config[:paths][:working]}NuGet/SimpleJson/Content/"
	cp "#{build_config[:paths][:src]}SimpleJson/SimpleJson.cs", "#{build_config[:paths][:working]}NuGet/SimpleJson/Content/"

end

task :nuget => [:nuspec] do
	Dir["#{build_config[:paths][:working]}*/*/*.nuspec"].each do |name|
        nugetpack :nuget do |nuget|
            nuget.command = "#{build_config[:paths][:nuget]}"
            nuget.nuspec  = name            
            nuget.output = "#{build_config[:paths][:working]}Nuget/"
        end
    end
end

task :powershell => ["#{build_config[:paths][:working]}"] do
	cp "#{build_config[:paths][:src]}simplejson.script.ps1", "#{build_config[:paths][:working]}simplejson.psm1"

	File.open("#{build_config[:paths][:src]}SimpleJson/SimpleJson.cs", 'r') do |cs|
		File.open("#{build_config[:paths][:working]}simplejson.psm1", 'a') do |ps|
			ps.puts
			ps.puts "$source = @\""
			ps.puts
			ps.puts "#define SIMPLE_JSON_DATACONTRACT"
			ps.puts "#define SIMPLE_JSON_REFLECTIONEMIT"			
			ps.puts
			while line = cs.gets
				ps.puts line
			end
			ps.puts "\"@"
            ps.puts "Export-ModuleMember ConvertFrom-Json"
            ps.puts "Export-ModuleMember ConvertTo-Json"
			ps.puts "Add-Type -ReferencedAssemblies System.Runtime.Serialization -TypeDefinition $source -Language CSharp"
		end
	end
end
