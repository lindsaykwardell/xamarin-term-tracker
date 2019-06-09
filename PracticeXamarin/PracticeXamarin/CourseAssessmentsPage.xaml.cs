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
    public partial class CourseAssessmentsPage : ContentPage
    {
        private Course course;
        private CoursesDataAccess courses;
        private AssessmentsDataAccess assessments = new AssessmentsDataAccess();
        private ObservableCollection<Assessment> assessmentsForCourse = new ObservableCollection<Assessment>();

        public CourseAssessmentsPage(Course course, CoursesDataAccess courses)
        {
            InitializeComponent();
            this.course = course;
            this.courses = courses;

            assessments.Assessments.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChangedMethod);

            this.PopulateAssessmentList();
            AssessmentListView.ItemsSource = assessmentsForCourse;
        }

        private void PopulateAssessmentList()
        {
            this.assessmentsForCourse.Clear();
            foreach (var assessment in assessments.Assessments)
            {
                if (assessment.CourseId == this.course.Id)
                {
                    this.assessmentsForCourse.Add(assessment);
                }
            }
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.PopulateAssessmentList();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewAssessmentModal(course, assessments));
        }

        private async void AssessmentListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var assessment = e.SelectedItem as Assessment;
            if (assessment != null)
            {
                await Navigation.PushAsync(new AssessmentDetailPage(assessment, assessments));
                AssessmentListView.SelectedItem = null;
                this.PopulateAssessmentList();
            }
        }
    }
}