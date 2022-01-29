using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetSight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Classes.ThreadReceive tr;
        public MainWindow()
        {
            InitializeComponent();
            Classes.SendPacket.Send("apertura", "172.16.102.83");
            tr = new Classes.ThreadReceive();
            tr.StartThreadRicezione();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //using (var connection = new SqliteConnection(@"Data Source=dbs\database.db"))
            //{
            //    connection.Open();
            //    var command = connection.CreateCommand();

            //    //string table_name = "tabella";



            //    /* controllare se la tabella 'tabella' esiste
            //     * SELECT name FROM sqlite_master WHERE type='table' AND name='{table_name}';
            //     * se esiste nella reader.GetString(0) uscirà la stringa 'tabella' altrimenti niente.
            //     * 
            //     * creare la tabella 'tabella' e aggiungere ID (auto-increment e chiave primaria), Nome (text) e Cognome (text)
            //     * CREATE TABLE "tabella" ("ID"	INTEGER, "Nome"	TEXT, "Cognome"	TEXT, PRIMARY KEY("ID" AUTOINCREMENT));
            //     * 
            //     * aggiungere dati alla tabella 'tabella'
            //     * INSERT INTO tabella (Nome, Cognome) VALUES ('{txtNome.Text}','{txtCognome.Text}'); 
            //     * 
            //     */


            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var output = reader.GetString(0);

            //            // decidere di capire cosa fare con l'output
            //        }
            //    }
            //}
        }
    }

}

