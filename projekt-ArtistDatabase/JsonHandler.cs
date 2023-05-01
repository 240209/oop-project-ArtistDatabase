using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projekt_ArtistDatabase.Commands;
using projekt_ArtistDatabase.EFCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace projekt_ArtistDatabase
{
    public static class JsonHandler
    {
        #region Simple database models
        // defines the JSON fields to export / import
        // redundant data from database are not exported
        private class ArtistModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<Guid> AlbumIds { get; set; }
        }
        private class AlbumModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Year { get; set; }
            public Guid ArtistId { get; set; }
        }
        private class GenreModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        #endregion
        private class ArtistGenreModel
        {
            public Guid ArtistId { get; set; }
            public Guid GenreId { get; set; }
        }
        /// <summary>
        /// Database model for .json parsing
        /// </summary>
        private class JsonExportFormat
        {
            public List<ArtistModel> Artists { get; set; }
            public List<AlbumModel> Albums { get; set; }
            public List<GenreModel> Genres { get; set; }
            public List<ArtistGenreModel> ArtistGenre { get; set; }
        }
        /// <summary>
        /// Exports currently used database into a single .csv file in JSON format
        /// </summary>
        /// <param name="csvFilePath">path to the .csv file</param>
        /// <returns>true if successfuly exported</returns>
        public static bool Export(string csvFilePath)
        {
            List<Artist> dbArtists = new(App.context.Artists.ToList());
            List<Album> dbAlbums = new(App.context.Albums.ToList());
            List<Genre> dbGenres = new(App.context.Genres.ToList());

            List<ArtistModel> Artists = new();
            List<AlbumModel> Albums = new();
            List<GenreModel> Genres = new();
            List<ArtistGenreModel> ArtistGenre = new();

            // handling Artist-Genre, Artist collections
            foreach (Artist artist in dbArtists)
            {
                var currentArtist = new ArtistModel
                {
                    Id = artist.Id,
                    Name = artist.Name
                };

                var currentArtistAlbums = new List<Guid>();
                foreach (Album album in artist.Albums)
                {
                    currentArtistAlbums.Add(album.Id);
                }

                var currentArtistGenres = new List<Guid>();
                foreach (Genre genre in artist.Genres)
                {
                    currentArtistGenres.Add(genre.Id);
                }

                currentArtist.AlbumIds = new();
                currentArtist.AlbumIds.AddRange(currentArtistAlbums);

                Artists.Add(currentArtist);
                foreach (Guid genreId in currentArtistGenres)
                {
                    ArtistGenre.Add(new ArtistGenreModel { ArtistId = currentArtist.Id, GenreId = genreId });
                }
            }

            // handling Albums
            foreach (Album album in dbAlbums)
            {
                Albums.Add(new AlbumModel
                {
                    Id = album.Id,
                    Name = album.Name,
                    Year = album.Year,
                    ArtistId = album.ArtistId
                });
            }

            // handling Genres
            foreach (Genre genre in dbGenres)
            {
                Genres.Add(new GenreModel
                { 
                    Id = genre.Id, 
                    Name = genre.Name
                });
            }

            Dictionary<string, object> JsonExport = new()
            {
                { "Artists", Artists },
                { "Albums", Albums },
                { "Genres", Genres },
                { "ArtistGenre", ArtistGenre }
            };
            var jsonText = JsonConvert.SerializeObject(JsonExport, Formatting.Indented);

            File.WriteAllText(csvFilePath, jsonText);
            return true;
        }
        /// <summary>
        /// Imports database data from a single .csv file in JSON format (deletes all current unexported data)
        /// </summary>
        /// <param name="csvFilePath">path to the .csv file</param>
        /// <returns>true if successfuly imported</returns>
        public static bool Import(string csvFilePath)
        {
            List<Artist> artists = new();
            List<Album> albums = new();
            List<Genre> genres = new();

            var jsonText = File.ReadAllText(csvFilePath);
            var jsonData = JsonConvert.DeserializeObject<JsonExportFormat>(jsonText);

            foreach (ArtistModel artistModel in jsonData.Artists)
            {
                artists.Add(new Artist
                {
                    Id = artistModel.Id,
                    Name = artistModel.Name
                });
            }
            foreach (AlbumModel albumModel in jsonData.Albums)
            {
                albums.Add(new Album
                {
                    Id = albumModel.Id,
                    Name = albumModel.Name,
                    Year = albumModel.Year,
                    ArtistId = albumModel.ArtistId
                });
            }
            foreach (GenreModel genreModel in jsonData.Genres)
            {
                genres.Add(new Genre
                {
                    Id = genreModel.Id,
                    Name = genreModel.Name,
                    Artists = new List<Artist>(
                        jsonData.ArtistGenre
                            .Where(ArGr => ArGr.GenreId == genreModel.Id)
                            .Join(
                                artists,
                                ArGr => ArGr.ArtistId,
                                artists => artists.Id,
                                (ArGr, artists) => new { ArGr, artists })
                            .Select(x => x.artists)
                            .ToList()
                    )
                });
            }

            App.context.Albums.RemoveRange(App.context.Albums.ToList());
            App.context.Artists.RemoveRange(App.context.Artists.ToList());
            App.context.Genres.RemoveRange(App.context.Genres.ToList());

            App.context.SaveChanges();

            App.context.Artists.AddRange(artists);
            
            App.context.Albums.AddRange(albums);
            
            App.context.Genres.AddRange(genres);

            App.context.SaveChanges();

            return true;
        }

    }
}
