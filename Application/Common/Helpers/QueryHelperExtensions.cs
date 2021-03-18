namespace Application.Common.Helpers
{
    using Domain.Entities;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public static class QueryHelperExtensions
    {

        public static async Task<Func<TSource, bool>> FilterExpression<TSource>(string filter)
        {

            //var filter1 = "album => album.Quantity > 0";
            var options = ScriptOptions.Default.AddReferences(typeof(TSource).Assembly);

            Func<TSource, bool> FilterExpression = await CSharpScript.EvaluateAsync<Func<TSource, bool>>(filter, options);

            return FilterExpression;


            //var discountedAlbums = albums.Where(FilterExpression);
            //hooray now we have discountedAlbums!


        }

    }
}
