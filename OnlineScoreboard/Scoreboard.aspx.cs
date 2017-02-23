using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

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

        public int home { get; set; }

        public int guest { get; set; }
        
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

        public int minute { get; set; }

        public int second { get; set; }

        public string status { get; set; }
    }

    public partial class Scoreboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CloudStorageAccount myAccount = new CloudStorageAccount(new StorageCredentials("korfballdata", "MRTKQbWdclaeO3ceOKnIryOiuIRr4/kypWsoGzDmL2+Q3JMxpUB/2rRGH6AxFyMB5lmaW/iFIEAGeuB0PxBeFA=="), true);

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