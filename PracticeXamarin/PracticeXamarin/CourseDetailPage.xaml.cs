using PracticeXamarin.Controllers;
using PracticeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        private Course course;
        private CoursesDataAccess courses;

        public CourseDetailPage(Course course, CoursesDataAccess courses)
        {
            InitializeComponent();
            BindingContext = course;
            this.course = course;
            this.courses = courses;
            CourseNotesEditor.Text = course.Notes;
            course.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CourseUpdated);
            UpdateStatus();
        }

        private void CourseUpdated(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            switch (course.Status)
            {
                case Course.CourseStatus.PlanToTake:
                    StatusPicker.SelectedIndex = 0;
                    break;
                case Course.CourseStatus.Scheduled:
                    StatusPicker.SelectedIndex = 1;
                    break;
                case Course.CourseStatus.InProgress:
                    StatusPicker.SelectedIndex = 2;
                    break;
                case Course.CourseStatus.Completed:
                    StatusPicker.SelectedIndex = 3;
                    break;
                case Course.CourseStatus.Dropped:
                    StatusPicker.SelectedIndex = 4;
                    break;
            }
        }

        private void CourseNotesEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            course.Notes = e.NewTextValue;
            courses.UpdateCourse(course);
        }

        private async void ShareNotesButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = this.course.Notes,
                Title = "Share your notes for " + this.course.Name
            });
        }

        private void StatusPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (StatusPicker.SelectedIndex)
            {
                case 0:
                    course.Status = Course.CourseStatus.PlanToTake;
                    break;
                case 1:
                    course.Status = Course.CourseStatus.Scheduled;
                    break;
                case 2:
                    course.Status = Course.CourseStatus.InProgress;
                    break;
                case 3:
                    course.Status = Course.CourseStatus.Completed;
                    break;
                case 4:
                    course.Status = Course.CourseStatus.Dropped;
                    break;
            }
            courses.UpdateCourse(course);
        }
    }
}