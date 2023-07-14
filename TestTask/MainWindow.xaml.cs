using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.VisualBasic.CompilerServices;
using WinForms = System.Windows.Forms;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Node>? _nodes;

        private DatabaseController _databaseController = new DatabaseController();
        
        public MainWindow()
        {
            InitializeComponent();

            UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            TreeView1.ItemsSource = _databaseController.GetDataAsList();
        }

        private void AddEngineButton_OnClick(object sender, RoutedEventArgs e)
        {
            _databaseController.AddEngine();
            
            UpdateTreeView();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nodeToDelete = TreeView1.SelectedItem;

            if (nodeToDelete == null) { MessageBox.Show("Выберите объект из списка для удаления!"); return;}
            if (nodeToDelete.GetType() != typeof(Node)) throw new Exception("selected item is not 'Node' type");
            
            _databaseController.DeleteComponent((Node)nodeToDelete);
            
            UpdateTreeView();
        }

        private void RenameButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nodeToRename = TreeView1.SelectedItem;
            var newName = ComponentNameTextBox.Text.Trim();

            if (newName == "") { MessageBox.Show("Имя не должно быть пустым!"); return; }
            
            if (nodeToRename == null) { MessageBox.Show("Выберите объект из списка для переименования!"); return;}
            if (nodeToRename.GetType() != typeof(Node)) throw new Exception("selected item is not 'Node' type");

            Node node = (Node)nodeToRename;

            if (node.Name.Contains("Двигатель")) { MessageBox.Show("Имя не должно быть пустым!"); return; }

            if (!_databaseController.RenameComponent((Node)nodeToRename, newName)) { MessageBox.Show("Такое имя уже занято!"); return; }
            
            UpdateTreeView();
        }

        private void AddComponentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var parentNode = TreeView1.SelectedItem;
            var name = ComponentNameTextBox.Text.Trim();
            var value = ComponentValueTextBox.Text.Trim();
            var val = 0;
            
            if (name == "") { MessageBox.Show("Имя не должно быть пустым!"); return; }

            try { val = int.Parse(value); }
            catch { MessageBox.Show("Неверное значение в поле 'Количество'!"); return; }

            if (parentNode == null) { MessageBox.Show("Выберите объект из списка для добавления компонента!"); return;}
            if (parentNode.GetType() != typeof(Node)) throw new Exception("selected item is not 'Node' type");

            if (name.Contains("Двигатель")) { MessageBox.Show("Добавьте двигатель через специальную кнопку!"); return;}
            
            _databaseController.AddComponent((Node)parentNode, name, val);
            
            UpdateTreeView();
        }

        private void ShowLogButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                var folder = dialog.SelectedPath;
                var node = TreeView1.SelectedItem;
                
                if (node == null) { MessageBox.Show("Выберите объект из списка для формирования отчёта!"); return;}
                if (node.GetType() != typeof(Node)) throw new Exception("selected item is not 'Node' type");
                
                _databaseController.ShowLog((Node)node, folder);
            }
            else { MessageBox.Show("Не удалось открыть папку!"); return; }
        }
    }
}
