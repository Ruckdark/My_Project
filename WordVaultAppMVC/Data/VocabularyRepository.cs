using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq; // Required for .Any() if used
using WordVaultAppMVC.Models;
using WordVaultAppMVC.Data; // Assuming DatabaseContext is here

namespace WordVaultAppMVC.Data
{
    public class VocabularyRepository
    {
        // --- Constants ---
        private const string VocabularyTableName = "Vocabulary";
        private const string FavoriteWordsTableName = "FavoriteWords";
        private const string TopicTableName = "Topics";
        private const string VocabTopicTableName = "VocabularyTopic";

        // Sử dụng nameof() để an toàn hơn khi đổi tên thuộc tính trong Model
        private const string ColId = nameof(Vocabulary.Id);
        private const string ColWord = nameof(Vocabulary.Word);
        private const string ColMeaning = nameof(Vocabulary.Meaning);
        private const string ColPronunciation = nameof(Vocabulary.Pronunciation);
        private const string ColAudioUrl = nameof(Vocabulary.AudioUrl);
        // Tên cột trong các bảng khác (cần khớp với DB schema của bạn)
        private const string FavColVocabId = "VocabularyId";
        private const string TopicColName = "Name";
        private const string VTColVocabId = "VocabularyId";
        private const string VTColTopicId = "TopicId";

        // --- Helper: Map SqlDataReader to Vocabulary Object ---
        private Vocabulary MapReaderToVocabulary(SqlDataReader reader)
        {
            if (reader == null || !reader.HasRows) return null;
            try
            {
                int idOrdinal = reader.GetOrdinal(ColId); int wordOrdinal = reader.GetOrdinal(ColWord); int meaningOrdinal = reader.GetOrdinal(ColMeaning); int pronunciationOrdinal = reader.GetOrdinal(ColPronunciation); int audioUrlOrdinal = reader.GetOrdinal(ColAudioUrl);
                return new Vocabulary
                {
                    Id = reader.IsDBNull(idOrdinal) ? 0 : reader.GetInt32(idOrdinal),
                    Word = reader.IsDBNull(wordOrdinal) ? string.Empty : reader.GetString(wordOrdinal),
                    Meaning = reader.IsDBNull(meaningOrdinal) ? string.Empty : reader.GetString(meaningOrdinal),
                    Pronunciation = reader.IsDBNull(pronunciationOrdinal) ? string.Empty : reader.GetString(pronunciationOrdinal),
                    AudioUrl = reader.IsDBNull(audioUrlOrdinal) ? string.Empty : reader.GetString(audioUrlOrdinal)
                };
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Mapping Error: {ex.Message}"); return null; }
        }

        // --- Get All Vocabulary ---
        public List<Vocabulary> GetAllVocabulary()
        {
            List<Vocabulary> vocabularies = new List<Vocabulary>(); string query = $"SELECT {ColId}, {ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl} FROM {VocabularyTableName}"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { conn.Open(); using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { Vocabulary vocab = MapReaderToVocabulary(reader); if (vocab != null) vocabularies.Add(vocab); } } }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in GetAllVocabulary: {ex.Message}"); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return vocabularies;
        }

        // --- Get Vocabulary By ID ---
        public Vocabulary GetVocabularyById(int id)
        {
            Vocabulary vocab = null; if (id <= 0) return null;
            string query = $"SELECT {ColId}, {ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl} FROM {VocabularyTableName} WHERE {ColId} = @Id"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id; conn.Open(); using (SqlDataReader reader = cmd.ExecuteReader()) { if (reader.Read()) { vocab = MapReaderToVocabulary(reader); } } }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in GetVocabularyById for ID {id}: {ex.Message}"); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return vocab;
        }

