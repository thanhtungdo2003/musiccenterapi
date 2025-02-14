using MusicCenterAPI.Data;

namespace MusicCenterAPI.Models
{
    public class Artist
    {
        public Guid ArtistUid { get; set; }
        public string StageName { get; set; }
        public string Avata { get; set; }
        public int Visits { get; set; }

        public List<RecordData> Records = new List<RecordData>();

    }
}
