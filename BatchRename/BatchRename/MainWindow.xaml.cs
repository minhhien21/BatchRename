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

            public string error;
            public string Error
            {
                get => error;
                set
                {
                    error = value;
                    Notify("Error");
                }
            }

            public string errorDetail;

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
                            Error = null,
                            errorDetail = null
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
                            Error = null,
                            errorDetail = null
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
        BindingList<Action> ActionList = new BindingList<Action>();


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
            
            if (Global.action != null)
            {
                ActionList = new BindingList<Action>(Global.action);
            }
            AddlistListView.ItemsSource = Global.action;
            if (_filenames != null)
            {
                foreach (var item in _filenames)
                {
                    // cắt extension ra khỏi tên file: abc.txt -> abc và txt
                    var newName = item.Name.Replace(item.Extension, "");
                    var newExtension = item.Extension.Replace(".", "");
                    //string Error ="";
                    item.errorDetail = "";

                    for (int i = 0; i < ActionList.Count; i++)
                    {
                        if (ActionList[i].Check == true)
                        {
                            // thực thi action vô prename
                            var changeName = ActionList[i].Operate(newName, newExtension, ref item.errorDetail);

                            // Nếu thay đổi là đuôi ( chỉ đối với trường hợp replace đuôi)
                            if (ActionList[i].GetStringName() == "Extension")
                            {
                                // cập nhật lại newExtension mới nếu có sự thay đổi
                                newExtension = changeName;
                            }
                            else
                            {
                                // cập nhật lại newName mới nếu có sự thay đổi
                                newName = changeName;
                            }
                        }

                    }
                    item.Prename = newName + "." + newExtension;
                    if (item.errorDetail != "")
                    {
                        item.Error = "Fail";
                    }
                    else
                    {
                        item.Error = "Success";
                        item.errorDetail = "Success";
                    }
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
            if (Global.action != null)
            {
                ActionList = new BindingList<Action>(Global.action);
            }
            AddlistListView.ItemsSource = Global.action;
            if (_foldernames != null)
            {
                foreach (var item in _foldernames)
                {
                    var newName = item.Name;
                    item.errorDetail = "";

                    for (int i = 0; i < ActionList.Count; i++)
                    {
                        if (ActionList[i].Check == true)
                        {
                            // thực thi action vô prename
                            var changeName = ActionList[i].Operate(newName, "", ref item.errorDetail);

                            // cập nhật lại newName mới nếu có sự thay đổi
                            newName = changeName;
                        }

                    }
                    item.Prename = newName;
                    if (item.errorDetail != "")
                    {
                        item.Error = "Fail";
                    }
                    else
                    {
                        item.Error = "Success";
                        item.errorDetail = "Success";
                    }
                }
            }
        }

        private void BtnStartPatch_ClickFolder(object sender, RoutedEventArgs e)
        {

        }

        ActionMain actionSeleted = null; // variable for saving action is selected before

        private void Button_ShowControl(object sender, RoutedEventArgs e)
        {
            ActionMain actionMain = ActionsListView.SelectedItem as ActionMain;

            if (actionSeleted != null)
            {
                if (actionSeleted.ClassName != actionMain.ClassName)
                {
                    actionSeleted.control.Visibility = Visibility.Hidden;
                    actionSeleted.count = 0;
                    actionSeleted.Expand = "+";
                }
            }

            actionSeleted = actionMain;

            actionMain.ShowControl();
        }

        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            ActionMain actionMain = ActionsListView.SelectedItem as ActionMain;
            UserControlGrid.Children.Remove(actionMain.control);
            UserControlGrid.Children.Add(actionMain.control);

        }

        private void refresh_Clicked(object sender, RoutedEventArgs e)
        {
            if (_filenames != null)
            {
                _filenames.Clear();
            }
            if (_foldernames != null)
            {
                _foldernames.Clear();
            }
            //if (_actionlist != null)
            //{
            //    _actionlist.Clear();
            //}
            _actionlist = new BindingList<ActionMain>
            {
                new ActionMain(new ReplaceAction(){ }, new ReplaceControl()),
                new ActionMain(new NewCaseAction(){ }, new NewCaseControl()),
                new ActionMain(new FullnameNormalizeAction(){ }, new FullnameNormalizeControl()),
                new ActionMain(new MoveAction(){ }, new MoveControl()),
                new ActionMain(new UniqueNameAction(){ }, new UniqueNameControl()),
            };
            ActionsListView.ItemsSource = _actionlist;

            ActionList = new BindingList<Action>();
            AddlistListView.ItemsSource = ActionList;
            Global.action = new BindingList<Action>();
        }
        private void up_Clicked(object sender, RoutedEventArgs e)
        {
            var select = AddlistListView.SelectedItem; 
            if(select != null)
            {
                var index = AddlistListView.SelectedIndex;
                var size = AddlistListView.Items.Count;
                if(index >= 1)
                {
                    var temp = Global.action[index];
                    Global.action[index] = Global.action[index - 1];
                    Global.action[index - 1] = temp;
                }
            }

        }

        private void upall_Clicked(object sender, RoutedEventArgs e)
        {
            var select = AddlistListView.SelectedItem;
            if (select != null)
            {
                var index = AddlistListView.SelectedIndex;
                var size = AddlistListView.Items.Count;
                if (index >= 1)
                {
                    var temp = Global.action[index];
                    for (int i = index - 1; i >= 0; i--) 
                    {
                        Global.action[i + 1] = Global.action[i];
                    }
                    Global.action[0] = temp;
                }
            }
        }

        private void down_Clicked(object sender, RoutedEventArgs e)
        {
            var select = AddlistListView.SelectedItem;
            if (select != null)
            {
                var index = AddlistListView.SelectedIndex;
                var size = AddlistListView.Items.Count;
                if(index < size - 1)
                {
                    var temp = Global.action[index];
                    Global.action[index] = Global.action[index + 1];
                    Global.action[index + 1] = temp;
                }
            }
        }

        private void downall_Clicked(object sender, RoutedEventArgs e)
        {
            var select = AddlistListView.SelectedItem;
            if (select != null)
            {
                var index = AddlistListView.SelectedIndex;
                var size = AddlistListView.Items.Count;
                if (index < size - 1)
                {
                    var temp = Global.action[index];
                    for (int i = index; i < size - 1; i++) 
                    {
                        Global.action[i] = Global.action[i + 1];
                    }
                    Global.action[size - 1] = temp;
                }
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                AddlistListView.ItemsSource = Global.action;
            }
        }
        
        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            var index = AddlistListView.SelectedIndex;
            Global.action[index].Check = true;
            AddlistListView.ItemsSource = Global.action;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var index = AddlistListView.SelectedIndex;
            Global.action[index].Check = false;
            AddlistListView.ItemsSource = Global.action;
        }

        private void ErrorDetailFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = fileNameListView.SelectedItem as FileName;
            MessageBox.Show(item?.errorDetail, "ErrorDetail");
        }

        private void ErrorDetailFolderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = folderNameListView.SelectedItem as FolderName;
            MessageBox.Show(item?.errorDetail, "ErrorDetail");
        }
    }
}