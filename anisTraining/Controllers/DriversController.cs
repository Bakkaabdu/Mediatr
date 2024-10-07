using anisTraining.Commands;
using anisTraining.Entities.DbSet;
using anisTraining.Entities.Dtos.Requests;
using anisTraining.Entities.Dtos.Responses;
using anisTraining.Handlers;
using anisTraining.Queries;
using anisTraining.Services.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anisTraining.Controllers
{

    public class DriversController : BaseController
    {


        public DriversController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        { 
        }


        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var query = new GetDriverQuery(driverId);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var command = new CreateDriverInfoRequest(driver);
            var result = _mediator.Send(command);

            return CreatedAtAction(nameof(GetDriver), new {driverId = result.Result.DriverId},result.Result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driver)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateDriverInfoRequest(driver);
            var result = await _mediator.Send(command);

            return result ? NoContent() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var query = new GetAllDriversQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var command = new DeleteDriverInfoRequest(driverId);
            var result = await _mediator.Send(command);



            return result ? NoContent() : BadRequest();
        }


    }
}
