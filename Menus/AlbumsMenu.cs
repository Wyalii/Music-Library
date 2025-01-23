using MusicLibrary;

namespace Music_Library
{
    public class AlbumsMenu
    {
        private readonly AlbumsRepository _albumsRepository;
        public AlbumsMenu(AlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository ?? throw new ArgumentNullException(nameof(albumsRepository));
        }
        bool start = true;
        public void ShowAlbumsMenu()
        {
            start = true;
            Console.Clear();
            do
            {
                Console.WriteLine("2. Albums Repostiory: ");
                Console.WriteLine("    2.1 Add New Album");
                Console.WriteLine("    2.2 Update Album");
                Console.WriteLine("    2.3 Delete Album");
                Console.WriteLine("    2.4 List Of Album");
                Console.WriteLine("    2.5 Update Rating Of Album");
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
                        _albumsRepository.AddAlbum();
                        break;

                    case 2:
                        _albumsRepository.UpdateAlbum();
                        break;

                    case 3:
                        _albumsRepository.RemoveAlbum();
                        break;

                    case 4:
                        _albumsRepository.PrintAlbums();
                        break;

                    case 5:
                        _albumsRepository.UpdateAlbumsRating();
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        start = false;
                        break;

                    case 0:

                        start = false;
                        break;
                }
            } while (start);
        }
    }
}