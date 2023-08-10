using Application.Features.Brands.Commands.Update;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>
{
    public Guid Id { get; set; }


    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public DeleteBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

            await _brandRepository.DeleteAsync(brand);

            DeletedBrandResponse response = _mapper.Map<DeletedBrandResponse>(brand);
            return response;
        }
    }

}