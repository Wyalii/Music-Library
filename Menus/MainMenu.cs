namespace Music_Library
{
    public class MainMenu
    {
        private readonly ArtistsMenu _artistsMenu;
        private readonly AlbumsMenu _albumsMenu;
        private readonly AnalitycsMenu _analitycsMenu;
        private readonly SongsMenu _songsMenu;
        private readonly PlaylistsMenu _playlistsMenu;


        public MainMenu(ArtistsMenu artistsMenu, AlbumsMenu albumsMenu, AnalitycsMenu analitycsMenu, SongsMenu songsMenu, PlaylistsMenu playlistsMenu)
        {
            _artistsMenu = artistsMenu ?? throw new ArgumentNullException(nameof(artistsMenu));
            _albumsMenu = albumsMenu ?? throw new ArgumentNullException(nameof(albumsMenu));
            _analitycsMenu = analitycsMenu ?? throw new ArgumentNullException(nameof(analitycsMenu));
            _songsMenu = songsMenu ?? throw new ArgumentNullException(nameof(songsMenu));
            _playlistsMenu = playlistsMenu ?? throw new ArgumentNullException(nameof(playlistsMenu));
        }
        public void ShowMainMenu()
        {
            bool start = true;
            do
            {
                Console.Clear();
                Console.WriteLine();

                Console.WriteLine("Choose Menu:");

                Console.WriteLine();
                Console.WriteLine("1. Artists Repository: ");
                Console.WriteLine("    1.1 Add New Artist");
                Console.WriteLine("    1.2 Update Artist");
                Console.WriteLine("    1.3 Delete Artist");
                Console.WriteLine("    1.4 List Of Artists");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("2. Albums Repostiory: ");
                Console.WriteLine("    2.1 Add New Album");
                Console.WriteLine("    2.2 Update Album");
                Console.WriteLine("    2.3 Delete Album");
                Console.WriteLine("    2.4 List Of Album");
                Console.WriteLine("    2.5 Update Rating Of Album");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("3. Songs Repository");
                Console.WriteLine("    3.1 Add New Song");
                Console.WriteLine("    3.2 Update Song");
                Console.WriteLine("    3.3 Delete Song");
                Console.WriteLine("    3.4 Play Music");
                Console.WriteLine("    3.5 List Of Songs");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("4. AnalityscRepostiory");
                Console.WriteLine("    4.1 Most Listened Songs");
                Console.WriteLine("    4.2 Genre Statistics");
                Console.WriteLine("    4.3 Artists Ratings");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("5. PlaylistRepository");
                Console.WriteLine("    5.1 Create Playlist");
                Console.WriteLine("    5.2 add song to a  Playlist");
                Console.WriteLine("    5.3 play your Playlist");

                Console.WriteLine();
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
                    case 0:
                        start = false;
                        break;

                    case 1:
                        _artistsMenu.ShowArtistsMenu();
                        break;

                    case 2:
                        _albumsMenu.ShowAlbumsMenu();
                        break;

                    case 3:
                        _songsMenu.ShowSongsMenu();
                        break;

                    case 4:
                        _analitycsMenu.ShowAnalitycsMenu();
                        break;

                    case 5:
                        _playlistsMenu.ShowPlaylistsMenu();
                        break;
                }


            } while (start);
        }
    }
}