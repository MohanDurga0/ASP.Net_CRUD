using AppCrud.Models;
using AppCrud.Repositories.Contract;
using System.Data;
using System.Data.SqlClient;

namespace AppCrud.Repositories.Implementation
{
    public class EmployeeRepository : IGenericRepository<Employee>
    {
        private readonly IConfiguration _configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Employee>> GetList()
        {
            List<Employee> _List = new List<Employee>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_EmployeeList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _List.Add(new Employee()
                        {
                            IdEmployee = Convert.ToInt32(dr["IdEmployee"]),
                            FullName = dr["FullName"].ToString(),
                            RefDepartment = new Department() { 
                                IdDepartment = Convert.ToInt32(dr["IdDepartment"]),
                                Name = dr["Name"].ToString(),
                            },
                            Salary = Convert.ToInt32(dr["Salary"]),
                            HireDate = dr["HireDate"].ToString()

                        });
                    }
                }
            }
            return _List;
        }

        public async Task<bool> Save(Employee entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_SaveEmployee", connection);
                cmd.Parameters.AddWithValue("FullName",entity.FullName);
                cmd.Parameters.AddWithValue("IdDepartment", entity.RefDepartment.IdDepartment);
                cmd.Parameters.AddWithValue("Salary", entity.Salary);
                cmd.Parameters.AddWithValue("HireDate", entity.HireDate);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
       
        public async Task<bool> Edit(Employee entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_EditEmployee", connection);
                cmd.Parameters.AddWithValue("IdEmployee", entity.IdEmployee);
                cmd.Parameters.AddWithValue("FullName", entity.FullName);
                cmd.Parameters.AddWithValue("IdDepartment", entity.RefDepartment.IdDepartment);
                cmd.Parameters.AddWithValue("Salary", entity.Salary);
                cmd.Parameters.AddWithValue("HireDate", entity.HireDate);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", connection);
                cmd.Parameters.AddWithValue("IdEmployee", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }


    }
}
