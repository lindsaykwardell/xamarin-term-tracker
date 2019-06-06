using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PracticeXamarin.Interfaces;
using Models;
using System;

namespace PracticeXamarin.Controllers
{
    public class TermsDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Term> Terms { get; set; }

        public TermsDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Term>();
            this.Terms = new ObservableCollection<Term>(database.Table<Term>());
        }

        public void AddNewTerm(Term newTerm)
        {
            this.Terms.
              Add(newTerm);
            this.SaveAllTerms();
        }

        public void UpdateTerm(Term term)
        {
            var foundTerm = this.Terms.FirstOrDefault(t => t.Id == term.Id);
            if (foundTerm != null)
            {
                foundTerm.TermName = term.TermName;
                foundTerm.StartDate = term.StartDate;
                foundTerm.EndDate = term.EndDate;
                this.SaveAllTerms();
            }
        }

        public void SaveAllTerms()
        {
            foreach(var term in this.Terms)
            {
                if(term.Id != 0)
                {
                    database.Update(term);
                }
                else
                {
                    database.Insert(term);
                }
            }
        }

        public void DeleteTerm(Term term)
        {
            var id = term.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Term>(id);
                }
            }
            this.Terms.Remove(term);
        }
    }
}
