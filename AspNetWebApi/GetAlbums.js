/// <reference path="scripts/jquery.js"/>
/// <reference path="scripts/ww.jquery.js"/>
/// <reference path="scripts/knockout-2.0.0.js" />
/// <reference path="scripts/knockout-mapping.js" />

globals = {    
    // display view for a single album instance
    albumView: null,
    // display view for a album array
    albumsView: null,
    // view for editing an album
    albumEditView: null,
    // flag that determines whether we're on the first bind
    editalbumFirstBind: true,
}

$(document).ready(function () {

    showStatus({ autoHide: true });

    // load albums when page loads
    loadAlbums();

    // reload data from server
    $("#btnLoadData").click(loadAlbums);

    // Album click handling
    $(".albumlink").live("click", function () {
        var title = $(this).data("id"); // title
        $.getJSON("albums/" + encodeURI(title), function (album) {
            var $dialog = $("#divAlbumDialog");
            if (!globals.albumView) {
                globals.albumView = ko.mapping.fromJS(album);
                ko.applyBindings(globals.albumView, $dialog[0]);
                $dialog.centerInClient();
            }
            else
                ko.mapping.fromJS(album, globals.albumView);

            $dialog
                .show()
                .closable()
                .draggable();
        });
    });

    // Delete button handling
    $(".removeimage").live("click", function () {
        var $el = $(this).parent(".album");
        var txt = $el.find("a").text();
        $.ajax({
            url: "albums/" + encodeURIComponent(txt),
            type: "Delete",
            success: function (result) {
                $el.fadeOut().remove();
            },
            error: jqError
        });
    });


    // post a static album to the server
    $("#btnSendAlbum").click(function () {
        var id = new Date().getTime().toString();
        var album = {
            "Id": id,
            "AlbumName": "Power Age",
            "Artist": "AC/DC",
            "YearReleased": 1977,
            "Entered": "2002-03-11T18:24:43.5580794-10:00",
            "AlbumImageUrl": "http://ecx.images-amazon.com/images/I/510oasvdvsL._SL500_AA300_.jpg",
            "AmazonUrl": "http://www.amazon.com/gp/product/B00008WT5E/ref=as_li_ss_tl?ie=UTF8&tag=westwindtechn-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B00008WT5E",
            "Songs": [
                { "SongName": "Rock 'n Roll Damnation" },
                { "SongName": "Downpayment Blues" },
                { "SongName": "Riff Raff" }
            ]
        }

        $.ajax(
        {
            url: "albums/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(album),
            processData: false,
            beforeSend: function (xhr) {
                // not required since JSON is default output
                xhr.setRequestHeader("Accept", "application/json");
            },
            success: function (result) {
                // reload list of albums
                loadAlbums();
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error";
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).message;
                alert(err);
            }
        });
    });



    // show add new Album Window
    $("#btnAddNewAlbum").click(function () {
        $el = $("#divAddAlbumDialog");
        $el.show()
            .draggable()
            .closable()
            .centerInClient( {centerOnceOnly: true} )

        // bind with empty data
        var data = getEmptyAlbum();

        // map to ko view model
        if (globals.editalbumFirstBind) {
            albumEditView = ko.mapping.fromJS(data);            
            ko.applyBindings(albumEditView, $("#divAddAlbumDialog")[0]);
            globals.editalbumFirstBind = false;
        }
        else 
            ko.mapping.fromJS(data, albumEditView);        
    });


    $("#btnAddSong").click(function () {

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
    });

    $("#btnSaveAlbum").click(function () {
        // turn into plain object
        var album = ko.toJS(albumEditView);

        $.ajax(
        {
            url: "albums/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(album),
            processData: false,
            beforeSend: function (xhr) {
                // not required since JSON is default output
                xhr.setRequestHeader("Accept", "application/json");
            },
            success: function (result) {
                // reload list of albums
                loadAlbums();

                $("#divAddAlbumDialog").hide();
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error";
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).message;
                alert(err);
            }
        });
    });


    $("#btnReloadAlbums").click(function () {
        // force original albums to display            
        $.getJSON("albums/rpc/ResetAlbumData", function () {
            loadAlbums();
        });
    });




});



function loadAlbums() {
    $.getJSON("albums/", function (albums) {
        if (!globals.albumsView) {
            // first time bind
            globals.albumsView = ko.mapping.fromJS(albums);
            var view = { albums: globals.albumsView };
            ko.applyBindings(view, $("#divAlbumContainer")[0]);
        }
        else
            globals.albumsView = ko.mapping.fromJS(albums, globals.albumsView);

        var view = { albums: globals.albumsView };

        // clear out list and make template visible
        var $albums = $(".album");
        //$albums.not(":first").remove();
        $albums.show();


        $("#divDialogStatus").text(albums.length + " albums");
    });
}

function getEmptyAlbum()  {    
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


function jqError(xhr, status) {
    var err = "Error";
    if (xhr.responseText &&
        xhr.responseText[0] == "{")
        err = JSON.parse(xhr.responseText).message;
    showStatus(err, 4000);
}



