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
        private void ClearTextboxes()
        {
            NameTextbox.Clear();
            MailTextbox.Clear();
        }
        private void UpdateLabels()
        {
            foreach (var user in User.users)
            {
                if (ListOfUsers.SelectedItem == user)
                {
                    UserMailLabel.Content = "Mail: " + user.MailAddress;
                }
                else if(ListOfUsers.SelectedItem == null)
                {
                    UserMailLabel.Content = "Mail: ";
                }
            }
            foreach (var admin in User.admins)
            {
                if (ListOfAdmins.SelectedItem == admin)
                {
                    AdminMailLabel.Content = "Mail: " + admin.MailAddress;
                }
                else if (ListOfAdmins.SelectedItem == null)
                {
                    AdminMailLabel.Content = "Mail: ";
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Add(new User(NameTextbox.Text, MailTextbox.Text));
            UpdateListboxes();
            UpdateLabels();
            ClearTextboxes();
        }
        private void ListOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeAdminButton.IsEnabled = true;
            MakeUserButton.IsEnabled = false;
            ChangeButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            UpdateLabels();
        }
        private void ListOfAdmins_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MakeUserButton.IsEnabled = true;
            MakeAdminButton.IsEnabled = false;
            ChangeButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            UpdateLabels();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in User.users)
            {
                if(ListOfUsers.SelectedItem == user)
                {
                    user.Name = NameTextbox.Text;
                    user.MailAddress = MailTextbox.Text;
                }
            }
            foreach (var userAdmin in User.admins)
            {
                if(ListOfAdmins.SelectedItem == userAdmin)
                {
                    userAdmin.Name = NameTextbox.Text;
                    userAdmin.MailAddress = MailTextbox.Text;
                }
            }
            UpdateListboxes();
            UpdateLabels();
            ClearTextboxes();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Remove(ListOfUsers.SelectedItem as User);
            User.admins.Remove(ListOfAdmins.SelectedItem as User);
            UpdateListboxes();
            UpdateLabels();
        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.admins.Add(ListOfUsers.SelectedItem as User);
            User.users.RemoveAt(ListOfUsers.SelectedIndex);
            UpdateListboxes();
            UpdateLabels();
        }

        private void MakeUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Add(ListOfAdmins.SelectedItem as User);
            User.admins.RemoveAt(ListOfAdmins.SelectedIndex);
            UpdateListboxes();
            UpdateLabels();
        }
    }
}
