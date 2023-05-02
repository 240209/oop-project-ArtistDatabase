using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace projekt_ArtistDatabase
{
    static class DatabaseHandler
    {
        /// <summary>
        /// Inserts sample data into music artist database
        /// </summary>
        static public void InsertSampleData()
        {
            var trash_metal = new Genre { Name = "Trash Metal" };
            var metal = new Genre { Name = "Metal" };
            var rock = new Genre { Name = "Rock" };
            var pop = new Genre { Name = "Pop" };
            var punk = new Genre { Name = "Punk" };
            var psychedelic_rock = new Genre { Name = "Psychedelic Rock" };
            var alternative_rock = new Genre { Name = "Alternative Rock" };
            var glam_rock = new Genre { Name = "Glam Rock" };
            var hard_rock = new Genre { Name = "Hard Rock" };
            var grunge = new Genre { Name = "Grunge" };
            var country = new Genre { Name = "Country" };
            var funk_rock = new Genre { Name = "Funk Rock" };
            var hip_hop = new Genre { Name = "Hip-Hop" };
            var RnB = new Genre { Name = "RnB" };

            var metallica = new Artist
            {
                Name = "Metallica",
                Genres = { trash_metal, metal },
                Albums =
            {
                new Album { Name = "Kill 'Em All", Year = 1983 },
                new Album { Name = "Ride the Lightning", Year = 1984 },
                new Album { Name = "Master of Puppets", Year = 1986 },
                new Album { Name = "...And Justice for All", Year = 1988 },
                new Album { Name = "Metallica (Black Album)", Year = 1991 },
                new Album { Name = "Load", Year = 1996 },
                new Album { Name = "Reload", Year = 1997 },
                new Album { Name = "St. Anger", Year = 2003 },
                new Album { Name = "Death Magnetic", Year = 2008 },
                new Album { Name = "Hardwired... to Self-Destruct", Year = 2016 }
            }
            };
            var queen = new Artist
            {
                Name = "Queen",
                Genres = { rock, glam_rock, hard_rock },
                Albums =
                {
                    new Album { Name = "Queen", Year = 1973 },
                    new Album { Name = "Queen II", Year = 1974 },
                    new Album { Name = "Sheer Heart Attack", Year = 1974 },
                    new Album { Name = "A Night at the Opera", Year = 1975 },
                    new Album { Name = "A Day at the Races", Year = 1976 },
                    new Album { Name = "News of the World", Year = 1977 },
                    new Album { Name = "Jazz", Year = 1978 },
                    new Album { Name = "The Game", Year = 1980 },
                    new Album { Name = "Hot Space", Year = 1982 },
                    new Album { Name = "The Works", Year = 1984 },
                    new Album { Name = "A Kind of Magic", Year = 1986 },
                    new Album { Name = "The Miracle", Year = 1989 },
                    new Album { Name = "Innuendo", Year = 1991 },
                    new Album { Name = "Made in Heaven", Year = 1995 }
                }
            };

            var pinkFloyd = new Artist
            {
                Name = "Pink Floyd",
                Genres = { rock, psychedelic_rock },
                Albums =
                {
                    new Album { Name = "The Piper at the Gates of Dawn", Year = 1967 },
                    new Album { Name = "A Saucerful of Secrets", Year = 1968 },
                    new Album { Name = "More", Year = 1969 },
                    new Album { Name = "Ummagumma", Year = 1969 },
                    new Album { Name = "Atom Heart Mother", Year = 1970 },
                    new Album { Name = "Meddle", Year = 1971 },
                    new Album { Name = "Obscured by Clouds", Year = 1972 },
                    new Album { Name = "The Dark Side of the Moon", Year = 1973 },
                    new Album { Name = "Wish You Were Here", Year = 1975 },
                    new Album { Name = "Animals", Year = 1977 },
                    new Album { Name = "The Wall", Year = 1979 },
                    new Album { Name = "The Final Cut", Year = 1983 },
                    new Album { Name = "A Momentary Lapse of Reason", Year = 1987 },
                    new Album { Name = "The Division Bell", Year = 1994 },
                    new Album { Name = "The Endless River", Year = 2014 }
                }
            };
            var avril_lavigne = new Artist
            {
                Name = "Avril Lavigne",
                Genres = { pop, punk },
                Albums =
                {
                    new Album { Name = "Let Go", Year = 2002 },
                    new Album { Name = "Under My Skin", Year = 2004 },
                    new Album { Name = "The Best Damn Thing", Year = 2007 },
                    new Album { Name = "Goodbye Lullaby", Year = 2011 },
                    new Album { Name = "Avril Lavigne", Year = 2013 },
                    new Album { Name = "Head Above Water", Year = 2019 },
                    new Album { Name = "Love Sux", Year = 2022 }
                }
            };
            var acdc = new Artist
            {
                Name = "AC/DC",
                Genres = { rock, hard_rock },
                Albums =
                    {
                        new Album { Name = "High Voltage", Year = 1975 },
                        new Album { Name = "Dirty Deeds Done Dirt Cheap", Year = 1976 },
                        new Album { Name = "Let There Be Rock", Year = 1977 },
                        new Album { Name = "Highway to Hell", Year = 1979 },
                        new Album { Name = "Back in Black", Year = 1980 },
                        new Album { Name = "For Those About to Rock We Salute You", Year = 1981 },
                        new Album { Name = "Flick of the Switch", Year = 1983 },
                        new Album { Name = "Fly on the Wall", Year = 1985 },
                        new Album { Name = "Blow Up Your Video", Year = 1988 },
                        new Album { Name = "The Razor's Edge", Year = 1990 },
                        new Album { Name = "Ballbreaker", Year = 1995 },
                        new Album { Name = "Stiff Upper Lip", Year = 2000 },
                        new Album { Name = "Black Ice", Year = 2008 },
                        new Album { Name = "Rock or Bust", Year = 2014 },
                        new Album { Name = "Power Up", Year = 2020 }
                    }
            };
            var nirvana = new Artist
            {
                Name = "Nirvana",
                Genres = { grunge, alternative_rock },
                Albums =
                    {
                        new Album { Name = "Bleach", Year = 1989 },
                        new Album { Name = "Nevermind", Year = 1991 },
                        new Album { Name = "In Utero", Year = 1993 }
                    }
            };
            var rhcp = new Artist
            {
                Name = "Red Hot Chili Peppers",
                Genres = { funk_rock, alternative_rock },
                Albums =
                    {
                        new Album { Name = "The Red Hot Chili Peppers", Year = 1984 },
                        new Album { Name = "Freaky Styley", Year = 1985 },
                        new Album { Name = "The Uplift Mofo Party Plan", Year = 1987 },
                        new Album { Name = "Mother's Milk", Year = 1989 },
                        new Album { Name = "Blood Sugar Sex Magik", Year = 1991 },
                        new Album { Name = "One Hot Minute", Year = 1995 },
                        new Album { Name = "Californication", Year = 1999 },
                        new Album { Name = "By the Way", Year = 2002 },
                        new Album { Name = "Stadium Arcadium", Year = 2006 },
                        new Album { Name = "I'm with You", Year = 2011 },
                        new Album { Name = "The Getaway", Year = 2016 }
                    }
            };
            var black_sabbath = new Artist
            {
                Name = "Black Sabbath",
                Genres = { metal, rock },
                Albums =
                    {
                        new Album { Name = "Black Sabbath", Year = 1970 },
                        new Album { Name = "Paranoid", Year = 1970 },
                        new Album { Name = "Master of Reality", Year = 1971 },
                        new Album { Name = "Vol. 4", Year = 1972 },
                        new Album { Name = "Sabbath Bloody Sabbath", Year = 1973 },
                        new Album { Name = "Sabotage", Year = 1975 },
                        new Album { Name = "Technical Ecstasy", Year = 1976 },
                        new Album { Name = "Never Say Die!", Year = 1978 },
                        new Album { Name = "Heaven and Hell", Year = 1980 },
                        new Album { Name = "Mob Rules", Year = 1981 },
                        new Album { Name = "Born Again", Year = 1983 },
                        new Album { Name = "Seventh Star", Year = 1986 },
                        new Album { Name = "The Eternal Idol", Year = 1987 },
                        new Album { Name = "Headless Cross", Year = 1989 },
                        new Album { Name = "Tyr", Year = 1990 },
                        new Album { Name = "Dehumanizer", Year = 1992 },
                        new Album { Name = "13", Year = 2013 },
                    }
            };
            var eminem = new Artist
            {
                Name = "Eminem",
                Genres = { hip_hop },
                Albums =
                    {
                        new Album { Name = "The Slim Shady LP", Year = 1999 },
                        new Album { Name = "The Marshall Mathers LP", Year = 2000 },
                        new Album { Name = "The Eminem Show", Year = 2002 },
                        new Album { Name = "Encore", Year = 2004 },
                        new Album { Name = "Relapse", Year = 2009 },
                        new Album { Name = "Recovery", Year = 2010 },
                        new Album { Name = "The Marshall Mathers LP 2", Year = 2013 },
                        new Album { Name = "Revival", Year = 2017 },
                        new Album { Name = "Kamikaze", Year = 2018 },
                        new Album { Name = "Music to Be Murdered By", Year = 2020 }
                    }
            };
            var taylorSwift = new Artist
            {
                Name = "Taylor Swift",
                Genres = { pop, country },
                Albums =
                    {
                        new Album { Name = "Taylor Swift", Year = 2006 },
                        new Album { Name = "Fearless", Year = 2008 },
                        new Album { Name = "Speak Now", Year = 2010 },
                        new Album { Name = "Red", Year = 2012 },
                        new Album { Name = "1989", Year = 2014 },
                        new Album { Name = "Reputation", Year = 2017 },
                        new Album { Name = "Lover", Year = 2019 },
                        new Album { Name = "Folklore", Year = 2020 },
                        new Album { Name = "Evermore", Year = 2020 }
                    }
            };
            var michael_jackson = new Artist
            {
                Name = "Michael Jackson",
                Genres = { pop, RnB },
                Albums =
                    {
                        new Album { Name = "Off the Wall", Year = 1979 },
                        new Album { Name = "Thriller", Year = 1982 },
                        new Album { Name = "Bad", Year = 1987 },
                        new Album { Name = "Dangerous", Year = 1991 },
                        new Album { Name = "HIStory: Past, Present and Future, Book I", Year = 1995 },
                        new Album { Name = "Invincible", Year = 2001 }
                    }
            };
            var green_day = new Artist
            {
                Name = "Green Day",
                Genres = { punk, alternative_rock },
                Albums =
                    {
                        new Album { Name = "39/Smooth", Year = 1990 },
                        new Album { Name = "Kerplunk", Year = 1992 },
                        new Album { Name = "Dookie", Year = 1994 },
                        new Album { Name = "Insomniac", Year = 1995 },
                        new Album { Name = "Nimrod", Year = 1997 },
                        new Album { Name = "Warning", Year = 2000 },
                        new Album { Name = "American Idiot", Year = 2004 },
                        new Album { Name = "21st Century Breakdown", Year = 2009 },
                        new Album { Name = "¡Uno!", Year = 2012 },
                        new Album { Name = "¡Dos!", Year = 2012 },
                        new Album { Name = "¡Tré!", Year = 2012 },
                        new Album { Name = "Revolution Radio", Year = 2016 }
                    }
            };
            var slayer = new Artist
            {
                Name = "Slayer",
                Genres = { trash_metal, metal },
                Albums =
                    {
                        new Album { Name = "Show No Mercy", Year = 1983 },
                        new Album { Name = "Hell Awaits", Year = 1985 },
                        new Album { Name = "Reign in Blood", Year = 1986 },
                        new Album { Name = "South of Heaven", Year = 1988 },
                        new Album { Name = "Seasons in the Abyss", Year = 1990 },
                        new Album { Name = "Divine Intervention", Year = 1994 },
                        new Album { Name = "Undisputed Attitude", Year = 1996 },
                        new Album { Name = "Diabolus in Musica", Year = 1998 },
                        new Album { Name = "God Hates Us All", Year = 2001 },
                        new Album { Name = "Christ Illusion", Year = 2006 },
                        new Album { Name = "World Painted Blood", Year = 2009 }
                    }
            };
            var horkyze_slize = new Artist
            {
                Name = "Horkýže Slíže",
                Genres = { rock, punk },
                Albums =
                    {
                        new Album { Name = "Terapia", Year = 1997 },
                        new Album { Name = "Horkýže Slíže", Year = 1999 },
                        new Album { Name = "Ja chaču tebja", Year = 2001 },
                        new Album { Name = "Kýže sliz", Year = 2002 },
                        new Album { Name = "Ritero Xaperle Bax", Year = 2003 },
                        new Album { Name = "XXX", Year = 2004 },
                        new Album { Name = "Šampón", Year = 2005 },
                        new Album { Name = "Kosmodisk", Year = 2006 },
                        new Album { Name = "Struny z bavlny", Year = 2009 },
                        new Album { Name = "Kým ťa mám", Year = 2011 },
                        new Album { Name = "Výlet do Amsterodamu", Year = 2013 },
                        new Album { Name = "Fajront život", Year = 2017 }
                    }
            };
            var olympic = new Artist
            {
                Name = "Olympic",
                Genres = { rock, pop },
                Albums =
                    {
                        new Album { Name = "Prázdniny na Zemi...?", Year = 1980 },
                        new Album { Name = "Jako za mlada", Year = 1983 },
                        new Album { Name = "Když ti svítí zelená", Year = 1984 },
                        new Album { Name = "Marathon", Year = 1985 },
                        new Album { Name = "Já nejsem já", Year = 1986 },
                        new Album { Name = "Dej mi víc své lásky", Year = 1988 },
                        new Album { Name = "Souhvězdí štěstí", Year = 1991 },
                        new Album { Name = "Aréna", Year = 1995 },
                        new Album { Name = "Oheň", Year = 2001 },
                        new Album { Name = "Přesun", Year = 2005 },
                        new Album { Name = "Ikarus", Year = 2009 },
                        new Album { Name = "Praha - Rio - L.A.", Year = 2015 }
                    }
            };



            List<object> sampleData = new()
            {
                trash_metal,
                metal,
                rock,
                pop,
                punk,
                psychedelic_rock,
                alternative_rock,
                glam_rock,
                hard_rock,
                grunge,
                country,
                funk_rock,
                hip_hop,
                RnB,

                metallica,
                queen,
                pinkFloyd,
                avril_lavigne,
                acdc,
                nirvana,
                rhcp,
                black_sabbath,
                eminem,
                taylorSwift,
                michael_jackson,
                green_day,
                slayer,
                horkyze_slize,
                olympic
            };

            StringBuilder existingRecords = new("Database already contains following records:\n");
            bool showReport = false;

            foreach (var record in sampleData)
            {
                if (!InsertRecord(record))
                {
                    existingRecords.Append("\n");
                    existingRecords.Append(record);
                    showReport = true;
                }
            }

            existingRecords.Append($"\n\nRecords have not been added to the database.");

            App.context.SaveChanges();

            if (showReport)
            {
                MessageBox.Show(existingRecords.ToString(), "Sample data fill report");
            }
        }



        /// <summary>
        /// Inserts a record passed as parameter to the database if not existing already
        /// </summary>
        /// <param name="record">the record to be added to the database</param>
        /// <returns>true if inserted successfuly; false if record already exists</returns>
        /// <exception cref="ArgumentException">Thrown when record doesn't exist in the database but is somehow retyped later to not fit the database</exception>
        static public bool InsertRecord(object record)
        {
            ArtistContext context = App.context;

            if (!Contains(record))
            {
                if (record is Artist artist)
                {
                    artist.Id = Guid.NewGuid();
                    context.Artists.Add(artist);
                }
                else if (record is Album album)
                {
                    album.Id = Guid.NewGuid();
                    context.Albums.Add(album);
                }
                else if (record is Genre genre)
                {
                    genre.Id = Guid.NewGuid();
                    context.Genres.Add(genre);
                }
                else
                {
                    throw new ArgumentException("Cannot insert argumented type.");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates record in the database
        /// </summary>
        /// <param name="oldRecord">old entry to be updated</param>
        /// <param name="newRecord">updated instance</param>
        /// <returns>true if updated successfuly; false if one of inputs are null</returns>
        /// <exception cref="ArgumentException">thrown when there is not such entry to be updated</exception>
        static public bool UpdateRecord(object oldRecord, object newRecord)
        {
            if (!Contains(oldRecord))
            {
                throw new ArgumentException("Given record doesn't exist in database context.");
            }

            if (oldRecord != null && newRecord != null)
            {
                if (oldRecord is Artist oldArtist && newRecord is Artist newArtist)
                {
                    var update = App.context.Artists.Find(oldArtist.Id);
                    update.Name = newArtist.Name;
                    App.context.Artists.Update(update);

                    return true;
                }
                else if (oldRecord is Album oldAlbum && newRecord is Album newAlbum)
                {
                    var update = App.context.Albums.Find(oldAlbum.Id);
                    update.Name = newAlbum.Name;
                    update.Year = newAlbum.Year;
                    App.context.Albums.Update(update);

                    return true;
                }
                else if (oldRecord is Genre oldGenre && newRecord is Genre newGenre)
                {
                    var update = App.context.Genres.Find(oldGenre.Id);
                    update.Name = newGenre.Name;
                    App.context.Genres.Update(update);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <param name="record">Record to be deleted</param>
        /// <returns>removed object from database</returns>
        /// <exception cref="ArgumentException">thrown when there is not such entry to be updated</exception>
        static public object DeleteRecord(object record)
        {
            if (record == null)
            {
                return false;
            }
            if (!Contains(record))
            {
                throw new ArgumentException("Given record doesn't exist in database context.");
            }

            return App.context.Remove(record);
        }



        /// <summary>
        /// Method finding matching record in the database
        /// </summary>
        /// <param name="record">record to find the match in the database</param>
        /// <returns>true if record is in the database, false if not</returns>
        /// <exception cref="ArgumentException">Thrown when record doesn't match any database class.</exception>
        static public bool Contains(object record)
        {
            ArtistContext context = App.context;
            if (record is Artist artist)
            {
                if (context.Artists.Any(i => i.Name == artist.Name))
                {
                    return true;
                }
            }

            else if (record is Album album)
            {
                if (context.Albums.Any(i =>
                    i.Name == album.Name
                    && i.Year == album.Year
                    && i.Artist.Name == album.Artist.Name
                ))
                {
                    return true;
                }
            }

            else if (record is Genre genre)
            {
                if (context.Genres.Any(i => i.Name == genre.Name))
                {
                    return true;
                }
            }

            else
            {
                throw new ArgumentException("Input data type doesn't match connected database.");
            }

            return false;
        }
    }
}
