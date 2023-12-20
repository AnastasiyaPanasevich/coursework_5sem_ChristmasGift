using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Dictionary_Server
{
    public static class DataBaseReader
    {
        private static string _dataBasePath = @"Data Source=
        C:\Users\matil\OneDrive\Документы\5 sem 2023-2024\kursKPO2023\ChristmasGift\ChristmasGiftServer\Model\GiftsDB.db;";
        public static string Read()
        {
            string data="";
            // создание подключения к базе данных
            SqliteConnection DBconnection = new SqliteConnection(_dataBasePath);
            DBconnection.Open();

            //задаём команду
            string sqlExpression = "SELECT * FROM Words";
            SqliteCommand command = DBconnection.CreateCommand();
            command.CommandText = sqlExpression;

            //читаем БД
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data += reader.GetString(0) + "#" +
                                 reader.GetString(1) + "#" + 
                                 reader.GetString(2) + "|";
                    }
                }
            }
            DBconnection.Close();
            return data;
        }
        public static void Write(string buf)
        {
            // создание подключения к базе данных
            using (var connection = new SqliteConnection(_dataBasePath))
            {
                // открытие подключения
                connection.Open();

                // SQL-запрос на добавление записи в таблицу
                string sql = "INSERT INTO Words (Слово, Расшифровка, Определение) VALUES (@value1, @value2, @value3)";

                // создание команды для выполнения SQL-запроса
                using (var command = new SqliteCommand(sql, connection))
                {
                    string defaultValue = "-";
                    // добавление параметров в команду
                    string[] dataBuf = buf.Split("#");
                    command.Parameters.AddWithValue("@value1", dataBuf[0] != "" ? dataBuf[0] : defaultValue);
                    command.Parameters.AddWithValue("@value2", dataBuf[1] != "" ? dataBuf[1] : defaultValue);
                    command.Parameters.AddWithValue("@value3", dataBuf[2] != "" ? dataBuf[2] : defaultValue);

                    // выполнение команды
                    int rowsAffected = command.ExecuteNonQuery();

                    // вывод количества добавленных записей
                    Console.WriteLine($"{rowsAffected} запись добавлена");
                }
            }
        }

    }
}
