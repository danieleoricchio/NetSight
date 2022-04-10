﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utente utente;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnAccesso_Click(object sender, RoutedEventArgs e)
        {
            utente = new Utente(txtEmail.Text, psw.Password.ToString());
            if (utente.valid)
            {
                VisualizzaLaboratorio visualizzaLaboratorio = new VisualizzaLaboratorio();
                visualizzaLaboratorio.Show();
                this.Close();
            }
            else
                MessageBox.Show("Email e/o password sbagliati.", "Errore");
        }
    }
}
