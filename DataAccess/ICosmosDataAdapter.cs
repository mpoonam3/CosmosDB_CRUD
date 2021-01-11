using CosmosDB_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB_CRUD.DataAccess
{
    public interface ICosmosDataAdapter
    {
        Task<dynamic> GetStudentDetails(string dbName, string name);
        Task<bool> AddNewStudent(string dbName, string name, StudentDetails userInfo);
        Task<StudentDetails> UpsertStudentDetails(string dbName, string name, StudentDetails details);
        Task<StudentDetails> DeleteStudentDetails(string dbName, string name, string id);
    }
}
