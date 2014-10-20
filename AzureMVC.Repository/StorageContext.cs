using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureMVC.Repository
{
    public class StorageContext
    {
        public CloudTable CustomerTable { get; set; }

        public StorageContext()
        {
            const string connectionString = "UseDevelopmentStorage=true";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();

            CustomerTable = table;
        }  
    }
}
