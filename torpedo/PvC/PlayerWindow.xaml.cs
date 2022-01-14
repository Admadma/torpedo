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

namespace torpedo.PvC
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        PvCViewModel vm;
        ComputerWindow cw;
        public PlayerWindow(PvCViewModel vm)
        {
            InitializeComponent();

            this.vm = vm;
        }

        public void onKeyADown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.A)
            {
                cw = new ComputerWindow();
                cw.Show();
            }
        }
    }
}
