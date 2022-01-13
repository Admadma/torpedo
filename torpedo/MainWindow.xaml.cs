using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Data;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onPvc(object sender, RoutedEventArgs e)
        {  
            PlayerVsComputer pvcWindow = new PlayerVsComputer();
            pvcWindow.Show();
            this.Close();
            
        }
        private void onPvp(object sender, RoutedEventArgs e)
        {
            PlayerVsPlayer pvpWindow = new PlayerVsPlayer();
            pvpWindow.Show();
            this.Close();
        }
        private void onScoreboard(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml("Scores.xml");
            dataGrid1.ItemsSource = dataSet.Tables[0].DefaultView;
            dataGrid1.Visibility = Visibility.Visible;
        }
    }
}
