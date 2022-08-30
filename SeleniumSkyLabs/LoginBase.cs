using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SeleniumTask
{
    public class LoginBase
    {
        protected IWebDriver driver;
        private String URL;
        private String UserName;
        private String Password;

        public LoginBase() //constructor.
        {
            // create driver for chrome browser.
            driver = new ChromeDriver();
            // getting data
            String[] data = FindInfoFromExcel();
            URL = data[2]; // URL
            UserName = data[0]; // UserName
            Password = data[1]; // Password
        }
        public void Init()
        {
            
            // Navigate to the site.

            driver.Url = URL;
            driver.Manage().Window.Maximize(); // Maximize the browser.

        }

        public void Cleanup()
        {
            driver.Quit();
        }
        
        // Function to extract data (User name & Password) from Excel sheet.
        public String[] FindInfoFromExcel()
        {
            try
            {
                
                String[] listB = new String[3];

                String user = Environment.UserName;
                String[] paths = { @"C:\Users\", user, "source","repos", "SeleniumSkyLabs","SeleniumSkyLabs","Information.csv" };
                String fullPath = Path.Combine(paths);
                
                using (var reader = new StreamReader(@fullPath)) 
                {
                    int counter = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (counter >= 0)
                        {
                            listB[0] = values[1]; //UserName
                            listB[1] = values[2]; //Password
                            listB[2] = values[0]; //URL
                        }
                        counter++;
                    }

                }
                return listB;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at: " + ex.Message);
                return null;
            }
        }
        public String GetUserName()
        {
            return UserName;
        }
        public String GetPassword()
        {
            return Password;
        }
    }

}
