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
    public partial class NewCourseModal : ContentPage
    {
        CoursesDataAccess courses;
        Term term;

        public NewCourseModal(Term term, CoursesDataAccess courses)
        {
            InitializeComponent();
            this.term = term;
            this.courses = courses;
            StartDatePicker.Date = DateTime.Today;
            EndDatePicker.Date = DateTime.Today.AddMonths(1);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Course newCourse = new Course
            {
                TermId = this.term.Id,
                Name = CourseNameEntry.Text,
                StartDate = StartDatePicker.Date,
                EndDate = EndDatePicker.Date,
                Status = Course.CourseStatus.Scheduled,
                InstructorName = InstructorNameEntry.Text,
                InstructorEmail = InstructorEmailEntry.Text,
                InstructorPhone = InstructorPhoneEntry.Text,
                Notes = "",
                ShowNotifications = true
            };

            this.courses.AddNewCourse(newCourse);

            await Navigation.PopModalAsync();
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            EndDatePicker.Date = e.NewDate.AddMonths(1);
        }
    }
}