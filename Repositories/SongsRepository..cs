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
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating System Logs File...");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System Logs File on your desktop.");
            }
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

                try
                {
                    Song NewSong = new Song { AlbumId = AlbumIdValue, Title = NewSongtTitle, TimeSpan = TimeSpanValue, MusicUrl = MusicUrlInput, TrackNumber = TrackNumberValue, TimesPlayed = TimesPlayedValue };
                    musicLibraryDb.Songs.Add(NewSong);
                    musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Created Song - {NewSong.Title} in Album - {album.Title}";
                    File.AppendAllText(SystemLogsFile, logEntry);


                    Console.WriteLine($"Song: {NewSong.Title} was added to Album: {album.Title}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.InnerException}");
                }

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
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine("Provide Song Id");
            int SongId;
            if (!int.TryParse(Console.ReadLine(), out SongId))
            {
                Console.WriteLine("Invalid Song Id input.");
                return;
            }

            var song = musicLibraryDb.Songs
            .Include(s => s.Album)
            .Include(s => s.Album.Artist)
            .FirstOrDefault(s => s.Id == SongId);

            if (song == null)
            {
                Console.WriteLine("Song With Provided Id doesn't exists.");
                return;
            }

            try
            {
                LibVLC libVLC = new LibVLC();
                var client = new WebClient();
                string DownloadedSong = Path.Combine(desktopPath, $"{song.Title}");
                if (File.Exists(DownloadedSong))
                {
                    Media media2 = new Media(libVLC, DownloadedSong);
                    MediaPlayer mediaPlayer2 = new MediaPlayer(media2);
                    Console.WriteLine();
                    Console.WriteLine($"Playing Song: {song.Title} - Song Artist: {song.Album.Artist.Name}");
                    Console.WriteLine($"Timespan: {song.TimeSpan}");
                    mediaPlayer2.Play();
                    Console.ReadKey();
                    mediaPlayer2.Stop();
                    Console.WriteLine("Stopped Playing.");
                    return;
                }
                client.DownloadFile(song.MusicUrl, DownloadedSong);
                Console.WriteLine("Downloading Song...");
                Media media = new Media(libVLC, DownloadedSong);
                MediaPlayer mediaPlayer = new MediaPlayer(media);
                Console.WriteLine();
                Console.WriteLine($"Playing Song: {song.Title} - Song Artist: {song.Album.Artist.Name}");
                Console.WriteLine($"Timespan: {song.TimeSpan}");
                mediaPlayer.Play();
                Console.ReadKey();
                mediaPlayer.Stop();
                Console.WriteLine("Stopped Playing.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


        }
        public void UpdateSong()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");

            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file on your desktop.");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System logs.");

            }
            Console.WriteLine("Provide Song Id.");
            int SongId;
            if (!int.TryParse(Console.ReadLine(), out SongId))
            {
                Console.WriteLine("Invalid Song Id Input.");
                return;
            }
            var song = musicLibraryDb.Songs.Include(s => s.Album).Include(s => s.Album.Artist).FirstOrDefault(s => s.Id == SongId);
            if (song != null)
            {
                Console.WriteLine();
                Console.WriteLine("Provide new Song Title:");
                string NewSongtTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(NewSongtTitle))
                {
                    Console.WriteLine("Invalid Song Title Input");
                    return;
                }

                Console.WriteLine("Provide new song Timespan:");
                TimeSpan NewSongTimespan;
                if (!TimeSpan.TryParse(Console.ReadLine(), out NewSongTimespan))
                {
                    Console.WriteLine("Invalid Timespan Input.");
                    return;
                }

                Console.WriteLine("Provide new music url:");
                string NewSongUrl = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(NewSongUrl))
                {
                    Console.WriteLine("invalid new song url input.");
                    return;
                }

                Console.WriteLine("Provide new TrackNumber");
                string NewTrackNumberInput = Console.ReadLine();
                int NewTrackNumber;
                if (string.IsNullOrWhiteSpace(NewTrackNumberInput) || !int.TryParse(NewTrackNumberInput, out NewTrackNumber))
                {
                    Console.WriteLine("invalid TrackNumber input.");
                    return;
                }
                var AllSongs = musicLibraryDb.Songs.Include(al => al.Album).Where(al => al.AlbumId == song.AlbumId).ToList();
                while (AllSongs.Any(al => al.TrackNumber == NewTrackNumber))
                {
                    Console.WriteLine("Track Number taken, Try Again.");
                    NewTrackNumberInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(NewTrackNumberInput) || !int.TryParse(NewTrackNumberInput, out NewTrackNumber))
                    {
                        Console.WriteLine("Invalid Track Number Input");
                        return;
                    }

                }

                Console.WriteLine("Provide new timesplayed:");
                int NewSongTimesPlayed;
                if (!int.TryParse(Console.ReadLine(), out NewSongTimesPlayed))
                {
                    Console.WriteLine("Invalid Times Played Input.");
                    return;
                }


                string logEntry = $"{Environment.NewLine} {DateTime.Now}: Updated Song - {song.Title} in Album - {song.Album.Title}";
                File.AppendAllText(SystemLogsFile, logEntry);
                song.Title = NewSongtTitle;
                song.MusicUrl = NewSongUrl;
                song.TimeSpan = NewSongTimespan;
                song.TimesPlayed = NewSongTimesPlayed;
                song.TrackNumber = NewTrackNumber;
                musicLibraryDb.SaveChanges();
                Console.WriteLine("Updated song.");
                return;

            }
            else
            {
                Console.WriteLine("Song with provided id was not found.");
                return;
            }
        }
        public void PrintSongs()
        {
            Console.Clear();
            var AllSongs = musicLibraryDb.Songs
            .Include(s => s.Album).
             Include(s => s.Album.Artist)
            .ToList();

            if (AllSongs.Count > 0)
            {
                Console.WriteLine($"All Songs List: ");
                foreach (var song in AllSongs)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Song Id: {song.Id}, Song Name: {song.Title}");
                    Console.WriteLine($"Artist Id: {song.Album.Artist.Id}, Artists Name: {song.Album.Artist.Name}");
                    Console.WriteLine($"Timespan: {song.TimeSpan}, Genre: {song.Album.Genre}");
                    Console.WriteLine();
                    Console.WriteLine();

                }
                return;
            }
            else
            {
                Console.WriteLine("No Songs Avaiable.");
                return;
            }

        }
        public void DeleteSong()
        {
            Console.Clear();
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file on your desktop.");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System logs.");
            }
            Console.WriteLine("Provide id of song to delete:");
            int SongIdValue;
            if (!int.TryParse(Console.ReadLine(), out SongIdValue))
            {
                Console.WriteLine($"invalid song id input.");
                return;
            }

            var song = musicLibraryDb.Songs
            .Include(s => s.Album)
            .Include(s => s.Album.Artist)
            .FirstOrDefault(s => s.Id == SongIdValue);
            if (song != null)
            {
                try
                {

                    musicLibraryDb.Songs.Remove(song);
                    musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Deleted Song - {song.Title} in Album - {song.Album.Title}";
                    File.AppendAllText(SystemLogsFile, logEntry);
                    Console.WriteLine($"removed song: {song.Title}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.InnerException}");
                }
            }
            else
            {
                Console.WriteLine($"song with provided id: {SongIdValue} was not found.");
                return;
            }
        }

    }
}
