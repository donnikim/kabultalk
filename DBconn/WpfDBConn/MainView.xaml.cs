using System.Windows;
using WpfDBConn.Repositories;
using WpfDBConn.ViewModels;

namespace WpfDBConn
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainView : Window
  {
    public MainView()
    {
      InitializeComponent();

      AccountRepository? accountRepository = new AccountRepository();
      DataContext = new MainViewModel(accountRepository);
    }
  }
}
