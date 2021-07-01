using System;
using System.ComponentModel;
using PIMTool.Client.Dictionary;
namespace PIMTool.Services.Resource
{
    public class SaveProjectDto : INotifyPropertyChanged
    {

        private int _groupId;
        private int _projectNumber;
        private string _name;
        private string _customer;
        private EStatusType _status;
        private DateTime _startDate;
        private DateTime? _finishDate;
        private string _member;
        public string this[string columnName] => throw new NotImplementedException();

        public virtual int GroupId
        {
            get { return _groupId; }
            set 
            {
                _groupId = value;
                RaisePropertyChanged(nameof(GroupId));
            }
        }
        public virtual int ProjectNumber
        {
            get { return _projectNumber; }
            set
            {
                _projectNumber = value;
                RaisePropertyChanged(nameof(ProjectNumber));
            }
        }
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public virtual string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                RaisePropertyChanged(nameof(Customer));
            }
        }
        public virtual EStatusType Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        public virtual DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged(nameof(StartDate));
            }
        }
        public virtual DateTime? FinishDate
        {
            get { return _finishDate; }
            set
            {
                _finishDate = value;
                RaisePropertyChanged(nameof(FinishDate));
            }
        }
        public virtual string Member
        {
            get { return _member; }
            set
            {
                _member = value;
                RaisePropertyChanged(nameof(Member));
            }
        }
   
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}