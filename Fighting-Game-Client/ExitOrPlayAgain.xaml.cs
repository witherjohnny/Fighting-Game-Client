using Fighting_Game_Client.Data;
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

namespace Fighting_Game_Client
{
    
    public partial class ExitOrPlayAgain : UserControl
    {
        public delegate void PlayAgainDelegate();
        public delegate void ExitDelegate();

        public event PlayAgainDelegate playAgain;
        public event ExitDelegate exit;
        public ExitOrPlayAgain(string label)
        {
            InitializeComponent();
            labelTitolo.Content += label;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (playAgain != null)
            {
                playAgain?.Invoke();
            }
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playAgain != null)
            {
                exit?.Invoke();
            }

        }
    }
}