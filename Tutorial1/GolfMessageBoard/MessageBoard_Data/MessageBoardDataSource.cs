using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard_Data
{
    public class MessageBoardDataSource
    {

        private const string messageTableName = "MessageTable";
        private const string connectionStringName = "DataConnectionString";
        private static CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;


        //The default constructor initializes the storage account by reading its settings from
        //the configuration and then uses CreateTableIfNotExist method in the CloudTableClient
        //class to create the table used by the application.
        public MessageBoardDataSource()
        {
            string connectionString = RoleEnvironment.GetConfigurationSettingValue(connectionStringName);

            storageAccount = CloudStorageAccount.Parse(connectionString);
            tableClient = new CloudTableClient(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials);
            tableClient.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));
            tableClient.CreateTableIfNotExist(messageTableName);
        }


        //The GetMessageBoardEntries method retrieves today's message board entries by constructing 
        //a LINQ statement that filters the retrieved information using the current date as the 
        //partition key value. The web role uses this method to bind to a data grid and display the 
        //message board.
        public IEnumerable<MessageBoardEntry> GetEntries()
        {
            TableServiceContext tableServiceContext = tableClient.GetDataServiceContext();

            var results = from g in tableServiceContext.CreateQuery<MessageBoardEntry>(messageTableName)
                          where g.PartitionKey == DateTime.UtcNow.ToString("MMddyyyy")
                          select g;
            return results;
        }

        //The AddMessageBoardEntry method inserts new entries into the table.
        public void AddEntry(MessageBoardEntry newItem)
        {
            TableServiceContext tableServiceContext = tableClient.GetDataServiceContext();
            tableServiceContext.AddObject(messageTableName, newItem);
            tableServiceContext.SaveChanges();
        }
		
    }
}
