using MusicLibrary;

namespace Music_Library
{
    public class ArtistsMenu
    {
        private readonly ArtistsRepository _artistsRepository;
        public ArtistsMenu(ArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository ?? throw new ArgumentNullException(nameof(artistsRepository));
        }
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
                        _artistsRepository.AddNewArtist();
                        break;

                    case 2:
                        _artistsRepository.UpdateArtist();
                        break;

                    case 3:
                        _artistsRepository.RemoveArtist();
                        break;

                    case 4:
                        _artistsRepository.PrintArtists();
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