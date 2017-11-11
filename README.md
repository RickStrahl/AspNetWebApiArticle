# Getting Started with ASP.NET Web API
[Sample code for Code Magazine Article](http://www.west-wind.com/weblog/posts/2012/Aug/21/An-Introduction-to-ASPNET-Web-API)

This project is the source code for the above article and provides a small example application that demonstrates some of the core features of the ASP.NET Web API. It contains the code samples referenced in the article above.

Most of the examples are contained in the GetAlbums.htm/js pages with the AlbumApiController.cs and AlbumRpcApiController.cs providing the backend Web API controllers. Additional examples are shown
in the SamplesApiController.

### Solution Configuration
This project works with the ASP.NET MVC Release using .NET 4.0 runtime. 

This is a VS2012 project, but it opens fine and works in VS2010.

Project should run as is after initial compilation in Visual Studio. Note
that NuGet packages are not shipped and NuGet Package Restore is enabled
so you need to be online (or have NuGet packages cached) in order for
the project to pull in all the MVC/WebAPI NuGet references.
