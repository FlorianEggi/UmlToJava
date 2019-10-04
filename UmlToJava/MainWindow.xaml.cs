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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewDragEnter(object sender, DragEventArgs e)
        {

            //e.Effects = (DragDropEffects)cboDropEffects.SelectedItem;
            //if (chkHandled.IsChecked.HasValue)
            //{
            //    e.Handled = chkHandled.IsChecked.Value;
            //}
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = string.Format("{0}", ((string[])text)[0]);
            }
        }

        private void ItemDropped(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] dropPath = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (string dropfilepath in dropPath)
                {
                    var listBoxItem = new ListBoxItem();
                    if (System.IO.Path.GetExtension(dropfilepath).Contains(".txt"))
                    {
                        listBoxItem.Content = System.IO.Path.GetFileNameWithoutExtension(dropfilepath);
                        listBoxItem.ToolTip = dropPath;
                        txtbDragDrop.Text = (string)listBoxItem.Content;
                    }

                }
            }
        }
    }
}

    
