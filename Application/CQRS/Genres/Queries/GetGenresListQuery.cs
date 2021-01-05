//using Application;
//using Application.Common.Interfaces;
//using Application.Common.Mappings;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Domain.Entities;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Application.CQRS.Genres.Queries
//{

//    //#region "Query Request"
//    //public class GetGenresListQuery : IRequest<GenresListResponse>
//    //{
//    //}

//    //#endregion

//    //#region "Query Request Handler"
//    //public class GetGenresListQueryHandler : IRequestHandler<GetGenresListQuery, GenresListResponse>
//    //{
//    //    private readonly IApplicationDbContext context;
//    //    private readonly IMapper mapper;

//    //    public GetGenresListQueryHandler(IApplicationDbContext context, IMapper mapper)
//    //    {
//    //        this.context = context;
//    //        this.mapper = mapper;
//    //    }

//    //    public async Task<GenresListResponse> Handle(GetGenresListQuery request, CancellationToken cancellationToken)
//    //    {
//    //        var genres = await context.Genre
//    //            .ProjectTo<GenreListDto>(mapper.ConfigurationProvider)
//    //            .ToListAsync(cancellationToken);

//    //        var vm = new GenresListResponse
//    //        {
//    //            Genres = genres,
//    //            Count = genres.Count
//    //        };

//    //        return vm;
//    //    }
//    //}



//    //#endregion

//    //#region "Dto"
//    //public class GenreListDto : IMapFrom<Genre>
//    //{
//    //    public string Id { get; set; }
//    //    public string Name { get; set; }

//    //    public void Mapping(Profile profile)
//    //    {
//    //        profile.CreateMap<Genre, GenreListDto>()
//    //            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
//    //    }

//    //}


//    //#endregion

//    //#region "Query Response"
//    //public class GenresListResponse
//    //{
//    //    public List<GenreListDto> Genres { get; set; }
//    //    public int Count { get; set; }


//    //}





//    //#endregion




//    #region "Query Request"
//    public class GetGenresListQuery : IRequest<PagedList<GenreListDto>>
//    {
//        public PagingParameters PagingParameters { get; set; }

//    }

//    #endregion

//    #region "Query Request Handler"
//    public class GetGenresListQueryHandler : IRequestHandler<GetGenresListQuery, PagedList<GenreListDto>>
//    {
//        private readonly IApplicationDbContext context;
//        private readonly IMapper mapper;

//        public GetGenresListQueryHandler(IApplicationDbContext context, IMapper mapper)
//        {
//            this.context = context;
//            this.mapper = mapper;
//        }

//        public async Task<PagedList<GenreListDto>> Handle(GetGenresListQuery request, CancellationToken cancellationToken)
//        { 
//            var collection = context.Genre as IQueryable<Genre>;
//            var result = await PagedList<GenreListDto>.CreateAsync(collection, request.PagingParameters.PageNumber, request.PagingParameters.PageSize, mapper);         
//            return result;
//        }
//    }



//    #endregion

//    #region "Dto"
//    public class GenreListDto : IMapFrom<Genre>
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }

//        public void Mapping(Profile profile)
//        {
//            profile.CreateMap<Genre, GenreListDto>()
//                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.GenreName));
//        }

//    }


//    #endregion

//    #region "Query Response"
//    public class GenresListResponse
//    {
//        public List<GenreListDto> Genres { get; set; }
//        public int Count { get; set; }


//    }





//    #endregion
//}
