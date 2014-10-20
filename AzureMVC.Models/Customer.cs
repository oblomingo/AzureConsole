using Microsoft.WindowsAzure.Storage.Table;

namespace AzureMVC.Models
{
    public class Customer : TableEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public Customer() { }
    }
}
