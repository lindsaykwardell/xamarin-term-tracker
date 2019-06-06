using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PracticeXamarin.Models
{
    [Table("Courses")]
    public class Course : INotifyPropertyChanged
    {
        public enum CourseStatus
        {
            PlanToTake,
            Scheduled,
            InProgress,
            Completed,
            Dropped,
        }

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _termId;
        public int TermId
        {
            get
            {
                return _termId;
            }
            set
            {
                this._termId = value;
                OnPropertyChanged(nameof(TermId));
            }
        }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                this._startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                this._endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        private CourseStatus _status;
        public CourseStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                this._status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private string _instructorName;
        public string InstructorName
        {
            get
            {
                return _instructorName;
            }
            set
            {
                this._instructorName = value;
                OnPropertyChanged(nameof(InstructorName));
            }
        }
        private string _instructorPhone;
        public string InstructorPhone
        {
            get
            {
                return _instructorPhone;
            }
            set
            {
                this._instructorPhone = value;
                OnPropertyChanged(nameof(InstructorPhone));
            }
        }
        private string _instructorEmail;
        public string InstructorEmail
        {
            get
            {
                return _instructorEmail;
            }
            set
            {
                this._instructorEmail = value;
                OnPropertyChanged(nameof(InstructorEmail));
            }
        }
        private string _notes;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                this._notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        private Boolean _showNotifications;
        public Boolean ShowNotifications
        {
            get
            {
                return _showNotifications;
            }
            set
            {
                this._showNotifications = value;
                OnPropertyChanged(nameof(ShowNotifications));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
