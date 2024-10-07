using anisTraining.Entities.Dtos.Responses;
using MediatR;
namespace anisTraining.Handlers 
{ 
    public class GetDriverQuery : IRequest<GetDriverResponse> 
    {
        public Guid DriverId { get; }

        public GetDriverQuery(Guid driverId)
        {
            DriverId = driverId; 
        } 
    } 
}