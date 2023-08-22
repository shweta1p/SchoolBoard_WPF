using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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



namespace LoginPage
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Dictionary<string, string> users = new Dictionary<string, string>();
		public MainWindow()
		{
			InitializeComponent();
			sectionBottomID.Visibility = Visibility.Collapsed;
			btnSendPassword.Visibility = Visibility.Collapsed;
			loadUsers();
		}

		private void loadUsers()
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

		private bool checkEmailDomain(string email, out string domainCode)
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

		private void btnSignIn_Click(object sender, RoutedEventArgs e)
		{
			if (txtUserName.Text == "" || txtPassword.Password == "")
			{
				SolidColorBrush redBrush = new SolidColorBrush();
				redBrush.Color = Colors.Red;
				lblError.Foreground = redBrush;
				lblError.Content = "Username or Password must not be empty!";
			
			} else
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
						lblError.Content = "Login is successfully!";
						//Kirin_2.MainWindow mw = new Kirin_2.MainWindow();
						//mw.Show();
						
					} else
					{
						SolidColorBrush redBrush = new SolidColorBrush();
						redBrush.Color = Colors.Red;
						lblError.Foreground = redBrush;
						lblError.Content = "Username or Password are wrong! Please check again!";
						
					}
					dr.Close();
				}
			}
			
		}

		private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
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
			} else
			{
				if (users.ContainsKey(txtUserName.Text))
				{
					if(users[txtUserName.Text].ToString() == "")
					{
						btnSendPassword.Visibility = Visibility.Visible;
					} else
					{
						sectionBottomID.Visibility = Visibility.Visible;
					}
				} else
				{
					SolidColorBrush redBrush = new SolidColorBrush();
					redBrush.Color = Colors.Red;
					lblError.Foreground = redBrush;
					lblError.Content = "Email has not been registered with the application!";
				}
			}
		}

		private string getRandomPassword(int length)
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

		private void btnSendPassword_Click(object sender, RoutedEventArgs e)
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
				PasswordChange changePassword = new PasswordChange(txtUserName.Text,initialPassword);
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
