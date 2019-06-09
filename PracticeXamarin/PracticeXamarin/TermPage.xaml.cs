using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using PracticeXamarin.Controllers;
using Models;
using Plugin.LocalNotifications;

namespace PracticeXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        TermsDataAccess terms = new TermsDataAccess();
        CoursesDataAccess courses = new CoursesDataAccess();
        AssessmentsDataAccess assessments = new AssessmentsDataAccess();

        public TermPage()
        {
            InitializeComponent();

            TermListView.ItemsSource = terms.Terms;
            foreach(var term in terms.Terms)
            {
                if (term.StartDate > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Upcoming Term", term.TermName + " is starting tomorrow! Are you ready?", 10000 + term.Id, term.StartDate.AddDays(-1));
                } else if (term.EndDate > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Term Ending Soon", term.TermName + " is ending next week! Are you ready?", 10000 + term.Id, term.EndDate.AddDays(-7));
                }
            }
            foreach(var course in courses.Courses)
            {
                if (course.ShowNotifications)
                {
                    if (course.StartDate > DateTime.Now)
                    {
                        CrossLocalNotifications.Current.Show("Upcoming Course", course.Name + " is starting tomorrow! Are you ready?", 100000 + course.Id, course.StartDate.AddDays(-1));
                    }
                    else if (course.EndDate > DateTime.Now)
                    {
                        CrossLocalNotifications.Current.Show("Course Ending Soon", course.Name + " is ending next week! Are you ready?", 100000 + course.Id, course.EndDate.AddDays(-7));
                    }
                }
            }
            foreach(var assessment in assessments.Assessments)
            {
                if (assessment.ShowNotifications)
                {
                    if (assessment.DueDate > DateTime.Now)
                    {
                        CrossLocalNotifications.Current.Show("Upcoming Assessment", assessment.Name + " is due tomorrow! Are you ready?", 200000 + assessment.Id, assessment.DueDate.AddDays(-1));
                    }
                }
            }
            
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            //terms.AddNewTerm();
            await Navigation.PushModalAsync(new NewTermModal(terms));
        }

        private async void TermListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var term = e.SelectedItem as Term;
            if (term != null)
            {
                await Navigation.PushAsync(new TermDetailPage(term, terms));
                TermListView.SelectedItem = null;
            }
        }
    }
}