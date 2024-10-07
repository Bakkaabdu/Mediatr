using anisTraining.Commands;
using anisTraining.Entities.DbSet;
using anisTraining.Entities.Dtos.Responses;
using anisTraining.Services.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace anisTraining.Handlers
{
    public class GetDriverInfoHandler : IRequestHandler<CreateDriverInfoRequest, GetDriverResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetDriverInfoHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetDriverResponse> Handle(CreateDriverInfoRequest request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.DriverRequest);

            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetDriverResponse>(driver);
        }
    }
}
