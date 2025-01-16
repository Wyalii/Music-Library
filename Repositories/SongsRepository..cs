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
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string downloadFolder = Path.Combine(desktopPath, "Downloaded Songs");
            if (Directory.Exists(downloadFolder))
            {
                Console.WriteLine("Folder Already Exists");
            }
            else
            {
                Console.WriteLine("CreatingFolder...");
                Directory.CreateDirectory(downloadFolder);
                Console.WriteLine($"Created Folder : {Path.GetFileName(downloadFolder)}");
                var AllSongs = musicLibraryDb.Songs.ToList();
                foreach (var song in AllSongs)
                {
                    string FileName = $"{song.Title}.mp3";
                    string FilePath = Path.Combine(downloadFolder, FileName);
                    if (File.Exists(FilePath))
                    {
                        Console.WriteLine("File Already Exists.");
                        continue;
                    }

                    Console.WriteLine($"Downloading {song.Title}...");
                    try
                    {
                        var client = new WebClient();
                        client.DownloadFile(song.MusicUrl, FilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading {song.Title}: {ex.Message}");
                    }
                }
            }

            var mp3files = Directory.GetFiles(downloadFolder, "*.mp3").ToList();
            Console.WriteLine("All Files: ");
            foreach (var mp3 in mp3files)
            {
                Console.WriteLine(mp3);
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
