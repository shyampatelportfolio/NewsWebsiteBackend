using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NewsBackend.Data;
using NewsBackend.Functions;



namespace NewsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Articles : ControllerBase
    {
        private readonly IMongoCollection<Article> _collection;
        private readonly IMongoCollection<ArticleFull> _collection2;
        private readonly IConfiguration _config;


        public Articles(IMongoClient client, IConfiguration config)
        {
            var database = client.GetDatabase("News");
            _collection = database.GetCollection<Article>("ArticleShortlist");
            _collection2 = database.GetCollection<ArticleFull>("ArticleFull");
            _config = config;
        }


        [HttpPost("ArticleShortlistWithImagesFull")]
        public ActionResult<List<Article>> FindArticleShortlistWithImagesFull(List<int> articleList)
        {
            List<Article> articles = new List<Article>();

            foreach (int articleId in articleList)
            {
                var filter = Builders<Article>.Filter
                                .Eq(x => x.ArticleId, articleId);
                var fetchResult = _collection.Find(filter).ToList();
                if (fetchResult.Count == 0)
                {
                    break; 
                }
                Article article = fetchResult[0];
                article.ArticleMainImage = $"{_config["Azure:BlobStorage:NewsImages"]}/{article.ArticleMainImage}";
                articles.Add(article);

            }
            return Ok(articles);
        }

        [HttpPost("ArticleShortlistWithImagesFullCategory")]
        public ActionResult<List<Article>> FindArticleShortlistWithImagesFullCategory(string category)
        {

            Console.WriteLine("Category");
            List<Article> articles = new List<Article>();
     
                var filter = Builders<Article>.Filter
                                .Eq(x => x.ArticleCategory, category);
                var result = _collection.Find(filter).ToList();
            if(result.Count == 0)
            {
                return Ok("Article Not Found");

            }
            foreach ( Article article in result )
                {
                    Article myArticle = article;
                    myArticle.ArticleMainImage = $"{_config["Azure:BlobStorage:NewsImages"]}/{article.ArticleMainImage}";
                    articles.Add(myArticle);
                }

            return Ok(articles);
        }
        
        [HttpPost("ArticleFullWithImage")]
        public ActionResult<ArticleFull> FindArticleFullWithImage(int articleId)
        {
            var filter = Builders<ArticleFull>.Filter
                            .Eq(x => x.ArticleId, articleId);
            var fetchResult = _collection2.Find(filter).ToList();
            if(fetchResult.Count == 0)
            {
                return Ok("Article Not Found");
            }
            else
            {
                ArticleFull articleFull = fetchResult[0];
                articleFull.ArticleMainImage = $"{_config["Azure:BlobStorage:NewsImages"]}/{articleFull.ArticleMainImage}";
                return Ok(articleFull);
            }
        }

        [HttpPost("CreateArticleFull")]
        public async Task<IActionResult> CreateArticleFull(ArticleDataTransfer articleDataTransfer)
        {
            bool emptyFields = Functions1.CheckArticleEmptyFields(articleDataTransfer);
            if (!emptyFields)
            {
                return Ok("Server Overloaded");
            }
            bool result = Functions1.CheckArticleDataTransferSize(articleDataTransfer);
            if (!result)
            {
                return Ok("Too Large");
            }
            var maxId = _collection2.Find(new BsonDocument())
                     .Sort(Builders<ArticleFull>.Sort.Descending("ArticleId"))
                     .FirstOrDefault()
                     .ArticleId;
            if(maxId > 1000)
            {
                return Ok("Server Overloaded");

            }
            int imageRef = Functions1.generateRandomInteger(120) + 1;
            ArticleFull articleFull = new ArticleFull();
            articleFull.ArticleId = maxId + 1;
            articleFull.ArticlePreface = articleDataTransfer.ArticlePreface;
            articleFull.ArticleTitle = articleDataTransfer.ArticleTitle;
            articleFull.ArticleDate = articleDataTransfer.ArticleDate;
            articleFull.ArticleCategory = articleDataTransfer.ArticleCategory;
            articleFull.ArticleAuthor = articleDataTransfer.ArticleAuthor;
            articleFull.ArticleChunks = articleDataTransfer.ArticleChunks;
            articleFull.ArticleMainImage = "Image" + imageRef.ToString();
            await _collection2.InsertOneAsync(articleFull);

            return Ok(maxId + 1);
        }

    




    }
}
