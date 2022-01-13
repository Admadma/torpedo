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
using torpedo.ViewModels;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for TempWindow.xaml
    /// </summary>
    public partial class TempWindow : Window
    {
        Player1Window p1w;
        Player2Window p2w;
        PvPViewModel vm;

        public void setParameters(Player1Window p1w, Player2Window p2w, PvPViewModel vm)//TODO: PvPViewModel helyett egy közös szülő osztály (pl GameViewModel) és ennek lehet majd értékül adni a leszármazottait: PvPViewModel/PvCViewModel
        {
            if(p1w is not null)
            {
                this.p1w = p1w;

            }
            if(p2w is not null)
            {
                this.p2w = p2w;

            }
            this.vm = vm;
        }

        private int currentPlayer = 0;

        public TempWindow()
        {
            InitializeComponent();
        }

        public TempWindow(Player1Window p1w, Player2Window p2w, PvPViewModel vm)//TODO: PvPViewModel helyett egy közös szülő osztály (pl GameViewModel) és ennek lehet majd értékül adni a leszármazottait: PvPViewModel/PvCViewModel
        {
            InitializeComponent();
            this.p1w = p1w;
            this.p2w = p2w;
            this.vm = vm;

            
        }


        public void onButtonClick(object sender, RoutedEventArgs e)
        {
            currentPlayer = vm.getCurrentPlayer();

            if (currentPlayer == 0)
            {
                //MessageBox.Show($"the current player is {currentPlayer}");
                p1w.setParameters(vm, this);
                p1w.Show();
                this.Hide();
            } 
            else if (currentPlayer == 1)
            {
                p2w.setParameters(vm, this);
                p2w.Show();
                this.Hide();
            }
        }
    }
}
