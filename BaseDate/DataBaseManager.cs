using System.Data.SQLite;

namespace BaseDate
{
    public class DataBaseManager
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source = bd1.db");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SQLiteConnection GetConnection { get { return connection; } }

    }
}
