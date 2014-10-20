using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureMVC.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureMVC.Repository
{
    public class CustomersRepository : StorageContext
    {
        public void InsertCustomer(Customer customer)
        {
            // Create the TableOperation that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(customer);

            // Execute the insert operation.
            CustomerTable.Execute(insertOperation);
        }

        public Customer GetCustomerByKey(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Customer>(partitionKey, rowKey);

            // Execute the retrieve operation.
            TableResult retrievedResult = CustomerTable.Execute(retrieveOperation);

            if (retrievedResult.Result != null)
                return (Customer) retrievedResult.Result;

            return null;
        }

        public List<Customer> GetCustomers()
        {
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = new TableQuery<Customer>();
            List<Customer> customers = CustomerTable.ExecuteQuery(query).ToList();

            return customers;
        }

        public void DeleteCustomer(string rowKey, string partitionKey)
        {
            var customerToDelete = GetCustomerByKey(partitionKey, rowKey);
            TableOperation deleteOperation = TableOperation.Delete(customerToDelete);

            // Execute the operation.
            CustomerTable.Execute(deleteOperation);
        }
    }
}
