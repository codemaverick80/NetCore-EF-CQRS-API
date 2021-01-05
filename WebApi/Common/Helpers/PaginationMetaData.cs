using Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Common.Helpers
{
    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPageLink { get; set; }
        public string NextPageLink { get; set; }
    }

    public enum ResourceUriType
    {
        PreviousPage,
        NextPage
    }

    public static class PaginationMetaData
    {
        public static void CreatePaginationMetaData<T>(PagedList<T> result, PagingParameters pagingParameters, string routeName, IUrlHelper url, IHttpContextAccessor context)
        {
            
            //var previousPageLink = result.HasPrevious ? CreateGenresResourceUri(pagingParameters, ResourceUriType.PreviousPage,routeName,url) : null;
            //var nextPageLink = result.HasNext ? CreateGenresResourceUri(pagingParameters, ResourceUriType.NextPage, routeName,url) : null;
            var paginationMetadata = new PaginationMetadata()
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                PreviousPageLink = result.HasPrevious ? CreateGenresResourceUri(pagingParameters, ResourceUriType.PreviousPage, routeName, url) : null,
                NextPageLink = result.HasNext ? CreateGenresResourceUri(pagingParameters, ResourceUriType.NextPage, routeName, url) : null
            };            
            // return paginationMetadata;
            // TODO: Adding X-Pagination to resonse header
            context.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));           
        }

        private static string CreateGenresResourceUri(PagingParameters pagingParameters, ResourceUriType type,string routeName, IUrlHelper Url)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(routeName,
                      new
                      {
                          pageNumber = pagingParameters.PageNumber - 1,
                          pageSize = pagingParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return Url.Link(routeName,
                      new
                      {
                          pageNumber = pagingParameters.PageNumber + 1,
                          pageSize = pagingParameters.PageSize
                      });

                default:
                    return Url.Link(routeName,
                    new
                    {
                        pageNumber = pagingParameters.PageNumber,
                        pageSize = pagingParameters.PageSize
                    });
            }

        }
    }
}
