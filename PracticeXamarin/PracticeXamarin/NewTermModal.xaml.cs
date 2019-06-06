using Models;
using PracticeXamarin.Controllers;
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
    public partial class NewTermModal : ContentPage
    {
        TermsDataAccess terms;
        private string newTermName;
        private DateTime startDate = DateTime.Today;
        private DateTime endDate = DateTime.Today.AddMonths(6);

        public NewTermModal(TermsDataAccess terms)
        {
            InitializeComponent();
            this.terms = terms;
            StartDatePicker.Date = this.startDate;
            EndDatePicker.Date = this.endDate;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Term newTerm = new Term
            {
                TermName = this.newTermName,
                StartDate = this.startDate,
                EndDate = this.endDate
            };

            this.terms.AddNewTerm(newTerm);

            await Navigation.PopModalAsync();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.newTermName = e.NewTextValue;
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.startDate = e.NewDate;
            this.endDate = e.NewDate.AddMonths(6);
            EndDatePicker.Date = this.endDate;
        }

        private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.endDate = e.NewDate;
        }
    }
}