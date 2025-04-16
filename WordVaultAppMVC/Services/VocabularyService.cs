using System;
using System.Collections.Generic;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Services
{
    public class VocabularyService
    {
        private readonly VocabularyRepository _vocabularyRepository;
        private static readonly Random rnd = new Random();

        public VocabularyService()
        {
            _vocabularyRepository = new VocabularyRepository();
        }

        /// <summary>
        /// Lấy một từ vựng ngẫu nhiên từ cơ sở dữ liệu.
        /// Trả về từ vựng và ID của nó qua out parameter.
        /// </summary>
        public string GetRandomWord(out string wordId)
        {
            List<Vocabulary> vocabularies = _vocabularyRepository.GetAllVocabulary();
            if (vocabularies == null || vocabularies.Count == 0)
            {
                wordId = null;
                return string.Empty;
            }
            int index = rnd.Next(vocabularies.Count);
            wordId = vocabularies[index].Id.ToString();
            return vocabularies[index].Word;
        }

        /// <summary>
        /// Lấy nghĩa của từ vựng theo ID.
        /// </summary>
        public string GetWordMeaning(string wordId)
        {
            if (int.TryParse(wordId, out int id))
            {
                Vocabulary vocab = _vocabularyRepository.GetVocabularyById(id);
                if (vocab != null)
                {
                    return vocab.Meaning;
                }
            }
            return "Không tìm thấy nghĩa!";
        }
    }
}
