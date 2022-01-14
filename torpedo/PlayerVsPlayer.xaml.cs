using System;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using torpedo.ViewModels;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for PlayerVsPlayer.xaml
    /// </summary>
    public partial class PlayerVsPlayer : Window
    {

        public PlayerVsPlayer()
        {
            InitializeComponent();
        }

        public string Playerone { get; private set; }
        public string Playertwo { get; private set; }

        private void onPlay(object sender, RoutedEventArgs e)
        {
            //TODO: todo
            //név fogadás már működik...   amíg többször futtatom és tesztelgetem ne várjon neveket, ezért kikommentelem és placeholder nevekkel pótlom
            //ezeket később ki kell majd törölni
            Player1.Text = "asd";
            Player2.Text = "baaaaad";

           
            Regex regex = new Regex(@"[^a-zA-Z0-9$]");
            if (regex.IsMatch(Player1.Text) || string.IsNullOrWhiteSpace(Player1.Text))
            {
                MessageBox.Show("Player1 name is invalid");
                return;
            }

            if (regex.IsMatch(Player2.Text) || string.IsNullOrWhiteSpace(Player2.Text))
            {
                MessageBox.Show("Player2 name is invalid");
                return;
            }

            Playerone = Player1.Text;
            Playertwo = Player2.Text;

            if (true)
            {
                PvPViewModel vm = new PvPViewModel();
                vm.player1Name = Playerone;
                vm.player2Name = Playertwo;
                
                PlaceShips placeShips = new PlaceShips(vm);
                placeShips.Show();
                this.Close();
                
                /*
                TempWindow tmpW = new TempWindow();
                Player1Window p1w = new Player1Window(vm, tmpW);
                Player2Window p2w = new Player2Window(vm, tmpW);

                tmpW = new TempWindow(p1w, p2w, vm);

                p1w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                p2w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                tmpW.WindowStartupLocation = WindowStartupLocation.CenterScreen;


                tmpW.Show();
                this.Close();
                */
                
                
            }
            
        }
    }
}
