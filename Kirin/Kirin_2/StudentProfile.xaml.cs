using Kirin_2.ViewModel;
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
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for StudentProfile.xaml
    /// </summary>
    public partial class StudentProfile : Window
    {
        public StudentProfile(STUDENTDESC student)
        {
            InitializeComponent();
            ProfPic.ImageSource = student.PHOTO;
            this.Title = student.FULLNAME;
            FullName.Content = student.FULLNAME;
            Address.Content = student.ADDRESS;
            Gender.Content = student.GENDER;
            Ethnicity.Content = student.ETHNICITY;
            Language.Content = student.LANGUAGE;
            Citizenship.Content = student.CITIZENSHIP;
            GradeLevel.Content = student.CITIZENSHIP;
            Birthdate.Content = student.BIRTHDATE;


        }
    }
}
