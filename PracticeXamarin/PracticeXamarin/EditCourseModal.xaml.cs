using Models;
using PracticeXamarin.Controllers;
using PracticeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourseModal : ContentPage
    {
        CoursesDataAccess courses;
        Course course;

        public EditCourseModal(Course course, CoursesDataAccess courses)
        {
            InitializeComponent();
            this.course = course;
            this.courses = courses;
            CourseNameEntry.Text = course.Name;
            StartDatePicker.Date = course.StartDate;
            EndDatePicker.Date = course.EndDate;
            InstructorNameEntry.Text = course.InstructorName;
            InstructorEmailEntry.Text = course.InstructorEmail;
            InstructorPhoneEntry.Text = course.InstructorPhone;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            course.Name = CourseNameEntry.Text;
            course.StartDate = StartDatePicker.Date;
            course.EndDate = EndDatePicker.Date;
            course.Status = Course.CourseStatus.Scheduled;
            course.InstructorName = InstructorNameEntry.Text;
            course.InstructorEmail = InstructorEmailEntry.Text;
            course.InstructorPhone = InstructorPhoneEntry.Text;

            this.courses.UpdateCourse(course);

            await Navigation.PopModalAsync();
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            EndDatePicker.Date = e.NewDate.AddMonths(1);
        }
    }
}