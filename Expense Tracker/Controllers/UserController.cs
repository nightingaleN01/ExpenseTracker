using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expense_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this._unitOfWork.userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var users = await this._unitOfWork.userRepository.GetUserById(id);
            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser (UserEntity userEntity)
        {
            if(userEntity==null)
            { 
                return NotFound();
            }
            var users = await this._unitOfWork.userRepository.AddUser(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser (int id, UserEntity userEntity)
        {
            if (id==0 || userEntity ==null)
            {
                return NotFound();
            }
            var existingUser = await this._unitOfWork.userRepository.GetUserById(id);
            if(existingUser == null)
            {
                return BadRequest("User not found");
            }
            if (!(string.IsNullOrEmpty(userEntity.UserName))) existingUser.UserName = userEntity.UserName;
            if (!(string.IsNullOrEmpty(userEntity.Email))) existingUser.Email = userEntity.Email;
            if (!(string.IsNullOrEmpty(userEntity.Password))) existingUser.Password = userEntity.Password;
            var result = await this._unitOfWork.userRepository.UpdateUser(id, existingUser);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id==0)
            {
                return BadRequest("Invalid User");
            }
            var result = await this._unitOfWork.userRepository.DeleteUser(id);
            await _unitOfWork.SaveChangesAsync();   
            return Ok("User deleted successfully");
        }
    }
}
