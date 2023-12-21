using Microsoft.Data.Sqlite;

namespace Dictionary_Server
{
    public static class DataBaseReader
    {

        public static List<string[]> ReadEverything(string lang)
        {
            string _dataBasePath = $"Data Source=GiftsDB-{lang}.db";
            string[] tables = { "Candles", "Clothes", "Cookies", "Ornaments" };
            List<string[]> list = new List<string[]>();

            // создание подключения к базе данных
            SqliteConnection DBconnection = new SqliteConnection(_dataBasePath);
            DBconnection.Open();

            // в цикле читаем все таблицы
            foreach (string table in tables)
            {
                string sqlExpression = $"SELECT * FROM {table}";
                SqliteCommand command = DBconnection.CreateCommand();
                command.CommandText = sqlExpression;

                //читаем БД
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] serializedData = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) };
                        list.Add(serializedData);
                    }
                }
            }
            DBconnection.Close();
            return list;
        }
    }
}
