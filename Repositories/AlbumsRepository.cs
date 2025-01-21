using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class AlbumsRepository
    {
        MusicLibraryDb musicLibraryDb = new MusicLibraryDb();
        public void AddAlbum()
        {
            Console.Clear();
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

            var artist = musicLibraryDb.Artists.FirstOrDefault(a => a.Id == IdValue);

            if (artist != null)
            {
                Album NewAlbum = new Album { ArtistId = artist.Id, Title = TitleInput, ReleaseYear = ReleaseYearValue, Genre = GenreInput, Rating = RatingValue };
                musicLibraryDb.Albums.Add(NewAlbum);
                musicLibraryDb.SaveChanges();
                Console.WriteLine($"Added Album: {NewAlbum.Title}");
                return;
            }
            else
            {
                Console.WriteLine($"artist with provided id: {IdValue} coudn't be found.");
                return;
            }


        }

        public void RemoveAlbum()
        {
            Console.Clear();
            Console.WriteLine("Provide Id of album to remove: ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !int.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var AlbumToRemove = musicLibraryDb.Albums.FirstOrDefault(a => a.Id == IdValue);
            if (AlbumToRemove != null)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string FolderPath = Path.Combine(desktopPath, $"{AlbumToRemove.Title}");
                if (Directory.Exists(FolderPath))
                {
                    Directory.Delete(FolderPath, true);
                    Console.WriteLine($"Removed Folder: {AlbumToRemove.Title}");
                }
                musicLibraryDb.Albums.Remove(AlbumToRemove);
                musicLibraryDb.SaveChanges();
                Console.WriteLine($"Removed Album from database: {AlbumToRemove.Title}");
                return;
            }
            else
            {
                Console.WriteLine($"album with provided id: {IdValue} coudn't be found.");
                return;
            }
        }

        public void UpdateAlbum()
        {
            Console.Clear();
            Console.WriteLine("Provide Id of album to update:  ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !int.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var AlbumToUpdate = musicLibraryDb.Albums.FirstOrDefault(a => a.Id == IdValue);
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

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string FolderPath = Path.Combine(desktopPath, AlbumToUpdate.Title);
                string NewFolderPath = Path.Combine(desktopPath, TitleInput);
                try
                {

                    if (Directory.Exists(FolderPath))
                    {

                        Directory.Move(FolderPath, NewFolderPath);
                        Console.WriteLine("Updated Folder.");
                    }
                    AlbumToUpdate.Title = TitleInput;
                    AlbumToUpdate.ReleaseYear = ReleaseYearValue;
                    AlbumToUpdate.Genre = GenreInput;
                    AlbumToUpdate.Rating = RatingValue;
                    musicLibraryDb.SaveChanges();

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
            var AllAlbums = musicLibraryDb.Albums.Include(a => a.Artist).ToList();
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
            Console.WriteLine("Provide id of album: ");
            string IdInput = Console.ReadLine();
            decimal IdValue;
            if (string.IsNullOrWhiteSpace(IdInput) || !decimal.TryParse(IdInput, out IdValue))
            {
                Console.WriteLine("Invalid Id Input.");
                return;
            }

            var album = musicLibraryDb.Albums.FirstOrDefault(a => a.Id == IdValue);
            if (album != null)
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
                musicLibraryDb.SaveChanges();
                Console.WriteLine("Updated Rating!");
                return;
            }
            else
            {
                Console.WriteLine($"Album with provided id: {IdValue} was not found.");
                return;
            }
        }
    }
}