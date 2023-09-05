using System.Data.SqlClient;
using System.Data;
using WebAPI_Demo.Models;

namespace WebAPI_Demo.Services
{

        public class service : Iservice
        {
            private readonly IConfiguration _configuration;
            public service(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public void SendEmail()
            {
                Console.WriteLine($"SendEmail :Sending email is in process..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            public DataTable SyncData()
            {
                string connection = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                SqlConnection con = new SqlConnection(connection);
                var query = "SELECT * FROM CarDb";
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(ds);

                Console.WriteLine($"SyncData :sync is going on..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                return ds.Tables[0];
            }

            public void InsertRecords(car car)
            {
                try
                {
                    string connection = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                    var query = "INSERT INTO CarDb (CarId,CarName,CarPrice) VALUES(@CarId,@CarName,@CarPrice)";
                    SqlConnection con = new SqlConnection(connection);
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("id", car.CarId);
                        cmd.Parameters.AddWithValue("name", car.CarName);
                        cmd.Parameters.AddWithValue("price", car.CarPrice);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                }
                Console.WriteLine($"UpdatedDatabase :Updating the database is in process..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            public List<car> GetAllRecords()
            {
                DataTable cars = SyncData();
                return (from DataRow dr in cars.Rows
                        select new car()
                        {
                            CarId = Convert.ToInt32(dr["CarId"]),
                            CarName = Convert.ToString(dr["CarName"]),
                            CarPrice = Convert.ToInt32(dr["CarPrice"]),

                        }).ToList();


            }
        }
    }
