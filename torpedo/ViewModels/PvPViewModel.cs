using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using torpedo.Models;
using System.Xml;
using System.Xml.Linq;


namespace torpedo.ViewModels
{
    public class PvPViewModel
    {

        public string Asd { get; set; }

        public int aasd { get; set; }

        private int _p1Hits { get; set; }
        private int _p2Hits { get; set; }
        private int _p1Misses { get; set; }
        private int _p2Misses { get; set; }

        private bool[,] wasCoordinateAlreadyAttackedByPlayer1 = new bool[10, 10];
        private bool[,] wasCoordinateAlreadyAttackedByPlayer2 = new bool[10, 10];

        private static int _numberOfShipCoordinates = 5;        //TODO: 5 helyére majd 17 kell (összesen annyi hajó koordináta van)
        private int[][] shipCoordinatesPlayer1 = new int[_numberOfShipCoordinates][];       
        private int[][] shipCoordinatesPlayer2 = new int[_numberOfShipCoordinates][];

        private int currentPlayer = 0;      //0 player1  1 player2




        //addShipOnCoordinate(int x, int y){
        //shipCoordinates[0] = new int[]{ x, y };


        public PvPViewModel()
        {
            Asd = "asd";
        }

        public PvPViewModel(string player1Name, string player2Name)
        {
            //Asd = new DelegateCommand(onAsd);
            Asd = "asd";
            aasd = 5;

            shipCoordinatesPlayer1[0] = new int[] { 1, 1 };
            shipCoordinatesPlayer1[1] = new int[] { 1, 2 };
            shipCoordinatesPlayer1[2] = new int[] { 1, 3 };
            shipCoordinatesPlayer1[3] = new int[] { 1, 4 };
            shipCoordinatesPlayer1[4] = new int[] { 1, 5 };

            shipCoordinatesPlayer2[0] = new int[] { 2, 1 };
            shipCoordinatesPlayer2[1] = new int[] { 2, 2 };
            shipCoordinatesPlayer2[2] = new int[] { 2, 3 };
            shipCoordinatesPlayer2[3] = new int[] { 2, 4 };
            shipCoordinatesPlayer2[4] = new int[] { 2, 5 };

        }

        public int getHits(int player)
        {
            if (player == 0)
            {
                return _p1Hits;
            }
            else
            {
                return _p2Hits;
            }
        }

        public bool isMyTurn(int playerNumber)
        {
            return playerNumber == currentPlayer;
        }

        public bool isUntouchedCoordinate(int x, int y)
        {
            if (currentPlayer == 0)
            {
                if (wasCoordinateAlreadyAttackedByPlayer1[x, y])
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (wasCoordinateAlreadyAttackedByPlayer2[x, y])
                {
                    return false;
                }
                return true;
            }
        }

        public bool isThereAShip(int x, int y)
        {
            if (currentPlayer == 0)
            {
                currentPlayer = 1;
                wasCoordinateAlreadyAttackedByPlayer1[x, y] = true;
                for (int i = 0; i < _numberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer2[i][0] == x && shipCoordinatesPlayer2[i][1] == y)
                    {
                        _p1Hits++;
                        //checkIfGameIsOver();
                        return true;
                    }
                }

                return false;
            }
            else
            {
                currentPlayer = 0;
                wasCoordinateAlreadyAttackedByPlayer2[x, y] = true;
                for (int i = 0; i < _numberOfShipCoordinates; i++)
                {
                    if (shipCoordinatesPlayer1[i][0] == x && shipCoordinatesPlayer1[i][1] == y)
                    {
                        _p2Hits++;
                        //checkIfGameIsOver();
                        return true;
                    }
                }

                return false;
            }
        }

        public bool checkIfGameIsOver()
        {
            if(_p1Hits >= _numberOfShipCoordinates)
            {
                //TODO: game over p1 won
                exportScore();
                return true;
            }
            else if(_p2Hits >= _numberOfShipCoordinates)
            {
                //TODO: game over p2 won
                exportScore();
                return true;
            }
            return false;
        }

        private void exportScore()
        {
            //eredmény kiiratása file-ba
            /*
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
            }*/
        }
            


    }
}
