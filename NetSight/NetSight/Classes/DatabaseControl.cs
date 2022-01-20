using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight.Classes
{
    static class DatabaseControl
    {
        static SqliteConnection connection = new SqliteConnection();
        static SqliteCommand command = new SqliteCommand();
        public static void ConnectToDatabase()
        {
            using (connection = new SqliteConnection(@"Data Source=dbs\database.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
            }
        }

        public static void AddToDatabase(string nometabella, List<Pc> pcs)
        {
            // controllo la tabella con il nome opportuno
            command.CommandText = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{nometabella}';";

            if (nometabella.Equals("pc"))
            {
                command.CommandText = $"create table '{nometabella}' (Nome varchar(20), ipAddress varchar(20), stato bit, Luogo varchar(20)";
                for (int i = 0; i < pcs.Count; i++)
                {
                    command.CommandText = $"INSERT INTO tabella(Nome, ipAddress, stato, Luogo) VALUES('{pcs[0].Nome}', '{pcs[1].ipAddress}', '{pcs[2].Stato}', '{pcs[3].Luogo}')";
                }
            }
        }
    }
}
