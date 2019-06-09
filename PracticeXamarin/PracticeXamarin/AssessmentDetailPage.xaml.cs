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
    public partial class AssessmentDetailPage : ContentPage
    {
        private AssessmentsDataAccess assessments;
        private Assessment assessment;

        public AssessmentDetailPage(Assessment assessment, AssessmentsDataAccess assessments)
        {
            InitializeComponent();
            this.assessment = assessment;
            this.assessments = assessments;

            BindingContext = this.assessment;

            NotificationSwitch.IsToggled = this.assessment.ShowNotifications;
        }

        private async void EditAssessmentButton_Activated(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditAssessmentModal(assessment, assessments));
            this.assessment = assessments.Assessments.FirstOrDefault(a => a.Id == assessment.Id);
        }

        private async void DeleteAssessmentButton_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete " + this.assessment.Name, "Are you sure you want to delete " + this.assessment.Name + "?", "Delete", "Cancel");
            if (response)
            {
                assessments.DeleteAssessment(this.assessment);
                await Navigation.PopAsync();
            }
        }

        private void NotificationSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                if (assessment.DueDate > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Upcoming Assessment", assessment.Name + " is due tomorrow! Are you ready?", 200000 + assessment.Id, assessment.DueDate.AddDays(-1));
                }
            }
            else
            {
                CrossLocalNotifications.Current.Cancel(20000 + assessment.Id);
            }

            this.assessment.ShowNotifications = e.Value;

            this.assessments.UpdateAssessment(this.assessment);
        }
    }
}