using anisTraining.Entities.Dtos.Requests;
using anisTraining.Entities.Dtos.Responses;
using MediatR;

namespace anisTraining.Commands
{
    public class CreateDriverInfoRequest : IRequest<GetDriverResponse>
    {
        public CreateDriverRequest DriverRequest { get; }

        public CreateDriverInfoRequest(CreateDriverRequest driverRequest)
        {
            DriverRequest = driverRequest;
        }
    }
}
