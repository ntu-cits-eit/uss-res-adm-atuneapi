using AtuneAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AtuneAPI
{
    public class DownloadUsers
    {
        public static void DownloadExistingUsers()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string sendEmail = string.Empty;
            PersonDB db = new PersonDB();
            DataTable dt = new DataTable();
           // dt = db.getDeltaStaffList();

            //to bypass 12C connectivity by creating dummy table
            // dt = CreateDummyTable();

            string environemnt = ConfigurationManager.AppSettings["APP_ENVIRONMENT"];
            string userName = ConfigurationManager.AppSettings["SERVICE_ACCOUNT"];
            string password = ConfigurationManager.AppSettings["SERVICE_PASSWORD"];

            string workerID = string.Empty;
            try
            {
                //Change connection string at web.config
                if (environemnt == "PRO")
                {
                    PersonWebServicePRO.UserWebService pro_service = new PersonWebServicePRO.UserWebService();
                    pro_service.Credentials = new NetworkCredential(userName, password, "staff");
                    pro_service.PreAuthenticate = true;
                    CreateExcelFile();
                    try
                    {
                        int counter = 2;
                        foreach (PersonWebServicePRO.ServiceUser u in pro_service.GetUsers(new PersonWebServicePRO.ServiceUserFilter()))
                        {
                            AddNewRowsToExcelFilePRO(u, counter);
                            counter++;
                        }

                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("\nRecords Added successfully...");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    catch (Exception ex)
                    {

                    }
                }
               else if (environemnt == "UAT")
                {
                    PersonWebServiceUAT.UserWebService uat_service = new PersonWebServiceUAT.UserWebService();
                    uat_service.Credentials = new NetworkCredential(userName, password, "staff");
                    uat_service.PreAuthenticate = true;
                    CreateExcelFile();
                    try
                    {
                        int counter = 2;
                        foreach (PersonWebServiceUAT.ServiceUser u in uat_service.GetUsers(new PersonWebServiceUAT.ServiceUserFilter()))
                        {
                            AddNewRowsToExcelFileUAT(u, counter);
                            counter++;
                        }

                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("\nRecords Added successfully...");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //https://www.c-sharpcorner.com/article/create-update-delete-and-reading-the-excel-file-in-c-sharp/

        public static string filePath = @"D:\Atune\UserProfile\Atune_User_Profiles_With_Roles.xlsx";

        private static void CreateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("Excel is not installed in the system...");
                return;
            }

            object misValue = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            xlWorkSheet.Cells[1, 1] = "Title";
            xlWorkSheet.Cells[1, 2] = "First Name";
            xlWorkSheet.Cells[1, 3] = "Last Name";
            xlWorkSheet.Cells[1, 4] = "Network Account";
            xlWorkSheet.Cells[1, 5] = "Email";
            xlWorkSheet.Cells[1, 6] = "Assigned Roles";

            xlWorkBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

           
        }

        private static void AddNewRowsToExcelFileUAT(PersonWebServiceUAT.ServiceUser user, int row)
        {


            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range xlRange = xlWorkSheet.UsedRange;
           // int rowNumber = xlRange.Rows.Count + 1;

            xlWorkSheet.Cells[row, 1] = user.Title;
            xlWorkSheet.Cells[row, 2] = user.FirstName;
            xlWorkSheet.Cells[row, 3] = user.LastName;
            xlWorkSheet.Cells[row, 4] = user.Login;
            xlWorkSheet.Cells[row, 5] = user.Email;
            xlWorkSheet.Cells[row, 6] = user.Roles;

          

            // Disable file override confirmaton message  
            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
            xlWorkBook.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

           
        }

        private static void AddNewRowsToExcelFilePRO(PersonWebServicePRO.ServiceUser user, int row)
        {


            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.Range xlRange = xlWorkSheet.UsedRange;
            // int rowNumber = xlRange.Rows.Count + 1;

            xlWorkSheet.Cells[row, 1] = user.Title;
            xlWorkSheet.Cells[row, 2] = user.FirstName;
            xlWorkSheet.Cells[row, 3] = user.LastName;
            xlWorkSheet.Cells[row, 4] = user.Login;
            xlWorkSheet.Cells[row, 5] = user.Email;
            xlWorkSheet.Cells[row, 6] = user.Roles;



            // Disable file override confirmaton message  
            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
            xlWorkBook.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);


        }

        public class UserData
        {
            //person to active  or inactive
            // active = False; inactive = True
            public bool IsLocked;

            //staff position
            public string Title;

            //addressing staff e.g. Mr Ms Dr Professor, etc
            public string Saluation;

            //NTU network account
            public string Login;

            public string FirstName;
            public string LastName;
            public string Email;

            //NTU Home Unit
            public string Department;

            public string[] Role;
        }
    }
}
