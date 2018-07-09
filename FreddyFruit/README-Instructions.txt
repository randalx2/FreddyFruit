
***************************************************************************************************************************

           Freddy Fruits Online Store By Praneel Misthry

                       Set Up Instructions

***************************************************************************************************************************

This is an ASP.NET Web Forms project which was developed in Visual Studio.

Technologies used / required: C#, HTML, CSS, Javascript, SQL Server 2017 / SQL Express / SQL LocalDb, Entity Framework

Set up Guide:

1. Clone the repo from the Github link provided in the submission email

2. Open up the soloution in Visual Studio 2015 / 2017

3. Note that the database has been set to reset itself i.e clear all info upon each launch of the application.
   This helps with running the application on a different machine the first time. (No need to execute the mdf file)
   To change this feature (keep previous data), make the following changes:

   3.1 Update the "ProductDatabaseInitializer.cs" class in the Models folder such that it inherits from 
       "DropCreateDatabaseIfModelChanges<ProductContext>" instead. See the comments in this file.
        This will also allow the app to run quicker in future.

4. Make sure you have installed the frameworks necessary to run the app in the Visual Studio Environment.

5. It is also recommended that you have both Firefox and Chrome installed.

6. Build the application with Visual Studio.This may take some time to import Nuget packages etc.

7. Run the app for the first time by pressing CTRL + F5 after a successful build. The app should run in the browser you set.
   On first run, the app will be slower to start due to the database getting seeded with test data for the first time.

8. Navigate through the pages as per project requirements. Remember to use the UPDATE button on the ShoppingCart.aspx page
   in order to view the discounts as per project requirements.

*******************************************************************************************************************************