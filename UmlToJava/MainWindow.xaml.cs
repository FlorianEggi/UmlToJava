using Microsoft.Win32;
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
        string fileName = "";
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if ((((string[])e.Data.GetData(DataFormats.FileDrop))[0]).Contains(".graphml"))
                    e.Effects = DragDropEffects.Copy;
            }
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        private void TxtbDragDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                if ((((string[])e.Data.GetData(DataFormats.FileDrop))[0]).EndsWith(".graphml"))
                {
                    string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
                    fileName = filenames[0];
                    FileChosen();


                }
                else LogMessage("Falsches Format! (*.graphml)");
            }
        }

        private void FileChosen()
        {

            txtbDragDrop.Text = fileName;

            if (txtPackage.Text.Equals(""))
            {
                var fileNameArr = fileName.Split('\\');
                txtPackage.Text = fileNameArr[fileNameArr.Length - 1].Substring(0, fileNameArr[fileNameArr.Length - 1].Length - 8);
            }
        }

        private void LogMessage(string v)
        {
            txblLogs.Text = v;
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

        private void TxtbDragDrop_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "graphml",
                Filter = "graphml files (*.graphml)|*.graphml",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog().Value)
            {
                if (openFileDialog1.FileName.EndsWith(".graphml"))
                {
                    fileName = openFileDialog1.FileName;
                    FileChosen();
                }
                else LogMessage("Falsches Format!(*.graphml)");
            }
        }

        private bool CheckIfConvertable()
        {
            if (fileName.Equals("")) LogMessage("Keine Datei ausgewählt");
            else
            {
                if (txtPackage.Text.Equals("")) LogMessage("Kein Projektordnername ausgewählt");
                else return true;
            }
            return false;
        }

        private void BtnConvert_Clicked(object sender, RoutedEventArgs e)
        {
            if (CheckIfConvertable())
            {
                //fileName ist der Pfad zum .graphml file

                //Zum Lesen vom File:
                //var text = File.ReadAllText(fileName);

                //Zum Fehlermeldungen ausgeben gibts die Methode LogMessage
                Parser p = new Parser();
                var pathForProject = txbName.Text + "\\" + txtPackage.Text;
                var xmlPath = fileName;
                p.parse(xmlPath, pathForProject);

                Directory.CreateDirectory(pathForProject);

                //Wenn erfolgreich erstellt:
                txblLogs.Foreground = new SolidColorBrush(Colors.Green);
                txblLogs.Text = "Projektordner erfolgreich erstellt!";
            }


        }

        public void Compile(List<Package> packages, string path)
        {
            string actpath;
            foreach (var package in packages)
            {
                string pathstring = System.IO.Path.Combine(path, package.name);
                actpath = pathstring;
                foreach (var clazz in package.classes)
                {
                    string classPath = $@"{actpath} + \ + {clazz.name} +.java";
                    //string pathstring2 = System.IO.Path.Combine(actpath, clazz.name);
                    if (clazz.isInterface)
                    {
                        TextWriter tw = new StreamWriter(classPath);
                        tw.WriteLine($"interface {clazz.name}");
                        tw.WriteLine("{");
                        foreach (var method in clazz.methods)
                        {
                            tw.WriteLine($"public {method.type} {method.name}({method.args});");
                        }
                        tw.WriteLine("}");
                    }
                    else
                    {
                        TextWriter tw = new StreamWriter(classPath);
                        tw.WriteLine($"public class {clazz.name}");
                        tw.WriteLine("{");
                        foreach (var parameter in clazz.parameters)
                        {
                            tw.WriteLine($"{ parameter.type} {parameter.name}");
                        }
                        foreach (var method in clazz.methods)
                        {
                            tw.WriteLine($"public {method.type} {method.name}({method.args})");
                            tw.WriteLine("{");
                            tw.WriteLine("}");
                            tw.WriteLine();
                        }
                        tw.WriteLine("}");
                    }
                }
            }
        }
    }
}


