using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastDeepCloner.tests.Entitys.testClasses
{
    abstract class abUser
    {
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new Exception("Gelieve een correct email te geven");
                }
            }
        }
        public string Password { get; set; }

        public abUser()
        {

        }

        public abUser(string email, string password)
        {
            Email = email;
            Password = password;
        }

        private bool IsValidEmail(string email)
        {
            //check for explanation on: http://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
            string regexPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            return Regex.IsMatch(email, regexPattern);
        }
    }
}