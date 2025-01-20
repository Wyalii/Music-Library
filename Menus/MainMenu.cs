namespace Music_Library
{
    public class MainMenu
    {
        ArtistsMenu artistsMenu = new ArtistsMenu();
        AlbumsMenu albumsMenu = new AlbumsMenu();
        AnalitycsMenu analitycsMenu = new AnalitycsMenu();
        SongsMenu songsMenu = new SongsMenu();

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
                        artistsMenu.ShowArtistsMenu();
                        break;

                    case 2:
                        albumsMenu.ShowAlbumsMenu();
                        break;

                    case 3:
                        songsMenu.ShowSongsMenu();
                        break;

                    case 4:
                        analitycsMenu.ShowAnalitycsMenu();
                        break;
                }


            } while (start);
        }
    }
}