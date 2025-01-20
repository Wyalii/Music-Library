using MusicLibrary;

namespace Music_Library
{
    public class AnalitycsMenu
    {
        AnalitycsRepository analitycsRepository = new AnalitycsRepository();
        bool start = true;
        public void ShowAnalitycsMenu()
        {
            Console.Clear();

            do
            {
                Console.WriteLine();
                Console.WriteLine("4. AnalityscRepostiory");
                Console.WriteLine("    4.1 Most Listened Songs");
                Console.WriteLine("    4.2 Genre Statistics");
                Console.WriteLine("    4.3 Artists Ratings");
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
                        analitycsRepository.PrintMostListenedSongs();
                        break;

                    case 2:
                        analitycsRepository.PrintGenreStatistics();
                        break;

                    case 3:
                        analitycsRepository.PrintArtistsRatings();
                        break;

                    case 0:
                        start = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Input.");
                        start = false;
                        break;
                }
            } while (start);
        }
    }
}