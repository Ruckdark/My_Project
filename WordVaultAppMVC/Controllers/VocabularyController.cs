using System;
using System.Collections.Generic;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Controllers
{
    public class VocabularyController
    {
        private readonly VocabularyRepository _vocabularyRepository;
        private VocabularyRepository _repository;

        public VocabularyController()
        {
            _vocabularyRepository = new VocabularyRepository();
            _repository = new VocabularyRepository();
        }

        // Lấy danh sách từ vựng
        public List<Vocabulary> GetAllVocabulary()
        {
            return _vocabularyRepository.GetAllVocabulary();
        }
        public List<Vocabulary> GetVocabularyByTopic(string topic)
        {
            return _repository.GetVocabularyByTopic(topic);
        }

        public void RemoveVocabulary(int id)
        {
            _repository.DeleteVocabulary(id);
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
