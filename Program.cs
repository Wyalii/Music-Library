namespace MusicLibrary
{
    public class Program
    {
        static void Main()
        {
            ArtistsRepository artistsRepository = new ArtistsRepository();
            AlbumsRepository albumsRepository = new AlbumsRepository();

            albumsRepository.PrintAlbums();
        }
    }
}