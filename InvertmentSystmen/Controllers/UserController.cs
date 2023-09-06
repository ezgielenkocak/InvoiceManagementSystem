using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("adduser")]
        public IActionResult Add(UserAddMultipleDto userAddDto)
        {
            var result = _userService.Add(userAddDto);
            return Ok(result);
        }
        [HttpPost("update")]
        public IActionResult Update(UserUpdateDto userUpdateDto)
        {
            var result = _userService.Update(userUpdateDto);
            return Ok(result);
        }
        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _userService.GetList();
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            return Ok(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            return Ok(result);
        }

        [HttpPost("makepassive")]
        public IActionResult Passive(int id)
        {
            var result = _userService.MakePassiveUser(id);
            return Ok(result);
        }

    }
}
