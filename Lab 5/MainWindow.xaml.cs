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

namespace Lab_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListOfAdmins.ItemsSource = User.admins;
            ListOfAdmins.DisplayMemberPath = "Name";
            ListOfUsers.ItemsSource = User.users;
            ListOfUsers.DisplayMemberPath = "Name";
        }
        private void UpdateListboxes()
        {
            ListOfAdmins.Items.Refresh();
            ListOfUsers.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Add(new User(NameTextbox.Text, MailTextbox.Text));
            UpdateListboxes();
        }

        private void ListOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToAdminButton.IsEnabled = true;
            ChangeButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }
        private void ListOfAdmins_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ToUserButton.IsEnabled = true;
            ChangeButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Remove(ListOfUsers.SelectedItem as User);
            UpdateListboxes();
        }
    }
}
