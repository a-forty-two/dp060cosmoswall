using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.Azure.Cosmos;

namespace potato
{
    public class Program
    {
        private static readonly string _endpoint = "https://monkey.documents.azure.com:443/";
        private static readonly string _conString = "0H9nkigvXxuja8dz8lb8YXeRyzs4adOqx7P8nkNbrnDfC9SWsSCdgu7OzvoTu59NAh0mlw5IV57SvNyYzJ2vzg==";
        public static async Task Main(string[] args)
        {
            CosmosClient client = new CosmosClient(_endpoint, _conString);
            
                DatabaseResponse dbr = await client.CreateDatabaseIfNotExistsAsync("lizard");
                Database targetDatabase = dbr.Database;
                await Console.Out.WriteLineAsync($"Db id:\t{targetDatabase.Id}");
            
        }
    }
}
