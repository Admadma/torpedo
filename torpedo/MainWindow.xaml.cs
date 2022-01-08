﻿using System;
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
            // TODO: visszaállítani a pvc játék indítását
            /*
            PlayerVsComputer pvcWindow = new PlayerVsComputer();
            pvcWindow.Show();
            this.Close();
            */
            
            PlayerVsComputerGame pvcgWindow = new PlayerVsComputerGame();
            pvcgWindow.Show();
            this.Close();
        }
        private void onPvp(object sender, RoutedEventArgs e)
        {
            PlayerVsPlayer pvpWindow = new PlayerVsPlayer();
            pvpWindow.Show();
            this.Close();
        }
    }
}
