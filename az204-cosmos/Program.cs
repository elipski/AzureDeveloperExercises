using Microsoft.Azure.Cosmos;
/* Run from bash shell to create the resource group and cosmos DB 

    az login

    az group create --location eastus --name az204-cosmos-rg

    az cosmosdb create --name  elipskicosmosdb --resource-group az204-cosmos-rg

    # Retrieve the primary key
    az cosmosdb keys list --name elipskicosmosdb --resource-group az204-cosmos-rg
*/
//Added the following package to this project:
//dotnet add package Microsoft.Azure.Cosmos

public class Program
{
    // Replace <documentEndpoint> with the information created earlier
    private static readonly string EndpointUri = "https://elipskicosmosdb.documents.azure.com:443/";

    // Set variable to the Primary Key from earlier.
    private static readonly string PrimaryKey = "w0DAGsVUeOtqTLSP4uVpk1sdgV5aeqHrocsfRLsHZn1vU7F1L5SGblEr1dHjCjsUtBl1kkZo4ab6ACDbB7XX7Q==";

    // The Cosmos client instance
    private CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    // The container we will create.
    private Container container;

    // The names of the database and container we will create
    private string databaseId = "az204Database";
    private string containerId = "az204Container";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            await p.CosmosAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }
    public async Task CosmosAsync()
    {
        // Create a new instance of the Cosmos Client
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // Runs the CreateDatabaseAsync method
        await this.CreateDatabaseAsync();

        // Run the CreateContainerAsync method
        await this.CreateContainerAsync();
    }
    private async Task CreateDatabaseAsync()
    {
        // Create a new database using the cosmosClient
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
     Console.WriteLine("Created Database: {0}\n", this.database.Id);
    }
    private async Task CreateContainerAsync()
    {
        // Create a new container
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", this.container.Id);
    }

}