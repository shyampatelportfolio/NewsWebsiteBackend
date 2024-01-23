using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace NewsBackend.Data
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public int ArticleId { get; set; }
        [MaxLength(255)]

        public string? ArticleCategory { get; set; }
        [MaxLength(255)]

        public string? ArticleTitle { get; set; }

        [MaxLength(2000)]

        public string? ArticlePreface { get; set;}
        [MaxLength(255)]

        public string? ArticleAuthor { get; set;}
        [MaxLength(255)]

        public string? ArticleDate { get; set; }
        [MaxLength(255)]

        public string? ArticleMainImage { get; set; }
    }
}
