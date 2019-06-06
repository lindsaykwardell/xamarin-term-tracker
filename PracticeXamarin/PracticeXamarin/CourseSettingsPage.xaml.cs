using Plugin.LocalNotifications;
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
    public partial class CourseSettingsPage : ContentPage
    {
        Course course;
        CoursesDataAccess courses;

        public CourseSettingsPage(Course course, CoursesDataAccess courses)
        {
            InitializeComponent();
            this.course = course;
            this.courses = courses;
            NotificationSwitch.IsToggled = course.ShowNotifications;
        }

        private void NotificationSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                if (course.StartDate > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Upcoming Course", course.Name + " is starting tomorrow! Are you ready?", 100000 + course.Id, course.StartDate.AddDays(-1));
                }
                else if (course.EndDate > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Course Ending Soon", course.Name + " is ending next week! Are you ready?", 100000 + course.Id, course.EndDate.AddDays(-7));
                }
            } else
            {
                CrossLocalNotifications.Current.Cancel(10000 + course.Id);
            }
            course.ShowNotifications = e.Value;
            courses.UpdateCourse(course);
        }
    }
}