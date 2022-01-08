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
        private void FooCommand(object sender, MouseEventArgs e)
        {
            Grid clickedGrid = (Grid)sender;

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


    }
}
