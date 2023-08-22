using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Kirin_2.ViewModel
{
    public class FinancialViewModel /*: INotifyPropertyChanged*/
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;

        public FinancialViewModel()
        {
            string startupPath = Environment.CurrentDirectory;
            string excelFilePath = startupPath + ConfigurationManager.AppSettings.Get("excelPath") + ConfigurationManager.AppSettings.Get("excelFileName");
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
            Conn = new OleDbConnection(excelConnectionString);
        }

        /// <summary>  
        /// Method to Get All the Records from Excel  
        /// </summary>  
        /// <returns></returns>  
        public async Task<ObservableCollection<BUDGET_DESC>> ReadRecordFromEXCELAsync()
        {
            ObservableCollection<BUDGET_DESC> budgetList = new ObservableCollection<BUDGET_DESC>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                if (!string.IsNullOrWhiteSpace(Reader["Revenue"].ToString().Trim()) && !string.IsNullOrWhiteSpace(Reader["ApprovedBudget"].ToString().Trim()) &&
                    Reader["ApprovedBudget"].ToString().Trim() != "0" && Reader["ApprovedBudget"].ToString().Trim() != "$" && !Reader["Revenue"].ToString().StartsWith("Page") &&
                    !Reader["Revenue"].ToString().StartsWith("London District Catholic School Board") &&
                    !Reader["Revenue"].ToString().StartsWith("2019/2020 Approved Budget") && !Reader["Revenue"].ToString().StartsWith("Operating Fund")
                    ) 
                {
                    budgetList.Add(new BUDGET_DESC()
                    {
                        Revenue = Reader["Revenue"].ToString(),
                        ApprovedBudget = Reader["ApprovedBudget"].ToString() + "$",
                        isBold = Reader["Revenue"].ToString().StartsWith("Total") ? true : false
                    });
                }
                   
            }
            Reader.Close();
            Conn.Close();

            return budgetList;
        }


    }

    public class BUDGET_DESC
    {
        //public int ID { get; set; }
        public string Revenue { get; set; }
        public string ApprovedBudget { get; set; }
        public bool isBold { get; set; }
        
    }


}
