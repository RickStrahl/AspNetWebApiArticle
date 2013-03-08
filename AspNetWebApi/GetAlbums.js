/// <reference path="scripts/jquery.js"/>
/// <reference path="scripts/ww.jquery.js"/>
/// <reference path="scripts/knockout-2.0.0.js" />
/// <reference path="scripts/knockout-mapping.js" />

page = {
    // display view for a single album instance
    albumView: null,
    // display view for a album array
    albumsView: null,
    // view for editing an album
    albumEditView: null,
    // flag that determines whether we're on the first bind
    editalbumFirstBind: true,
    // AJAX Service Proxy used to call WebAPI actions
    proxy: null
}

$(document).ready(function () {

    page.initialize();
    page.hookupEvents();    
});

page.initialize = function () {       
    page.proxy = new JsonProxy("");

    // status bar configuration (ww.jquery.js)
    showStatus({ autoClose: true });

    page.resizeFrame();
    setTimeout(function () { $(window).resize(page.resizeFrame) }, 30);

    // load albums when page loads
    page.loadAlbums(true);
}
page.hookupEvents = function() {
    // reload data from server
    $("#btnLoadData").click(page.loadAlbums);

    // Album click handling
    $(".albumlink,.album").live("click", page.loadAlbum);    

    // Delete button handling
    $(".removeimage").live("click", page.deleteAlbum);

    // post a static album to the server
    $("#btnSendAlbum").click(page.saveStaticAlbum)

    // show add new Album Window
    $("#btnAddNewAlbum").click(page.newAlbumDialog);

    $("#btnAddSong").click(page.addSong);

    $("#btnSaveAlbum").click(page.saveAlbum)

    $("#btnReloadAlbums").click(page.reloadAlbumsClick);
}
page.loadAlbums = function (showFirst) {

    page.proxy.invoke({
        route: "albums/",
        success: function (albums) {
            if (!page.albumsView) {
                // first time bind
                page.albumsView = ko.mapping.fromJS(albums);

                var view = { albums: page.albumsView };
                ko.applyBindings(view, $("#divAlbumContainer")[0]);
            }
            else
                ko.mapping.fromJS(albums, page.albumsView);

            var view = { albums: page.albumsView };

            // clear out list and make template visible
            var $albums = $(".album");
            //$albums.not(":first").remove();
            $albums.show();

            $("#divDialogStatus").text(albums.length + " albums");

            if (typeof (showFirst) == "boolean" && showFirst) {
                page.loadAlbum(albums[0].AlbumName);
            }
        }
    });

    //$.getJSON("albums/", function (albums) {
    //    if (!page.albumsView) {
    //        // first time bind
    //        page.albumsView = ko.mapping.fromJS(albums);

    //        var view = { albums: page.albumsView };
    //        ko.applyBindings(view, $("#divAlbumContainer")[0]);
    //    }
    //    else
    //        ko.mapping.fromJS(albums, page.albumsView);

    //    var view = { albums: page.albumsView };

    //    // clear out list and make template visible
    //    var $albums = $(".album");
    //    //$albums.not(":first").remove();
    //    $albums.show();

    //    $("#divDialogStatus").text(albums.length + " albums");

    //    if (typeof (showFirst) == "boolean" && showFirst) {                       
    //        page.loadAlbum(albums[0].AlbumName);
    //    }
    //});
}

page.loadAlbum = function (id) {
    if (typeof (id) != "string")
        id = $(this).data("id"); // title

    page.proxy.invoke({
        route: "albums/" + encodeURI(id),
        success: function (album) {            
            var $dialog = $("#divAlbumDialog");
            if (!page.albumView) {
                page.albumView = ko.mapping.fromJS(album);
                ko.applyBindings(page.albumView, $dialog[0]);
            }
            else            
                ko.mapping.fromJS(album, page.albumView);
        }
    });
    //$.getJSON("albums/" + encodeURI(id), function (album) {
    //    var $dialog = $("#divAlbumDialog");
    //    if (!page.albumView) {
    //        page.albumView = ko.mapping.fromJS(album);
    //        ko.applyBindings(page.albumView, $dialog[0]);
    //    }
    //    else            
    //        ko.mapping.fromJS(album, page.albumView);
    //});
}

page.getEmptyAlbum = function()  {    
    var obj =
    {
        AlbumName: "",
        Artist: "",
        YearReleased: 1970,
        AlbumImageUrl: "http://ecx.images-amazon.com/images/I/613yMZ7V32L._SL500_AA300_.jpg",
        AmazonUrl: "",
        Songs: [  ]
    }
    return obj;
}

