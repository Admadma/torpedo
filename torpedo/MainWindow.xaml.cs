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

        //TODO place this button at the game end screen
        private void onxmlSave(object sender, EventArgs e)
        {
            if (File.Exists("Scores.xml") == false)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create("Scores.xml", xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Scores");
                    xmlWriter.WriteStartElement("Game");
                    xmlWriter.WriteElementString("PlayerOne", "Asd");
                    xmlWriter.WriteElementString("PlayerTwo", "Das");
                    xmlWriter.WriteElementString("Result", "Asd" + " Won");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {
                XDocument xDocument = XDocument.Load("Scores.xml");
                XElement root = xDocument.Element("Scores");
                IEnumerable<XElement> rows = root.Descendants("Game");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("Game",
                   new XElement("PlayerOne", "Abc"),
                   new XElement("PlayerTwo", "Def"),
                   new XElement("Result", "Def" + " Won")));
                xDocument.Save("Scores.xml");
            }
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
