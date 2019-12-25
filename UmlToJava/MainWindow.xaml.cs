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
            }

        private void TxtbDragDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("*.graphml"))
            {

                //Contact contact = e.Data.GetData("myFormat") as Contact;
                //ListView listView = sender as ListView;
                //listView.Items.Add(contact);
            }
        }

        private void TxtbDragDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("*.graphml") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}


