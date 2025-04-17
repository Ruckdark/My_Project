using System;
using System.Collections.Generic;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Controllers
{
    public class VocabularyController
    {
        private readonly VocabularyRepository _vocabularyRepository;

        public VocabularyController()
        {
            _vocabularyRepository = new VocabularyRepository();
        }

        // Lấy danh sách từ vựng
        public List<Vocabulary> GetAllVocabulary()
        {
            return _vocabularyRepository.GetAllVocabulary();
        }

        // Lấy danh sách từ vựng theo chủ đề
        public List<Vocabulary> GetVocabularyByTopic(string topic)
        {
            return _vocabularyRepository.GetVocabularyByTopic(topic);
        }

        // Xóa từ vựng theo ID
        public void RemoveVocabulary(int id)
        {
            _vocabularyRepository.DeleteVocabulary(id);
        }

        // Thêm từ vựng mới
        public void AddVocabulary(string word, string meaning, string pronunciation, string audioUrl)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("Từ vựng không được để trống.");
            }

            _vocabularyRepository.AddVocabulary(word, meaning, pronunciation, audioUrl);
        }
    }
}
