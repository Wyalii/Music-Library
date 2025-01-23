using MusicLibrary;

namespace Music_Library
{
    public class AnalitycsMenu
    {
        private readonly AnalitycsRepository _analitycsRepository;
        public AnalitycsMenu(AnalitycsRepository analitycsRepository)
        {
            _analitycsRepository = analitycsRepository ?? throw new ArgumentNullException(nameof(analitycsRepository));
        }
        bool start = true;
        public void ShowAnalitycsMenu()
        {
            start = true;
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
                        _analitycsRepository.PrintMostListenedSongs();
                        break;

                    case 2:
                        _analitycsRepository.PrintGenreStatistics();
                        break;

                    case 3:
                        _analitycsRepository.PrintArtistsRatings();
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