﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace NewsBackend.Data
{
    public class ArticleDataTransfer
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [MaxLength(255)]

        public string? ArticleCategory { get; set; }
        [MaxLength(255)]

        public string? ArticleTitle { get; set; }
        [MaxLength(2000)]

        public string? ArticlePreface { get; set; }
        [MaxLength(255)]

        public string? ArticleAuthor { get; set; }
        [MaxLength(255)]

        public string? ArticleDate { get; set; }
        public List<ArticleChunk>? ArticleChunks { get; set; }
    }
}