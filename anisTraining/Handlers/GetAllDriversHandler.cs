using anisTraining.Entities.Dtos.Responses;
using anisTraining.Queries;
using anisTraining.Services.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace anisTraining.Handlers
{
    public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<GetDriverResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetAllDriversHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetDriverResponse>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.Drivers.All();

            return _mapper.Map<IEnumerable<GetDriverResponse>>(driver);
        }
    }
}
