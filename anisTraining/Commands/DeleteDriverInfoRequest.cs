using MediatR;

namespace anisTraining.Commands
{
    public class DeleteDriverInfoRequest : IRequest<bool>
    {
        public Guid DriverId { get;}

        public DeleteDriverInfoRequest(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}
