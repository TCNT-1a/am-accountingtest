 using Microsoft.EntityFrameworkCore;
using FZC.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
namespace FZC.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ChungTu> ChungTus { get; set; }
        public DbSet<ChiTietChungTu> ChiTietChungTus { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Vocabulary>()
            //    .HasMany(v => v.Topics)
            //    .WithMany(t => t.Vocabularies)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "VocabularyTopic",
            //        j => j.HasOne<Topic>().WithMany().HasForeignKey("TopicId"),
            //        j => j.HasOne<Vocabulary>().WithMany().HasForeignKey("VocabularyId"),
            //        j =>
            //        {
            //            j.HasKey("VocabularyId", "TopicId");
            //            j.ToTable("VocabularyTopics");
            //        });

            //// Cấu hình State
            //modelBuilder.Entity<Grammar>()
            //    .HasKey(s => s.Id);

            ////SeedDataFromCsv(modelBuilder);

            //// Cấu hình County
            //modelBuilder.Entity<Vocabulary>()
            //    .HasKey(c => c.Id);
        }

        private void SeedDataFromCsv(ModelBuilder modelBuilder)
        {
            // Đọc dữ liệu từ state.csv
            //var states = ReadCsv<State>("seed/state.csv", new Mapping.StateMap());
            //modelBuilder.Entity<State>().HasData(states);

            // Đọc dữ liệu từ county.csv
            //var counties = ReadCsv<County>("seed/county.csv", new Mapping.CountyMap());
            //modelBuilder.Entity<County>().HasData(counties);
        }

        private List<T> ReadCsv<T>(string filePath, ClassMap<T> classMap) where T : class, new()
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap(classMap);
            return csv.GetRecords<T>().ToList();
        }
        //public void SeedData()
        //{
        //    // Đường dẫn tới file CSV
        //    string filePath = @"c:\Users\n\Documents\project\llm-lazypromp\data\tu_vung_theo_chu_de.csv";

        //    // Đọc dữ liệu từ file CSV
        //    var vocabulariesAndTopics = ReadVocabularyAndTopicsFromCsv(filePath);

        //    // Lấy danh sách các chủ đề duy nhất
        //    var topics = vocabulariesAndTopics
        //        .Select(vt => vt.Topic)
        //        .DistinctBy(t => t.Name)
        //        .ToList();

        //    // Lấy danh sách từ vựng
        //    var vocabularies = vocabulariesAndTopics
        //        .Select(vt => vt.Vocabulary)
        //        .ToList();

        //    // Thêm dữ liệu vào database nếu chưa tồn tại
        //    if (!Topics.Any())
        //    {
        //        Topics.AddRange(topics);
        //        SaveChanges();
        //    }

        //    if (!Vocabularies.Any())
        //    {
        //        Vocabularies.AddRange(vocabularies);
        //        SaveChanges();
        //    }
        //}
        // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        // {
        //     var entries = ChangeTracker.Entries<BaseEntity>();

        //     foreach (var entry in entries)
        //     {
        //         if (entry.State == EntityState.Added)
        //         {
        //             entry.Entity.createDate = DateTime.UtcNow;
        //             entry.Entity.updateDate = DateTime.UtcNow;
        //             entry.Entity.isDeleted = false;
        //         }
        //         else if (entry.State == EntityState.Modified)
        //         {
        //             entry.Entity.updateDate = DateTime.UtcNow;
        //         }
        //         else if (entry.State == EntityState.Deleted)
        //         {
        //             entry.State = EntityState.Modified; // Soft delete
        //             entry.Entity.isDeleted = true;
        //             entry.Entity.updateDate = DateTime.UtcNow;
        //         }
        //     }

        //     return await base.SaveChangesAsync(cancellationToken);
        // }

        //private List<(Vocabulary Vocabulary, Topic Topic)> ReadVocabularyAndTopicsFromCsv(string filePath)
        //{
        //    var result = new List<(Vocabulary, Topic)>();
        //    var existingTopics = Topics.ToList();
        //    using var reader = new StreamReader(filePath);
        //    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        //    csv.Read();
        //    csv.ReadHeader();

        //    while (csv.Read())
        //    {
        //        var word = csv.GetField<string>("Từ vựng");
        //        var phonetic = csv.GetField<string>("Phát âm");
        //        var meaning = csv.GetField<string>("Nghĩa");
        //        var topicName = csv.GetField<string>("Chủ đề");

        //        var topic = existingTopics.FirstOrDefault(t => t.Name == topicName);
        //        if (topic == null)
        //        {
        //            topic = new Topic { Name = topicName };
        //            existingTopics.Add(topic); // Thêm vào danh sách để tránh tạo lại
        //        }
        //        var vocabulary = new Vocabulary
        //        {
        //            Word = word,
        //            Phonetic = phonetic,
        //            MeaningVN = meaning,
        //            Topics = new List<Topic> { topic }
        //        };

        //        result.Add((vocabulary, topic));
        //    }

        //    return result;
        //}
    }
}