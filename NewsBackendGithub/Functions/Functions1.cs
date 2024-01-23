using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NewsBackend.Data;
using System.Reflection;

namespace NewsBackend.Functions
{
    public class Functions1
    {
       
        public static int generateRandomInteger(int n)
        {
            var random = new Random();
            int randomNumber = random.Next(0, n );
            return randomNumber;
        }
        public static int generateRandomIntegerScope(int n, int m)
        {
            var random = new Random();
            int randomNumber = random.Next(n, m);
            return randomNumber;
        }

        public static List<int> generateRandomIntegerList(int n, int m)
        {

            List<int> list = new List<int>();

            int randomNumber;

            for ( int i = 0; i < m ; i++)
            {
                randomNumber = generateRandomInteger(n);
                while (list.Contains(randomNumber))
                {
                    randomNumber = generateRandomInteger(n);
                }
                list.Add(randomNumber);
            }

            return list;
        }

        public static List<int> generateRandomIntegerListExclude(int n, int m, int k)
        {

            List<int> list = new List<int>();

            list.Add(k);

            int randomNumber;

            for (int i = 0; i < m - 1; i++)
            {
                randomNumber = generateRandomInteger(n);
                while (list.Contains(randomNumber))
                {
                    randomNumber = generateRandomInteger(n);
                }
                list.Add(randomNumber);
            }

            return list;
        }



        public static List<int> generateRandomIntegerListExcludeList(int n, int m, List<int> ints)
        {

            List<int> list = new List<int>();


            foreach( int k in ints)
            {
                list.Add(k);
            }

            int randomNumber;

            for (int i = 0; i < m - 1; i++)
            {
                randomNumber = generateRandomInteger(n);
                while (list.Contains(randomNumber))
                {
                    randomNumber = generateRandomInteger(n);
                }
                list.Add(randomNumber);
            }

            return list;
        }

        public static bool CheckArticleDataTransferSize(ArticleDataTransfer articleDataTransfer)
        {
            bool result = true;
            int chunkCount = articleDataTransfer.ArticleChunks.Count;
            int maxChunkLength = articleDataTransfer.ArticleChunks[0].Data.Length;
            foreach (var chunk in articleDataTransfer.ArticleChunks)
            {
                int chunkDataLength = chunk.Data.Length;
                if (chunkDataLength > maxChunkLength) maxChunkLength = chunkDataLength;
            }
            if (chunkCount > 20 || maxChunkLength > 1500)
            {
                result = false;
            }
            Type type = articleDataTransfer.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(articleDataTransfer);
                    if (value.Length > 1000)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool CheckArticleEmptyFields(ArticleDataTransfer articleDataTransfer)
        {
            bool result = true;
            int chunkCount = articleDataTransfer.ArticleChunks.Count;
            if (chunkCount == 0) { return false; };
       
            Type type = articleDataTransfer.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(articleDataTransfer);
                    if (value.Length == 0)
                    {
                        Console.WriteLine('2');
                        result = false;
                    }
                }
            }
            return result;
        }

        public static string generateRandomName()
        {
            string[] names = {
                                "Emma", "Olivia", "Ava", "Isabella", "Sophia", "Mia", "Charlotte", "Amelia", "Harper", "Evelyn",
                                "Abigail", "Emily", "Elizabeth", "Mila", "Ella", "Avery", "Scarlett", "Luna", "Chloe", "Grace",
                                "Penelope", "Layla", "Victoria", "Riley", "Zoey", "Nora", "Lily", "Eleanor", "Hannah", "Lillian",
                                "Addison", "Aubrey", "Ellie", "Stella", "Natalie", "Zoe", "Leah", "Hazel", "Violet", "Aurora",
                                "Savannah", "Audrey", "Brooklyn", "Bella", "Claire", "Skylar", "Lucy", "Paisley", "Everly",
                                "Anna", "Caroline", "Nova", "Genesis", "Emilia", "Kennedy", "Samantha", "Maya", "Willow", "Kinsley",
                                "Naomi", "Aaliyah", "Elena", "Sarah", "Ariana", "Allison", "Gabriella", "Alice", "Madelyn", "Cora",
                                "Ruby", "Eva", "Serenity", "Autumn", "Adeline", "Hailey", "Gianna", "Valentina", "Isla", "Eliana",
                                "Quinn", "Nevaeh", "Ivy", "Sadie", "Piper", "Lydia", "Alexa", "Josephine", "Emery", "Julia",
                                "Liam", "Noah", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander",
                                "Mason", "Michael", "Ethan", "Daniel", "Jacob", "Logan", "Jackson", "Levi", "Sebastian", "Mateo",
                                "Jack", "Owen", "Theodore", "Aiden", "Samuel", "Joseph", "John", "David", "Wyatt", "Matthew",
                                "Luke", "Asher", "Carter", "Julian", "Grayson", "Leo", "Jayden", "Gabriel", "Isaac", "Lincoln",
                                "Anthony", "Hudson", "Dylan", "Ezra", "Thomas", "Charles", "Christopher", "Jaxon", "Maverick",
                                "Josiah", "Isaiah", "Andrew", "Elias", "Joshua", "Nathan", "Caleb", "Ryan", "Adrian", "Miles",
                                "Eli", "Nolan", "Christian", "Aaron", "Cameron", "Blake", "Landon", "Jordan", "Connor", "Santiago",
                                "Jeremiah", "Ezekiel", "Colton", "Amir", "Maxwell", "Ian", "Adam", "Axel", "Eric", "Brody",
                                "Bennett", "Finn", "Declan", "Silas", "Emmett", "Kai", "Rowan", "Everett", "Xavier", "Zachary"
                                };
            string[] surnames = {
                                "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
                                "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
                                "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King",
                                "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter",
                                "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins",
                                "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey",
                                "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez", "James",
                                "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross", "Henderson",
                                "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington", "Butler"
                                };
            int rand1 = generateRandomInteger(175);
            int rand2 = generateRandomInteger(85);

            string s1 = names[rand1];
            string s2 = surnames[rand2];
            string result = s1 + " " + s2;
            return result;
        }

        public static string generateRandomDate()
        {
            Random random = new Random();

            DateTime startDate = new DateTime(2020, 12, 1);
            DateTime endDate = new DateTime(2023, 12, 31);

            int range = (endDate - startDate).Days;

            int randomDays = random.Next(range);

            DateTime randomDate = startDate.AddDays(randomDays);

            string formattedDate = $"{GetOrdinalDay(randomDate.Day)} {randomDate:MMMM yyyy}";

            return formattedDate;
        }


        public static string GetOrdinalDay(int day)
        {
            if (day >= 11 && day <= 13)
            {
                return $"{day}th";
            }

            switch (day % 10)
            {
                case 1:
                    return $"{day}st";
                case 2:
                    return $"{day}nd";
                case 3:
                    return $"{day}rd";
                default:
                    return $"{day}th";
            }
        }
    }
}
