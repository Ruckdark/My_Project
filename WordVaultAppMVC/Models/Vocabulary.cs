using System;

namespace WordVaultAppMVC.Models
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public string Word { get; set; }          // Từ vựng
        public string Meaning { get; set; }         // Nghĩa của từ
        public string Pronunciation { get; set; }   // Phát âm (ví dụ: /ˈɛɡzæmpəl/)
        public string AudioUrl { get; set; }        // URL âm thanh phát âm   
    }
}
