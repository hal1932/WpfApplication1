﻿using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _controlPanel.TargetType = typeof(TestConfigItem);
            _controlPanel.TargetItem = new TestConfigItem();
        }
    }
}
