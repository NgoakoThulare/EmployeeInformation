using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeInformation.Services
{
    /// <summary>
    /// Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets employees.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetAll();

        /// <summary>
        /// Gets individual employee.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns></returns>
        Task<Employee> Get(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        Task<bool> Create(Employee employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="employee">Employee object.</param>
        /// <returns></returns>
        Task<bool> Update(Employee employee);

        /// <summary>
        /// Deletes employee.
        /// </summary>
        /// <param name="Id">Employee object.</param>
        /// <returns></returns>
        Task<bool> Delete(int Id);

        /// <summary>
        /// Check if employee exists.
        /// </summary>
        /// <param name="id">Employee object.</param>
        /// <returns></returns>
        Task<bool> Exists(int id);
    }
}
