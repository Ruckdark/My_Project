using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using WordVaultAppMVC.Models;
using Newtonsoft.Json.Linq;

namespace WordVaultAppMVC.Helpers
{
    public static class DictionaryApiClient
    {
        // Sử dụng HttpClient để tái sử dụng kết nối và cải thiện hiệu suất
        private static readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30)  // Đặt timeout lên 30 giây
        };

        // Hàm bất đồng bộ lấy chi tiết từ điển
        public static async Task<WordDetails> GetWordDetailsAsync(string word)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(word))
                    return null;

                string url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word.Trim().ToLower()}";

                // Gửi yêu cầu GET đến API
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Không tìm thấy từ \"{word}\" hoặc API trả về mã lỗi: {(int)response.StatusCode}");
                    return null;
                }

                // Đọc dữ liệu JSON từ API
                string jsonString = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<DictionaryApiResponse[]>(jsonString);

                if (root == null || root.Length == 0)
                    return null;

                var entry = root[0];

                // Lấy phát âm
                string pronunciation = entry.phonetics?.FirstOrDefault(p => !string.IsNullOrEmpty(p.text))?.text ?? "";
                // Lấy URL âm thanh phát âm
                string audioUrl = entry.phonetics?.FirstOrDefault(p => !string.IsNullOrEmpty(p.audio))?.audio ?? "";
                // Lấy nghĩa của từ
                string meaning = entry.meanings?
                    .FirstOrDefault()?
                    .definitions?
                    .FirstOrDefault()?
                    .definition ?? "Không rõ nghĩa.";

                return new WordDetails
                {
                    Word = entry.word,
                    Pronunciation = pronunciation,
                    AudioUrl = audioUrl,
                    Meaning = meaning
                };
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Yêu cầu API bị hủy do quá thời gian chờ (timeout). Hãy kiểm tra kết nối mạng.");
                return null;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Lỗi mạng hoặc lỗi khi gọi API: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message);
                return null;
            }
        }
        // Change 1:
        //public static async Task<string> TranslateToVietnamese(string text)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|vi";
        //        string json = await client.GetStringAsync(url);
        //        var data = Newtonsoft.Json.Linq.JObject.Parse(json);
        //        return data["responseData"]?["translatedText"]?.ToString();
        //    }
        //}
        public static async Task<string> TranslateToVietnamese(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|vi";
                var response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);
                return json["responseData"]?["translatedText"]?.ToString() ?? "";
            }
        }



        // ❌ Bỏ hàm đồng bộ này vì có thể gây treo ứng dụng trong WinForms
        // public static WordDetails GetWordDetails(string word)
        // {
        //     return GetWordDetailsAsync(word).GetAwaiter().GetResult();
        // }

        // === Mô hình JSON từ dictionaryapi.dev ===
        private class DictionaryApiResponse
        {
            public string word { get; set; }
            public Phonetic[] phonetics { get; set; }
            public Meaning[] meanings { get; set; }
        }

        private class Phonetic
        {
            public string text { get; set; }
            public string audio { get; set; }
        }

        private class Meaning
        {
            public string partOfSpeech { get; set; }
            public Definition[] definitions { get; set; }
        }

        private class Definition
        {
            public string definition { get; set; }
        }
    }
}
