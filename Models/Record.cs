using MusicCenterAPI.Data;

namespace MusicCenterAPI.Models
{
    public class Record
    {
        public Guid uid { get; set; }
        public Guid categoryUid { get; set; }
        public string displayName { get; set; }
        public int views { get; set; }
        public ArtistData artist { get; set; }
        public string record { get; set; }
        public string Poster { get; set; }
        public string CoverPhoto { get; set; }
        public string getImgPath(string filename, string folder)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folder, filename);

            return filePath;
        }
    }
}
