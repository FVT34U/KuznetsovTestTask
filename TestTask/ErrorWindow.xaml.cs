using System.Windows;

namespace TestTask;

public partial class ErrorWindow : Window
{
    public ErrorWindow(string message)
    {
        InitializeComponent();

        Label.Content = message;
    }
}