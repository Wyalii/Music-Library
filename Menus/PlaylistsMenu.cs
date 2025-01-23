namespace Music_Library
{
    public class PlaylistsMenu
    {
        private readonly PlaylistRepository _playlistRepository;
        public PlaylistsMenu(PlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(PlaylistRepository));
        }
        public void ShowPlaylistsMenu()
        {

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
                        _playlistRepository.CreatePlayList();
                        break;

                    case 2:
                        _playlistRepository.AddSongToPlaylist();
                        break;

                    case 3:
                        _playlistRepository.PlayPlaylistSongs();
                        break;

                    case 0:
                        start = false;
                        break;
                }
            } while (start);
        }
    }
}