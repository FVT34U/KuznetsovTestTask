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

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Node>? _nodes;
        
        public MainWindow()
        {
            InitializeComponent();

            GenerateStartTreeView();
        }

        private void GenerateStartTreeView()
        {
            _nodes = new ObservableCollection<Node>
            {
                new Node
                {
                    Name = "Двигатель 1",
                    Nodes = new ObservableCollection<Node>
                    {
                        new Node
                        {
                            Value = 1,
                            Name = "Компрессор",
                            Nodes = new ObservableCollection<Node>
                            {
                                new Node { Value = 2, Name = "Цилиндр" },
                                new Node { Value = 1, Name = "Статор" },
                                new Node { Value = 1, Name = "Поршень" },
                                new Node { Value = 1, Name = "Вал" },
                            }
                        },
                        new Node { Value = 2, Name = "Камера сгорания" },
                        new Node { Value = 1, Name = "Турбина" }
                    }
                }
            };

            TreeView1.ItemsSource = _nodes;
        }
    }
}
