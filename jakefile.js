var fs = require('fs'),
	njake = require('./src/njake'),
	msbuild = njake.msbuild,
	nuget = njake.nuget,
	config = {
		version: fs.readFileSync('VERSION', 'utf-8')
	};

console.log('SimpleJson v' + config.version)

msbuild.setDefaults({
	properties: { Configuration: 'Release' },
	processor: 'x86',
	version: 'net4.0'
})

nuget.setDefaults({
	_exe: 'src/packages/NuGet.CommandLine.1.7.0/tools/NuGet.exe',
	verbose: true
})

desc('Build all')
task('default', ['clean', 'build'])

namespace('build', function () {

	desc('Build .NET 4.0')
	task('net40', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net40.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build .NET 3.5')
	task('net35', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net35.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build .NET 2.0')
	task('net20', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net20.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build Silverlight 5')
	task('sl5', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL5.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build Silverlight 4')
	task('sl4', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL4.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build Silverlight 3')
	task('sl3', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL3.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build Windows Phone 7.0')
	task('wp70', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-WP7.csproj',
			targets: ['Build']
		})
	}, { async: true })

	desc('Build WinRT(Metro)')
	task('winrt', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-WinRT.csproj',
			targets: ['Build']
		})
	}, { async: true })

	task('all', ['build:net40', 'build:net35', 'build:net20', 'build:sl5', 'build:sl4', 'build:sl3', 'build:wp70', 'build:winrt' ]);

})

task('build', ['build:all'])

namespace('clean', function () {

	task('net40', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net40.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('net35', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net35.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('net20', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-Net20.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('sl5', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL5.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('sl4', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL4.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('sl3', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-SL3.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('wp70', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-WP7.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('winrt', function () {
		msbuild({
			file: 'src/SimpleJson/SimpleJson-WinRT.csproj',
			targets: ['Clean']
		})
	}, { async: true })

	task('all', ['clean:net40', 'clean:net35', 'clean:net20', 'clean:sl5', 'clean:sl4', 'clean:sl3', 'clean:wp70', 'clean:winrt' ]);

})

desc('Clean')
task('clean', ['clean:all'], function () {
	jake.rmRf('working/')
	jake.rmRf('bin/')
})