[esec](http://code.google.com/p/esec) is an evolutionary computation (EC) framework for Python. `esecui` is a C# 4.0, Windows Forms based application for designing, conducting and visualising an esec experiment.

[IronPython](http://ironpython.codeplex.com) is used to host `esec` within the application. The IronPython binaries and current supported version of `esec` are included as part of the source distribution.

**New**: A GettingStarted guide is now available on our wiki for those who are new to `esec` and `esecui`.


`esecui` is very much in alpha. A lot of functionality has not been implemented and there is a 100% guarantee of show-stopping bugs :-). Patches are very welcome.

`esecui` includes code from the [SharpDevelop](http://www.icsharpcode.net/) project (the text editor control). The relevant source code is included in the repository (slightly modified from the original), but only the compiled binary is required to execute `esecui`.

`esecui` may be built and run with the latest version of [Mono](http://www.mono-project.com/), although some functionality is restricted compared to when built using the .NET Framework. Windows is required for the best experience. (Note that Ubuntu doesn't have a package for the latest version of Mono - you'll have to build it from source.)

A preview executable is now available for download. This is guaranteed to contain bugs, so please report any you find or any suggestions you may have. These screenshots are from the latest downloadable binary.

![![](http://stevedower.id.au/images/esecui/system_thumb.png)](http://stevedower.id.au/images/esecui/system.png) ![![](http://stevedower.id.au/images/esecui/python_thumb.png)](http://stevedower.id.au/images/esecui/python.png) ![![](http://stevedower.id.au/images/esecui/landscape_thumb.png)](http://stevedower.id.au/images/esecui/landscape.png) ![![](http://stevedower.id.au/images/esecui/chart_thumb.png)](http://stevedower.id.au/images/esecui/chart.png) ![![](http://stevedower.id.au/images/esecui/vis_thumb.png)](http://stevedower.id.au/images/esecui/vis.png)

This screenshot is from a more recent revision, available by cloning the repository and building manually.

![![](http://stevedower.id.au/images/esecui/preview_thumb.png)](http://stevedower.id.au/images/esecui/preview.png)