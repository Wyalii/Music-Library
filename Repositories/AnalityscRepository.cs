using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class AnalitycsRepository
    {
        private readonly MusicLibraryDb _musicLibraryDb;
        public AnalitycsRepository(MusicLibraryDb musicLibraryDb)
        {
            _musicLibraryDb = musicLibraryDb ?? throw new ArgumentNullException(nameof(musicLibraryDb));
        }
        public void PrintMostListenedSongs()
        {
            Console.Clear();
            Console.WriteLine("Most Listened Songs");
            var AllSongs = _musicLibraryDb.Songs.Include(s => s.Album).Include(s => s.Album.Artist).OrderByDescending(al => al.TimesPlayed).ToList();
            if (AllSongs.Count > 0)
            {
                foreach (var song in AllSongs)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Artist: {song.Album.Artist.Name}");
                    Console.WriteLine();
                    Console.WriteLine($"Album Id: {song.AlbumId}, Album Name: {song.Album.Title}");
                    Console.WriteLine();
                    Console.WriteLine($"Song Id: {song.Id}, Song Name: {song.Title}, Times Played: {song.TimesPlayed}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                return;
            }
            else
            {
                Console.WriteLine("No avaible Songs.");
                return;
            }

        }

        public void PrintGenreStatistics()
        {
            Console.Clear();
            var AllAlbums = _musicLibraryDb.Albums.Include(a => a.Artist).OrderByDescending(a => a.Rating).ToList();
            if (AllAlbums.Count > 0)
            {
                Console.WriteLine("All Genre Statistics");
                foreach (var album in AllAlbums)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Album: {album.Title}");
                    Console.WriteLine($"Genre: {album.Genre}, Rating: {album.Rating}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                return;
            }
            else
            {
                Console.WriteLine("No Albums Avaiable");
                return;
            }
        }

        public void PrintArtistsRatings()
        {
            Console.Clear();
            var AllArtists = _musicLibraryDb.Artists.Include(a => a.Albums).ToList();
            if (AllArtists.Count > 0)
            {
                foreach (var artist in AllArtists)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Artist Id: {artist.Id}, Artist Name: {artist.Name}");
                    Console.WriteLine($"Artist Genre: {artist.Genre}");
                    if (artist.Albums.Count > 0)
                    {
                        foreach (var album in artist.Albums)
                        {
                            Console.WriteLine($"Album Id: {album.Id}, Album Title: {album.Title} , Rating: {album.Rating}");
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No albums released.");
                        return;
                    }
                    return;
                }
            }
            else
            {
                Console.WriteLine("No Artists Avaiable.");
                return;
            }

        }


    }
}