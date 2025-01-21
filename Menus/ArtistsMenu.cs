using MusicLibrary;

namespace Music_Library
{
    public class ArtistsMenu
    {
        ArtistsRepository artistsRepository = new ArtistsRepository();
        bool start = true;
        public void ShowArtistsMenu()
        {
            start = true;
            Console.Clear();
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Artists Repository: ");
                Console.WriteLine("    1.1 Add New Artist");
                Console.WriteLine("    1.2 Update Artist");
                Console.WriteLine("    1.3 Delete Artist");
                Console.WriteLine("    1.4 List Of Artists");
                Console.WriteLine();

                Console.WriteLine("0 - Exit");
                string ActionInput = Console.ReadLine();
                int ActionValue;

                if (string.IsNullOrWhiteSpace(ActionInput) || !int.TryParse(ActionInput, out ActionValue))
                {
                    Console.WriteLine("Invalid Action Input.");
                    return;
                }

                switch (ActionValue)
                {
                    case 1:
                        artistsRepository.AddNewArtist();
                        break;

                    case 2:
                        artistsRepository.UpdateArtist();
                        break;

                    case 3:
                        artistsRepository.RemoveArtist();
                        break;

                    case 4:
                        artistsRepository.PrintArtists();
                        break;

                    case 0:
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input.");
                        break;
                }
            } while (start);

        }
    }
}