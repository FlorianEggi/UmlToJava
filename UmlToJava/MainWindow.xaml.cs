﻿using Microsoft.Win32;
using System;
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

namespace UmlToJava
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetStandardPath();
        }

        private void SetStandardPath()
        {
            txbName.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        
        private void TxtbDragDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("*.graphml"))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        private void TxtbDragDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("*.graphml"))
            {
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (txtPackage.Text.Equals(""))
                {
                    var fileNameArr = filenames[0].Split('\\');
                    txtPackage.Text = fileNameArr[fileNameArr.Length - 1].Substring(0, fileNameArr[fileNameArr.Length - 1].Length - 8);
                }

                //txtContent.Text = File.ReadAllText(filenames[0]);


            }
            
        }


        private void BtnExplore_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fbd.ShowDialog();
                txbName.Text = fbd.SelectedPath;
            }
        }
    }
}


