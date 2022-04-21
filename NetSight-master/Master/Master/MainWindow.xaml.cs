﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utente utente;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void btnAccesso_Click(object sender, RoutedEventArgs e)
        {
            utente = await Utente.GetUserObject(txtEmail.Text, psw.Password.ToString());
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
