using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.Azure.Cosmos;

namespace potato
{
    public class Food{
        public string id;
        public int price;

    }

  
    public class Program
    {
        private static readonly string _endpoint = "https://monkey.documents.azure.com:443/";
        private static readonly string _conString = "0H9nkigvXxuja8dz8lb8YXeRyzs4adOqx7P8nkNbrnDfC9SWsSCdgu7OzvoTu59NAh0mlw5IV57SvNyYzJ2vzg==";

        
        public static async Task Main(string[] args)
        {
                CosmosClient client = new CosmosClient(_endpoint, _conString);
            
                //DatabaseResponse dbr = await client.CreateDatabaseIfNotExistsAsync("lizard");
                //Database targetDatabase = dbr.Database;
                //await Console.Out.WriteLineAsync($"Db id:\t{targetDatabase.Id}");
                Database myDb = client.GetDatabase("lizard");
                // Custom Indexing
                // PATH and expression
                //     / could be a path, and * could be expressions inside it 
                // Container -> Index 
                /*IndexingPolicy indPolicy = new IndexingPolicy{
                    IndexingMode = IndexingMode.Consistent,
                    Automatic = true,
                    IncludedPaths = {
                        new IncludedPath{
                            Path = "/*"
                        }
                    }
                };
                var containerProps = new ContainerProperties("CustomCollection","/type")
                {
                    IndexingPolicy = indPolicy
                };
                var containerResponse = await myDb.CreateContainerIfNotExistsAsync(containerProps,1000);
                var customContainer = containerResponse.Container;
                await Console.Out.WriteLineAsync($"Container id:\t{customContainer.Id}");*/
                var customContainer = myDb.GetContainer("CustomCollection");
                var foods = new Bogus.Faker<Food>()
                .RuleFor(i => i.id, (fake) => Guid.NewGuid().ToString())
                .RuleFor(i => i.price, (fake) => fake.Random.Number(1,5))
                .GenerateLazy(10);
                foreach(var food in foods)
                {
                    ItemResponse<Food> result = await customContainer.CreateItemAsync(food);
                    await Console.Out.WriteLineAsync($"Row id:\t{result.Resource.id}");
                }



        }
    }
}
