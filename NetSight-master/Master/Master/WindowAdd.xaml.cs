using Master.Classi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Master
{
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        string type = string.Empty;
        Edificio edificio;
        Laboratorio lab;
        Utente user;
        Pc pc;
        public WindowAdd(string Type, Utente utente)
        {
            InitializeComponent();
            this.type = Type;
            this.user = utente;
            switch (type)
            {
                case "edificio":
                    setXamlEdificio();
                    break;
                case "laboratorio":
                    setXamlLaboratorio();
                    break;
                case "pc":
                    lblAdd.Content = "Aggiungi pc";
                    break;
                default:
                    break;
            }
        }
        public WindowAdd(Edificio edificio, Utente user)
        {
            this.user = user;
            this.edificio = edificio;
            InitializeComponent();
            type = "laboratorio";
            setXamlLaboratorio();
        }
        private void setXamlEdificio()
        {
            lblAdd.Content = "Aggiungi edificio";
            lblAdd.FontFamily = new FontFamily("Arial");
            lblAdd.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl2.Content = "Inserisci indirizzo edificio";
            lbl2.FontFamily = new FontFamily("Arial");
            lbl2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl1.Content = "Inserisci nome edificio";
            lbl1.FontFamily = new FontFamily("Arial");
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void setXamlLaboratorio()
        {
            lblAdd.Content = "Aggiungi laboratorio";
            lblAdd.FontFamily = new FontFamily("Arial");
            lblAdd.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            lbl1.Content = "Inserisci nome laboratorio";
            lbl1.FontFamily = new FontFamily("Arial");
            lbl1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            txt2.Visibility = Visibility.Hidden;
            lbl2.Visibility = Visibility.Hidden;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "edificio":
                    Dictionary<string, string> valuesEd = new Dictionary<string, string>()
                    {
                        { "nome", txt1.Text },
                        { "indirizzo", txt2.Text },
                        { "type", "edificio" }
                    };
                    JsonMessage? messageEd = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_add, valuesEd);
                    if (messageEd == null || !messageEd.result)
                    {
                        MessageBox.Show("Edificio non aggiunto", "Errore nell'aggiunta");
                    }
                    else
                    {
                        MessageBox.Show("Edificio aggiunto");
                    }
                    break;
                case "laboratorio":
                    Dictionary<string, string> valuesLab = new Dictionary<string, string>()
                    {
                        { "nome", txt1.Text },
                        { "codedificio", edificio.cod.ToString() },
                        { "type", "laboratorio" }
                    };
                    JsonMessage? messageLab = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_add, valuesLab);
                    if (messageLab == null || !messageLab.result)
                    {
                        MessageBox.Show("laboratorio non aggiunto", "Errore nell'aggiunta");
                    }
                    else
                    {
                        MessageBox.Show("laboratorio aggiunto");
                    }
                    break;
                case "pc":
                    break;
                default:
                    break;
            }
        }
        private void btnIndietroPagAdd_Click(object sender, RoutedEventArgs e)
        {
            SceltaLaboratorio scelta = new SceltaLaboratorio(user);
            scelta.Show();
            this.Close();
        }
    }
}
