using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private User currentUser = null;
        private User currentAdmin = null;
        string nameInput;
        string mailInput;
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
            AdminMailLabel.Content = $"Mail: ";
            UserMailLabel.Content = $"Mail: ";
        }
        private bool CheckIfNameAndMailValid(string name, string mail, string titel)
        {

            if (name == "" || mail == "")
            {
                MessageBox.Show("Name or mail is missing.", titel);
                return false;
            }
            else if (!mail.Contains("@"))
            {
                MessageBox.Show("Mail is not correct, use @.", titel);
                return false;
            }
            foreach (var user in User.users)
            {
                if (user.Name == name && user.MailAddress == mail)
                {
                    MessageBox.Show("User already exist.", titel);
                    return false;
                }
            }
            foreach (var admin in User.admins)
            {
                if (admin.Name == name && admin.MailAddress == mail)
                {
                    MessageBox.Show("User already exist.", titel);
                    return false;
                }
            }
            return true;
        }
        private void ListOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListOfUsers.SelectedItem != null)
            {
                MakeAdminButton.IsEnabled = true;
                MakeUserButton.IsEnabled = false;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;

                ListOfAdmins.UnselectAll();
                ClearTextboxes();
                currentAdmin = null;
                currentUser = (ListOfUsers.SelectedItem as User);
                NameTextbox.Text = currentUser.Name;
                MailTextbox.Text = currentUser.MailAddress;
                UserMailLabel.Content = $"Mail: {currentUser.MailAddress}";
            }
            else
            {
                MakeUserButton.IsEnabled = false;
                MakeAdminButton.IsEnabled = false;
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }
        private void ListOfAdmins_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfAdmins.SelectedItem != null)
            {
                MakeUserButton.IsEnabled = true;
                MakeAdminButton.IsEnabled = false;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;

                ListOfUsers.UnselectAll();
                ClearTextboxes();
                currentUser = null;
                currentAdmin = (ListOfAdmins.SelectedItem as User);
                NameTextbox.Text = currentAdmin.Name;
                MailTextbox.Text = currentAdmin.MailAddress;
                AdminMailLabel.Content = $"Mail: {currentAdmin.MailAddress}";
            }
            else
            {
                MakeUserButton.IsEnabled = false;
                MakeAdminButton.IsEnabled = false;
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            nameInput = NameTextbox.Text.Trim();
            mailInput = MailTextbox.Text.Replace(" ", "");
            if (!CheckIfNameAndMailValid(nameInput, mailInput, "Register user"))
            {
                return;
            }
            User.users.Add(new User(NameTextbox.Text, MailTextbox.Text));
            UpdateListboxes();
            ClearTextboxes();
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            nameInput = NameTextbox.Text.Trim();
            mailInput = MailTextbox.Text.Replace(" ", "");
            if(!CheckIfNameAndMailValid(nameInput, mailInput, "Change user info"))
            {
                return;
            }
            if(currentUser != null)
            {
                currentUser.Name = nameInput;
                currentUser.MailAddress = mailInput;
                UpdateListboxes();
                ClearTextboxes();
                ListOfUsers.UnselectAll();
            }
            else if (currentAdmin != null)
            {
                currentAdmin.Name = nameInput;
                currentAdmin.MailAddress = mailInput;
                UpdateListboxes();
                ClearTextboxes();
                ListOfAdmins.UnselectAll();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Remove(ListOfUsers.SelectedItem as User);
            User.admins.Remove(ListOfAdmins.SelectedItem as User);
            UpdateListboxes();
            ClearTextboxes();
        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            User.admins.Add(ListOfUsers.SelectedItem as User);
            User.users.RemoveAt(ListOfUsers.SelectedIndex);
            UpdateListboxes();
            ClearTextboxes();
            ListOfUsers.UnselectAll();
        }

        private void MakeUserButton_Click(object sender, RoutedEventArgs e)
        {
            User.users.Add(ListOfAdmins.SelectedItem as User);
            User.admins.RemoveAt(ListOfAdmins.SelectedIndex);
            UpdateListboxes();
            ClearTextboxes();
            ListOfAdmins.UnselectAll();
        }
    }
}
