using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Logica di interazione per GestioneFinestra.xaml
    /// </summary>
    public partial class GestioneFinestra : Window
    {
        MainApp app;
        public GestioneFinestra(MainApp app)
        {
            InitializeComponent();
            this.app = app;
            labelThisIp.Content = app.thisIp;
            labelLabIp.Content = app.labIp;
            rectangleConnesso.Fill = app.connesso ? Brushes.Green : Brushes.Red;
            buttonDisconnetti.IsEnabled = app.connesso;
            new Thread(() =>
            {
                while (true)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { labelLabIp.Content = app.labIp; }));
                    Application.Current.Dispatcher.Invoke(new Action(() => { rectangleConnesso.Fill = app.connesso ? Brushes.Green : Brushes.Red; }));
                    Application.Current.Dispatcher.Invoke(new Action(() => { buttonDisconnetti.IsEnabled = app.connesso; }));
                    Thread.Sleep(1000);
                }
            }).Start();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (app.ChiediDisconnessione())
            {
                MessageBox.Show("Disconnessione da laboratorio accettata.", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Disconnessione da laboratorio rifutata.", "Richiesta rifiutata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
