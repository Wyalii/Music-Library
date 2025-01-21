using System.Data;
using MusicLibrary;

namespace Music_Library
{
    public class SongsMenu
    {
        bool start = true;
        SongsRepository songsRepository = new SongsRepository();
        public void ShowSongsMenu()
        {
            start = true; ;
            Console.Clear();


            do
            {
                Console.WriteLine("3. Songs Repository");
                Console.WriteLine("    3.1 Add New Song");
                Console.WriteLine("    3.2 Update Song");
                Console.WriteLine("    3.3 Delete Song");
                Console.WriteLine("    3.4 Play Music");
                Console.WriteLine("    3.5 List Of Songs");

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
                        songsRepository.AddSong();
                        break;

                    case 2:
                        songsRepository.UpdateSong();
                        break;

                    case 3:
                        songsRepository.DeleteSong();
                        break;

                    case 4:
                        songsRepository.PlayMusic();
                        break;

                    case 5:
                        songsRepository.PrintSongs();
                        break;

                    case 0:
                        start = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        start = false;
                        break;
                }

            } while (start);
        }
    }
}