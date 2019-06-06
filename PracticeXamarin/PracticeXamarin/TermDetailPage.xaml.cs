using Models;
using PracticeXamarin.Controllers;
using PracticeXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticeXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetailPage : ContentPage
    {
        private TermsDataAccess terms;
        private CoursesDataAccess courses = new CoursesDataAccess();
        private ObservableCollection<Course> coursesForTerm;
        private Term term;

        public TermDetailPage(Term term, TermsDataAccess terms)
        {
            InitializeComponent();
            BindingContext = term;
            this.term = term;
            this.terms = terms;

            courses.Courses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChangedMethod);

            this.coursesForTerm = new ObservableCollection<Course>();
            var list = courses.Courses.Where(c => c.TermId == this.term.Id).ToList();
            foreach(var course in list)
            {
                this.coursesForTerm.Add(course);
            }
            CourseListView.ItemsSource = this.coursesForTerm;
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.coursesForTerm.Clear();
            foreach(var course in this.courses.Courses)
            {
                if (course.TermId == this.term.Id)
                {
                    this.coursesForTerm.Add(course);
                }
            }
        }

        private async void EditTermButton_Activated(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTermModal(term, terms));
        }

        private async void DeleteTermButton_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete " + this.term.TermName, "Are you sure you want to delete " + this.term.TermName + "?", "Delete", "Cancel");
            if (response)
            {
                terms.DeleteTerm(term);
                await Navigation.PopAsync();
            }
        }

        private async void AddCourseButton_Activated(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewCourseModal(term, courses));
        }

        private async void CourseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var course = e.SelectedItem as Course;
            if (course != null)
            {
                await Navigation.PushAsync(new CourseMasterPage(course, courses));
                CourseListView.SelectedItem = null;
            }
        }
    }
}