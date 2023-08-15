using Application.Models.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelQuery:IRequest<GetListResponse<GetListByDynamicModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
    public bool WithDeleted { get; set; }


    public class GetListByDynamicModelQueryHandler : IRequestHandler<GetListByDynamicModelQuery, GetListResponse<GetListByDynamicModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListByDynamicModelQueryHandler(IMapper mapper, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
        }

        public async Task<GetListResponse<GetListByDynamicModelListItemDto>> Handle(GetListByDynamicModelQuery request, CancellationToken cancellationToken)
        {
            Paginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                dynamic:request.DynamicQuery,
                include: x =>
                    x.Include(x => x.Brand).
                        Include(x => x.Fuel).
                        Include(x => x.Transmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: request.WithDeleted,
                cancellationToken:cancellationToken

            );

            var response = _mapper.Map<GetListResponse<GetListByDynamicModelListItemDto>>(models);

            return response;
        }
    }
}