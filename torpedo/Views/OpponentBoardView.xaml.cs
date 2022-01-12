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
    /// Interaction logic for OpponentBoardView.xaml
    /// </summary>
    public partial class OpponentBoardView : UserControl
    {
        Models.GameModel gameModel;
        public OpponentBoardView()
        {
            InitializeComponent();
            gameModel = new Models.GameModel();
        }
        private void FooCommand(object sender, MouseEventArgs e)
        {
            Grid clickedGrid = (Grid)sender;

            colorAtCoordinate(0, 0);

            switch (gameModel.getMyFieldState(new Position(Grid.GetColumn(clickedGrid), Grid.GetRow(clickedGrid))))
            {
                case GameModel.fieldState.Untouched:
                    MessageBox.Show("untouched");
                    clickedGrid.Background = Brushes.Gray;
                    break;
                case GameModel.fieldState.Hit:
                    clickedGrid.Background = Brushes.Red;
                    MessageBox.Show("hit");
                    break;
                case GameModel.fieldState.Miss:
                    clickedGrid.Background = Brushes.Blue;
                    MessageBox.Show("miss");
                    break;
                default:
                    break;
            }
        }

        public void colorAtCoordinate(int x, int y)
        {
            //MessageBox.Show("problemo");
            Grid clickedGrid = (Grid)OpponentBoardGrid.Children
                .Cast<UIElement>()
                .First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);

            clickedGrid.Background = Brushes.Purple;

            //MessageBox.Show($"{clickedGrid.Background}");

            //PlayerBoardGrid.Children
            //  .Cast<UIElement>()
            //.First(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y)
            //clickedGrid.Background = Brushes.Purple;

        }
    }
}
