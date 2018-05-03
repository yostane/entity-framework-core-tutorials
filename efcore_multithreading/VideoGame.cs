using System.ComponentModel.DataAnnotations;

namespace efcore_multithreading {
    public class VideoGame {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Platform { get; set; }

        public int ReleaseYear { get; set; }
    }
}