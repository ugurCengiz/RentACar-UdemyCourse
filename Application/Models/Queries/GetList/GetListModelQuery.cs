using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public bool WithDeleted { get; set; }


    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListModelQueryHandler(IMapper mapper, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            Paginate<Model> models = await _modelRepository.GetListAsync(
                 include: x =>
                     x.Include(x => x.Brand).
                         Include(x => x.Fuel).
                         Include(x => x.Transmission),
                 index: request.PageRequest.PageIndex,
                 size: request.PageRequest.PageSize,
                 withDeleted:request.WithDeleted,
                 cancellationToken:cancellationToken

             );

            var response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);

            return response;
        }
    }
}