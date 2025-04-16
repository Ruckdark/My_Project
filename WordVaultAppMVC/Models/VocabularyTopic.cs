namespace WordVaultAppMVC.Models
{
    // Lớp trung gian để liên kết từ vựng và chủ đề (quan hệ nhiều - nhiều)
    public class VocabularyTopic
    {
        public int VocabularyId { get; set; }
        public int TopicId { get; set; }
    }
}
