namespace MusicLibrary
{
    public class Program
    {
        static void Main()
        {
            ArtistsRepository artistsRepository = new ArtistsRepository();
            AlbumsRepository albumsRepository = new AlbumsRepository();
            SongsRepository songsRepository = new SongsRepository();
            artistsRepository.PrintArtists();


        }
    }
}