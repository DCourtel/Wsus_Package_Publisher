using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Wsus_Package_Publisher
{
    internal sealed class SqlHelper
    {
        private static SqlHelper instance;
        private SqlConnection sqlConnection = new SqlConnection();

        private SqlHelper()
        {
            Logger.EnteringMethod();  
            ServerName = @"\\.\pipe\MSSQL$MICROSOFT##SSEE\sql\query";
            DataBaseName = "SUSDB";
        }

        internal static SqlHelper GetInstance()
        {
            Logger.EnteringMethod();  
            if (instance == null)
                instance = new SqlHelper();
            return instance;
        }

        internal string ServerName { get; set; }

        internal string DataBaseName { get; set; }

        internal bool Connect(string login, string password)
        {
            Logger.EnteringMethod();
            Logger.Write(ServerName + " : " + DataBaseName);
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                    sqlConnection.ConnectionString = string.Format("Server={0};Database={1};Integrated Security=sspi;User id={2};Password={3};", ServerName, DataBaseName, login, password);
                else
                    sqlConnection.ConnectionString = string.Format("Server={0};Database={1};Integrated Security=sspi;", ServerName, DataBaseName);
                sqlConnection.Open();
                Logger.Write("Connected to SQL !");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            Logger.Write("Not connected to SQL");
            return false;
        }

        internal void Disconnect()
        {
            Logger.EnteringMethod();  
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
            }
            catch (Exception) { }
        }

        internal void ShowUpdatesInConsole(List<Guid> updateIDs)
        {
            Logger.EnteringMethod();  
            QuerySql(updateIDs, 0);
        }

        internal void HideUpdatesInConsole(List<Guid> updateIDs)
        {
            Logger.EnteringMethod();  
            QuerySql(updateIDs, 1);
        }

        private void QuerySql(List<Guid> updateIDs, int value)
        {
            Logger.EnteringMethod();  
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = sqlConnection;
                        foreach (Guid updateID in updateIDs)
                        {
                            command.CommandText = "UPDATE [SUSDB].[dbo].[tbUpdate] SET [IsLocallyPublished] = " + value.ToString() + " WHERE [UpdateID] = '" + updateID.ToString() + "'";
                            Logger.Write(command.CommandText);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n" + command.CommandText);
                    }
                }
        }
    }
}
