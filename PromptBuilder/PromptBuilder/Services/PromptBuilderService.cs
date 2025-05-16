using PromptBuilder.Constants;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PromptBuilder.Services
{
    public class PromptBuilderService
    {
        private readonly Random _random = new Random();
        // Danh sách chủ đề từ vựng (Vocabulary Topics)
        public static readonly string[] VocabularyTopics = {
            "Family", "Jobs and Occupations", "School and Education", "Food and Drinks", "Fruits and Vegetables",
            "Animals", "Colors", "Numbers", "Weather", "Clothes", "House and Furniture", "Body Parts",
            "Transportation", "Hobbies and Free Time", "Sports", "Daily Routines", "Feelings and Emotions",
            "Places in Town", "Shopping", "Holidays and Celebrations", "Travel", "Nature and Environment",
            "Technology", "Health and Illness", "Time and Dates", "Verbs", "Adjectives", "Prepositions",
            "Opposites", "English Slang"
        };

        public string GeneratePrompt()
        {
            // Chọn ngẫu nhiên một chủ đề từ vựng
            string vocabularyTopic = VocabularyTopics[_random.Next(VocabularyTopics.Length)];

            // Lấy ngẫu nhiên các giá trị khác từ PromptOptions
            var grammarCategory = PromptOptions.GrammarTopics[_random.Next(PromptOptions.GrammarTopics.Count)];
            string grammarTopic = grammarCategory.Topics[_random.Next(grammarCategory.Topics.Count)]; // Use the category name as the main topic

            string studentContext = PromptOptions.StudentContexts[_random.Next(PromptOptions.StudentContexts.Length)];
            string writingStyle = PromptOptions.WritingStyles[_random.Next(PromptOptions.WritingStyles.Length)];
            string narrativeVoice = PromptOptions.NarrativeVoices[_random.Next(PromptOptions.NarrativeVoices.Length)];

            string str =  $"Viết một bài nội dung tiếng Anh bao gồm các thành phần sau:\r\n\r\n" +
                $"Tiêu đề video:  chủ đề từ vựng {vocabularyTopic}, chủ đề ngữ pháp {grammarTopic}, và một vài từ như luyện nghe, để tối ưu SEO trên YouTube.\r\n\r\n" +
                "Bắt đầu là: Please listen to  the following passage and then answer the questions. Sau đó là \n" +
                $"Đoạn văn khoảng 360 từ, kết hợp " +
                $"chủ đề từ vựng \"{vocabularyTopic}\" và " +
                $"chủ đề ngữ pháp \"{grammarTopic}\".\r\n\r\n" +
                $"Quy tắc ngắt câu:\r\n\r\nDấu chấm (.) → Ngắt mạnh → Xuống 2 dòng\r\n\r\nDấu phẩy (,) → Ngắt nhẹ → Xuống 1 dòng\r\n\r\nTừ nối (Then, After that, So, etc) → Xuống 1 dòng\r\n\r\nBắt đầu đoạn mới → Ngắt dài hơn → Xuống 5 dòng\r\n\r\nSau đoạn văn, thêm 3 câu hỏi, ví dụ:\r\n\r\nNumber 1: ...\r\n\r\nNumber 2: ...\r\n\r\nNumber 3: ...\r\n\r\nSau 3 câu hỏi, trình bày thêm:\r\nDanh sách 15 từ vựng chính được sử dụng trong đoạn văn, bao gồm:\r\n\r\nPhiên âm IPA\r\n\r\nTừ loại\r\n\r\nNghĩa tiếng Việt\r\n\r\nPhân tích cấu trúc ngữ pháp về “Mạo từ (Articles) – A, An, The”, gồm có:\r\n\r\nCông thức (formulas)\r\n\r\nVí dụ trong đoạn văn (examples)\r\n\r\nGhi chú đơn giản, dễ hiểu (notes)\r\n\r\n" +
                $"Ngữ cảnh: {studentContext}.\r\n\r\n" +
                $"Phong cách viết: {writingStyle}\r\nNgôi kể: {narrativeVoice}";
            return str;
        }
    }
}
