using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GiftLib;
using Microsoft.Data.Sqlite;

namespace Dictionary_Server
{
    public static class DataBaseReader
    {
        private static string _dataBasePath = @"Data Source=E:\проги\C#\xnasgift\GiftServer\Resource\GiftsDB.db";
        public static string ReadEverything()
        {
            //"You need to call .  If you are using a bundle package, this is done by calling SQLitePCL.Batteries.Init()."
            string data="";
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
            return data;
        }
    }
}
