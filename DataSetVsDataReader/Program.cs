using System;
using System.Data;
using System.Data.SqlClient;
namespace DataSetVsDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connection.
            var objConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=GridViewDataBase;Integrated Security=True");
            objConnection.Open();

            //Command(SQL).
            var objCommand = new SqlCommand("select * from Department", objConnection);

            //Dataset and Datareader.
            var objAdapter = new SqlDataAdapter(objCommand);
            var objDataSet = new DataSet();

            objAdapter.Fill(objDataSet);//Adapter will fill the dataset object.

            SqlDataReader objReader = objCommand.ExecuteReader();

            //**********************Note***********************************

            //objConnection.Close();     //When I close the objConnection here DataSet will display the result because it's a Disconnected Architecture.
                                         //But when I will try ro read objReader it will throw the exeception(Additional information: Invalid attempt to call Read when reader is closed.)
                                         //Why because it's a Connected Architecture.

            foreach (DataRow row in objDataSet.Tables[0].Rows)
            {
                Console.WriteLine("Using DataSet.");
                Console.WriteLine("Department Id :" + row["Id"] + " " + "Department Name" + row["DepName"] + " " + row["Salary"]);
            }
            while (objReader.Read())
            {
                Console.WriteLine("Using SqlDataReader.");
                Console.WriteLine("Department Id :" + objReader["Id"] + " " + "Department Name" + objReader["DepName"] + " " + objReader["Salary"]);
            }
            Console.ReadLine();

            objConnection.Close();
        }
    }
}
