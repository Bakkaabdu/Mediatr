using anisTraining.Entities.Dtos.Requests;
using MediatR;

namespace anisTraining.Commands
{
    public class UpdateDriverInfoRequest : IRequest<bool>
    {
        public UpdateDriverRequest Driver {  get;}

        public UpdateDriverInfoRequest(UpdateDriverRequest driver)
        {
            Driver = driver;
        }

    }
}
