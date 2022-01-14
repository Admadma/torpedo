using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using torpedo.ViewModels;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for PlayerVsComputer.xaml
    /// </summary>
    public partial class PlayerVsComputer : Window
    {
        public PlayerVsComputer()
        {
            InitializeComponent();
        }

        private void onPvcPlay(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9$]");
            if (regex.IsMatch(Player1c.Text) || string.IsNullOrWhiteSpace(Player1c.Text))
            {
                MessageBox.Show("Player1 name is invalid");
                return;
            }
            //TODO: rendes PvC osztályt megcsinálni
            //PlayerVsComputerGame pvcgWindow = new PlayerVsComputerGame();
            //pvcgWindow.Show();

            PvCViewModel vm = new PvCViewModel();
            PvCPlaceShips pvcPlaceShips = new PvCPlaceShips(vm);
            this.Close();
        }
    }
}
