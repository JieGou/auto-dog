﻿using System.Windows.Controls;
using System.Windows;
namespace TestExerciserPro.TestViews
{
    /// <summary>
    /// Interaction logic for TilesExample.xaml
    /// </summary>
    public partial class TilesExample : UserControl
    {
        public TilesExample()
        {
            InitializeComponent();
        }

        private void AutoTesting_OnClick(object sender, RoutedEventArgs e)
        {
            MainAutoTesting main = new MainAutoTesting();
            main.Show();
        }
    }
}