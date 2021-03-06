/*
Sniperkit-Bot
- Status: analyzed
*/

using System;
using MySql.Data.MySqlClient;

public class Example
{
	static void Main()
	{
		string cs = @"server=localhost;port=9306;userid=root;password=;database=testdb";
		MySqlConnection conn = null;
		try
		{
			conn = new MySqlConnection(cs);
			conn.Open();

			MySqlCommand cmd = new MySqlCommand("SELECT * FROM rt", conn);
			MySqlDataReader r;
			r = cmd.ExecuteReader();

			// dump columns to make sure if "id" is really System.UInt64
			DataTable schemaTable = r.GetSchemaTable();
			foreach (DataRow myField in schemaTable.Rows)
				Console.WriteLine(myField["ColumnName"] + " = " + myField["DataType"]);

			while(r.Read())
			{
				Console.WriteLine(
					r.GetString(0) + " | " +
					r.GetString(1) + " | " +
					r.GetString(2) + " | " +
					r.GetString(3));
			}
			r.Close();
			conn.Close();
		} catch (MySqlException ex)
		{
			Console.WriteLine("Error: {0}", ex.ToString());
		} finally
		{
			if (conn != null)
				conn.Close();
		}
	}
}
