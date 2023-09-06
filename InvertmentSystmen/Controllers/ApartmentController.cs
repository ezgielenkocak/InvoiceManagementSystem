using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Abstract.RabitMQ;
using InvoiceManagementSystem.Entity.Dtos.ApartmentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public ApartmentController(IApartmentService apartmentService, IRabitMQProducer rabitMQProducer)
        {
            _apartmentService = apartmentService;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpPost("addapartment")]
        public IActionResult Add(AddMultipleApartmentDto dto)
        {
            var result = _apartmentService.Add(dto);
            _rabitMQProducer.SendProductMessage(result);

            return Ok(result);
        }

        [HttpPost("deleteapartment")]
        public IActionResult Delete(int id, string token)
        {
            var result = _apartmentService.Delete(id, token);
            return Ok(result);
        }

        [HttpPost("updateapartment")]
        public IActionResult Update(ApartmentUpdateDto apartmentUpdate)
        {
            var result = _apartmentService.Update(apartmentUpdate);
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _apartmentService.GetById(id);
            return Ok(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _apartmentService.GetList();
            return Ok(result);
        }
    }
}
