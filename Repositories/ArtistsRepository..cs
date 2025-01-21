using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary
{
    public class ArtistsRepository
    {
        MusicLibraryDb musicLibraryDb = new MusicLibraryDb();
        public void AddNewArtist()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating System Logs File...");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System Logs File on your desktop.");
            }
            Console.WriteLine("Provide new Artist's Name: ");
            string NewArtistName = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Country:");
            string NewArtistCountry = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Genre:");
            string NewArtistGenre = Console.ReadLine();
            Console.WriteLine("Provide new Artist's Descriptio");
            string NewArtistDescription = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(NewArtistName))
            {
                Console.WriteLine("Invalid Name Input");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewArtistCountry))
            {
                Console.WriteLine("Invalid Country Input");
                return;
            }


            if (string.IsNullOrWhiteSpace(NewArtistGenre))
            {
                Console.WriteLine("Invalid Genre Input");
                return;
            }


            if (string.IsNullOrWhiteSpace(NewArtistDescription))
            {
                Console.WriteLine("Invalid Description Input");
                return;
            }

            var ExistsOrNot = musicLibraryDb.Artists.FirstOrDefault(a => a.Name.ToLower() == NewArtistName.ToLower());
            try
            {
                if (ExistsOrNot == null)
                {
                    Artist NewArtist = new Artist { Name = NewArtistName, Country = NewArtistCountry, Genre = NewArtistGenre, Description = NewArtistDescription };
                    musicLibraryDb.Artists.Add(NewArtist);
                    musicLibraryDb.SaveChanges();
                    Console.WriteLine($"Added new artist: {NewArtist.Name}");
                    Console.WriteLine();
                    Console.WriteLine($"Now Fill {NewArtist.Name}'s Details:");
                    Console.WriteLine();
                    Console.WriteLine("FormationYear: ");
                    string FormationYearInput = Console.ReadLine();
                    int FromationYearValue;

                    if (string.IsNullOrWhiteSpace(FormationYearInput) || !int.TryParse(FormationYearInput, out FromationYearValue))
                    {
                        Console.WriteLine("Invalid Formation Year Input.");
                        return;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Website: ");
                    string WebsiteInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(WebsiteInput))
                    {
                        Console.WriteLine("Invalid Website Input.");
                        return;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Total Albums: ");
                    string TotalAlbumsInput = Console.ReadLine();
                    int TotalAlbumsValue;
                    if (string.IsNullOrWhiteSpace(TotalAlbumsInput) || !int.TryParse(TotalAlbumsInput, out TotalAlbumsValue))
                    {
                        Console.WriteLine("Invalid Total Albums Input.");
                        return;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Is Active: ");
                    string IsActiveInput = Console.ReadLine();
                    bool IsActiveValue;
                    if (string.IsNullOrWhiteSpace(IsActiveInput) || !bool.TryParse(IsActiveInput, out IsActiveValue))
                    {

                        Console.WriteLine("Invalid Formation Year Input.");
                        return;
                    }


                    ArtistDetails NewArtistsDetails = new ArtistDetails { ArtistId = NewArtist.Id, FormationYear = FromationYearValue, Website = WebsiteInput, TotalAlbums = TotalAlbumsValue, IsActive = IsActiveValue };
                    NewArtist.ArtistDetails = NewArtistsDetails;
                    musicLibraryDb.SaveChanges();
                    string logEntry = $"{Environment.NewLine} {DateTime.Now}: Created Artist - {NewArtist.Name}";
                    File.AppendAllText(SystemLogsFile, logEntry);

                    Console.WriteLine($"Success, Filled {NewArtist.Name}'s Details, Finished Adding Artist to database. ");
                    return;


                }
                else
                {
                    Console.WriteLine($"Artist with provided name: {NewArtistName}, already exists.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

        }

        public void RemoveArtist()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating System Logs File...");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System Logs File on your desktop.");
            }

            Console.WriteLine("Provide Id of Artist to remove:");
            string IdInput = Console.ReadLine();
            int IdValue;

            if (!string.IsNullOrWhiteSpace(IdInput) && int.TryParse(IdInput, out IdValue))
            {
                try
                {
                    var ExistsOrNot = musicLibraryDb.Artists.FirstOrDefault(a => a.Id == IdValue);
                    if (ExistsOrNot != null)
                    {
                        musicLibraryDb.Artists.Remove(ExistsOrNot);
                        musicLibraryDb.SaveChanges();
                        string logEntry = $"{Environment.NewLine} {DateTime.Now}: Removed Artist - {ExistsOrNot.Name}";
                        File.AppendAllText(SystemLogsFile, logEntry);
                        Console.WriteLine($"Success, Removed Artist: {ExistsOrNot.Name}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Artist with provided Id: {IdValue}, doesn't exists");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            else
            {
                Console.WriteLine("Invalid Id Input");
                return;
            }
        }

        public void UpdateArtist()
        {
            Console.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string SystemLogsFile = Path.Combine(desktopPath, "music_system_log.txt");
            if (!File.Exists(SystemLogsFile))
            {
                Console.WriteLine("Creating System Logs File...");
                FileStream fs = File.Create(SystemLogsFile);
                fs.Close();
                Console.WriteLine("Created System Logs File on your desktop.");
            }

            Console.WriteLine("Provide Artist Id to update: ");
            string IdInput = Console.ReadLine();
            int IdValue;
            if (!string.IsNullOrWhiteSpace(IdInput) && int.TryParse(IdInput, out IdValue))
            {
                try
                {
                    var ExistsOrNot = musicLibraryDb.Artists.Include(a => a.ArtistDetails).FirstOrDefault(a => a.Id == IdValue);
                    if (ExistsOrNot != null)
                    {
                        Console.WriteLine("Wirte Artist's New name:");
                        string NewName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(NewName))
                        {
                            Console.WriteLine("Wrong Name input.");
                            return;
                        }

                        Console.WriteLine("Write Artist's New Genre:");
                        string NewGenre = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(NewGenre))
                        {
                            Console.WriteLine("Wrong Genre input.");
                            return;
                        }

                        Console.WriteLine("Write Artist's New Country:");
                        string NewCountry = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(NewCountry))
                        {
                            Console.WriteLine("Wrong Country input.");
                            return;
                        }

                        Console.WriteLine("Write Artist's New Total Albums: ");
                        string TotalAlbumsInput = Console.ReadLine();
                        int TotalAlbumsValue;

                        if (string.IsNullOrWhiteSpace(TotalAlbumsInput) || !int.TryParse(TotalAlbumsInput, out TotalAlbumsValue))
                        {
                            Console.WriteLine("Invalid Total Albums Input.");
                            return;
                        }

                        Console.Write("Write Artist's new Formation Year:");
                        string FormationYearInput = Console.ReadLine();
                        int FormationYearValue;

                        if (string.IsNullOrWhiteSpace(FormationYearInput) || !int.TryParse(FormationYearInput, out FormationYearValue))
                        {
                            Console.WriteLine("Wrong Formation Year input.");
                            return;
                        }

                        Console.Write("Write Artist's new Website:");
                        string WebsiteInput = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(WebsiteInput))
                        {
                            Console.WriteLine("Wrong Website input.");
                            return;
                        }

                        Console.Write("Write Artist's new Is Active:");
                        string IsActiveInput = Console.ReadLine();
                        bool IsActiveValue;

                        if (string.IsNullOrWhiteSpace(IsActiveInput) || !bool.TryParse(IsActiveInput, out IsActiveValue))
                        {
                            Console.WriteLine("Wrong Is Active input.");
                            return;
                        }


                        Console.WriteLine("Write Artist's New Description:");
                        string NewDescription = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(NewDescription))
                        {
                            Console.WriteLine("Wrong Name input.");
                            return;
                        }



                        ExistsOrNot.Name = NewName;
                        ExistsOrNot.Country = NewCountry;
                        ExistsOrNot.Genre = NewGenre;
                        ExistsOrNot.Description = NewDescription;

                        ExistsOrNot.ArtistDetails.TotalAlbums = TotalAlbumsValue;
                        ExistsOrNot.ArtistDetails.FormationYear = FormationYearValue;
                        ExistsOrNot.ArtistDetails.IsActive = IsActiveValue;
                        ExistsOrNot.ArtistDetails.Website = WebsiteInput;
                        musicLibraryDb.SaveChanges();
                        string logEntry = $"{Environment.NewLine} {DateTime.Now}: Updated Artist - {ExistsOrNot.Name}";
                        File.AppendAllText(SystemLogsFile, logEntry);
                        Console.WriteLine("Success, Updated Artist!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Artist with provided Id: {IdValue}, doesn't exists");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            else
            {
                Console.WriteLine("Invalid Id Input");
                return;
            }
        }

        public void PrintArtists()
        {
            Console.Clear();
            List<Artist> AllArtists = musicLibraryDb.Artists.Include(a => a.ArtistDetails).ToList();
            if (AllArtists.Count > 0)
            {
                Console.WriteLine("All Artists: ");
                Console.WriteLine();
                foreach (Artist artist in AllArtists)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Artist Id: {artist.Id}, Name: {artist.Name},");
                    Console.WriteLine();
                    Console.WriteLine($"Is Active: {artist.ArtistDetails.IsActive}");
                    Console.WriteLine();
                    Console.WriteLine($"Formation Year: {artist.ArtistDetails.FormationYear}");
                    Console.WriteLine();
                    Console.WriteLine($"Country: {artist.Country}");
                    Console.WriteLine();
                    Console.WriteLine($"Webstie: {artist.ArtistDetails.Website}");
                    Console.WriteLine();
                    Console.WriteLine($"Genre: {artist.Genre}");
                    Console.WriteLine();
                    Console.WriteLine($"Total Albums: {artist.ArtistDetails.TotalAlbums}");
                    Console.WriteLine();
                    Console.WriteLine("Description:");
                    Console.WriteLine($"{artist.Description}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No Artists Added.");
            }
        }
    }
}