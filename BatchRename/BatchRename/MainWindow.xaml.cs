﻿using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            public string Extension {
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

        BindingList<string> presetList = new BindingList<string>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            string presetPath = $"{AppDomain.CurrentDomain.BaseDirectory}preset\\";
            var presets = Directory.GetFiles(presetPath);
            foreach(var preset in presets)
            {
                presetList.Add(System.IO.Path.GetFileNameWithoutExtension(preset));
            }

            presetCombobox.ItemsSource = presetList;
        }

        private void BtnAdd_ClickFile(object sender, RoutedEventArgs e)
        {
            var files = NameDao.GetFileName();
            var flag = 0;
            if (files != null)
            {
                if (_filenames != null)
                {
                    for (var i = 0; i < _filenames.Count; i++)
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

        public void optionAfterRenamefile()
        {
            int flag = 0;
            int flagoption = 0;
            ComboBoxItem typeItem = (ComboBoxItem)OptionComboBox.SelectedItem;
            if (typeItem.Content.ToString() == "Keep the old name")
                flagoption = 0;
            else
                flagoption = 1;
            while (flag == 0)
            {
                int i = 0;
                int flag1 = 0;
                // giữ lại tên cũ
                if (flagoption == 0)
                {
                    for (i = 0; i < _filenames.Count - 1; i++)
                    {
                        flag1 = 0;
                        for (int j = i + 1; j < _filenames.Count; j++)
                        {
                            if (_filenames[i].Path == _filenames[j].Path && (_filenames[i].Prename == _filenames[j].Prename))
                            {
                                if (_filenames[j].Prename != _filenames[j].Name)
                                {
                                    if (_filenames[j].Error == "Success")
                                    {
                                        _filenames[j].Error = "Fail";
                                        _filenames[j].errorDetail = "duplicate name:" + _filenames[j].Prename;
                                    }
                                    else
                                    {
                                        _filenames[j].errorDetail += "duplicate name:" + _filenames[j].Prename;
                                    }
                                    _filenames[j].Prename = _filenames[j].Name;
                                    flag1 = 1;
                                }
                            }
                        }
                        if (flag1 == 1 && _filenames[i].Prename != _filenames[i].Name)
                        {
                            if (_filenames[i].Error == "Success")
                            {
                                _filenames[i].Error = "Fail";
                                _filenames[i].errorDetail = "duplicate name:" + _filenames[i].Prename;
                            }
                            else
                            {
                                _filenames[i].errorDetail += "duplicate name:" + _filenames[i].Prename;
                            }
                            _filenames[i].Prename = _filenames[i].Name;
                        }

                    }
                }

                // thêm hậu tố
                if (flagoption == 1)
                {
                    int suffix = 1;
                    for (i = 0; i < _filenames.Count - 1; i++)
                    {
                        flag1 = 0;
                        for (int j = i + 1; j < _filenames.Count; j++)
                        {
                            if (_filenames[i].Path == _filenames[j].Path && _filenames[i].Prename == _filenames[j].Prename)
                            {
                                if (_filenames[j].Prename != _filenames[j].Name)
                                {
                                    var newName = _filenames[j].Prename.Replace(_filenames[j].Extension, "");
                                    _filenames[j].Prename = newName + suffix.ToString() + _filenames[j].Extension;
                                    suffix++;
                                }
                                flag1 = 1;
                            }
                        }
                        if (flag1 == 1 && _filenames[i].Prename != _filenames[i].Name)
                        {
                            var newName = _filenames[i].Prename.Replace(_filenames[i].Extension, "");
                            _filenames[i].Prename = newName + suffix.ToString() + _filenames[i].Extension;
                            suffix++;
                        }
                    }
                }
                // kiểm tra có còn trùng hay không để tiếp tục vòng lặp
                flag = 1;
                for (i = 0; i < _filenames.Count - 1; i++)
                {
                    for (int j = i + 1; j < _filenames.Count; j++)
                    {
                        if (_filenames[i].Path == _filenames[j].Path)
                        {
                            if (_filenames[i].Prename == _filenames[j].Prename)
                            {
                                flag = 0;
                                break;
                            }
                        }
                    }
                }
            }
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
                    item.Extension = "." + newExtension;



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
                optionAfterRenamefile();
                fileNameListView.ItemsSource = _filenames;
            }
        }

        private void BtnStartBatch_File(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thực hiện thay đổi?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                
                List<string> oldname = new List<string>();

                if (_filenames != null)
                {
                    int idx = 0;
                    foreach (var item in _filenames)
                    {
                        oldname.Add(item.Path + @"\" + item.Name);
                        idx++;
                    }

                    int index = 0;
                    foreach (var item in _filenames)
                    {
                        //  var newName = item.Name.Replace(item.Extension, "");
                        var newName = item.Name.Replace(item.Extension, "");    // cắt extension ra khỏi tên file: abc.txt -> abc và txt
                        var newExtension = item.Extension.Replace(".", "");
                        item.errorDetail = "";                        //string Error ="";

                        for (int i = 0; i < ActionList.Count; i++)
                        {
                          
                                var changeName = ActionList[i].Operate(newName, newExtension, ref item.errorDetail);  // thực thi action vô prename

                                if (ActionList[i].GetStringName() == "Extension")  // Nếu thay đổi là đuôi ( chỉ đối với trường hợp replace đuôi)
                                {
                                    newExtension = changeName;                        // cập nhật lại newExtension mới nếu có sự thay đổi
                                }
                                else
                                {
                                    newName = changeName;                            // cập nhật lại newName mới nếu có sự thay đổi
                                }
                            

                        }

                        item.Prename = item.Path + @"\" + newName + "." + newExtension;  //Lấy đường dẫn đầy đủ của file hiện tại
                        FileInfo fi = new FileInfo(oldname.ElementAt(index));            //Lấy thông tin file
                        fi.MoveTo(item.Prename);                                        //Đổi tên file
                        index++;

                        item.Prename = newName + "." + newExtension;                  //Cập nhật lại để hiển thị
                        item.Extension = "." + newExtension;

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

                    optionAfterRenamefile();
                    fileNameListView.ItemsSource = _filenames;
                }
            }
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

            if (MessageBox.Show("Bạn muốn thực hiện thay đổi?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                List<string> oldname = new List<string>();

                if (_foldernames != null)
                {

                    int idx = 0;
                    foreach (var item in _foldernames)
                    {
                        oldname.Add(item.Path + @"\" + item.Name);
                        idx++;
                    }
                    int index = 0;
                    foreach (var item in _foldernames)
                    {
                        var newName = item.Name;
                        item.errorDetail = "";

                        for (int i = 0; i < ActionList.Count; i++)
                        {
                                // thực thi action vô prename
                                var changeName = ActionList[i].Operate(newName, "", ref item.errorDetail);

                                // cập nhật lại newName mới nếu có sự thay đổi
                                newName = changeName;
                        }

                        item.Prename = item.Path + @"\" + newName;
                        FileInfo fi = new FileInfo(oldname.ElementAt(index));
                        fi.MoveTo(item.Prename);
                        index++;
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

            actionSeleted.control.Visibility = Visibility.Hidden;
            actionSeleted.count = 0;
            actionSeleted.Expand = "+";

        }

        int indexSelect = -1;

        private void up_Clicked(object sender, RoutedEventArgs e)
        {
            var size = AddlistListView.Items.Count;
            if (indexSelect >= 1 && indexSelect != -1) 
            {
                var temp = Global.action[indexSelect];
                Global.action[indexSelect] = Global.action[indexSelect - 1];
                Global.action[indexSelect - 1] = temp;

                var newActionList = new BindingList<Action>();
                AddlistListView.ItemsSource = newActionList;
                AddlistListView.ItemsSource = Global.action;
                indexSelect--;
            }
        }

        private void upall_Clicked(object sender, RoutedEventArgs e)
        {
            var size = AddlistListView.Items.Count;
            if (indexSelect >= 1 && indexSelect != -1)
            {
                var temp = Global.action[indexSelect];
                for (int i = indexSelect - 1; i >= 0; i--)
                {
                    Global.action[i + 1] = Global.action[i];
                }
                Global.action[0] = temp;

                var newActionList = new BindingList<Action>();
                AddlistListView.ItemsSource = newActionList;
                AddlistListView.ItemsSource = Global.action;
                indexSelect = 0;
            }
        }

        private void down_Clicked(object sender, RoutedEventArgs e)
        {
            var size = AddlistListView.Items.Count;
            if (indexSelect < size - 1 && indexSelect != -1)
            {
                var temp = Global.action[indexSelect];
                Global.action[indexSelect] = Global.action[indexSelect + 1];
                Global.action[indexSelect + 1] = temp;

                var newActionList = new BindingList<Action>();
                AddlistListView.ItemsSource = newActionList;
                AddlistListView.ItemsSource = Global.action;
                indexSelect++;
            }
        }

        private void downall_Clicked(object sender, RoutedEventArgs e)
        {
            var size = AddlistListView.Items.Count;
            if (indexSelect < size - 1 && indexSelect != -1)
            {
                var temp = Global.action[indexSelect];
                for (int i = indexSelect; i < size - 1; i++)
                {
                    Global.action[i] = Global.action[i + 1];
                }
                Global.action[size - 1] = temp;

                var newActionList = new BindingList<Action>();
                AddlistListView.ItemsSource = newActionList;
                AddlistListView.ItemsSource = Global.action;
                indexSelect = size - 1;
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
            indexSelect = index;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var index = AddlistListView.SelectedIndex;
            Global.action[index].Check = false;
            AddlistListView.ItemsSource = Global.action;
            indexSelect = index;
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

        private void listView_Click(object sender, MouseButtonEventArgs e)
        {
            indexSelect = AddlistListView.SelectedIndex;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(openFileDialog.FileName);
                if (lines == null) return;
                //cat tung dong roi bo vo mang
                const string nganCach = " * ";

                foreach (string line in lines)
                {
                    string[] tokens = line.Split(new string[] { nganCach }, StringSplitOptions.None);
                    // token[0]: class name
                    // token[1]: description
                        
                    switch(tokens[0])
                    {
                        case "New Case":
                            {
                                //xử lý description
                                if (tokens[1].Contains("Make string ")) // kiểm tra description của new case
                                {
                                    tokens[1] = tokens[1].Replace("Make string ", "");
                                    // tạo action add vô global
                                    var newcase = new NewCaseAction()
                                    {
                                        Args = new NewCaseArgs()
                                        {
                                            type = tokens[1]
                                        }
                                    };
                                    if (Global.action == null)
                                    {
                                        Global.action = new BindingList<Action>();
                                    }
                                    Global.action.Add(newcase);
                                }
                                break;
                            }
                        case "Fullname Normalize":
                            {
                                //kiểm tra description fullname normalize
                                if (tokens[1] == "No space at two endings" || tokens[1] == "Capitalize first letter" || tokens[1] == "No extra spaces among words")
                                {
                                    // tạo action add vô global
                                    var fullnameNormalize = new FullnameNormalizeAction()
                                    {
                                        Args = new FullnameNormalizeArgs()
                                        {
                                            option = tokens[1]
                                        }
                                    };
                                    if (Global.action == null)
                                    {
                                        Global.action = new BindingList<Action>();
                                    }
                                    Global.action.Add(fullnameNormalize);
                                }
                                break;
                            }
                        case "Unique Name":
                            {
                                // tạo action add vô global
                                var uniquename = new UniqueNameAction()
                                {
                                    Args = new UniqueNameArgs()
                                    {
                                        option = tokens[1]
                                    }
                                };
                                if (Global.action == null)
                                {
                                    Global.action = new BindingList<Action>();
                                }
                                Global.action.Add(uniquename);
                                break;
                            }
                        case "Replace":
                            {
                                //xử lý description "Replace '{args.From}' to '{args.To}' in '{args.StringChange}'"
                                var pos = 0;
                                string from = "";
                                string to = "";
                                string strChange = "";
                                string pattern = @"\'(.*?)\'";
                                foreach (Match match in Regex.Matches(tokens[1], pattern))
                                {
                                    if (match.Success && match.Groups.Count > 0)
                                    {
                                        if(pos == 0)
                                            from = match.Groups[1].Value;
                                        if(pos == 1)
                                            to = match.Groups[1].Value;
                                        if(pos == 2)
                                            strChange = match.Groups[1].Value;
                                        pos++;
                                    }
                                }
                                if (from != "" && to != "" && strChange != "") // kiểm tra description replace
                                {
                                    // tạo action add vô global
                                    var replace = new ReplaceAction()
                                    {
                                        Args = new ReplaceArgs()
                                        {
                                            From = from,
                                            To = to,
                                            StringChange = strChange
                                        }
                                    };
                                    if (Global.action == null)
                                    {
                                        Global.action = new BindingList<Action>();
                                    }
                                    Global.action.Add(replace);
                                }
                                break;
                            }
                        case "Move":
                            {
                                //xử lý description "Move [{args.length}] character(s) from index [{args.startAt}] to the [{args.moveAt}]";
                                var pos = 0;
                                int len = -1;
                                int startat = -1;
                                string moveat = "";
                                var n = 0;
                                var m = 0;
                                string pattern = @"\[(.*?)\]";
                                foreach (Match match in Regex.Matches(tokens[1], pattern))
                                {
                                    if (match.Success && match.Groups.Count > 0)
                                    {
                                        if (pos == 0)
                                        {
                                            if(int.TryParse(match.Groups[1].Value,out n))
                                                len = int.Parse(match.Groups[1].Value);
                                        }
                                        if (pos == 1)
                                        {
                                            if(int.TryParse(match.Groups[1].Value, out m))
                                                startat = int.Parse(match.Groups[1].Value);
                                        }
                                        if (pos == 2)
                                            moveat = match.Groups[1].Value;
                                        pos++;
                                    }
                                }
                                if (len > 0 && startat > 0 && moveat == "Begin" || moveat == "End") // bắt lỗi description move
                                {
                                    // tạo action add vô global
                                    var move = new MoveAction()
                                    {
                                        Args = new MoveArgs()
                                        {
                                            startAt = startat,
                                            length = len,
                                            moveAt = moveat
                                        }
                                    };
                                    if (Global.action == null)
                                    {
                                        Global.action = new BindingList<Action>();
                                    }
                                    Global.action.Add(move);
                                }
                                break;
                            }
                    }
                }
                AddlistListView.ItemsSource = Global.action;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save text Files",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true,
            };

            // get filename
            var path = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            if (path != "")// bắt lỗi người dùng không chọn file
            {
                FileStream fs = new FileStream(path, FileMode.Create);

                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);

                foreach (var act in Global.action)
                {
                    var str = act.Classname + " * " + act.Description;
                    sWriter.WriteLine(str);
                }

                sWriter.Flush();
                fs.Close();
            }  
        }

        private void PresetCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lines = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}preset\\{presetCombobox.SelectedItem as string}.txt");
            if (lines == null) return;
            //cat tung dong roi bo vo mang
            const string nganCach = " * ";

            foreach (string line in lines)
            {
                string[] tokens = line.Split(new string[] { nganCach }, StringSplitOptions.None);
                // token[0]: class name
                // token[1]: description

                switch (tokens[0])
                {
                    case "New Case":
                        {
                            //xử lý description
                            if (tokens[1].Contains("Make string ")) // kiểm tra description của new case
                            {
                                tokens[1] = tokens[1].Replace("Make string ", "");
                                // tạo action add vô global
                                var newcase = new NewCaseAction()
                                {
                                    Args = new NewCaseArgs()
                                    {
                                        type = tokens[1]
                                    }
                                };
                                if (Global.action == null)
                                {
                                    Global.action = new BindingList<Action>();
                                }
                                Global.action.Add(newcase);
                            }
                            break;
                        }
                    case "Fullname Normalize":
                        {
                            //kiểm tra description fullname normalize
                            if (tokens[1] == "No space at two endings" || tokens[1] == "Capitalize first letter" || tokens[1] == "No extra spaces among words")
                            {
                                // tạo action add vô global
                                var fullnameNormalize = new FullnameNormalizeAction()
                                {
                                    Args = new FullnameNormalizeArgs()
                                    {
                                        option = tokens[1]
                                    }
                                };
                                if (Global.action == null)
                                {
                                    Global.action = new BindingList<Action>();
                                }
                                Global.action.Add(fullnameNormalize);
                            }
                            break;
                        }
                    case "Unique Name":
                        {
                            // tạo action add vô global
                            var uniquename = new UniqueNameAction()
                            {
                                Args = new UniqueNameArgs()
                                {
                                    option = tokens[1]
                                }
                            };
                            if (Global.action == null)
                            {
                                Global.action = new BindingList<Action>();
                            }
                            Global.action.Add(uniquename);
                            break;
                        }
                    case "Replace":
                        {
                            //xử lý description "Replace '{args.From}' to '{args.To}' in '{args.StringChange}'"
                            var pos = 0;
                            string from = "";
                            string to = "";
                            string strChange = "";
                            string pattern = @"\'(.*?)\'";
                            foreach (Match match in Regex.Matches(tokens[1], pattern))
                            {
                                if (match.Success && match.Groups.Count > 0)
                                {
                                    if (pos == 0)
                                        from = match.Groups[1].Value;
                                    if (pos == 1)
                                        to = match.Groups[1].Value;
                                    if (pos == 2)
                                        strChange = match.Groups[1].Value;
                                    pos++;
                                }
                            }
                            if (from != "" && to != "" && strChange != "") // kiểm tra description replace
                            {
                                // tạo action add vô global
                                var replace = new ReplaceAction()
                                {
                                    Args = new ReplaceArgs()
                                    {
                                        From = from,
                                        To = to,
                                        StringChange = strChange
                                    }
                                };
                                if (Global.action == null)
                                {
                                    Global.action = new BindingList<Action>();
                                }
                                Global.action.Add(replace);
                            }
                            break;
                        }
                    case "Move":
                        {
                            //xử lý description "Move [{args.length}] character(s) from index [{args.startAt}] to the [{args.moveAt}]";
                            var pos = 0;
                            int len = -1;
                            int startat = -1;
                            string moveat = "";
                            var n = 0;
                            var m = 0;
                            string pattern = @"\[(.*?)\]";
                            foreach (Match match in Regex.Matches(tokens[1], pattern))
                            {
                                if (match.Success && match.Groups.Count > 0)
                                {
                                    if (pos == 0)
                                    {
                                        if (int.TryParse(match.Groups[1].Value, out n))
                                            len = int.Parse(match.Groups[1].Value);
                                    }
                                    if (pos == 1)
                                    {
                                        if (int.TryParse(match.Groups[1].Value, out m))
                                            startat = int.Parse(match.Groups[1].Value);
                                    }
                                    if (pos == 2)
                                        moveat = match.Groups[1].Value;
                                    pos++;
                                }
                            }
                            if (len > 0 && startat > 0 && moveat == "Begin" || moveat == "End") // bắt lỗi description move
                            {
                                // tạo action add vô global
                                var move = new MoveAction()
                                {
                                    Args = new MoveArgs()
                                    {
                                        startAt = startat,
                                        length = len,
                                        moveAt = moveat
                                    }
                                };
                                if (Global.action == null)
                                {
                                    Global.action = new BindingList<Action>();
                                }
                                Global.action.Add(move);
                            }
                            break;
                        }
                }
            }
            AddlistListView.ItemsSource = Global.action;
        }
        private void Delectaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Do you want to delect '{Global.action[indexSelect].Description}'", "Notify", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Global.action.RemoveAt(indexSelect);
                    indexSelect = -1;
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }
    }
}