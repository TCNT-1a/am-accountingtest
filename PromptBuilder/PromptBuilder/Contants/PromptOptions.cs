
using System.Collections.Generic;

namespace PromptBuilder.Constants
{
    public class GrammarCategory
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public List<string> Topics { get; set; } = new List<string>();
    }

    public static class PromptOptions
    {
        public static readonly List<GrammarCategory> GrammarTopics = new List<GrammarCategory> {
            new GrammarCategory { Level = 0, Name = "Basic Grammar", Topics = new List<string> {"Từ loại (Parts of Speech) - Danh từ, Động từ, Tính từ, Trạng từ, Đại từ, Giới từ, Liên từ, Thán từ", "Mạo từ (Articles) - A, An, The", "Danh từ số ít / số nhiều (Singular & Plural Nouns)", "Đại từ (Pronouns) - Nhân xưng, Sở hữu, Phản thân", "Thì (Tenses) - Hiện tại đơn, Hiện tại tiếp diễn, Hiện tại hoàn thành, Quá khứ đơn, Tương lai đơn, Các thì còn lại (12 thì)", "Động từ khuyết thiếu (Modal Verbs) - Can, Could, May, Might, Will, Would, Shall, Should, Must", "Giới từ (Prepositions) - Thời gian, Địa điểm, Phương hướng, Nguyên nhân", "Câu nghi vấn (Questions) - Wh-questions, Yes/No questions, Tag questions", "Phủ định (Negatives) - Not, Never, Hardly", "So sánh (Comparatives & Superlatives) - So sánh hơn, So sánh nhất" }},

            new GrammarCategory { Level = 1, Name = "Intermediate Grammar", Topics = new List<string> { "Câu điều kiện (Conditional Sentences) - Loại 0, Loại 1, Loại 2, Loại 3", "Câu bị động (Passive Voice)", "Câu gián tiếp (Reported Speech)", "Danh động từ & động từ nguyên mẫu (Gerunds & Infinitives)", "Mệnh đề quan hệ (Relative Clauses)", "Liên từ và mệnh đề nối (Conjunctions and Clauses)", "Các loại mệnh đề - Mệnh đề danh từ, Mệnh đề trạng ngữ, Mệnh đề tính ngữ", "Thể giả định (Subjunctive Mood)" }},

            new GrammarCategory { Level = 2, Name = "Advanced Grammar", Topics = new List<string> { "Câu đảo ngữ (Inversion)", "Câu chẻ (Cleft Sentences)", "Mệnh đề rút gọn (Reduced Clauses)", "Cấu trúc song song (Parallel Structure)", "Đảo ngữ với Only, Never, Rarely...", "Cụm từ cố định (Fixed Expressions / Idiomatic Structures)", "Sự hòa hợp chủ - vị (Subject-Verb Agreement)", "Mạo từ nâng cao (Advanced Use of Articles)", "Lỗi ngữ pháp phổ biến (Common Grammar Mistakes)", "Từ nối (Linking Words & Transition Signals)", "Động từ nối (Linking Verbs)", "Ngữ pháp trong văn viết học thuật (Grammar for Academic Writing)" }}
        };
        public static readonly string[] StudentContexts = {
            "For high school students", "For university students", "For adults"
        };

        public static readonly string[] WritingStyles = {
            "Everyday communication", "Semi-formal", "Formal", "Travel", "Business"
        };

        public static readonly string[] NarrativeVoices = {
            "First person (I)", "Second person (You)", "Third person (Imaginary character)"
        };

    }
}