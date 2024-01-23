using System.ComponentModel.DataAnnotations;

namespace NewsBackend.Data
{
    public class ArticleChunk
    {
        public string? Type { get; set; }
        public string? Data { get; set; }
    }
}
