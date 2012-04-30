using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicAlbums
{

public class Song
{    
    public string AlbumId { get; set; }
    [Required,StringLength(80)]
    public string SongName { get; set; }
    [StringLength(5)]
    public string SongLength { get; set; }

}

public class Album
{
    public string Id  {get; set;}
    [Required,StringLength(80)]
    public string AlbumName {get; set; }
    [StringLength(80)]
    public string Artist { get; set; }        
    public int YearReleased {get; set; }
    public DateTime Entered { get; set; }
    [StringLength(150)]        
    public string AlbumImageUrl { get; set; }
    [StringLength(200)]
    public string AmazonUrl { get; set; }                
        
    public virtual List<Song> Songs {get; set;}
        
    public Album()
    {
        Songs = new List<Song>();
        Entered = DateTime.Now;

        // Poor man's unique Id off GUID hash
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


        }
       
}