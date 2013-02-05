using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;

namespace MessageBoard_Data
{
    public class MessageBoardEntry : TableServiceEntity
    {

        //In addition to the properties required by the data model, every entity in table 
        //storage has two key properties: the PartitionKey and the RowKey. These properties 
        //together form the table's primary key and uniquely identify each entity in the table. 
        public MessageBoardEntry()
        {
            PartitionKey = DateTime.UtcNow.ToString("MMddyyyy");

            // Row key allows sorting, so we make sure the rows come back in time order.
            RowKey = string.Format("{0:10}_{1}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks, Guid.NewGuid());
        }

        public string GolferName { get; set; }
        public string GolferMessage { get; set; }
		
    }
}
