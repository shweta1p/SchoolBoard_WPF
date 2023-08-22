
using Kirin_2.ViewModel;
using Syncfusion.Windows.Controls.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Dictionary<string, string> users = new Dictionary<string, string>();
        public App app;
        public Login()
        {
            app = (App)Application.Current;
            InitializeComponent();

            //busyIndicator.AnimationType = AnimationTypes.Gear;
            //busyIndicator.Foreground = Brushes.Blue;
            //busyIndicator.Background = Brushes.Transparent;
            //busyIndicator.ViewboxHeight = 100;
            //busyIndicator.ViewboxWidth = 100;
            //busyIndicator.IsBusy = true;
            //busyIndicator.Visibility = Visibility.Visible;

            //BackgroundWorker worker = new BackgroundWorker();

            ////this is where the long running process should go
            //worker.DoWork += (o, ea) =>
            //{
            //    UserViewModel uvm = new UserViewModel();
            //    uvm.LoadSchoolDB("All");
            //    app.Properties["SchoolBoard"] = uvm.SCHOOLDB_ALL;
            //};

            //worker.RunWorkerCompleted += async (o, ea) =>
            //{
            //    await Task.Delay(1000);

            //    //work has completed. you can now interact with the UI
            //    busyIndicator.IsBusy = false;
            //    busyIndicator.Visibility = Visibility.Collapsed;
            //};
            //worker.RunWorkerAsync();

            sectionBottomID.Visibility = Visibility.Collapsed;
            btnSendPassword.Visibility = Visibility.Collapsed;
            loadUsers();
        }

        public void loadUsers()
        {
            using (SqlConnection cnn = new SqlConnection(Utils.GetConnectionStrings()))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "select username, COALESCE(password, '') from [user]";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    users.Clear();
                    while (dr.Read())
                    {
                        string key = dr.GetString(0);
                        string value = dr.GetString(1);
                        users.Add(key, value);
                    }
                }
                dr.Close();
            }
        }

        public bool checkEmailDomain(string email, out string domainCode)
        {
            bool ret = true;
            domainCode = "";
            string domain = email.Split('@')[1];
            using (SqlConnection cnn = new SqlConnection(Utils.GetConnectionStrings()))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = string.Format("select MD5 from Domain where name = '{0}'", domain);
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    lblError.Content = "Email has not been registered with the application!";
                    ret = false;
                }
                else
                {
                    if (dr.Read()) domainCode = dr.GetString(0);
                }
                dr.Close();
            }
            return ret;
        }

        public bool isAuthenticated;
        public async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "" || txtPassword.Password == "")
            {
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;
                lblError.Foreground = redBrush;
                lblError.Content = "Username or Password must not be empty!";
                isAuthenticated = false;
            }
            else
            {
                string encrytpPassword = Utils.EncryptString(txtPassword.Password);
                using (SqlConnection cnn = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandText = "select * from [user] where username = @username and password = @password";
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", encrytpPassword);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        SolidColorBrush greenBrush = new SolidColorBrush();
                        greenBrush.Color = Colors.Green;
                        lblError.Foreground = greenBrush;
                        lblError.Content = "Login successful!";
                        isAuthenticated = true;

                        app.Properties["USERNAME"] = txtUserName.Text.Trim();

                        ////await LoadSchoolDB();
                        //var w1 = Task.Run(() => LoadSchoolDBData());
                        //await Task.WhenAll(w1);

                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                    }
                    else
                    {
                        SolidColorBrush redBrush = new SolidColorBrush();
                        redBrush.Color = Colors.Red;
                        lblError.Foreground = redBrush;
                        lblError.Content = "Username or Password are wrong! Please check again!";
                        isAuthenticated = false;
                    }
                    dr.Close();
                }
            }
        }

        //public static async Task LoadSchoolDB()
        //{
        //    App app = (App)Application.Current;
        //    await Task.Run(() =>
        //        {
        //            UserViewModel uvm = new UserViewModel();
        //            uvm.LoadSchoolDB("All");
        //            app.Properties["SchoolBoard"] = uvm.SCHOOLDB_ALL;
        //        });
        //}

        //private void LoadSchoolDBData()
        //{
        //    UserViewModel uvm = new UserViewModel();
        //    uvm.LoadSchoolDB("All");
        //    app.Properties["SchoolBoard"] = uvm.SCHOOLDB_ALL;
        //}

        public void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblError.Content = "";
            sectionBottomID.Visibility = Visibility.Collapsed;
            btnSendPassword.Visibility = Visibility.Collapsed;
            if (!Utils.IsValidEmailAddress(txtUserName.Text))
            {
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;
                lblError.Foreground = redBrush;
                lblError.Content = "Email is invalid!";
            }
            else
            {
                if (users.ContainsKey(txtUserName.Text))
                {
                    if (users[txtUserName.Text].ToString() == "")
                    {
                        btnSendPassword.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        sectionBottomID.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    SolidColorBrush redBrush = new SolidColorBrush();
                    redBrush.Color = Colors.Red;
                    lblError.Foreground = redBrush;
                    lblError.Content = "Email has not been registered with the application!";
                }
            }
        }

        public string getRandomPassword(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }

        public void btnSendPassword_Click(object sender, RoutedEventArgs e)
        {
            string initialPassword = getRandomPassword(8);
            SolidColorBrush greenBrush = new SolidColorBrush();
            greenBrush.Color = Colors.Green;
            lblError.Foreground = greenBrush;
            lblError.Content = "";

            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = 587;
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("anh_tranquoc2@outlook.com", "6%namnhumoinamA");
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("anh_tranquoc2@outlook.com", txtUserName.Text, "Bounce Back Digital - Initial Password!", "Here is the initial password: " + initialPassword + "\n Please use it to change your password!");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                client.Send(mm);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

                this.Hide();
                PasswordChange changePassword = new PasswordChange(txtUserName.Text, initialPassword);
                changePassword.ShowDialog();

                if (changePassword.IsSuccess)
                {
                    sectionBottomID.Visibility = Visibility.Visible;
                    btnSendPassword.Visibility = Visibility.Collapsed;
                }
                else
                {
                    sectionBottomID.Visibility = Visibility.Collapsed;
                    btnSendPassword.Visibility = Visibility.Visible;
                }

                this.Show();
            }
            catch (Exception error)
            {
                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Colors.Red;
                lblError.Foreground = redBrush;
                lblError.Content = error.Message;
            }
        }
    }
}
