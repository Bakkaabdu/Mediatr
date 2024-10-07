using anisTraining.Entities.Dtos.Responses;
using MediatR;

namespace anisTraining.Queries
{
    public class GetAllDriversQuery : IRequest<IEnumerable<GetDriverResponse>>
    {
    }
}
