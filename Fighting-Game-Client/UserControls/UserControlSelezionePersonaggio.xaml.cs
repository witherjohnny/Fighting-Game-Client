﻿using Fighting_Game_Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Emit;
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

namespace Fighting_Game_Client.UserControls
{
    //schermata per selezionare il personaggio
    public partial class UserControlSelezionePersonaggio : UserControl
    {
        public event Action<string> PlayClicked; // Usa un Action con un parametro stringa
        private string personaggioScelto = null;
        protected virtual void OnPlayClicked(string personaggio)
        {
            if (PlayClicked != null)
            {
                PlayClicked?.Invoke(personaggio);
            }
        }

        public UserControlSelezionePersonaggio()
        {
            InitializeComponent();
            stileButtonPlay();
        }

        private void stileButtonPlay()
        {
            // Mouse hover effect
            buttonPlay.MouseEnter += (s, e) =>
            {
                buttonPlay.BorderBrush = new SolidColorBrush(Colors.White);
                buttonPlay.BorderThickness = new Thickness(2);
            };

            buttonPlay.MouseLeave += (s, e) =>
            {
                buttonPlay.BorderBrush = null;
                buttonPlay.BorderThickness = new Thickness(0);
            };
            buttonPlay.Content = "PLAY";
            buttonPlay.FontSize = 18;
            buttonPlay.FontWeight = FontWeights.Bold;
            buttonPlay.Foreground = System.Windows.Media.Brushes.White;
            buttonPlay.Background = System.Windows.Media.Brushes.Red;
        }

        private void loadSelected(String nomePersonaggio)
        {
            try
            {
                // rimuovi eventuali controlli esistenti nel TableLayoutPanel
                PanelPersonaggio.Children.Clear();

                AnimationBox characterBox = new AnimationBox(nomePersonaggio, "Idle", 0, 0, PanelPersonaggio.ActualWidth, PanelPersonaggio.ActualHeight, Enums.Direction.Right);

                personaggioScelto = nomePersonaggio;
                PanelPersonaggio.Children.Add(characterBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel caricamento delle animazioni: {ex.Message}", "Errore");
            }

        }
        private void pictureBoxWarrior_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Example character selection process
            string nomePersonaggio = "Warrior_2"; // example
            labelNomeGiocatoreSelezionato.Content = "Warrior";
            loadSelected(nomePersonaggio);
            buttonPlay.IsEnabled = true;
            buttonPlay.Background = System.Windows.Media.Brushes.Green;
        }


        private void pictureBoxFireWizard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string nomePersonaggio = "FireWizard"; // example
            labelNomeGiocatoreSelezionato.Content = "Fire Wizard";
            loadSelected(nomePersonaggio);
            buttonPlay.IsEnabled = true;
            buttonPlay.Background = System.Windows.Media.Brushes.Green;
        }
        private async void buttonPlay_Click_1(object sender, RoutedEventArgs e)
        {
            //vuol dire che il personaggio è stato confermato, si comunica al server il personaggio selezionato+
            String personaggio = personaggioScelto;
            String messaggio = "ready;" + personaggio;

            //lo manda al server
            string serverAddress = ServerSettings.Ip; //IP del server
            int serverPort = (int)ServerSettings.Port; //porta del server

            //scrittura
            //usa UdpClient per la comunicazione
            UdpClient udpClient = UdpClientSingleton.Instance;

            //messaggio di richiesta al server
            byte[] data = Encoding.ASCII.GetBytes(messaggio);

            //invia il messaggio al server
            udpClient.Send(data, data.Length, serverAddress, serverPort);


            //aspetta dal server la conferma per poter giocare
            // (che entrambi i giocatori hanno scelto)

            //lettura
            IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

            byte[] dataReceived = null;

            //gestione thread
            Task t = Task.Factory.StartNew(() =>
            {
                try
                {
                    dataReceived = udpClient.Receive(ref riceveEP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore :" + ex.ToString());
                }
            });
            await t;

            if (dataReceived == null)
                return;
            String risposta = Encoding.ASCII.GetString(dataReceived);

            //gestisce la risposta del server
            if (risposta == "Gioco iniziato")
            {
                OnPlayClicked(personaggio); // passa il personaggio selezionato
            }
            else
            {
                MessageBox.Show("In attesa del secondo player");
            }
        }

       
    }
}
