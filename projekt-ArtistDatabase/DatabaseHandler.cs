using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            var metallica = new Artist
            {
                Name = "Metallica",
                Genres = { trash_metal, metal },
                Albums =
                {
                    new Album { Name = "Kill 'Em All", Year = 1983 },
                    new Album { Name = "Metallica (Black Album)", Year = 1991 },
                }
            };
            var olympic = new Artist
            {
                Name = "Olympic",
                Genres = { rock, pop },
                Albums =
                {
                    new Album { Name = "Prázdniny na Zemi...?", Year = 1980 }
                }
            };
            var avril_lavigne = new Artist
            {
                Name = "Avril Lavigne",
                Genres = { pop, punk },
                Albums =
                {
                    new Album { Name = "Let Go", Year = 2002 },
                    new Album { Name = "Love Sux", Year = 2022 },
                }
            };


            List<object> sampleData = new()
            {
                trash_metal,
                metal,
                rock,
                pop,
                punk,

                metallica,
                olympic,
                avril_lavigne
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
        /// <param name="context">database context</param>
        /// <returns>true if inserted successfuly; false if record already exists</returns>
        /// <exception cref="ArgumentException">Thrown when record doesn't exist in the database but is somehow retyped later to not fit the database</exception>
        static public bool InsertRecord(object record)
        {
            ArtistContext context = App.context;

            if (!Contains(record))
            {
                if (record is Artist artist)
                {
                    context.Artists.Add(artist);
                }
                else if (record is Album album)
                {
                    context.Albums.Add(album);
                }
                else if (record is Genre genre)
                {
                    context.Genres.Add(genre);
                }
                else
                {
                    throw new ArgumentException("Unknown error");
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
                    oldArtist.Name = newArtist.Name;
                    return true;
                }
                else if (oldRecord is Album oldAlbum && newRecord is Album newAlbum)
                {
                    oldAlbum.Name = newAlbum.Name;
                    oldAlbum.Year = newAlbum.Year;

                    return true;
                }
                else if (oldRecord is Genre oldGenre && newRecord is Genre newGenre)
                {
                    oldGenre.Name = newGenre.Name;

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