page.newAlbumDialog = function () {
    $el = $("#divAddAlbumDialog");
    $el.show()
        .draggable()
        .closable()
        .centerInClient({ centerOnceOnly: true })

    // bind with empty data
    var data = page.getEmptyAlbum();
    
    // map to ko view model
    if (!page.albumEditView) {
        albumEditView = ko.mapping.fromJS(data);
        ko.applyBindings(albumEditView, $("#divAddAlbumDialog")[0]);
        //page.editalbumFirstBind = false;
    }
    else
        ko.mapping.fromJS(data, albumEditView);
}


page.deleteAlbum = function() {
    var $el = $(this).parent(".album");
    var txt = $el.find("a").text();

    page.proxy.invoke({
        route: "albums/" + encodeURIComponent(txt),
        verb: "DELETE",
        success: function (result) {
            $el.fadeOut("slow", function () { $(this).remove(); });
        }
    })

    //$.ajax({
    //    url: "albums/" + encodeURIComponent(txt),
    //    type: "DELETE",
    //    success: function (result) {
    //        $el.fadeOut().remove();
    //    },
    //    error: jqError
    //});
}

page.saveStaticAlbum = function() {
    var id = new Date().getTime().toString();
    var album = {
        "Id": id,
        "AlbumName": "Power Age",
        "Artist": "AC/DC",
        "Description": "PowerAge is one of AC/DCs best, with fine production quality that captures the AC/DC signature sound cleanly. Even though it's one of AC/DC's lighter rockers, it still remains one of the best that the band has produced over the years.",
        "YearReleased": 1976,
        "Entered": "2002-03-11T18:24:43.5580794-10:00",
        "AlbumImageUrl": "http://ecx.images-amazon.com/images/I/510oasvdvsL._SL500_AA300_.jpg",
        "AmazonUrl": "http://www.amazon.com/gp/product/B00008WT5E/ref=as_li_ss_tl?ie=UTF8&tag=westwindtechn-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B00008WT5E",
        "Songs": [
            { "SongName": "Rock 'n Roll Damnation", "SongLength": 3.12},
            { "SongName": "Downpayment Blues", "SongLength": 4.22 },
            { "SongName": "Riff Raff", "SongLength": 2.42 }
        ]
    }

    page.proxy.invoke({
        route: "albums/",
        verb: "POST",
        data: album,
        success: function (result) {            
            page.loadAlbums();
        },
        error: onPageError
    });


    //$.ajax(
    //{
    //    url: "albums/",
    //    type: "POST",
    //    contentType: "application/json",
    //    data: JSON.stringify(album),
    //    processData: false,
    //    beforeSend: function (xhr) {
    //        // not really required since JSON is default output format
    //        xhr.setRequestHeader("Accept", "application/json");
    //    },
    //    success: function (result) {
    //        // reload list of albums
    //        page.loadAlbums();
    //    },
    //    error: jqError
    //});
}
page.saveAlbum = function () {
    // turn into plain object
    var album = ko.toJS(albumEditView);

    page.proxy.invoke({
        route: "albums/",
        verb: "POST",
        callback: function (result) {
            // refresh album list
            page.loadAlbums();

            $("#divAddAlbumDialog").hide();
        }
    });

    //$.ajax(
    //{
    //    url: "albums/",
    //    type: "POST",
    //    contentType: "application/json",
    //    data: JSON.stringify(album),
    //    processData: false,
    //    beforeSend: function (xhr) {
    //        // not required since JSON is default output
    //        xhr.setRequestHeader("Accept", "application/json");
    //    },
    //    success: function (result) {
    //        // reload list of albums
    //        page.loadAlbums();

    //        $("#divAddAlbumDialog").hide();
    //    },
    //    error: jqError
    //});
}
page.addSong = function () {
    var name = $("#SongName").val();
    if (!name) {
        showStatus("Must provide a songname",4000);          
        $("#SongName").focus();
        return;
    }

    var song = { SongName: name , SongLength: $("#SongLength").val() };
    albumEditView.Songs.push(song);

    $("#SongName").val("").focus();
    $("#SongLength").val("")
}
page.reloadAlbumsClick =     function () {
    // force original albums to display            
    $.getJSON("albums/rpc/ResetAlbumData", function () {
        page.loadAlbums();
    });
}
page.resizeFrame = function () {
    var $main = $("#divMainContainer");
    $main.stretchToBottom();
    $("#divAlbumContainer,#divAlbumDialog").stretchToBottom({ container: $main });
    $(".scrollbox").stretchToBottom({ container: $("#divAlbumContainer"),bottomOffset: 20 });
}


