using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MusicAlbums
{
    public class AlbumContext : DbContext
    {             
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
    }


    public class AlbumContextDbInitializer : DropCreateDatabaseIfModelChanges<AlbumContext>
        //DropCreateDatabaseAlways<AlbumContext>    
    {
        protected override void Seed(AlbumContext context)
        {
            // Seed initial data
            SeedInitialData(context);
        }

        private void SeedInitialData(AlbumContext context)
        {
            var albums = Album.CreateAlbumData();
            albums.ForEach(alb => context.Albums.Add(alb));
            context.SaveChanges();
        }
    }
 
}
