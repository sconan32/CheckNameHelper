using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
namespace CheckNameHelper
{
    class ExcelStuListReader
    {
        string filename;
        public ExcelStuListReader(string filename)
        {
            this.filename = filename;
        }
        public IList<StuInfo> ReadStuList()
        {
            IList<StuInfo> stus = new List<StuInfo>(150);
            string cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filename+";Excel 8.0;HDR=Yes;IMEX=1";
            OleDbConnection cn = new OleDbConnection(cs);
            cn.Open();
            //历遍Sheet，显示每个Sheet名。
            DataTable ttable = cn.GetSchema("Tables");
            foreach (DataRow row in ttable.Rows)
                if (row["Table_Type"].ToString() == "TABLE")
                    Debug.Print(row["Table_Name"].ToString());
            //历遍某一sheet的所有行，所有列，显示到dataGridView控件。
            string ss = "select * from [page 1$]";
            OleDbCommand CMD = new OleDbCommand(ss, cn);
            OleDbDataReader RAD = CMD.ExecuteReader();
            for (int i = 0; i < 5; i++)
            {
                if (!RAD.Read())
                    break;
               
            }
            while (RAD.Read())
            {
                
                StuInfo stu = new StuInfo();
                stu.No =Convert.ToInt32( RAD[1]);
                stu.StuNo =Convert.ToInt32( RAD[2]);
                stu.Name = RAD[3].ToString() ;
                stus.Add(stu);
                
            }
            return stus;
            //DataTable tab = new DataTable();
            //for (int i = 0; i < RAD.FieldCount; i++)
            //    tab.Columns.Add(RAD.GetName(i), RAD.GetFieldType(i));
            //DataRow row1 = null;
            //while (RAD.Read())
            //{
            //    row1 = tab.NewRow();
            //    for (int i = 0; i < tab.Columns.Count; i++)
            //        row1[i] = RAD.GetValue(i);
            //    tab.Rows.Add(row1);
            //}
            //dataGridView1.DataSource =tab;
        }
    }
}
