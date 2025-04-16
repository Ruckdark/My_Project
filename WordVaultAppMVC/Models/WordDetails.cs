using System.Collections.Generic;

namespace WordVaultAppMVC.Models
{
    public class WordDetails
    {
        public string Word { get; set; }
        public string Pronunciation { get; set; }
        public string AudioUrl { get; set; }
        public string Meaning { get; set; }

        // Mở rộng thêm nếu bạn muốn dùng sau:
        public List<string> AllMeanings { get; set; } = new List<string>();
        public string PartOfSpeech { get; set; } // noun, verb...
        public List<string> ExampleSentences { get; set; } = new List<string>();
    }
}
