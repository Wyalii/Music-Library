using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using LibVLCSharp.Shared;
using MusicLibrary;

namespace Music_Library
{
    public class PlaylistRepository
    {
        MusicLibraryDb musicLibraryDb = new MusicLibraryDb();
        public void CreatePlayList()
        {
            Console.Clear();

            Console.WriteLine("Provide Playlist Name:");
            string NameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(NameInput))
            {
                Console.WriteLine("Invalid Name Input.");
                return;
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string PlaylistPath = Path.Combine(desktopPath, $"Playlist_{NameInput}");
            if (!Directory.Exists(PlaylistPath))
            {
                Console.WriteLine($"Creating a Playlist: {NameInput} ...");
                Directory.CreateDirectory(PlaylistPath);
                Console.WriteLine("Created Folder.");
                return;
            }

            if (Directory.Exists(PlaylistPath))
            {
                Console.WriteLine($"Playlist: {NameInput} already exists on your desktop.");
                return;
            }
        }

        public void AddSongToPlaylist()
        {
            Console.Clear();
            Console.WriteLine("Name of playlist is case sensitive!!");
            Console.WriteLine("Provide Name of your Playlist:");
            string PlaylistName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(PlaylistName))
            {
                Console.WriteLine("Invalid Playlist Name Input.");
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var PlaylistPath = Path.Combine(desktopPath, PlaylistName);

            if (!Directory.Exists(PlaylistPath))
            {
                Console.WriteLine($"Folder with provided name: {PlaylistName} doesn't exists on your desktop");
                return;
            }

            if (Directory.Exists(PlaylistPath))
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Provide Song Id:");
                    int SongId;
                    if (!int.TryParse(Console.ReadLine(), out SongId))
                    {
                        Console.WriteLine("Invalid Song Id Input.");
                        return;
                    }

                    var song = musicLibraryDb.Songs.FirstOrDefault(s => s.Id == SongId);
                    if (song == null)
                    {
                        Console.WriteLine("Song with provided id doesn't exists.");
                        return;
                    }

                    var client = new WebClient();
                    var FileName = $"{song.Title}.mp3";
                    var FilePath = Path.Combine(PlaylistPath, FileName);
                    Console.WriteLine($"Downloading Song: {song.Title} ...");
                    client.DownloadFile(song.MusicUrl, FilePath);
                    Console.WriteLine($"Download Finished.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.InnerException}");
                }


            }

        }


        public void PlayPlaylistSongs()
        {
            Console.Clear();
            Console.WriteLine("Warning Playlist name is case sensitive!!");
            Console.WriteLine("Provide Your Playlist Name:");
            string PlaylistName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(PlaylistName))
            {
                Console.WriteLine("Invalid Playlist Name Input.");
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var PlaylistPath = Path.Combine(desktopPath, PlaylistName);
            if (!Directory.Exists(PlaylistPath))
            {
                Console.WriteLine($"Playlist with provided name: {PlaylistName} doesn't exits on desktop.");
                return;
            }

            var mp3Files = Directory.GetFiles(PlaylistPath, "*.mp3");
            if (mp3Files.Length == 0)
            {
                Console.WriteLine("No MP3 files found in the playlist folder.");
                return;
            }
            var LibVLC = new LibVLC();
            foreach (var mp3 in mp3Files)
            {
                var media = new Media(LibVLC, mp3);
                var Player = new MediaPlayer(media);
                Console.WriteLine($"Playing: {mp3}");
                Console.WriteLine("Press enter to move on to next song");
                Player.Play();
                Console.ReadKey();
                Player.Stop();
            }
            return;

        }
    }
}