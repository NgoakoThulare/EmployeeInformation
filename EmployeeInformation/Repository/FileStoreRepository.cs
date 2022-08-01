using Microsoft.Extensions.Options;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeeInformation.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class FileStoreRepository : IRepository
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Configuration settings object.
        /// </summary>
        /// <param name="appSettings"></param>
        public FileStoreRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await LoadEmployeesFile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee(int id)
        {
            var employees = await LoadEmployeesFile();

            return employees.SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> AddEmployee(Employee employee)
        {
            if (await Exists(employee.Id))
                return false;

            var employees = await LoadEmployeesFile();
            var employeeList = employees.ToList();
            employeeList.Add(employee);
            return await Save(employeeList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var employees = await LoadEmployeesFile();
            var exisitingEmployee = employees.Single(e => e.Id == employee.Id);

            exisitingEmployee.Age = employee.Age;
            exisitingEmployee.BusinessRole = employee.BusinessRole;
            exisitingEmployee.Gender = employee.Gender;
            exisitingEmployee.Name = employee.Name;
            exisitingEmployee.Image = employee.Image;
            exisitingEmployee.ReportingLine = employee.ReportingLine;
            exisitingEmployee.Address = employee.Address;
            exisitingEmployee.Project = employee.Project;

            return await Save(employees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployee(int id)
        {
            var employees = await LoadEmployeesFile();
            var exisitingEmployee = employees.Single(e => e.Id == id);
            var employeeList = employees.ToList();
            employeeList.Remove(exisitingEmployee);
            return await Save(employeeList);
        }

        private async Task<bool> Save(IEnumerable<Employee> employees)
        {
            string path = string.Concat(_appSettings.FilePath, _appSettings.FileName, ".json");

            try
            {
                string newFileContents = JsonConvert.SerializeObject(employees);
                await File.WriteAllTextAsync(path, newFileContents);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<Employee>> LoadEmployeesFile()
        {
            IEnumerable<Employee> employees = new List<Employee>();
            string fileContents;
            try
            {
                fileContents = await File.ReadAllTextAsync(string.Concat(_appSettings.FilePath, _appSettings.FileName, ".json"));
            }
            catch (Exception)
            {
                throw;
            }

            if (!string.IsNullOrWhiteSpace(fileContents))
                return JsonConvert.DeserializeObject<List<Employee>>(fileContents);

            return employees;
        }

        private async Task<bool> Exists(int id)
        {
            var employees = await LoadEmployeesFile();

            return employees.Any(x => x.Id == id);
        }
    }
}
