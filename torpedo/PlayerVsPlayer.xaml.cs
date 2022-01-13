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
using System.Windows.Shapes;

namespace torpedo
{
    /// <summary>
    /// Interaction logic for PlayerVsPlayer.xaml
    /// </summary>
    public partial class PlayerVsPlayer : Window
    {

        public PlayerVsPlayer()
        {
            InitializeComponent();
        }

        private void onPlay(object sender, RoutedEventArgs e)
        {
            //TODO: todo
            //név fogadás már működik...   amíg többször futtatom és tesztelgetem ne várjon neveket, ezért kikommentelem és placeholder nevekkel pótlom
            //ezeket később ki kell majd törölni
            Player1.Text = "asd";
            Player2.Text = "baaaaad";
            //if (!string.IsNullOrWhiteSpace(Player1.Text) && !string.IsNullOrWhiteSpace(Player2.Text))     
            if(true)
            {
                ViewModels.PvPViewModel vm = new ViewModels.PvPViewModel(Player1.Text, Player2.Text);

                //Player1Window p1Window = new Player1Window(vm);
                //Player2Window p2Window = new Player2Window(vm);
                
                TempWindow tmpW = new TempWindow();

                Player1Window p1w = new Player1Window(vm, tmpW);
                Player2Window p2w = new Player2Window(vm, tmpW);
                //p1w = new Player1Window(vm, p2w, tmpW);

                tmpW = new TempWindow(p1w, p2w, vm);


                tmpW.Show();
                this.Close();

                //p1Window.Show();
                //p2Window.Show();

                //if (p1Window.IsVisible)
                //this.Close();
            }
            else
            {
                MessageBox.Show("Érvénytelen név!");
            }
            

            
        }
    }
}
