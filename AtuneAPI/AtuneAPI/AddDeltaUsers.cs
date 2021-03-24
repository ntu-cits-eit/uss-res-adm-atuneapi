using AtuneAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AtuneAPI
{
    public class AddDeltaUsers
    {
        public static void AddDeltaUsersDaily()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string sendEmail = string.Empty;
            PersonDB db = new PersonDB();
            DataTable dt = new DataTable();
            dt = db.getDeltaStaffList();

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
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                PersonWebServicePRO.ServiceUser userDetail = new PersonWebServicePRO.ServiceUser();
                                userDetail = pro_service.GetUser(Convert.ToString(row["login"]));

                                PersonWebServicePRO.ServiceUser user = new PersonWebServicePRO.ServiceUser();
                                if (userDetail != null)
                                {
                                    user.Location = userDetail.Location;
                                    user.Roles = userDetail.Roles;
                                }


                                user.Title = Convert.ToString(row["title"]);
                                user.FirstName = Convert.ToString(row["first_name"]);
                                user.LastName = Convert.ToString(row["last_name"]);
                                user.Login = Convert.ToString(row["login"]);
                                user.Email = Convert.ToString(row["email"]);
                                user.Department = Convert.ToString(row["department"]);
                                user.ExternalId1 = Convert.ToString(row["externalid1"]);
                                workerID = Convert.ToString(row["externalid1"]);


                                if (Convert.ToString(row["active"]) == "Y")
                                {
                                    user.IsLocked = false;
                                }
                                else
                                    user.IsLocked = true;


                                pro_service.SaveOrUpdateUser(user);
                            }
                            catch (Exception ex)
                            {
                                sendEmail = ErrorLogging(ex, workerID);
                            }
                        }

                        db.sendEmailDeltaLoadList();
                    }
                }


                else if (environemnt == "UAT")
                {
                    PersonWebServiceUAT.UserWebService uat_service = new PersonWebServiceUAT.UserWebService();
                    uat_service.Credentials = new NetworkCredential(userName, password, "staff");
                    uat_service.PreAuthenticate = true;

                    
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                PersonWebServiceUAT.ServiceUser userDetail = new PersonWebServiceUAT.ServiceUser();
                                userDetail = uat_service.GetUser(Convert.ToString(row["login"]));

                                // userDetail = uat_service.GetUser("joekhaung");

                                PersonWebServiceUAT.ServiceUser user = new PersonWebServiceUAT.ServiceUser();
                                if (userDetail != null)
                                {
                                    user.Location = userDetail.Location;
                                    user.Roles = userDetail.Roles;
                                   // user.IsLocked = userDetail.IsLocked.Value;

                                }

                                user.Title = Convert.ToString(row["title"]);
                                user.FirstName = Convert.ToString(row["first_name"]);
                                user.LastName = Convert.ToString(row["last_name"]);
                                user.Login = Convert.ToString(row["login"]);
                                user.Email = Convert.ToString(row["email"]);
                                user.Department = Convert.ToString(row["department"]);
                                user.ExternalId1 = Convert.ToString(row["externalid1"]);
                                workerID = Convert.ToString(row["externalid1"]);


                                if (Convert.ToString(row["active"]) == "Y")
                                {
                                    user.IsLocked = false;
                                }
                                else
                                    user.IsLocked = true;

                                uat_service.SaveOrUpdateUser(user);
                            }
                            catch (Exception ex)
                            {
                                sendEmail = ErrorLogging(ex, workerID);
                            }
                        }

                        db.sendEmailDeltaLoadList();
                    }

                }

                else //dev environment
                {
                    PersonWebService.UserWebService dev_service = new PersonWebService.UserWebService();
                    dev_service.Credentials = new NetworkCredential(userName, password, "staff");
                    dev_service.PreAuthenticate = true;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                PersonWebService.ServiceUser userDetail = new PersonWebService.ServiceUser();
                                userDetail = dev_service.GetUser(Convert.ToString(row["login"]));

                                PersonWebService.ServiceUser user = new PersonWebService.ServiceUser();

                                if (userDetail != null)
                                {
                                    user.Location = userDetail.Location;
                                    user.Roles = userDetail.Roles;
                                  //  user.IsLocked = userDetail.IsLocked.Value;
                                }

                                user.Title = Convert.ToString(row["title"]);
                                user.FirstName = Convert.ToString(row["first_name"]);
                                user.LastName = Convert.ToString(row["last_name"]);
                                user.Login = Convert.ToString(row["login"]);
                                user.Email = Convert.ToString(row["email"]);
                                user.Department = Convert.ToString(row["department"]);
                                user.ExternalId1 = Convert.ToString(row["externalid1"]);
                                workerID = Convert.ToString(row["externalid1"]);


                                if (Convert.ToString(row["active"]) == "Y")
                                {
                                    user.IsLocked = false;
                                }
                                else
                                    user.IsLocked = true;

                                dev_service.SaveOrUpdateUser(user);
                            }
                            catch (Exception ex)
                            {
                                sendEmail = ErrorLogging(ex, workerID);
                            }
                        }

                        db.sendEmailDeltaLoadList();
                    }


                }

                if (sendEmail == "Y")
                {
                    db.SendErrorEmail();
                }
            }
            catch (Exception ex)
            {
                sendEmail = ErrorLogging(ex, workerID);
                if (sendEmail == "Y")
                {
                    db.SendErrorEmail();
                }
            }

        }

        private static DataTable CreateDummyTable()
        {
            DataTable dtDummy = new DataTable();

            DataRow dr = null;
            dtDummy.Columns.Add("login", typeof(string));
            dtDummy.Columns.Add("title", typeof(string));
            dtDummy.Columns.Add("first_name", typeof(string));
            dtDummy.Columns.Add("last_name", typeof(string));
            dtDummy.Columns.Add("email", typeof(string));
            dtDummy.Columns.Add("department", typeof(string));
            dtDummy.Columns.Add("externalid1", typeof(string));
            dtDummy.Columns.Add("active", typeof(string));


            dr = dtDummy.NewRow();
            dr["login"] = "joe123";
            dr["title"] = "Mr";
            dr["first_name"] = "Test Joe";
            dr["last_name"] = "Test Last Name 1";
            dr["email"] = "testing123@ntu.edu.sg";
            dr["department"] = "test department";
            dr["externalid1"] = "zzzzzzzz";
            dr["active"] = "Y";
            dtDummy.Rows.Add(dr);

            return dtDummy;
        }


        //ref https://stackoverflow.com/questions/3282780/the-request-failed-with-http-status-401-unauthorized
        //ref https://www.aspsnippets.com/Articles/Call-Consume-Web-Service-ASMX-in-Console-application-using-C-and-VBNet.aspx


        public static string ErrorLogging(Exception ex, string workerID)
        {
            string emailRequiredToSend = "N";
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_hh");

            string get_file_rootpath = ConfigurationManager.AppSettings["ERROR_LOG_FILE_PATH"];
            string rootPath = @get_file_rootpath;

            string filePath = rootPath + "Atune_Error_Log_" + timeStamp + "HR.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();

            }
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Worker ID: " + workerID);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

                emailRequiredToSend = "Y";
            }

            return emailRequiredToSend;
        }
    }
    
}
