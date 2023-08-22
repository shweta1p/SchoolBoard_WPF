
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
using System.Windows.Shapes;

namespace Kirin_2
{
	/// <summary>
	/// Interaction logic for PasswordChange.xaml
	/// </summary>
	public partial class PasswordChange : Window
	{
		string initialPassword = "";
		string username = "";
		bool isSuccess = false;

		public bool IsSuccess { get => isSuccess; set => isSuccess = value; }

		public PasswordChange(string username, string initialPassword)
		{
			InitializeComponent();
			SolidColorBrush greenBrush = new SolidColorBrush();
			greenBrush.Color = Colors.Green;
			lblError.Foreground = greenBrush;
			lblError.Content = "Please check your email to get the initial password!";
			this.initialPassword = initialPassword;
			this.username = username;
		}

		private void btnChangePassword_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (txtNewPassword.Password == "" || txtOldPassword.Password == "" || txtRetype.Password == "")
				{
					SolidColorBrush redBrush = new SolidColorBrush();
					redBrush.Color = Colors.Red;
					lblError.Foreground = redBrush;
					lblError.Content = "Please fill in all the field!";
				}
				else if (txtNewPassword.Password != txtRetype.Password)
				{
					SolidColorBrush redBrush = new SolidColorBrush();
					redBrush.Color = Colors.Red;
					lblError.Foreground = redBrush;
					lblError.Content = "Password and Confirm Password don't match. Please check!";
				}
				else
				{
					if (txtOldPassword.Password != this.initialPassword)
					{
						SolidColorBrush redBrush = new SolidColorBrush();
						redBrush.Color = Colors.Red;
						lblError.Foreground = redBrush;
						lblError.Content = "The initial password is invalid. Please check!";
					}
					else if (txtNewPassword.Password.Length < 8)
					{
						SolidColorBrush redBrush = new SolidColorBrush();
						redBrush.Color = Colors.Red;
						lblError.Foreground = redBrush;
						lblError.Content = "Password length is at least 8 character!";
					}
					else
					{
						using (SqlConnection cnn = new SqlConnection(Utils.GetConnectionStrings()))
						{
							cnn.Open();
							SqlCommand cmd = new SqlCommand();
							cmd.Connection = cnn;
							cmd.CommandText = "update [user] set password = @1 where username = @2";
							cmd.Parameters.AddWithValue("@1", Utils.EncryptString(txtNewPassword.Password));
							cmd.Parameters.AddWithValue("@2", username);
							cmd.ExecuteNonQuery();

							SolidColorBrush greenBrush = new SolidColorBrush();
							greenBrush.Color = Colors.Green;
							lblError.Foreground = greenBrush;
							lblError.Content = "Successfully change the initial password!";
							IsSuccess = true;
						}
					}
				}
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
