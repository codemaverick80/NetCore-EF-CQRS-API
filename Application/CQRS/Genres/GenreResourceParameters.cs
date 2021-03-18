namespace Application.CQRS.Genres
{
	using Application.Common;
	public class GenreResourceParameters: PagingParameters
	{
		//public string MainCategory { get; set; }
		public string SearchQuery { get; set; }
	}


}
