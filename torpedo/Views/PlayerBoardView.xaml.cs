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
using System.Windows.Navigation;
using System.Windows.Shapes;
using torpedo.Models;

namespace torpedo.Views
{
    /// <summary>
    /// Interaction logic for PlayerBoardView.xaml
    /// </summary>
    public partial class PlayerBoardView : UserControl
    {
        Models.GameModel gameModel;
        public PlayerBoardView()
        {
            InitializeComponent();
            gameModel = new Models.GameModel();
        }
        

        public void colorAtCoordinate(int x, int y)
        {
            MessageBox.Show("problemo");
            //PlayerBoardGrid.A1.Background = Brushes.Yellow;
            /*
             * Grid clickedGrid = (Grid)A1.Children
                .Cast<UIElement>()
                .First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);

            MessageBox.Show($"{clickedGrid.Background}");
            */

            //PlayerBoardGrid.Children
              //  .Cast<UIElement>()
                //.First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y)
            //clickedGrid.Background = Brushes.Purple;

        }


    }
}
