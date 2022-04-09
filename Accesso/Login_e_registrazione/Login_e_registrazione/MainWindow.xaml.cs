﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Login_e_registrazione
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void lbl_link_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_link.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void lbl_link_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_link.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private void lbl_link_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Registrazione r = new Registrazione(this);
            r.Show();
            this.Hide();
        }

        private void btn_login_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_login.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
            btn_login.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 0, 0, 0));
        }

        private void btn_login_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_login.Background = null;
            btn_login.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 255, 255, 255));
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string mail = txt_mail.Text;
            string password = txt_password.Text;
            if (mail != "" && password != "")
            {
                
                    var values = new Dictionary<string, string>
                    { 
                    { "email", mail },
                    { "password", password }
                    }; 

                    var content = new FormUrlEncodedContent(values);

                    var response = await client.PostAsync("http://localhost/dippolito/ConfirmLogin.php", content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);
                
            }
            else
            {
                MessageBox.Show("Inserire tutti i dati");
            }
        }
    }
}