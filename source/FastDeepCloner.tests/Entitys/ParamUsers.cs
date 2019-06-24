namespace FastDeepCloner.tests.Entitys
{
    public class ParamUsers
    {

        private string Test;

        private string Test2;

        private double Test3;

        public ParamUsers(string test, string test2, double test3)
        {
            Test = test;
            Test2 = test2;
            Test3 = test3;
        }


        public ParamUsers(string test, string test2)
        {
            Test = test;
            Test2 = test2;
        }
    }
}
