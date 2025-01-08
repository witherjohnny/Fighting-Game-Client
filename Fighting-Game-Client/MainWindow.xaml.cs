using Fighting_Game_Client.Data;
using Fighting_Game_Client.UserControls;
using System;
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
using static Fighting_Game_Client.Data.CharacterData;

namespace Fighting_Game_Client
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            CharactersData.LoadCharacters("CharacterSettings.json");
            ServerSettings.JsonLoadSettings("serverSettings.json");
            if (CharactersData.Characters == null)
                throw new InvalidOperationException("Characters data is not loaded. Please ensure the JSON file is correctly loaded.");
            if (ServerSettings.Ip == null || ServerSettings.Port == null)
                throw new InvalidOperationException("Sever data is not loaded. Please ensure the JSON file is correctly loaded.");
            this.SizeChanged += MainWindow_SizeChanged;
            // carica il primo controllo utente
            LoadHome();
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Update the current user control's size
            if (panelContainer.Children.Count > 0)
            {
                var currentControl = panelContainer.Children[0] as UserControl;
                if (currentControl != null)
                {
                    currentControl.Width = e.NewSize.Width;
                    currentControl.Height = e.NewSize.Height;
                }
            }
        }
        private void LoadHome()
        {
            //creazione controllo utente schermata home
            var home = new UserControlHome();
            //occupare tutto lo spazio disponibile nel pannello
            home.HorizontalAlignment = HorizontalAlignment.Stretch;
            home.VerticalAlignment = VerticalAlignment.Stretch;
            home.Width= this.Width;
            home.Height= this.Height;

            // Handle the PlayClicked event
            home.PlayClicked += (s, e) => LoadSelezionePersonaggio();

            panelContainer.Children.Clear();
            panelContainer.Children.Add(home);

        }
        private void LoadSelezionePersonaggio()
        {
            //creazione del controllo utente per la selezione del personaggio
            var selezionePersonaggio = new UserControlSelezionePersonaggio();
            selezionePersonaggio.HorizontalAlignment = HorizontalAlignment.Stretch;
            selezionePersonaggio.VerticalAlignment = VerticalAlignment.Stretch;
            selezionePersonaggio.Width = this.Width;
            selezionePersonaggio.Height = this.Height;

            //assegna la funzione LoadGioco all'evento PlayClicked
            selezionePersonaggio.PlayClicked += (s, e) => LoadGioco();

            //sostituisce i controlli precedenti nel pannello
            panelContainer.Children.Clear();
            panelContainer.Children.Add(selezionePersonaggio);
        }

        private void LoadGioco()
        {
            //creazione del controllo utente per il gioco, passando il personaggio selezionato
            var gioco = new UserControlGioco();
            gioco.displayExitOrPlayAgain += displayExitOrPlayAgain;
            gioco.HorizontalAlignment = HorizontalAlignment.Stretch;
            gioco.VerticalAlignment = VerticalAlignment.Stretch;
            gioco.Width = this.Width;
            gioco.Height = this.Height;

            //sostituisce i controlli precedenti nel pannello
            panelContainer.Children.Clear();
            panelContainer.Children.Add(gioco);
        }
        private void displayExitOrPlayAgain(string message)
        {
            var menu = new ExitOrPlayAgain(message);
            menu.HorizontalAlignment = HorizontalAlignment.Center;
            menu.VerticalAlignment = VerticalAlignment.Center;

            menu.Width = this.Width;
            menu.Height = this.Height;
            menu.playAgain += () => LoadSelezionePersonaggio();
            menu.exit += () => leaveAndLoadHome();

            
            panelContainer.Children.Add(menu);
        }
        private void leaveAndLoadHome()
        {
            leave();
            LoadHome();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            leave();
        }
       
        private void leave()
        {
            string serverAddress = ServerSettings.Ip;
            int serverPort = (int)ServerSettings.Port;
            UdpClient udpClient = UdpClientSingleton.Instance;

            String messaggio = "leave";
            byte[] data = Encoding.ASCII.GetBytes(messaggio);

            udpClient.Send(data, data.Length, serverAddress, serverPort);

        }
    }
}
