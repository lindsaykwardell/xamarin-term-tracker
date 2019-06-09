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
    public partial class EditAssessmentModal : ContentPage
    {
        AssessmentsDataAccess assessments;
        Assessment assessment;

        public EditAssessmentModal(Assessment assessment, AssessmentsDataAccess assessments)
        {
            InitializeComponent();
            this.assessments = assessments;
            this.assessment = assessment;

            AssessmentNameEntry.Text = assessment.Name;
            DueDatePicker.Date = assessment.DueDate;
            AssessmentTypePicker.SelectedIndex = assessment.Type == AssessmentType.Objective ? 0 : 1;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            assessment.Name = AssessmentNameEntry.Text;
            assessment.Type = AssessmentTypePicker.SelectedItem.ToString() == "Objective" ? AssessmentType.Objective : AssessmentType.Performance;
            assessment.DueDate = DueDatePicker.Date;

            this.assessments.UpdateAssessment(assessment);
            await Navigation.PopModalAsync();
        }
    }
}