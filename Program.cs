using Music_Library;

namespace MusicLibrary
{
    public class Program
    {
        static void Main()
        {
            MainMenu mainMenu = new MainMenu();
            PlaylistRepository playlistRepository = new PlaylistRepository();
            playlistRepository.PlayPlaylistSongs();



        }
    }
}