namespace MusicLibrary
{
    public class Song
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string Lyrics { get; set; }
        public int TrackNumber { get; set; }
        public int TimesPlayed { get; set; }
        public Album Album { get; set; }

    }
}