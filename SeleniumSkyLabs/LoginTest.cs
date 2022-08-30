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
                String userName = GetUserName();
                String password = GetPassword();
                elementEmail.SendKeys(userName);
                elementPass.SendKeys(password);

                run_Click(); // click the login button.
                WaitTenSeconds(); // start 10sec validation.
                ValidateLoad(); // trying to grab element from the loaded page.
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
        
        //Validate function to check if web element exist after the login.
        private void ValidateLoad()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            IWebElement we;
            try
            {
                    we = driver.FindElement(By.CssSelector("div[class = 'item-content-wrap']")); // Menu
                    //we.Click();
                    
            }catch (NoSuchElementException ex)
            {
                Console.WriteLine("Page was not loaded in 10 seconds!: " + ex.Message);
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
        public void WriteToTextFile(string text)
        {
            try
            {
                String user = Environment.UserName;
                String[] paths = { @"C:\Users\", user, "source", "repos", "SeleniumSkyLabs", "SeleniumSkyLabs", "TextTest.txt" };
                String fullPath = Path.Combine(paths);
                StreamWriter sw = new StreamWriter(@fullPath);
                sw.WriteLine(text);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void WaitTenSeconds()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {

                if (sw.ElapsedMilliseconds == 10000) // if time elapsed equals 10 seconds.
                {

                    Console.WriteLine("we've waited 10 seconds!");
                    break;
                }
                else
                {

                    sw.Start();
                }
            }
        }
    }
}
