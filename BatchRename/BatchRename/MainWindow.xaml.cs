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
        

        class FolderName : INotifyPropertyChanged
        {
            public string Name { get; set; }
            public string prename;
            public string Prename {
                get => prename; set
                {
                    prename = value;
                    Notify("Prename");
                }
            }
            public string Path { get; set; }
            public string Error { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
            public void Notify(string Prename)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Prename));
            }
        }

        class FileName : FolderName
        {
            public string extension;
            public string Extension{
                get => extension; set
                {
                    extension = value;
                    Notify("Extension");
                }
            }
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
                            Name = System.IO.Path.GetFileName(file),
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
            _actionlist = new BindingList<ActionMain>
            {
                new ActionMain(new ReplaceAction(){ }, new ReplaceControl()),
                new ActionMain(new NewCaseAction(){ }, new NewCaseControl()),
                new ActionMain(new FullnameNormalizeAction(){ }, new FullnameNormalizeControl()),
                new ActionMain(new MoveAction(){ }, new MoveControl()),
                new ActionMain(new UniqueNameAction(){ }, new UniqueNameControl()),
            };
            ActionsListView.ItemsSource = _actionlist;

        }

        BindingList<ActionMain> _actionlist;
        public List<Action> ActionList = new List<Action>();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_ClickFile(object sender, RoutedEventArgs e)
        {
            var files = NameDao.GetFileName();
            var flag = 0;
            if (files != null)
            {
                if (_filenames != null)
                {
                    for(var i=0;i<_filenames.Count;i++)
                    {
                        if (_filenames[i].Path == files[0].Path)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        foreach (var file in files)
                        {
                            _filenames.Add(file);
                        }
                    }
                }
                else
                {
                    _filenames = files;
                }
            }
            //binding 
            //Binding binding = new Binding("_filenames");
            //binding.Source = _filenames;
            //fileNameListView.SetBinding(ListView.ItemsSourceProperty, binding);
            fileNameListView.ItemsSource = _filenames;
        }

        private void BtnPreview_ClickFile(object sender, RoutedEventArgs e)
        {
            ActionList = new List<Action>(Global.action);
            foreach(var item in _filenames)
            {
                int j = ActionList.Count;
                for (int i = 0; i < ActionList.Count; i++) 
                {
                    item.Prename = ActionList[i].Operate(item.Name);
                    //Notify("_filenames");
                    // cắt extension ra khỏi tên file: abc.txt -> abc
                    item.Name = item.Name.Replace(item.Extension, "");
                    // thực thi action đồng thời gắn extension vô prename
                    item.Prename = ActionList[i].Operate(item.Name) + item.Extension;
                    // gắn lại extension cho tên file
                    item.Name += item.Extension;
                    MessageBox.Show(item.Prename);
                }
            }
        }

        private void BtnStartBatch_File(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_ClickFolder(object sender, RoutedEventArgs e)
        {
            var folders = NameDao.GetFolderName();
            var flag = 0;
            if (folders != null)
            {
                if (_foldernames != null)
                {
                    for (var i = 0; i < _foldernames.Count; i++)
                    {
                        if (_foldernames[i].Path == folders[0].Path)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        foreach (var folder in folders)
                        {
                            _foldernames.Add(folder);
                        }
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

        private void BtnPreview_ClickFolder(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStartPatch_ClickFolder(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowControl(object sender, RoutedEventArgs e)
        {
            ActionMain actionMain = ActionsListView.SelectedItem as ActionMain;
            actionMain.ShowControl();
        }

        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            ActionMain actionMain = ActionsListView.SelectedItem as ActionMain;
            UserControlGrid.Children.Remove(actionMain.control);
            UserControlGrid.Children.Add(actionMain.control);
        }
    }
}