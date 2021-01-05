using Application.Common;

namespace Application.CQRS.Genres
{
    public class GenreResourceParameters: PagingParameters
	{
		//public string MainCategory { get; set; }
		public string SearchQuery { get; set; }
	}


}
