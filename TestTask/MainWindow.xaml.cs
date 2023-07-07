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

            //GenerateStartTreeView();
            
            UpdateTreeView();

            PrintAllComponents();
        }

        private static void PrintAllComponents()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var engineComponents = db.EngineComponents.ToList();
                foreach (var eng in engineComponents)
                {
                    Console.WriteLine($"{eng.Id} {eng.Name} {eng.Value} {eng.ParentId}");
                }
            }
        }

        private void UpdateTreeView()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var components = db.EngineComponents.ToList();
                _nodes = new ObservableCollection<Node>();
                
                foreach (var component in components)
                {
                    if (component.ParentId == null) continue;

                    _nodes.Add(new Node
                    {
                        Value = component.Value,
                        Name = component.Name,
                        ParentId = component.ParentId
                    });
                    
                    components.Remove(component);
                }
                
                TreeView1.ItemsSource = _nodes;
            }
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

        private void AddEngineButton_OnClick(object sender, RoutedEventArgs e)
        {
            var component = new EngineComponent
            {
                Name = "Двигатель",
                Value = 0,
                ParentId = null
            };
            
            using (ApplicationContext db = new ApplicationContext())
            {
                db.EngineComponents.Add(component);
                db.SaveChanges();
            }
            
            UpdateTreeView();
            PrintAllComponents();
            //throw new NotImplementedException();
        }
    }
}
