using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MusicAlbums
{

    public class Song
    {
        [ForeignKey("Album")]
        public string AlbumId { get; set; }
        [Required, MaxLength(80)]
        public string SongName { get; set; }
        [MaxLength(5)]
        public string SongLength { get; set; }
    }

    public class Album
    {
        public string Id  {get; set;}
        [Required,MaxLength(80)]
        public string AlbumName {get; set; }
        [MaxLength(80)]
        public string Artist { get; set; }        
        public int? YearReleased {get; set; }
        public DateTime Entered { get; set; }
        [MaxLength(128)]        
        public string AlbumImageUrl { get; set; }
        [MaxLength(128)]
        public string AmazonUrl { get; set; }                
        public virtual IList<Song> Songs {get; set;}
        
        public Album()
        {
            Songs = new List<Song>();
            Entered = DateTime.Now;
            Id = Guid.NewGuid().GetHashCode().ToString("x");
        }

        public void AddSong(string songName, string songLength = null)
        {
            this.Songs.Add(new Song()
            {
                AlbumId = this.Id,
                SongName = songName,
                SongLength = songLength                  
            });
        }


        public static List<Album> CreateSampleAlbumData()
        {            
            List<Album> albums = new List<Album>();
            var album = new Album()
            {
                Artist = "Henry Rollins Band",
                AlbumName = "End of the Silence",
                YearReleased = 1992,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/51FO3rb1tuL._SL160_AA160_.jpg",
                AmazonUrl = "http://www.amazon.com/End-Silence-Rollins-Band/dp/B0000040OX/ref=sr_1_5?ie=UTF8&qid=1302232195&sr=8-5"
            };
            albums.Add(album);

            album.AddSong("Low Self Opinion", "5:24");
            album.AddSong("Grip","4:51");
            album.AddSong("Tearing","5:56");
            album.AddSong("You didn't need", "5:30");
            album.AddSong("Almost Real" );
            album.AddSong("Obscene" );
            album.AddSong("What do you do");
            album.AddSong("Another Life");
            album.AddSong("Just like You");
            

            album = new Album()
            {
                Artist = "Henry Rollins Band",
                AlbumName = "Weight",
                YearReleased = 1994,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/41eHEGu8NML._AA115_.jpg",
                AmazonUrl = "http://www.amazon.com/Weight-Rollins-Band/dp/B0000040P3/ref=sr_1_1?ie=UTF8&s=music&qid=1302232341&sr=8-1"
            };
            albums.Add(album);

            album.AddSong("Disconnect","4:57");
            album.AddSong("Fool","4:25");
            album.AddSong("Icon","3:41");
            album.AddSong("Civilized","3:45");
            album.AddSong("Divine","4:01");
            album.AddSong("Liar","6:34");
            album.AddSong("Step Back","3:58");
            album.AddSong("Wrong Man","4:19");
            album.AddSong("Volume 4","4:39");
            album.AddSong("Tired","3:47");
            album.AddSong("Alien Bluepring","3:44");
            album.AddSong("Shine","5:26");

            album = new Album()
            {
                Artist = "AC/DC",
                AlbumName = "Dirty Deeds Done Dirt Cheap",
                YearReleased = 1976,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/61kTaH-uZBL._AA115_.jpg",
                AmazonUrl = "http://www.amazon.com/Dirty-Deeds-Done-Dirt-Cheap/dp/B00008BXJ4/ref=sr_1_1?s=music&ie=UTF8&qid=1302232491&sr=1-1"
            };
            albums.Add(album);

            album.AddSong("Dirty Deeds Done Dirt Cheap","4:11");
            album.AddSong("Love at First Feel","3:10");
            album.AddSong("Big Balls", "2:38");
            album.AddSong("Rocker", "2:49");
            album.AddSong("Problem Child", "5:44");
            album.AddSong("There's going to be some Rocking", "3:17");
            album.AddSong("Ain't no fun (waiting around to be a millionaire", "7:29");
            album.AddSong("Ride on", "5:49");
            album.AddSong("Squealer", "5:14");

            return albums;
        }                
    }
       
}