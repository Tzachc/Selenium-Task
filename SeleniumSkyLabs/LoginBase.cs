using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SeleniumTask
{
    public class LoginBase
    {
        protected IWebDriver driver;

        public LoginBase() //constructor.
        {
            // create driver for chrome browser.
            driver = new ChromeDriver();

        }
        public void Init()
        {
            // Navigate to the site.
            String urlFromExcel = FindInfoFromExcel("URL"); // Search for the URL in excel file.
            driver.Url = urlFromExcel;
            driver.Manage().Window.Maximize(); // Maximize the browser.

        }

        public void Cleanup()
        {
            driver.Quit();
        }

        // Function to extract data (User name & Password) from Excel sheet.
        public String FindInfoFromExcel(String info)
        {
            try
            {
                String[] listB = new String[3];
                using (var reader = new StreamReader(@"C:\Users\Tzach\source\repos\SkyLab_Sele\SkyLab_Sele\Information.csv"))
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
                if (info.Equals("UserName"))
                {
                    return listB[0];
                }
                else if (info.Equals("Password"))
                {
                    return listB[1];
                }
                else if (info.Equals("URL"))
                {
                    return listB[2];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at: " + ex.Message);
                return null;
            }
        }

    }
}
