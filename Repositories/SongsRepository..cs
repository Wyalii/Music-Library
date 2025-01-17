using System.Data.Common;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using LibVLCSharp.Shared;
using System.Net;

namespace MusicLibrary
{
    public class SongsRepository
    {
        MusicLibraryDb musicLibraryDb = new MusicLibraryDb();
        public void AddSong()
        {
            Console.Clear();
            Console.WriteLine("Provide id of an album: ");
            string AlbumIdInput = Console.ReadLine();
            int AlbumIdValue;

            if (string.IsNullOrWhiteSpace(AlbumIdInput) || !int.TryParse(AlbumIdInput, out AlbumIdValue))
            {
                Console.WriteLine("Invalid Album Id Input.");
                return;
            }

            var album = musicLibraryDb.Albums.FirstOrDefault(a => a.Id == AlbumIdValue);
            if (album != null)
            {
                Console.WriteLine("Provide Song Title:");
                string NewSongtTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(NewSongtTitle))
                {
                    Console.WriteLine("Invalid Song Title input.");
                    return;
                }

                Console.WriteLine("Provide Song Timespan in mm/ss format: ");
                string TimeSpanInput = Console.ReadLine().Trim();
                TimeSpan TimeSpanValue;
                Console.WriteLine($"Received TimeSpan input: {TimeSpanInput}");
                if (string.IsNullOrWhiteSpace(TimeSpanInput) || !TimeSpan.TryParse(TimeSpanInput, out TimeSpanValue))
                {
                    Console.WriteLine("Invalid Timespan Input.");
                    return;
                }


                Console.WriteLine("Provide MP3 url of Music: ");
                string MusicUrlInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(MusicUrlInput) || !MusicUrlInput.Contains(".mp3"))
                {
                    Console.WriteLine("Invalid url input.");
                    return;
                }

                Console.WriteLine("Provide Songs Track Number:");
                string TrackNumberInput = Console.ReadLine();
                int TrackNumberValue;
                if (string.IsNullOrWhiteSpace(TrackNumberInput) || !int.TryParse(TrackNumberInput, out TrackNumberValue))
                {
                    Console.WriteLine("Invalid Track Number Input");
                    return;
                }

                var songs = musicLibraryDb.Songs.Where(s => s.AlbumId == AlbumIdValue).ToList();
                while (songs.Any(s => s.TrackNumber == TrackNumberValue))
                {
                    Console.WriteLine("Track Number is taken, try agaian.");
                    TrackNumberInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(TrackNumberInput) || !int.TryParse(TrackNumberInput, out TrackNumberValue))
                    {
                        Console.WriteLine("Invalid Track Number Input");
                        return;
                    }
                }

                Console.WriteLine("Provide Times Played input:");
                string TimesPlayedInput = Console.ReadLine();
                int TimesPlayedValue;
                if (string.IsNullOrWhiteSpace(TimeSpanInput) || !int.TryParse(TimesPlayedInput, out TimesPlayedValue))
                {
                    Console.WriteLine("Invalid Times Played Input.");
                    return;
                }