        // --- Get Vocabulary By Word (Phương thức mới) ---
        public Vocabulary GetVocabularyByWord(string word)
        {
            Vocabulary vocab = null; if (string.IsNullOrWhiteSpace(word)) return null;
            string query = $"SELECT {ColId}, {ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl} FROM {VocabularyTableName} WHERE {ColWord} = @Word"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word.Trim(); conn.Open(); using (SqlDataReader reader = cmd.ExecuteReader()) { if (reader.Read()) { vocab = MapReaderToVocabulary(reader); } } }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in GetVocabularyByWord for '{word}': {ex.Message}"); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return vocab;
        }

        // --- AddVocabulary (Phiên bản mới trả về Vocabulary) ---
        public Vocabulary AddVocabulary(Vocabulary vocab)
        {
            if (vocab == null || string.IsNullOrWhiteSpace(vocab.Word) || string.IsNullOrWhiteSpace(vocab.Meaning)) { Debug.WriteLine("[VocabularyRepository] AddVocabulary(obj) failed: Input invalid."); return null; }
            string query = $@"INSERT INTO {VocabularyTableName} ({ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl}) OUTPUT INSERTED.{ColId} VALUES (@Word, @Meaning, @Pronunciation, @AudioUrl);"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = vocab.Word.Trim();
                    cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 500).Value = vocab.Meaning.Trim();
                    cmd.Parameters.Add("@Pronunciation", SqlDbType.NVarChar, 100).Value = string.IsNullOrEmpty(vocab.Pronunciation) ? DBNull.Value : (object)vocab.Pronunciation;
                    cmd.Parameters.Add("@AudioUrl", SqlDbType.NVarChar, 300).Value = string.IsNullOrEmpty(vocab.AudioUrl) ? DBNull.Value : (object)vocab.AudioUrl;
                    conn.Open(); object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value) { vocab.Id = Convert.ToInt32(result); Debug.WriteLine($"[VocabularyRepository] Added vocabulary '{vocab.Word}' with ID: {vocab.Id}"); return vocab; }
                    else { Debug.WriteLine($"[VocabularyRepository] Failed to add '{vocab.Word}'. ExecuteScalar returned null/DBNull."); return null; }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in AddVocabulary(obj) for '{vocab.Word}': {ex.Message}"); return null; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
        }

        // --- AddVocabulary (Phiên bản cũ 4 tham số - Để tương thích) ---
        public void AddVocabulary(string word, string meaning, string pronunciation, string audioUrl)
        {
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(meaning)) { Debug.WriteLine("[VocabularyRepository-AddVocabulary(string)] Error: Word or Meaning is empty."); return; }
            SqlConnection conn = null;
            try
            {
                using (conn = DatabaseContext.GetConnection())
                {
                    conn.Open(); string query = $"INSERT INTO {VocabularyTableName} ({ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl}) VALUES (@Word, @Meaning, @Pronunciation, @AudioUrl)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word.Trim();
                        cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 500).Value = meaning.Trim();
                        cmd.Parameters.Add("@Pronunciation", SqlDbType.NVarChar, 100).Value = string.IsNullOrEmpty(pronunciation) ? DBNull.Value : (object)pronunciation;
                        cmd.Parameters.Add("@AudioUrl", SqlDbType.NVarChar, 300).Value = string.IsNullOrEmpty(audioUrl) ? DBNull.Value : (object)audioUrl;
                        cmd.ExecuteNonQuery(); Debug.WriteLine($"[VocabularyRepository] Executed AddVocabulary(string) for '{word}'.");
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository-AddVocabulary(string)] Error for '{word}': {ex.Message}"); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
        }

        // --- UpdateVocabulary (Chấp nhận cả object và tham số riêng - chọn 1 để dùng) ---
        // Phiên bản nhận object
        public bool UpdateVocabulary(Vocabulary vocab)
        {
            if (vocab == null || vocab.Id <= 0) return false;
            return UpdateVocabularyInternal(vocab.Id, vocab.Word, vocab.Meaning, vocab.Pronunciation, vocab.AudioUrl);
        }
        // Phiên bản nhận tham số riêng (giữ lại nếu Controller cũ gọi)
        public void UpdateVocabulary(int id, string word, string meaning, string pronunciation, string audioUrl)
        {
            UpdateVocabularyInternal(id, word, meaning, pronunciation, audioUrl);
        }
        // Hàm xử lý update nội bộ
        private bool UpdateVocabularyInternal(int id, string word, string meaning, string pronunciation, string audioUrl)
        {
            int rowsAffected = 0; SqlConnection conn = null;
            try
            {
                using (conn = DatabaseContext.GetConnection())
                {
                    conn.Open(); string query = $@"UPDATE {VocabularyTableName} SET {ColWord} = @Word, {ColMeaning} = @Meaning, {ColPronunciation} = @Pronunciation, {ColAudioUrl} = @AudioUrl WHERE {ColId} = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word;
                        cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 500).Value = meaning;
                        cmd.Parameters.Add("@Pronunciation", SqlDbType.NVarChar, 100).Value = string.IsNullOrEmpty(pronunciation) ? DBNull.Value : (object)pronunciation;
                        cmd.Parameters.Add("@AudioUrl", SqlDbType.NVarChar, 300).Value = string.IsNullOrEmpty(audioUrl) ? DBNull.Value : (object)audioUrl;
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in UpdateVocabulary for ID {id}: {ex.Message}"); return false; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return rowsAffected > 0;
        }


        // --- DeleteVocabulary ---
        public bool DeleteVocabulary(int id)
        {
            if (id <= 0) return false;
            int rowsAffected = 0; SqlConnection conn = null;
            try
            {
                using (conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    // Xóa liên kết trước
                    string deleteFavQuery = $"DELETE FROM {FavoriteWordsTableName} WHERE {FavColVocabId} = @Id";
                    using (SqlCommand cmdFav = new SqlCommand(deleteFavQuery, conn)) { cmdFav.Parameters.AddWithValue("@Id", id); cmdFav.ExecuteNonQuery(); }
                    string deleteVTQuery = $"DELETE FROM {VocabTopicTableName} WHERE {VTColVocabId} = @Id";
                    using (SqlCommand cmdVT = new SqlCommand(deleteVTQuery, conn)) { cmdVT.Parameters.AddWithValue("@Id", id); cmdVT.ExecuteNonQuery(); }
                    // ... Xóa khỏi LearningStatuses nếu cần ...

                    // Xóa từ bảng chính
                    string query = $"DELETE FROM {VocabularyTableName} WHERE {ColId} = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.AddWithValue("@Id", id); rowsAffected = cmd.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in DeleteVocabulary for ID {id}: {ex.Message}"); return false; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return rowsAffected > 0;
        }

        // --- Get Vocabulary By Topic ---
        public List<Vocabulary> GetVocabularyByTopic(string topicName)
        {
            var vocabularyList = new List<Vocabulary>(); if (string.IsNullOrWhiteSpace(topicName)) return vocabularyList;
            string query = $@"SELECT V.{ColId}, V.{ColWord}, V.{ColMeaning}, V.{ColPronunciation}, V.{ColAudioUrl} FROM {VocabularyTableName} V INNER JOIN {VocabTopicTableName} VT ON V.{ColId} = VT.{VTColVocabId} INNER JOIN {TopicTableName} T ON VT.{VTColTopicId} = T.{ColId} WHERE T.{TopicColName} = @TopicName ORDER BY V.{ColWord}"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand command = new SqlCommand(query, conn)) { command.Parameters.Add("@TopicName", SqlDbType.NVarChar, 100).Value = topicName; conn.Open(); using (SqlDataReader reader = command.ExecuteReader()) { while (reader.Read()) { Vocabulary vocab = MapReaderToVocabulary(reader); if (vocab != null) vocabularyList.Add(vocab); } } }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in GetVocabularyByTopic for '{topicName}': {ex.Message}"); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return vocabularyList;
        }

        // --- Get Favorite Vocabularies ---
        public List<Vocabulary> GetFavoriteVocabularies()
        {
            var favoriteList = new List<Vocabulary>(); string query = $@"SELECT V.{ColId}, V.{ColWord}, V.{ColMeaning}, V.{ColPronunciation}, V.{ColAudioUrl} FROM {VocabularyTableName} V INNER JOIN {FavoriteWordsTableName} F ON V.{ColId} = F.{FavColVocabId} ORDER BY V.{ColWord}"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { conn.Open(); Debug.WriteLine("[VocabularyRepository] Executing GetFavoriteVocabularies query."); using (SqlDataReader reader = cmd.ExecuteReader()) { while (reader.Read()) { Vocabulary vocab = MapReaderToVocabulary(reader); if (vocab != null) favoriteList.Add(vocab); } } Debug.WriteLine($"[VocabularyRepository] Found {favoriteList.Count} favorite vocabularies."); }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in GetFavoriteVocabularies: {ex.Message}"); favoriteList.Clear(); }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return favoriteList;
        }

        // --- Is Favorite ---
        public bool IsFavorite(int vocabularyId)
        {
            if (vocabularyId <= 0) return false; string query = $"SELECT COUNT(1) FROM {FavoriteWordsTableName} WHERE {FavColVocabId} = @VocabularyId"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.Add("@VocabularyId", SqlDbType.Int).Value = vocabularyId; conn.Open(); object result = cmd.ExecuteScalar(); return Convert.ToInt32(result) > 0; }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in IsFavorite for ID {vocabularyId}: {ex.Message}"); return false; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
        }

        // --- Add Favorite ---
        public bool AddFavorite(int vocabularyId)
        {
            if (vocabularyId <= 0) return false; string query = $"INSERT INTO {FavoriteWordsTableName} ({FavColVocabId}) VALUES (@VocabularyId)"; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.Add("@VocabularyId", SqlDbType.Int).Value = vocabularyId; conn.Open(); Debug.WriteLine($"[VocabularyRepository] Attempting AddFavorite for VocabularyId: {vocabularyId}"); cmd.ExecuteNonQuery(); Debug.WriteLine($"[VocabularyRepository] Successfully added favorite for VocabularyId: {vocabularyId}"); return true; }
            }
            catch (SqlException sqlEx) { if (sqlEx.Number == 2627 || sqlEx.Number == 2601) { Debug.WriteLine($"[VocabularyRepository] AddFavorite failed for ID {vocabularyId}: Already exists."); return true; } else { Debug.WriteLine($"[VocabularyRepository] SQL Error in AddFavorite for ID {vocabularyId}: {sqlEx.Message}"); return false; } }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] General Error in AddFavorite for ID {vocabularyId}: {ex.Message}"); return false; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
        }

        // --- Remove Favorite ---
        public bool RemoveFavorite(int vocabularyId)
        {
            if (vocabularyId <= 0) return false; string query = $"DELETE FROM {FavoriteWordsTableName} WHERE {FavColVocabId} = @VocabularyId"; int rowsAffected = 0; SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn)) { cmd.Parameters.Add("@VocabularyId", SqlDbType.Int).Value = vocabularyId; conn.Open(); Debug.WriteLine($"[VocabularyRepository] Executing RemoveFavorite for VocabularyId: {vocabularyId}"); rowsAffected = cmd.ExecuteNonQuery(); Debug.WriteLine($"[VocabularyRepository] Rows affected by RemoveFavorite: {rowsAffected}"); }
            }
            catch (Exception ex) { Debug.WriteLine($"[VocabularyRepository] Error in RemoveFavorite for VocabularyId {vocabularyId}: {ex.Message}"); return false; }
            finally { if (conn?.State == ConnectionState.Open) conn.Close(); }
            return rowsAffected > 0;
        }

        // --- GetWordById (Giữ lại nếu LearningController cần) ---
        // Lưu ý: Đã có GetVocabularyById(int id) ở trên với logic tương tự.
        // Nếu LearningController đã sửa dùng GetVocabularyById thì có thể xóa hàm này.
        public Vocabulary GetWordById(int id)
        {
            return GetVocabularyById(id); // Đơn giản là gọi hàm đã có
        }
        public int GetVocabularyCount()
        {
            int count = 0;
            string query = $"SELECT COUNT(*) FROM {VocabularyTableName}";
            SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        count = Convert.ToInt32(result);
                    }
                }
                Debug.WriteLine($"[VocabularyRepository] Total vocabulary count: {count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[VocabularyRepository] Error in GetVocabularyCount: {ex.Message}");
                // Trả về 0 nếu có lỗi
                count = 0;
            }
            finally
            {
                if (conn?.State == ConnectionState.Open) conn.Close();
            }
            return count;
        }
    }
}