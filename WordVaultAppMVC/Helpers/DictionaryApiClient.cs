using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json; // Vẫn cần Newtonsoft.Json
using System.Windows.Forms;
using WordVaultAppMVC.Models; // Namespace chứa WordDetails
using Newtonsoft.Json.Linq; // Vẫn cần cho Translate
using System.Diagnostics;     // Cho Debug
using System.Collections.Generic; // Cho List

namespace WordVaultAppMVC.Helpers
{
    public static class DictionaryApiClient
    {
        // Sử dụng HttpClient để tái sử dụng kết nối
        private static readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30)  // Đặt timeout
        };

        // Hàm bất đồng bộ lấy chi tiết từ điển từ dictionaryapi.dev
        // Trả về WordDetails
        public static async Task<WordDetails> GetWordDetailsAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return null;

            string url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word.Trim().ToLower()}";
            Debug.WriteLine($"[DictionaryApiClient] Calling dictionaryapi.dev: {url}");

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // API này thường trả về 404 nếu không tìm thấy
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Debug.WriteLine($"[DictionaryApiClient] Word '{word}' not found on dictionaryapi.dev.");
                        MessageBox.Show($"Không tìm thấy từ \"{word}\" trong từ điển.", "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Debug.WriteLine($"[DictionaryApiClient] API request failed. Status: {(int)response.StatusCode}");
                        MessageBox.Show($"API trả về mã lỗi: {(int)response.StatusCode}");
                    }
                    return null;
                }

                string jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("--- dictionaryapi.dev Response JSON ---");
                Debug.WriteLine(jsonString.Substring(0, Math.Min(500, jsonString.Length))); // Log một phần JSON
                Debug.WriteLine("--- End dictionaryapi.dev Response JSON ---");

                // API này trả về một mảng các kết quả
                var root = JsonConvert.DeserializeObject<List<DictionaryApiResponse>>(jsonString); // Parse thành List

                if (root == null || !root.Any())
                {
                    Debug.WriteLine("[DictionaryApiClient] JSON parsed as null or empty list.");
                    return null;
                }

                var entry = root.First(); // Lấy kết quả đầu tiên

                // Trích xuất thông tin (logic giống ban đầu)
                string pronunciation = entry.phonetics?.FirstOrDefault(p => !string.IsNullOrEmpty(p.text))?.text ?? "";
                // Ưu tiên audio US nếu có
                string audioUrl = entry.phonetics?.FirstOrDefault(p => !string.IsNullOrEmpty(p.audio) && p.audio.Contains("us.mp3"))?.audio
                                 ?? entry.phonetics?.FirstOrDefault(p => !string.IsNullOrEmpty(p.audio))?.audio ?? "";
                string meaning = entry.meanings?.FirstOrDefault()?.definitions?.FirstOrDefault()?.definition ?? "Không rõ nghĩa.";

                return new WordDetails
                {
                    Word = entry.word ?? word.Trim(), // Lấy word từ API hoặc từ input
                    Pronunciation = pronunciation,
                    AudioUrl = audioUrl,
                    Meaning = meaning
                };
            }
            catch (TaskCanceledException) { MessageBox.Show("Yêu cầu API quá thời gian chờ (timeout).", "Lỗi mạng", MessageBoxButtons.OK, MessageBoxIcon.Warning); return null; }
            catch (HttpRequestException ex) { MessageBox.Show("Lỗi mạng hoặc lỗi khi gọi API: " + ex.Message, "Lỗi mạng", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
            catch (JsonException ex) { Debug.WriteLine($"JSON parsing error: {ex}"); MessageBox.Show("Lỗi xử lý dữ liệu từ điển.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
            catch (Exception ex) { MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
        }

        // Hàm dịch thuật giữ nguyên, dùng API mymemory
        public static async Task<string> TranslateToVietnamese(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return ""; string translatedText = text;
            try
            {
                using (var tempClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) })
                {
                    var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|vi";
                    var response = await tempClient.GetStringAsync(url); var json = JObject.Parse(response);
                    string apiTranslated = json["responseData"]?["translatedText"]?.ToString();
                    if (!string.IsNullOrEmpty(apiTranslated) && !apiTranslated.Equals(text, StringComparison.OrdinalIgnoreCase)) { translatedText = apiTranslated; }
                    else { Debug.WriteLine("[DictionaryApiClient] TranslateToVietnamese: Translation same as original or empty."); }
                }
            }
            catch (Exception ex) { Debug.WriteLine($"[DictionaryApiClient] TranslateToVietnamese Error: {ex.Message}"); }
            return translatedText;
        }

        #region JSON Models for dictionaryapi.dev

        // Các lớp private để deserialize JSON từ dictionaryapi.dev
        // Đảm bảo các lớp này khớp với cấu trúc JSON của API này
        private class DictionaryApiResponse
        {
            public string word { get; set; }
            public List<Phonetic> phonetics { get; set; } // API trả về mảng phonetics
            public List<Meaning> meanings { get; set; } // API trả về mảng meanings
            // Có thể có các trường khác như license, sourceUrls nếu cần
        }

        private class Phonetic
        {
            public string text { get; set; }
            public string audio { get; set; }
            // Có thể có sourceUrl, license
        }

        private class Meaning
        {
            public string partOfSpeech { get; set; }
            public List<Definition> definitions { get; set; } // API trả về mảng definitions
            public List<string> synonyms { get; set; }
            public List<string> antonyms { get; set; }
        }

        private class Definition
        {
            public string definition { get; set; }
            public List<string> synonyms { get; set; } // Định nghĩa cũng có thể có syn/ant riêng
            public List<string> antonyms { get; set; }
            public string example { get; set; }
        }
        #endregion
    }
}