function jqError(xhr, status,p3,p4) {    
    var error = JSON.parse(xhr.responseText);
    var err = error.message || error.Message || "Error";
    if (error.errors) {
        for (var i = 0; i < error.errors.length; i++) {
            err += "\r\n" + error.errors[i];
        }
    }
    showStatus(err, 4000);
}

JsonProxy = function (serviceUrl) {
    /// <summary>
    /// Generic Service Proxy class that can be used to
    /// call JSON Services generically using jQuery
    /// depends on JSON2.js modified for non-recent browsers.
    /// </summary>
    /// <param name="serviceUrl" type="string">
    /// The base Url of the service ready to accept the method name
    /// should contain trailing slash (or other URL separator ?,&)
    /// 
    /// Alternately pass an object that extends this object's properties
    /// serviceUrl,defaultErrorCallback,defaultTimeout,defaultVerb,defaultContentType
    /// </param>


    var self = this;

    if (!serviceUrl)
        serviceUrl = "";

    this.serviceUrl = serviceUrl;
    this.defaultErrorCallback = onPageError;
    this.defaultTimeout = 10000;
    this.defaultVerb = "GET";
    this.defaultContentType = "json"; // form or actual contenttype

    if (typeof options == "object")
        $.extend(this, options);

    this.invoke = function (options) {
        /// <summary>
        /// jQuery AJAX wrapper optimized for JSON Callbacks
        /// Also supports other types of callbacks
        /// </summary>    
        /// <param name="options" type="object">Parms: route, params, success, error, contentType (sent: json or form or content type), dataType (received: json,text), timeout (ms)</param>
        var opt = {
            // relative path to base URL: api/customer/id
            route: null,
            // allow overriding of the serviceUrl - if specified it overrides the route
            url: null,
            // parameters to be JSON encoded for POST/PUT, UrlEncoded for GET/DELETE etc.
            data: null,
            // Http Verb
            verb: self.defaultVerb,
            // Callback handler                    
            success: null,
            // Error callback handler
            error: self.defaultErrorCallback,
            // content type for data sent: json, form or actual content type
            contentType: self.defaultContentType,
            // return type of the data. json or text
            dataType: "json",
            // Request timeout in milliseconds
            timeout: self.defaultTimeout
        }
        $.extend(opt, options);    

        if (opt.contentType == "json")
            opt.contentType = "application/json";
        else if (opt.contentType == "form")
            opt.contentType = "application/x-www-form-urlencoded";

        var data = null;
        
        if (opt.data != null) {            
            if (opt.contentType == "application/json" && (opt.verb == "POST" || opt.verb == "PUT"))
                data = JSON.stringify(opt.data);
            else {
                if (typeof opt.data == "object")
                    data = $.param(opt.data);
                else
                    data = opt.data.toString();
            }
        }

        var url = opt.url ? opt.url : self.serviceUrl + opt.route;

        $.ajax({
            url: url,
            data: data,
            type: opt.verb,
            processData: false,
            contentType: opt.contentType,
            timeout: opt.timeout,
            dataType: opt.dataType,
            success: opt.success,
            error: function (xhr, status) {                
                var err = null;
                if (xhr.readyState == 4) {
                    var res = xhr.responseText;
                    if (res && res.charAt(0) == '{')
                        var err = JSON.parse(res);
                    if (!err) {
                        if (xhr.status && xhr.status != 200)
                            err = new CallbackException(xhr.status + " " + xhr.statusText);
                        else
                            err = new CallbackException("Callback Error: " + status);
                        err.detail = res;
                    }
                }
                if (!err)
                    err = new CallbackException("Callback Error: " + status);

                if (opt.error)
                    opt.error(err, self, xhr);
            }
        });
    }
}
CallbackException = function (message, detail) {
    // identifies as error
    this.isCallbackError = true;

    // message
    if (typeof (message) == "object") {
        if (message.message)
            this.message = message.message;
        else if (message.Message)
            this.message = message.Message;
    }
    else
        this.message = message;

    //optional detail/trace
    if (detail)
        this.detail = detail;
    else
        this.detail = null;
}