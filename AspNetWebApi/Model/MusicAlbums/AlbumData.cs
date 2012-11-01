using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicAlbums;

namespace MusicAlbums
{
    // Sample data                  
    public static class AlbumData
    {
        // sample data - static list
        public static List<Album> Current = CreateSampleAlbumData();

        /// <summary>
        /// Create some sample data
        /// </summary>
        /// <returns></returns>
        public static List<Album> CreateSampleAlbumData()
        {
            List<Album> albums = new List<Album>();
            var album = new Album()
            {
                Artist = "Henry Rollins Band",
                AlbumName = "End of the Silence",
                YearReleased = 1992,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/51FO3rb1tuL._SL160_AA160_.jpg",
                AmazonUrl = "http://www.amazon.com/End-Silence-Rollins-Band/dp/B0000040OX/ref=sr_1_5?ie=UTF8&qid=1302232195&sr=8-5",
                Description = "This album is so hard and sooo heavy that it hurts, but that is the idea."
            };
            albums.Add(album);

            album.AddSong("Low Self Opinion", "5:24");
            album.AddSong("Grip", "4:51");
            album.AddSong("Tearing", "5:56");
            album.AddSong("You didn't need", "5:30");
            album.AddSong("Almost Real", "8:05");
            album.AddSong("Obscene", "8:46");
            album.AddSong("What do you do", "7:25");
            album.AddSong("Blues Jam", "11:49");
            album.AddSong("Another Life", "4:53");
            album.AddSong("Just like You", "10:59");

            album = new Album()
            {
                Artist = "Henry Rollins Band",
                AlbumName = "Weight",
                YearReleased = 1994,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/41eHEGu8NML._AA115_.jpg",
                AmazonUrl = "http://www.amazon.com/Weight-Rollins-Band/dp/B0000040P3/ref=sr_1_1?ie=UTF8&s=music&qid=1302232341&sr=8-1",
                Description = "Weight was the Rollins Band's breakthrough into the mainstream and remains their best album."
            };
            albums.Add(album);

            album.AddSong("Disconnect", "4:57");
            album.AddSong("Fool", "4:25");
            album.AddSong("Icon", "3:41");
            album.AddSong("Civilized", "3:45");
            album.AddSong("Divine", "4:01");
            album.AddSong("Liar", "6:34");
            album.AddSong("Step Back", "3:58");
            album.AddSong("Wrong Man", "4:19");
            album.AddSong("Volume 4", "4:39");
            album.AddSong("Tired", "3:47");
            album.AddSong("Alien Blueprint", "3:44");
            album.AddSong("Shine", "5:26");

            album = new Album()
            {
                Artist = "AC/DC",
                AlbumName = "Dirty Deeds Done Dirt Cheap",
                YearReleased = 1976,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/61kTaH-uZBL._AA115_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B00008BXJ4/ref=as_li_ss_tl?ie=UTF8&tag=westwindtechn-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B00008BXJ4",
                Description = "While Dirty Deeds Done Dirt Cheap sounds like every other AC/DC album, it is distinguished by a lyrical puerility spectacular even by Bon Scott's standards."
            };
            albums.Add(album);

            album.AddSong("Dirty Deeds Done Dirt Cheap", "4:11");
            album.AddSong("Love at First Feel", "3:10");
            album.AddSong("Big Balls", "2:38");
            album.AddSong("Rocker", "2:49");
            album.AddSong("Problem Child", "5:44");
            album.AddSong("There's going to be some Rocking", "3:17");
            album.AddSong("Ain't no fun (waiting around to be a millionaire", "7:29");
            album.AddSong("Ride on", "5:49");
            album.AddSong("Squealer", "5:14");

            album = new Album()
            {
                Artist = "Condemned?",
                AlbumName = "Condemned 2 Death",
                YearReleased = 2011,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/61CMcYAP%2BQL._SL500_AA280_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B004MC5YEY/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B004MC5YEY&linkCode=as2&tag=westwindtechn-20",
                Description = "Rocking out after a hefty hiatus for a release on Nuclear Blast Records, CONDEMNED? is proving that although the years may have passed, they can still thrash with the best of them."
            };
            albums.Add(album);
            album.AddSong("Big Time Game Hunting", "2:40");
            album.AddSong("Aggressive System", "1:22");
            album.AddSong("Thoughts of Equality", "3:57");
            album.AddSong("Practicing for War", "1:45");
            album.AddSong("Crutch", "0:37");
            album.AddSong("Anti-Social", "2:35");
            album.AddSong("Cavern in Time", "2:56");
            album.AddSong("Crucified System", "3:05");
            album.AddSong("Emotional Blur", "3:25");
            album.AddSong("Cleansing Pool", "2:26");
            album.AddSong("Ocean", "1:25");
            album.AddSong("Save Thy Brother", "2:57");
            album.AddSong("D-Day", "0:46");
            album.AddSong("When Worlds Collide", "3:16");

            album = new Album()
            {
                Artist = "Foo Fighters",
                AlbumName = "Echoes, Silence, Patience & Grace",
                YearReleased = 2007,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/41mtlesQPVL._SL500_AA280_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B000UFAURI/ref=as_li_ss_tl?ie=UTF8&tag=westwindtechn-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B000UFAURI",
                Description = "Undeniably, The Foo Fighters' new album is, to its core, rock music the way it was meant to be."
            };
            albums.Add(album);

            album.AddSong("The Pretender", "4:29");
            album.AddSong("Let it Die", "4:05");
            album.AddSong("Erase/Replay", "4:13");
            album.AddSong("Long Road to Ruin", "3:44");
            album.AddSong("Come Alive", "5:10");
            album.AddSong("Stranger Things have Happened", "5:21");
            album.AddSong("Cheer up Boys (Your Make up is Runing)", "3:41");
            album.AddSong("Summer's End", "4:38");
            album.AddSong("Ballad of the Beaconsfield Miners", "2:32");
            album.AddSong("Statues", "3;47");
            album.AddSong("But, Honestly", "4:35");
            album.AddSong("Home", "4:52");
            album.AddSong("Once & FOr All (Demo)", "3:47");


            album = new Album()
            {
                Artist = "Corrosion of Conformity",
                AlbumName = "Animosity",
                YearReleased = 1986,
                AlbumImageUrl = "http://ecx.images-amazon.com/images/I/61ngIfFbnyL._SL500_AA300_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B000001C88/ref=as_li_ss_tl?ie=UTF8&tag=westwindtechn-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B000001C88",
                Description = "One of the best punk metal cross-over albums ever made and maybe *the* album that defined the genre."
            };
            albums.Add(album);
            album.AddSong("Loss For Words", "4:05");
            album.AddSong("Mad World", "1:53");
            album.AddSong("Consumed", "2:52");
            album.AddSong("Holier", "2:26");
            album.AddSong("Positive Outlook", "3:04");
            album.AddSong("Prayer", "2:24");
            album.AddSong("Intervention", "2:25");
            album.AddSong("Kiss of Death", "1:31");
            album.AddSong("Hungry Child", "1:19");
            album.AddSong("Animosity", "4:16");


            album = new Album()
            {
                Artist = "Tool",
                AlbumName = "Undertow",
                YearReleased = 1993,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41mJt4IVkhL._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B000000993/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B000000993&linkCode=as2&tag=westwindtechn-20",
                Description = "Undertow is Tool's most musically adventurous album, lacking the occasionally numbing sameness of Aenima, and with considerably more sophistication than their previous work."
            };
            albums.Add(album);
            album.AddSong("Intolerance", "4:54");
            album.AddSong("Prison Sex", "4:56");
            album.AddSong("Sober", "5:06");
            album.AddSong("Bottom", "7:14");
            album.AddSong("Crawl Away", "5:29");
            album.AddSong("Swamp Song", "5:31");
            album.AddSong("Undertow", "5:21");
            album.AddSong("4 Degrees", "6:02");
            album.AddSong("Flood", "7:45");
            album.AddSong("Disgustipated", "15:47");

            album = new Album()
            {
                Artist = "Neurosis",
                AlbumName = "Enemy of the Sun",
                YearReleased = 1999,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61acQQ4IwZL._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B00000JQFB/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B00000JQFB&linkCode=as2&tag=westwindtechn-20",
                Description = "Enemy of the Sun is a carefully sustained sonic assault of epic proportions."
            };
            albums.Add(album);
            album.AddSong("Lost", "9:41");
            album.AddSong("Raze the Stray", "8:42");
            album.AddSong("Burning the Flesh in Year of Pig", "1:37");
            album.AddSong("Cold Ascending", "3:44");
            album.AddSong("Lexicon", "6:32");
            album.AddSong("Enemy of the Sun", "7:33");
            album.AddSong("The Time of the Beast", "7:59");
            album.AddSong("Cleanse", "15:53");
            album.AddSong("Takehnase", "7:44");
            album.AddSong("Cleanse II", "6:45");

            album = new Album()
            {
                Artist = "Soundgarden",
                AlbumName = "Superunknown",
                YearReleased = 1999,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41P1443RDHL._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B000002G2B/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B000002G2B&linkCode=as2&tag=westwindtechn-20",
                Description = "Fell on Black Days indeed. Seattle sludge slingers Soundgarden made a living out of cathartic, woe-is-me wailing, but this wallowing in grim depression ironically proved to be the band's most uplifting career effort."
            };
            albums.Add(album);
            album.AddSong("Let me drown", "3:51");
            album.AddSong("My Wave", "5:12");
            album.AddSong("Fell on black days", "4:42");
            album.AddSong("Mailman", "4:25");
            album.AddSong("Superunknown", "5:06");
            album.AddSong("Head Down", "6:08");
            album.AddSong("Black Hole Sun", "5:18");
            album.AddSong("Spoonman", "4:06");
            album.AddSong("Limo Wreck", "5:47");
            album.AddSong("The day I tried to live", "5:19");
            album.AddSong("KickStand", "1:34");
            album.AddSong("Fresh Tendrils", "4:16");
            album.AddSong("4th of July", "5:08");
            album.AddSong("Half", "2:14");
            album.AddSong("Like Suicide", "7:01");

            album = new Album()
            {
                Artist = "Prong",
                AlbumName = "Cleansing",
                YearReleased = 1996,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51Jy8EoV8kL._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B0000028TD/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B0000028TD&linkCode=as2&tag=westwindtechn-20",
                Description = "Prong are the evolution of Killing Joke into metal. Even though Killing Joke are still around and still great, Prong took the torch they dropped in the late 80's and created a great mix of Metal and industrial like starkness."
            };
            albums.Add(album);
            album.AddSong("Another Worldly Device", "3:25");
            album.AddSong("Whose fist is this anyway?", "4:41");
            album.AddSong("Snap your fingers snap your neck", "4:41");
            album.AddSong("Cut Rate", "4:52");
            album.AddSong("One Outnumbers", "4:57");
            album.AddSong("Out of this misery", "4:57");
            album.AddSong("No question", "5:18");
            album.AddSong("Spoonman", "4:06");
            album.AddSong("Limo Wreck", "5:47");
            album.AddSong("The day I tried to live", "4:17");
            album.AddSong("Not of this earth", "6:23");
            album.AddSong("Home rule", "3:58");
            album.AddSong("Sublime", "3:52");
            album.AddSong("Test", "6:39");

            album = new Album()
            {
                Artist = "Motorhead",
                AlbumName = "Ace of Spades",
                YearReleased = 1980,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/618Zuqc4J5L._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B00005NHO2/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B00005NHO2&linkCode=as2&tag=westwindtechn-20",
                Description = "Motörhead have never JUST been the best rock'n'roll band in the world. They've never JUST been the loudest. Or the hardest. Or the toughest. Or the bad-ass-est. No...Motörhead are also a lifestyle."
            };
            albums.Add(album);
            album.AddSong("Ace of Spades", "2:47");
            album.AddSong("Love me like a reptile", "3:21");
            album.AddSong("Shoot you in the back", "2:37");
            album.AddSong("Live to wind", "3:35");
            album.AddSong("Fast and Loose", "4:57");
            album.AddSong("We are the Roadcrew", "4:57");
            album.AddSong("Fire Fire", "5:18");
            album.AddSong("Jailbait", "4:06");
            album.AddSong("Dance", "5:47");
            album.AddSong("The Chase is better than the catch", "4:17");
            album.AddSong("Bite the Bullet", "1:38");
            album.AddSong("The Hammer", "2:45");

            album = new Album()
            {
                Artist = "UK Subs",
                AlbumName = "Crash Course",
                YearReleased = 1979,
                AlbumImageUrl = "https://images-na.ssl-images-amazon.com/images/I/613jNLYLRcL._SL110_.jpg",
                AmazonUrl = "http://www.amazon.com/gp/product/B0091JCXXS/ref=as_li_ss_tl?ie=UTF8&camp=1789&creative=390957&creativeASIN=B0091JCXXS&linkCode=as2&tag=westwindtechn-20",
                Description = "The Subs are the quintessential UK Punk band of the early days, and this live recording captures the feel intensity of the mid-seventies British Punk movement perfectly."
            };
            albums.Add(album);
            album.AddSong("CID", "2:47");
            album.AddSong("I couldn't be you", "3:21");
            album.AddSong("I live in a car", "2:37");
            album.AddSong("Tomorrows Girls", "3:35");
            album.AddSong("Kicks", "4:57");
            album.AddSong("Left for Dead", "4:57");
            album.AddSong("Rat Race", "5:18");
            album.AddSong("War Head", "4:06");
            album.AddSong("New York State Police", "5:47");
            album.AddSong("Telephone Numbers", "4:17");
            album.AddSong("Public Servant", "1:38");
            album.AddSong("Rockers", "2:45");
            album.AddSong("Public Servant", "1:31");
            album.AddSong("Crash Course", "2:43");
            album.AddSong("Emotional Blackmail", "3:35");

            return albums;

            return albums;
        }

    }
}