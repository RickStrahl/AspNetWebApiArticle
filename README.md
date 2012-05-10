#Getting Started with ASP.NET Web API
[Sample code for Code Magazine Article](http://code-magazine.com/Article.aspx?quickid=1206081)

This project is a small example that demonstrates some of the core features  of the ASP.NET Web API. It contains the code samples referenced in the article above.

Most of the examples are contained in the GetAlbums.htm/js pages with the AlbumApiController.cs and AlbumRpcApiController.cs providing the backend Web API controllers. Additional examples are shown
in the SamplesApiController.

### Solution Configuration###
This project works with the ASP.NET Web Stack Nightlies from the alternate NuGet feed.
In order for this to work you need to add the nightly NuGet feed to this project.

* Goto Tools | Options | Package Manager
* Add a new Package Source called ASP.NET MVC/WebAPI Nightlies and use: http://www.myget.org/F/aspnetwebstacknightly/ for the URL
* Once you've added this package source rebuild your project
* NuGet packages should now download and install into your project
