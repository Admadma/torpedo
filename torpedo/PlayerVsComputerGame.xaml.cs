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

using Position = torpedo.Models.Position;
using MainModel = torpedo.Models.MainModel;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for PlayerVsComputerGame.xaml
    /// </summary>
    public partial class PlayerVsComputerGame : Window
    {
        public PlayerVsComputerGame()
        {
            InitializeComponent();
        }
        /*
        public enum FieldStates { HIT, MISS, UNTOUCHED};
        Position position1 = new Position(1, 1);
        MainModel model = new MainModel(10);
        
       

        public void handleButtonClick(Position position, Grid clickedGrid)
        {

            //MessageBox.Show($"Button pressed on column: {position.X} and row: {position.Y}");
            //clickedGrid.Background = Brushes.Red;
            //int number =
        }

        private void mouseHoveredOverButton(object sender, MouseEventArgs e)
        {

        }

        private void FooCommand(object sender, MouseEventArgs e)
        {
            Grid clickedGrid = (Grid) sender;
            handleButtonClick(new Position(Grid.GetColumn(clickedGrid), Grid.GetRow(clickedGrid)), clickedGrid);
        }
        */
    }
}
