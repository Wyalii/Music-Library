using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class AlbumsRepository
    {
        private readonly MusicLibraryDb _musicLibraryDb;
        public AlbumsRepository(MusicLibraryDb musicLibraryDb)
        {
            _musicLibraryDb = musicLibraryDb ?? throw new ArgumentNullException(nameof(musicLibraryDb));
        }
        public void AddAlbum()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            Console.Clear();
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file...");
                FileStream fs = File.Create(SystemLogsFile);
                Console.WriteLine("Created system logs file.");
                Console.WriteLine();
            }
            Console.WriteLine("Provide Artist's Id: ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !int.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Artist Id Input.");
                return;
            }

            Console.WriteLine("Provide Title: ");
            string TitleInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(TitleInput))
            {
                Console.WriteLine("Invalid TitleInput");
                return;
            }

            Console.WriteLine("Provide Release Year: ");
            string ReleaseYearInput = Console.ReadLine();
            int ReleaseYearValue;
            if (string.IsNullOrWhiteSpace(ReleaseYearInput) || !int.TryParse(ReleaseYearInput, out ReleaseYearValue))
            {
                Console.WriteLine("Invalid Release Year Input.");
                return;
            }
            Console.WriteLine("Provide Genre: ");
            string GenreInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(GenreInput))
            {
                Console.WriteLine("Invalid Genre Input.");
                return;
            }
            Console.WriteLine("Provide Rating: ");
            string RatingInput = Console.ReadLine();
            decimal RatingValue;
            if (string.IsNullOrWhiteSpace(RatingInput) || !decimal.TryParse(RatingInput, out RatingValue))
            {
                Console.WriteLine("Invalid Release Year Input.");
                return;
            }

            var artist = _musicLibraryDb.Artists.Include(a => a.Albums).FirstOrDefault(a => a.Id == IdValue);

            try
            {
                if (artist != null)
                {

                    Album NewAlbum = new Album { ArtistId = artist.Id, Title = TitleInput, ReleaseYear = ReleaseYearValue, Genre = GenreInput, Rating = RatingValue };
                    _musicLibraryDb.Albums.Add(NewAlbum);
                    _musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Created Album - {NewAlbum.Title} - Artist: {artist.Name}";
                    File.AppendAllText(SystemLogsFile, logEntry);
                    Console.WriteLine($"Added Album: {NewAlbum.Title}");
                    return;
                }
                else
                {
                    Console.WriteLine($"artist with provided id: {IdValue} coudn't be found.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


        }

        public void RemoveAlbum()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file...");
                FileStream fs = File.Create(SystemLogsFile);
                Console.WriteLine("Created system logs file.");
                Console.WriteLine();
            }
            Console.WriteLine("Provide Id of album to remove: ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !int.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var AlbumToRemove = _musicLibraryDb.Albums.Include(a => a.Artist).FirstOrDefault(a => a.Id == IdValue);
            try
            {
                if (AlbumToRemove != null)
                {
                    _musicLibraryDb.Albums.Remove(AlbumToRemove);
                    _musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Deleted Album - {AlbumToRemove.Title} - Artist: {AlbumToRemove.Artist.Name}";
                    File.AppendAllText(SystemLogsFile, logEntry);
                    Console.WriteLine($"Removed Album from database: {AlbumToRemove.Title}");
                    return;
                }
                else
                {
                    Console.WriteLine($"album with provided id: {IdValue} coudn't be found.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void UpdateAlbum()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file...");
                FileStream fs = File.Create(SystemLogsFile);
                Console.WriteLine("Created system logs file.");
                Console.WriteLine();
            }

            Console.WriteLine("Provide Id of album to update:  ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !int.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var AlbumToUpdate = _musicLibraryDb.Albums.Include(a => a.Artist).FirstOrDefault(a => a.Id == IdValue);
            if (AlbumToUpdate != null)
            {

                Console.WriteLine("Provide new album title: ");
                string TitleInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(TitleInput))
                {
                    Console.WriteLine("Invalid Title Input.");
                    return;
                }

                Console.WriteLine("Provide new release year: ");
                string ReleaseYearInput = Console.ReadLine();
                int ReleaseYearValue;
                if (string.IsNullOrWhiteSpace(ReleaseYearInput) || !int.TryParse(ReleaseYearInput, out ReleaseYearValue))
                {
                    Console.WriteLine("Invalid Release Year Input.");
                    return;
                }

                Console.WriteLine("Provide new genre: ");
                string GenreInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(GenreInput))
                {
                    Console.WriteLine("Invalid Genre Input.");
                    return;
                }

                Console.WriteLine("Provide new rating: ");
                string RatingInput = Console.ReadLine();
                decimal RatingValue;
                if (string.IsNullOrWhiteSpace(RatingInput) || !decimal.TryParse(RatingInput, out RatingValue))
                {
                    Console.WriteLine("Invalid Rating Input.");
                    return;
                }



                try
                {
                    AlbumToUpdate.Title = TitleInput;
                    AlbumToUpdate.ReleaseYear = ReleaseYearValue;
                    AlbumToUpdate.Genre = GenreInput;
                    AlbumToUpdate.Rating = RatingValue;
                    _musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Updated Album - {AlbumToUpdate.Title} - Artist: {AlbumToUpdate.Artist.Name}";
                    File.AppendAllText(SystemLogsFile, logEntry);

                    Console.WriteLine("Success, Updated Album!");
                    return;
                }
                catch (Exception ex)
                {
                    Console.Write($"{ex.InnerException}");
                }
            }
            else
            {
                Console.WriteLine($"album with provided id: {IdValue} coudn't be found.");
                return;
            }
        }

        public void PrintAlbums()
        {
            Console.Clear();
            Console.WriteLine("All Albums: ");
            var AllAlbums = _musicLibraryDb.Albums.Include(a => a.Artist).ToList();
            if (AllAlbums == null)
            {
                Console.WriteLine("No Albums Added.");

            }
            else
            {
                foreach (var album in AllAlbums)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Album Id: {album.Id}, Album Title: {album.Title}");
                    Console.WriteLine($"Artist Id: {album.ArtistId}, Artist Name: {album.Artist.Name}");
                    Console.WriteLine($"Release Year: {album.ReleaseYear}, Genre: {album.Genre}");
                    Console.WriteLine($"Rating: {album.Rating}");
                    Console.WriteLine();
                    Console.WriteLine();
                }

            }

        }

        public void UpdateAlbumsRating()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating system logs file...");
                FileStream fs = File.Create(SystemLogsFile);
                Console.WriteLine("Created system logs file.");
                Console.WriteLine();
            }
            Console.WriteLine("Provide id of album: ");
            string IdInput = Console.ReadLine();
            decimal IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !decimal.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var album = _musicLibraryDb.Albums.Include(a => a.Artist).FirstOrDefault(a => a.Id == IdValue);
            if (album != null)
            {
                try
                {
                    Console.WriteLine("Provide New Rating: ");
                    string RatingInput = Console.ReadLine();
                    decimal RatingValue;
                    if (string.IsNullOrWhiteSpace(RatingInput) || !decimal.TryParse(RatingInput, out RatingValue))
                    {
                        Console.WriteLine("Invalid Rating Input.");
                        return;
                    }
                    album.Rating = RatingValue;
                    _musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Updated Album Rating - {album.Title} - Artist: {album.Artist.Name}";
                    File.AppendAllText(SystemLogsFile, logEntry);
                    Console.WriteLine("Updated Rating!");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            else
            {
                Console.WriteLine($"Album with provided id: {IdValue} was not found.");
                return;
            }
        }
    }
}