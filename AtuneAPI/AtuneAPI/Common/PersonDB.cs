using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtuneAPI.Common
{
    class PersonDB
    {
        public DataTable getDeltaStaffList()
        {
            DataTable dt = new DataTable();
            try
            {
                string connetionString; OracleConnection cnn;
                //string text = File.ReadAllText(@"D:\BatchJob\AtuneAPI\Dev\WriteText.txt");
                // connetionString = ConfigurationManager.ConnectionStrings["CONN_STRING_FILE"].ToString();

                connetionString = getConnectionString();
                OracleCommand Cmd = null;
                cnn = new OracleConnection(connetionString);
                cnn.Open();
                Cmd = cnn.CreateCommand();
                Cmd.CommandText = "ATUNE_API_SERVICES.GET_DELTA_STAFF_LIST";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(DBUtils.CreateParameter("O_RESULT_SET"));
                //Cmd.Parameters.Add(DBUtils.CreateParameter(I_APPTEE_SUBMISSION_ID, formAppointeeClassData.AppteeSubmissionID));

                dt = DBUtils.CreateTable(Cmd.ExecuteReader());

                Cmd.Dispose();
                cnn.Close();
                return dt;
            }
            catch(Exception ex)
            {
                ErrorLogging(ex);
                return dt;
            }
           
        }

        public void sendEmailDeltaLoadList()
        {
            DataTable dt = new DataTable();
            try
            {
                string connetionString; OracleConnection cnn;
                //string text = File.ReadAllText(@"D:\BatchJob\AtuneAPI\Dev\WriteText.txt");
                // connetionString = ConfigurationManager.ConnectionStrings["CONN_STRING_FILE"].ToString();

                connetionString = getConnectionString();
                OracleCommand Cmd = null;
                cnn = new OracleConnection(connetionString);
                cnn.Open();
                Cmd = cnn.CreateCommand();
                Cmd.CommandText = "ATUNE_API_SERVICES.SEND_EMAIL_DELTA_LOAD";
                Cmd.CommandType = CommandType.StoredProcedure;
               // Cmd.Parameters.Add(DBUtils.CreateParameter("O_RESULT_SET"));
                //Cmd.Parameters.Add(DBUtils.CreateParameter(I_APPTEE_SUBMISSION_ID, formAppointeeClassData.AppteeSubmissionID));

               // dt = DBUtils.CreateTable(Cmd.ExecuteReader());

                Cmd.Dispose();
                cnn.Close();
                
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
                
            }

        }

        public void ErrorLogging(Exception ex)
        {
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
                sw.WriteLine("=============Database Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

               
            }

          
        }

        public void SendErrorEmail()
        {

            string connetionString; OracleConnection cnn;
            connetionString = getConnectionString();

            OracleCommand Cmd = null;
            cnn = new OracleConnection(connetionString);
            cnn.Open();
            Cmd = cnn.CreateCommand();
            Cmd.CommandText = "ATUNE_API_SERVICES.SEND_EMAIL_ERROR";
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.ExecuteNonQuery();
            Cmd.Dispose();
            cnn.Close();
            

        }

        private string getConnectionString()
        {
            string connectionString = string.Empty;
            connectionString = setConnectionString(ConfigurationManager.AppSettings["APP_ENVIRONMENT"].ToString());
            return connectionString;
        }

        private string setConnectionString(string env)
        {
            string connectionString = string.Empty;
            if (env == "PRO")
                connectionString = ConfigurationManager.ConnectionStrings["pro_constring"].ToString();
            else if (env == "UAT")
                connectionString = ConfigurationManager.ConnectionStrings["uat_constring"].ToString();
            else
                connectionString = ConfigurationManager.ConnectionStrings["dev_constring"].ToString();

            return connectionString;

        }

        
    }
}
