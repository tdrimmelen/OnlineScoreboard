using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.CosmosDB.Table;

namespace OnlineScoreboard
{
    public class ScoreEntity : TableEntity
    {
        public ScoreEntity(string Hall, string Number)
        {
            this.PartitionKey = Hall;
            this.RowKey = Number;
        }

        public ScoreEntity() { }

        public long home { get; set; }

        public long guest { get; set; }
        
        public string status { get; set; }
    }

    public class TimeEntity : TableEntity
    {
        public TimeEntity(string Hall, string Number)
        {
            this.PartitionKey = Hall;
            this.RowKey = Number;
        }

        public TimeEntity() { }

        public long minute { get; set; }

        public long second { get; set; }

        public string status { get; set; }
    }

    public partial class Scoreboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CloudStorageAccount myAccount = new CloudStorageAccount(new StorageCredentials("korfballdata",), true);

            // Create the table client.
            CloudTableClient tableClient = myAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("time");
            TableQuery<TimeEntity> query = new TableQuery<TimeEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "DeKorf"));

            // Print the fields for each customer.
            foreach (TimeEntity entity in table.ExecuteQuery(query))
            {
                theTime.Text = String.Format("{0}", entity.minute) + ":" + String.Format("{0:00}", entity.second);
            }

            table = tableClient.GetTableReference("score");
            TableQuery<ScoreEntity> query1 = new TableQuery<ScoreEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "DeKorf"));

            // Print the fields for each customer.
            foreach (ScoreEntity entity in table.ExecuteQuery(query1))
            {
                theScore.Text = entity.home.ToString() + " - " + entity.guest.ToString();
            }
        }
    }
}