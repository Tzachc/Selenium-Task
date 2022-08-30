using System.Threading;

namespace SeleniumTask
{
    class Program
    {
        
        static void Main(string[] args)
        {

            LoginTest newTest = new LoginTest();
            newTest.Init(); // calling the initiallize function from base class.

            newTest.LogIn(); // preform login.

            newTest.LogOut(); // preform logout + writing to file.

            newTest.Cleanup(); // close when done.
        }
        
    }
}
