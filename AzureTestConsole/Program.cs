using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTable();
        }

        public static void CreateTable()
        {

            var connectionString = "UseDevelopmentStorage=true";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        //    CloudTableClient tableClient = new CloudTableClient(
        //new Uri("http://127.0.0.1:10002/"),
        //new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw=="));

            CloudTable table = tableClient.GetTableReference("people");
            table.CreateIfNotExists();

            //CustomerEntity customer1 = new CustomerEntity("Harp", "Walter");
            //customer1.Email = "Walter@contoso.com";
            //customer1.PhoneNumber = "425-555-0101";

            //// Create the TableOperation that inserts the customer entity.
            //var insertOperation = TableOperation.Insert(customer1);

            //// Execute the insert operation.
            //table.Execute(insertOperation);

            // Read storage
            TableQuery<CustomerEntity> query =
               new TableQuery<CustomerEntity>()
                  .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                      QueryComparisons.Equal, "Harp"));
            var list = table.ExecuteQuery(query).ToList();

            foreach (CustomerEntity customerEntity in list)
            {
                Console.WriteLine(customerEntity.RowKey.ToString());
                Console.WriteLine(customerEntity.PartitionKey.ToString());
                Console.WriteLine(customerEntity.Email);

                Console.ReadKey();
            }
        }
    }

    public class CustomerEntity : TableEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public CustomerEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public CustomerEntity() { }
    }
}
