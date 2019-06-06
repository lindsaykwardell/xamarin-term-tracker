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
    public partial class EditTermModal : ContentPage
    {
        TermsDataAccess terms;
        Term thisTerm;
        private string newTermName;
        private DateTime startDate = DateTime.Today;
        private DateTime endDate = DateTime.Today.AddMonths(6);

        public EditTermModal(Term thisTerm, TermsDataAccess terms)
        {
            InitializeComponent();
            this.thisTerm = thisTerm;
            this.terms = terms;
            this.newTermName = thisTerm.TermName;
            this.startDate = thisTerm.StartDate;
            this.endDate = thisTerm.EndDate;

            TermNameEntry.Text = this.newTermName; 
            StartDatePicker.Date = this.startDate;
            EndDatePicker.Date = this.endDate;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            this.thisTerm.TermName = this.newTermName;
            this.thisTerm.StartDate = this.startDate;
            this.thisTerm.EndDate = this.endDate;

            this.terms.UpdateTerm(this.thisTerm);

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