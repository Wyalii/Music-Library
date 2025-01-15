namespace MusicLibrary
{
    public class Album
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
        public Artist Artist { get; set; }

    }
}