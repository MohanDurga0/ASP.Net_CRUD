using AppCrud.Models;
using AppCrud.Repositories.Contract;
using System.Data;
using System.Data.SqlClient;

namespace AppCrud.Repositories.Implementation
{
    public class DepartmentRepository : IGenericRepository<Department>
    {

        private readonly IConfiguration _configuration;

        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Department>> GetList()
        {
            List<Department> _List = new List<Department>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))) {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListDepartment", connection);
                cmd.CommandType= CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync()) {
                    while (await dr.ReadAsync()) {
                        _List.Add(new Department()
                        {
                            IdDepartment = Convert.ToInt32(dr["IdDepartment"]),
                            Name = dr["Name"].ToString()

                        });
                    }
                }
            }
            return _List;
        }


        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(Department entity)
        {
            throw new NotImplementedException();
        }

    
        public Task<bool> Save(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
