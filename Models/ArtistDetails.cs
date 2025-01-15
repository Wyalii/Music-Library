namespace MusicLibrary
{
    public class ArtistDetails
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int FormationYear { get; set; }
        public string Website { get; set; }
        public int TotalAlbums { get; set; }
        public bool IsActive { get; set; }
        public Artist Artist { get; set; }


    }
}