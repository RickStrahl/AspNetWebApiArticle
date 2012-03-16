/// <reference path="scripts/jquery.js"/>
/// <reference path="scripts/ww.jquery.js"/>
/// <reference path="scripts/knockout-2.0.0.js" />
/// <reference path="scripts/knockout-mapping.js" />

albumview = null;
albumsview = null;
albumEditView = null;

$(document).ready(function () {

    // load albums when page loads
    loadAlbums();

    // reload data from server
    $("#btnLoadData").click(loadAlbums);

    function loadAlbums() {
        $.getJSON("albums/", function (albums) {

            if (!albumsview) {
                // first time bind
                albumsview = ko.mapping.fromJS(albums);
                var view = { albums: albumsview };
                ko.applyBindings(view, $("#divAlbumContainer")[0]);
            }
            else
                albumsview = ko.mapping.fromJS(albums, albumsview);

            var view = { albums: albumsview };

            // clear out list and make template visible
            var $albums = $(".album");
            //$albums.not(":first").remove();
            $albums.show();


            $("#divDialogStatus").text(albums.length + " albums");
        });
    }


    // Album click handling
    $(".albumlink").live("click", function () {
        var title = $(this).data("id"); // title
        $.getJSON("albums/" + encodeURI(title), function (album) {
            var $dialog = $("#divAlbumDialog");
            if (!albumview) {
                albumview = ko.mapping.fromJS(album);
                ko.applyBindings(albumview, $dialog[0]);
                $dialog.centerInClient();
            }
            else
                ko.mapping.fromJS(album, albumview);

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



    // show add window
    $("#btnAddNewAlbum").click(function () {
        $el = $("#divAddAlbumDialog");
        $el.show()
                .draggable()
                .closable();

        if (!$el.data("centered")) {
            $el.centerInClient()
               .data("centered", true);
        }


        var data = getEmptyAlbum();


        if (globals.editalbumFirstBind) {
            albumEditView = ko.mapping.fromJS(data);
            //albumEditView.Songs = ko.observableArray(albumEditView.Songs());
            ko.applyBindings(albumEditView, $("#divAddAlbumDialog")[0]);
            globals.editalbumFirstBind = false;
        }
        else {
            ko.mapping.fromJS(data, albumEditView);
        }

    });

    $("#btnAddSong").click(function () {

        var song = { SongName: $("#SongName").val(), SongLength: $("#SongLength").val() };
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

globals = {
    editalbum: getEmptyAlbum(),
    editalbumFirstBind: true
}

function getEmptyAlbum()  {    
    var obj =
    {
        AlbumName: "New Album",
        ArtistName: "Artist",
        YearReleased: "1900",
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
}



