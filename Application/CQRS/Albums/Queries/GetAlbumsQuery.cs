namespace Application.CQRS.Albums.Queries
{
    using Application.Common;
    using Application.Common.Interfaces;
    using Application.Common.Mappings;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public class GetAlbumsQuery : IRequest<PagedList<Album>>
    {
        public AlbumResourceParameters ResourceParameters { get; set; }

    }


    public class GetAlbumsQueryCommand : IRequestHandler<GetAlbumsQuery, PagedList<Album>>
    {

        private readonly IApplicationDbContext _context;

        public GetAlbumsQueryCommand(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Album>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Album as IQueryable<Album>;
            query = query.Select(a => new Album { Id = a.Id, AlbumName = a.AlbumName, Rating = a.Rating, Year = a.Year, Label = a.Label }).WhereEquals("Label", "MCA");
            var result = await PagedList<Album>.CreateAsync(query, request.ResourceParameters.PageNumber, request.ResourceParameters.PageSize);
            return result;
        }



    }



    public class GetAlbumsResponse : IMapFrom<Album>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Label { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, GetAlbumsResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AlbumName));
        }
    }


    public static class IQueryableExt
    {
        private sealed class holdPropertyValue<T>
        {
            public T v;
        }

        static async Task<Func<T, bool>> FilterExpressionGeneric<T>(string filterString)
        {
            try
            {
                var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
                Func<T, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<T, bool>>(filterString, options);
                return discountFilterExpression;
            }
            catch (Exception ex)
            {
                // Expression Evaluation Failed
                throw;
            }
        }

        public static IQueryable<T> WhereEquals<T, TValue>(this IQueryable<T> query, string propertyName, TValue propertyValue)
        {
            try
            {
                //// Entity Type (i.e. Employee, Album, User)
                var entityType = typeof(T);

                //// Parameter name :: e
                var parameter = Expression.Parameter(entityType, "e");

                //// Entity.PropertyName :: {e.propertyName}
                var property = Expression.PropertyOrField(parameter, propertyName);//throw exception if T does not have the matching PropertyOrFieldName


                var holdPropertyValue = new holdPropertyValue<TValue> { v = propertyValue };

                //// holdpv.v
                var value = Expression.PropertyOrField(Expression.Constant(holdPropertyValue), "v");

                //// e.{propertyName} == holdpv.v
                var whereBody = Expression.Equal(property, value);

                //// e => e.{propertyName} == holdpv.v
                var whereLambda = Expression.Lambda<Func<T, bool>>(whereBody, parameter);
                // var whereLambda = Expression.Lambda(whereBody, parameter);


                //// Queryable.Where(query, e => e.{propertyName} == holdpv.v)
                var whereCallExpression = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new[] { typeof(T) },
                    query.Expression,
                    whereLambda
                );

                //// query.Where(p => p.{propertyName} == holdpv.v)
                return query.Provider.CreateQuery<T>(whereCallExpression);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
