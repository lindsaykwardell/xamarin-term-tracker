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
    public partial class CourseMasterPage : TabbedPage
    {
        private Course course;
        private CoursesDataAccess courses;
        public CourseMasterPage(Course course, CoursesDataAccess courses)
        {
            InitializeComponent();
            BindingContext = course;
            this.course = course;
            this.courses = courses;

            this.Children.Add(new CourseDetailPage(course, courses));
            this.Children.Add(new CourseAssessmentsPage(course, courses));
            this.Children.Add(new CourseSettingsPage(course, courses));
        }

        private async void EditCourseButton_Activated(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditCourseModal(course, courses));
        }

        private async void DeleteCourseButton_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete " + this.course.Name, "Are you sure you want to delete " + this.course.Name + "?", "Delete", "Cancel");
            if(response)
            { 
                courses.DeleteCourse(this.course);
                await Navigation.PopAsync();
            }
        }
    }
}