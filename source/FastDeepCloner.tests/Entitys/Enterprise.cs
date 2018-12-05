using FastDeepCloner.tests.Entitys.testClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FastDeepCloner.tests.Entitys
{
    class Enterprise : abUser
    {
        public int EnterpriseID { get; set; }
        public string EnterpriseName { get; set; }
        public bool IsInDeKijker { get; set; }
        public string Type { get; set; }

        public ICollection<User> Promoties { get; set; } = new ObservableCollection<User>();
        public IList<OpeningHour> OpeningHours { get; set; } = new ObservableCollection<OpeningHour>();
        public ICollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

        public Address FirstAdress => Addresses.First();

        public Enterprise(string email, string password, string enterpriseName, List<Address> addresses) : base(email, password)
        {
            EnterpriseName = enterpriseName;
            Addresses = addresses;
            IsInDeKijker = false;
        }

        public Enterprise()
        {

        }

        public Enterprise Clone()
        {
            return FastDeepCloner.DeepCloner.Clone<Enterprise>(this);
        }
    }
}
