# Selenium C# Home Task
![image](https://www.testim.io/wp-content/uploads/2021/07/selenium_logo_large1.png) <br />
## About this task:
1. Perform a login flow using selenium driver, with the given credentials to site, data taking from excel.

2. Write a function that can validate that the site was loaded maximum 10 seconds after the login
to the site, in case it didn’t load in the given time, throw a proper exception. Use the function as
part of the login flow.
*don’t use built-in selenium wait functionality (such as: Timeouts().ImplicitWait).

3. Write another function that after login, write a flow that will logout from the site and extract the
text that popup after you hover on the question mark.
- save the text to an external file of your choice.

# Solution:
Based on OOP principles, we have 3 classes in the task:

## Program class : 
Main class to run the program from.

## LoginBase class : 
Base class that LoginTest will inheritance from, this class contain few basic methods:

 **Init** : Initiallize function for the web driver.
 
 **FindInfoFromExcel** : Function to search for URL,USERNAME,PASSWORD from excel file.
 
 **CleanUp** : Close everthing when done.

## LoginTest class : 

Inheritance from LoginBase and implement login and logout methods:

**LogIn** : getting the UserName and Password from excel and logging to the site.

**LogOut** : Log out from the site and extract the text from the question mark into a file.

**ExtractText** : Helper method to extract the data from question mark.

**MyWaitElement** : Method to check if the page load in 10 sec.

**run_Click** : Click login button helper method.

**WriteToTextFile** Helper method to write into file.

