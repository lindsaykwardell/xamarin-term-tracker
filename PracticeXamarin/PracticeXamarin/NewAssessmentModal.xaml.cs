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
    public partial class NewAssessmentModal : ContentPage
    {
        AssessmentsDataAccess assessments;
        Course course;

        public NewAssessmentModal(Course course, AssessmentsDataAccess assessments)
        {
            InitializeComponent();
            this.assessments = assessments;
            this.course = course;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var newAssessment = new Assessment
            {
                CourseId = this.course.Id,
                Name = AssessmentNameEntry.Text,
                Type = AssessmentTypePicker.SelectedItem.ToString() == "Objective" ? AssessmentType.Objective : AssessmentType.Performance,
                DueDate = DueDatePicker.Date,
                ShowNotifications = true
            };

            this.assessments.AddNewAssessment(newAssessment);
            await Navigation.PopModalAsync();
        }
    }
}