using OpenQA.Selenium;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SeleniumTask
{
    public class LoginTest : LoginBase
    {

        public void LogIn()
        {
            try
            {
                // find the email and passwords elements.
                IWebElement elementEmail = driver.FindElement(By.Name("UserName"));
                IWebElement elementPass = driver.FindElement(By.Name("Password"));

                //get the username and password from excel and then send them to the site.
                String userName = FindInfoFromExcel("UserName");
                String password = FindInfoFromExcel("Password");
                elementEmail.SendKeys(userName);
                elementPass.SendKeys(password);

                //prefom login and my own timeout method.
                MyWaitElement();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void LogOut()
        {

            try
            {

                driver.Navigate().Back();

                IWebElement outBtn = driver.FindElement(By.CssSelector("#ExitAlertbutton0"));
                outBtn.Click();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            ExtractText(); // function to extract and write into a file.

        }

        private void ExtractText()
        {
            try
            {

                IWebElement element = driver.FindElement(By.CssSelector("i[class='ask ico-wb-help']")); // Finding the element.
                string text = element.GetAttribute("title"); // Getting the attribute.
                Console.WriteLine("This is the question mark popup: " + text);

                WriteToTextFile(text); // Calling the writing to file function.

            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /*
         * My own implementaion for timeout, since I can't use build-in wait functions
           I ran the click function on a different Thread, and then start the timer.
           If the time that elapsed is bigger than 10sec (10,000 millisec) then I throw exception.
         */
        private bool MyWaitElement()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start(); // start the clock.
            var task = Task.Run(() => run_Click()); // run the Click function on different Thread.
            int TimeInMillisecond = 10000;            
            try
            {
                while (sw.ElapsedMilliseconds < TimeInMillisecond) // If the time elapsed < 10sec than throw exception.
                {
                    if (task.IsCompleted) // task completed within time.
                    {
                        return task.Result;
                    }

                }
                throw new TimeoutException("Page taking longer than 10sec to load! ");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return task.Result;
            }
           
        }
        private bool run_Click()
        {
            try
            {
                driver.FindElement(By.Id("btnOkLogin")).Click(); // Click the login button.
                return true;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return false;
            }
        }


        //function to write the text into a file.
        public static void WriteToTextFile(string text)
        {
            try
            {

                StreamWriter sw = new StreamWriter(@"C:\Users\Tzach\source\repos\SkyLab_Sele\SkyLab_Sele\TextTest.txt");
                sw.WriteLine(text);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

    }
}
