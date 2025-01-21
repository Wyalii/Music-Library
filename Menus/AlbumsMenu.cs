using MusicLibrary;

namespace Music_Library
{
    public class AlbumsMenu
    {
        AlbumsRepository albumsRepository = new AlbumsRepository();
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
                        albumsRepository.AddAlbum();
                        break;

                    case 2:
                        albumsRepository.UpdateAlbum();
                        break;

                    case 3:
                        albumsRepository.RemoveAlbum();
                        break;

                    case 4:
                        albumsRepository.PrintAlbums();
                        break;

                    case 5:
                        albumsRepository.UpdateAlbumsRating();
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