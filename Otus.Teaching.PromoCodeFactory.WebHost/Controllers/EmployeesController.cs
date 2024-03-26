using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Employee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns>Данные всех сотрудников</returns>
        [HttpGet]
        public async Task<List<GetEmployeesModel>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x => 
                new GetEmployeesModel()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return employeesModelList;
        }
        
        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns>Данные сотрудника</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetEmployeeModel>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return NotFound();
            
            var employeeModel = new GetEmployeeModel()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles?.Select(x => new GetRoleModel()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromoCodesCount = employee.AppliedPromoCodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee">Данные сотрудника</param>
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(CreateEmployeeModel employee)
        {
            var newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                AppliedPromoCodesCount = 0,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Roles = new List<Role>(employee.Roles!)
            };

            await _employeeRepository.CreateAsync(newEmployee);
            return Ok();
        }

        /// <summary>
        /// Изменить данные сотрудника
        /// </summary>
        /// <param name="updatedEmployee">Обновленные данные сотрудника</param>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeModel updatedEmployee)
        {
            var employee = await _employeeRepository.GetByIdAsync(updatedEmployee.Id);
            if (employee is null)
                return NotFound();
            
            await _employeeRepository.UpdateAsync(new Employee()
            {
                Id = updatedEmployee.Id,
                FirstName = updatedEmployee.FirstName,
                LastName = updatedEmployee.LastName,
                Email = updatedEmployee.Email,
                AppliedPromoCodesCount = updatedEmployee.AppliedPromoCodesCount,
                Roles = new List<Role>(updatedEmployee.Roles!)
            });
            
            return Ok();
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveEmployeeAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return NotFound();
            
            await _employeeRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
