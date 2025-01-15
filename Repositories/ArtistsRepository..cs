using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class ArtistsRepository
    {
        MusicLibraryDb musicLibraryDb = new MusicLibraryDb();
        public void AddNewArtist()
        {
            Console.WriteLine("Provide new Artist's Name: ");
            string NewArtistName = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Country:");
            string NewArtistCountry = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Genre:");
            string NewArtistGenre = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Descriptio");
            string NewArtistDescription = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(NewArtistName))
            {
                Console.WriteLine("Invalid Name Input");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewArtistCountry))
            {
                Console.WriteLine("Invalid Country Input");
                return;
            }


            if (string.IsNullOrWhiteSpace(NewArtistGenre))
            {
                Console.WriteLine("Invalid Genre Input");
                return;
            }


            if (string.IsNullOrWhiteSpace(NewArtistDescription))
            {
                Console.WriteLine("Invalid Description Input");
                return;
            }

            var ExistsOrNot = musicLibraryDb.Artists.FirstOrDefault(a => a.Name.ToLower() == NewArtistName.ToLower());
            if (ExistsOrNot == null)
            {
                Artist NewArtist = new Artist { Name = NewArtistName, Country = NewArtistCountry, Genre = NewArtistGenre, Description = NewArtistDescription };
                musicLibraryDb.Artists.Add(NewArtist);
                musicLibraryDb.SaveChanges();
                Console.WriteLine($"Added new artist: {NewArtist.Name}");
            }
            else
            {
                Console.WriteLine($"Artist with provided name: {NewArtistName}, already exists.");
                return;
            }

        }


    }
}