                Song NewSong = new Song { AlbumId = AlbumIdValue, Title = NewSongtTitle, TimeSpan = TimeSpanValue, MusicUrl = MusicUrlInput, TrackNumber = TrackNumberValue, TimesPlayed = TimesPlayedValue };
                musicLibraryDb.Songs.Add(NewSong);
                musicLibraryDb.SaveChanges();
                Console.WriteLine($"Song: {NewSong.Title} was added to Album: {album.Title}");
                return;

            }
            else
            {
                Console.WriteLine($"Album with provided id: {AlbumIdValue} was not found.");
                return;
            }


        }

        public void PlayMusic()
        {
            Console.Clear();
            Console.WriteLine("Write an Song Id");
            int SongIdValue;
            if (int.TryParse(Console.ReadLine(), out SongIdValue))
            {
                var song = musicLibraryDb.Songs
                .Include(s => s.Album)
                .Include(s => s.Album.Artist)
                .FirstOrDefault(s => s.Id == SongIdValue);
                if (song != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Insturctions:");
                    Console.WriteLine($"--- Folder Named: {song.Album.Title} Will be created on your desktop.");
                    Console.WriteLine($"--- Please Dont Modify Downloaded Folder on your own.");
                    Console.WriteLine($"--- Song: {song.Title} will be downloaded on folder.");
                    Console.WriteLine($"--- Make sure there are no folder on you desktop with same folder name: {song.Album.Title}");


                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string FolderPath = Path.Combine(desktopPath, $"{song.Album.Title}");

                    if (!Directory.Exists(FolderPath))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Creating Folder: {song.Album.Title} ...");
                        Directory.CreateDirectory(FolderPath);
                        Console.WriteLine();
                        Console.WriteLine("Folder Created.");
                        Console.WriteLine();
                        Console.WriteLine("downloading Mp3 Files...");
                        Console.WriteLine();
                        var AllSongs = musicLibraryDb.Songs.Where(s => s.AlbumId == song.AlbumId).ToList();
                        var client = new WebClient();
                        foreach (var music in AllSongs)
                        {
                            var FileName = $"{music.Title}.mp3";
                            var FilePath = Path.Combine(FolderPath, FileName);
                            try
                            {
                                Console.WriteLine($"Downloading Music: {music.Title}...");
                                client.DownloadFile(music.MusicUrl, FilePath);
                                Console.WriteLine($"Downloaded Music: {music.Title}");
                                Console.WriteLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Exception: {ex.InnerException}");
                            }
                        }


                        string SongFilePath = Path.Combine(FolderPath, $"{song.Title}.mp3");
                        if (SongFilePath != null)
                        {
                            var LibVLC = new LibVLC();
                            var media = new Media(LibVLC, SongFilePath);
                            var Player = new MediaPlayer(media);
                            Console.WriteLine();
                            Console.WriteLine($"Album Name: {song.Album.Title}");
                            Console.WriteLine();
                            Console.WriteLine($"Playing Song: {song.Title}");
                            Console.WriteLine();
                            Console.WriteLine($"Artist: {song.Album.Artist.Name}");
                            Console.WriteLine();
                            Console.WriteLine($"Song Duration: {song.TimeSpan}");
                            Player.Play();
                            Console.ReadKey();
                            Console.WriteLine($"Finished Playing {song.Title}");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Folder Already Exists.");
                        string SongFilePath = Path.Combine(FolderPath, $"{song.Title}.mp3");
                        if (!File.Exists(SongFilePath))
                        {
                            Console.WriteLine($"Updating Folder: {song.Album.Title}");
                            var client = new WebClient();
                            try
                            {
                                client.DownloadFile(song.MusicUrl, SongFilePath);
                                Console.WriteLine($"Downloaded Song: {song.Title}");
                                Console.WriteLine($"Update Finished.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Exception: {ex.InnerException}");
                            }

                        }

                        var LibVLC = new LibVLC();
                        var media = new Media(LibVLC, SongFilePath);
                        var Player = new MediaPlayer(media);
                        Console.WriteLine();
                        Console.WriteLine($"Album Name: {song.Album.Title}");
                        Console.WriteLine();
                        Console.WriteLine($"Playing Song: {song.Title}");
                        Console.WriteLine();
                        Console.WriteLine($"Artist: {song.Album.Artist.Name}");
                        Console.WriteLine();
                        Console.WriteLine($"Song Duration: {song.TimeSpan}");
                        Player.Play();
                        Console.ReadKey();
                        Console.WriteLine($"Finished Playing {song.Title}");
                        return;

                    }

                }
                else
                {
                    Console.WriteLine($"Song with provided id {SongIdValue} was not found.");
                    return;
                }

            }
            else
            {
                Console.WriteLine("Invalid Song Id Input.");
                return;
            }


        }


        public void DeleteSong()
        {
            Console.Clear();
            Console.WriteLine("Provide id of song to delete:");
            string SongIdInput = Console.ReadLine();
            int SongIdValue;
            if (string.IsNullOrWhiteSpace(SongIdInput) || int.TryParse(SongIdInput, out SongIdValue))
            {
                Console.WriteLine($"invalid song id input.");
                return;
            }

            var song = musicLibraryDb.Songs.FirstOrDefault(s => s.Id == SongIdValue);
            if (song != null)
            {
                musicLibraryDb.Songs.Remove(song);
                musicLibraryDb.SaveChanges();
                Console.WriteLine($"removed song: {song.Title}");
                return;
            }
            else
            {
                Console.WriteLine($"song with provided id: {SongIdValue} was not found.");
                return;
            }
        }

    }
}
