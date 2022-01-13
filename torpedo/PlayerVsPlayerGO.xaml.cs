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
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.IO;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for PlayerVsPlayerGO.xaml
    /// </summary>
    public partial class PlayerVsPlayerGO : Window
    {
        //TODO get real playernames and winner
        string Winner = "winner";
        string tPlayerone = "a";
        string tPlayertwo = "b";

        public PlayerVsPlayerGO()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    xmlWriter.WriteElementString("PlayerOne", tPlayerone);
                    xmlWriter.WriteElementString("PlayerTwo", tPlayertwo);
                    xmlWriter.WriteElementString("Result", Winner + " Won");
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
                   new XElement("PlayerOne", tPlayerone),
                   new XElement("PlayerTwo", tPlayertwo),
                   new XElement("Result", Winner + " Won")));
                xDocument.Save("Scores.xml");
            }
        }
    }
}
