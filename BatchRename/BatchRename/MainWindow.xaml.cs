using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<FileName> _filenames = null;

        BindingList<FolderName> _foldernames = null;

        class FolderName
        {
            public string Name { get; set; }
            public string Prename { get; set; }
            public string Path { get; set; }
            public string Error { get; set; }
        }

        class FileName : FolderName
        {
            public string Extension { get; set; }
        }



        class NameDao
        {
            /// <summary>
            /// Lấy danh sách tên file / folder
            /// </summary>
            /// <returns></returns>
            public static BindingList<FileName> GetFileName()
            {
                BindingList<FileName> result = null;
                //mở dialog cho người dùng chọn file
                var screen = new CommonOpenFileDialog();
                screen.IsFolderPicker = true;

                if (screen.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var path = screen.FileName;
                    var files = Directory.GetFiles(path);

                    result = new BindingList<FileName>();
                    //cắt file path
                    foreach (var file in files)
                    {
                        var fileName = new FileName()
                        {
                            Name = System.IO.Path.GetFileNameWithoutExtension(file),
                            Extension = System.IO.Path.GetExtension(file),
                            Prename = null,
                            Path = System.IO.Path.GetDirectoryName(file),
                            Error = null
                        };
                        result.Add(fileName);
                    }
                }
                return result;
            }

            public static BindingList<FolderName> GetFolderName()
            {
                BindingList<FolderName> result = null;
                //mở dialog cho người dùng chọn file
                var screen = new CommonOpenFileDialog();
                screen.IsFolderPicker = true;

                if (screen.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var path = screen.FileName;
                    var folders = Directory.GetDirectories(path);

                    result = new BindingList<FolderName>();
                    //cắt folder path
                    foreach (var folder in folders)
                    {
                        var folderName = new FolderName()
                        {
                            Name = System.IO.Path.GetFileName(folder),
                            Prename = null,
                            Path = System.IO.Path.GetDirectoryName(folder),
                            Error = null
                        };
                        result.Add(folderName);
                    }
                }
                return result;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        BindingList<ButtonItem> _buttonitem = null;
        public class ButtonItem
        {
            public string ButtonName { get; set; }

            public string ClickButtonName { get; set; }

            public string CheckName { get; set; }

        }

        class ButtonItemDao
        {
            /// <summary>
            /// Lấy danh sách laptop từ cơ sở dữ liệu
            /// </summary>
            /// <returns></returns>
            public static BindingList<ButtonItem> GetAll()
            {
                BindingList<ButtonItem> result = null;
                var lines = File.ReadAllLines("ButtonName.txt");
                var count = int.Parse(lines[0]);
                if (count > 0)
                {
                    result = new BindingList<ButtonItem>();
                    const string Separator = " / ";
                    for (int i = 0; i < count; i++)
                    {
                        var tokens = lines[i + 1].Split(new string[]
                            { Separator }, StringSplitOptions.RemoveEmptyEntries);
                        var buttonitem = new ButtonItem()
                        {
                            ButtonName = tokens[0],
                            ClickButtonName = tokens[1],
                            CheckName = tokens[2]
                        };
                        result.Add(buttonitem);
                    }
                }
                return result;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _buttonitem = ButtonItemDao.GetAll();
            ButtonNames.ItemsSource = _buttonitem;
        }


        private void Button_up(object sender, RoutedEventArgs e)
        {
            var x = _buttonitem[1];
            _buttonitem[1] = _buttonitem[0];
            _buttonitem[0] = x;
        }

        private void Button_up1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var index = NameFileTabControl.SelectedIndex;
            if (index == 0)
            {
                var files = NameDao.GetFileName();
                if (files != null)
                {
                    if (_filenames != null)
                    {
                        foreach (var file in files)
                        {
                            _filenames.Add(file);
                        }
                    }
                    else
                    {
                        _filenames = files;
                    }
                }
                //binding 
                fileNameListView.ItemsSource = _filenames;
            }
            else if (index == 1)
            {
                var folders = NameDao.GetFolderName();
                if (folders != null)
                {
                    if (_foldernames != null)
                    {
                        foreach (var folder in folders)
                        {
                            _foldernames.Add(folder);
                        }
                    }
                    else
                    {
                        _foldernames = folders;
                    }
                }

                //binding
                folderNameListView.ItemsSource = _foldernames;
            }
        }

        private void BtnAdd_ClickFile(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPreview_ClickFile(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStartBatch_File(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_ClickFolder(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPreview_ClickFolder(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStartPatch_ClickFolder(object sender, RoutedEventArgs e)
        {

        }
    }
}