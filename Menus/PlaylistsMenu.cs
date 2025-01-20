namespace Music_Library
{
    public class PlaylistsMenu()
    {
        public void ShowPlaylistsMenu()
        {
            PlaylistRepository playlistRepository = new PlaylistRepository();
            bool start = true;
            Console.Clear();
            do
            {
                Console.WriteLine("5. PlaylistRepository");
                Console.WriteLine("    5.1 Create Playlist");
                Console.WriteLine("    5.2 add song to a  Playlist");
                Console.WriteLine("    5.3 play your Playlist");
                Console.WriteLine();
                Console.WriteLine("0. Exit");

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
                        playlistRepository.CreatePlayList();
                        break;

                    case 2:
                        playlistRepository.AddSongToPlaylist();
                        break;

                    case 3:
                        playlistRepository.PlayPlaylistSongs();
                        break;
                }
            } while (start);
        }
    }
}