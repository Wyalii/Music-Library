using Music_Library;

namespace MusicLibrary
{
    public class Program
    {
        static void Main()
        {
            MusicLibraryDb musicLibraryDb = new MusicLibraryDb();


            ArtistsRepository artistsRepository = new ArtistsRepository(musicLibraryDb);
            SongsRepository songsRepository = new SongsRepository(musicLibraryDb);
            AlbumsRepository albumsRepository = new AlbumsRepository(musicLibraryDb);
            AnalitycsRepository analitycsRepository = new AnalitycsRepository(musicLibraryDb);
            PlaylistRepository playlistRepository = new PlaylistRepository(musicLibraryDb);


            ArtistsMenu artistsMenu = new ArtistsMenu(artistsRepository);
            SongsMenu songsMenu = new SongsMenu(songsRepository);
            AlbumsMenu albumsMenu = new AlbumsMenu(albumsRepository);
            AnalitycsMenu analitycsMenu = new AnalitycsMenu(analitycsRepository);
            PlaylistsMenu playlistsMenu = new PlaylistsMenu(playlistRepository);

            MainMenu mainMenu = new MainMenu(artistsMenu, albumsMenu, analitycsMenu, songsMenu, playlistsMenu);

            mainMenu.ShowMainMenu();




        }
    }
}