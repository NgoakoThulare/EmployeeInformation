using EmployeeInformation.Repository;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeInformation.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repository;
        /// <summary>
        /// Configuration settings object.
        /// </summary>
        /// <param name="repository"></param>
        public EmployeeService(IRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        public async Task<bool> Create(Employee employee)
        {
            return await _repository.AddEmployee(employee);
        }

        /// <summary>
        /// Deletes employee.
        /// </summary>
        /// <param name="Id">Employee object.</param>
        /// <returns></returns>
        public async Task<bool> Delete(int Id)
        {
            return await _repository.DeleteEmployee(Id);
        }

        /// <summary>
        /// Check if employee exists.
        /// </summary>
        /// <param name="id">Employee object.</param>
        /// <returns></returns>
        public async Task<bool> Exists(int id)
        {
            var employee = await _repository.GetEmployee(id);
            return employee != null;
        }

        /// <summary>
        /// Gets individual employee.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns></returns>
        public async Task<Employee> Get(int id)
        {
            return await _repository.GetEmployee(id);
        }

        /// <summary>
        /// Gets employees.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _repository.GetAll();
        }

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        public async Task<bool> Update(Employee employee)
        {
            return await _repository.UpdateEmployee(employee);
        }
    }
}
