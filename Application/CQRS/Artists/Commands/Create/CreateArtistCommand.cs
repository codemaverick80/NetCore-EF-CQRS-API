using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Artists.Commands.Create
{
    public class CreateArtistCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string YearActive { get; set; }
        public string Biography { get; set; }
        //public ArtistBasicInfo ArtistBasicInfo { get; set; }

        public string Born { get; set; }
        public string Died { get; set; }
        public string AKA { get; set; }
    }


    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand,string>
    {

        private readonly IApplicationDbContext _context;

        public CreateArtistCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var entity = new Artist
            {
                Id = id,
                ArtistName = request.Name,
                Biography = request.Biography,
                YearActive = request.YearActive,
                ArtistBasicInfo=new ArtistBasicInfo
                {
                    ArtistId=id,
                    Born=request.Born,
                    Died=request.Died,
                    AlsoKnownAs=request.AKA
                }
            };

            _context.Artist.Add(entity);
             await _context.SaveChangesAsync(cancellationToken);
            return entity.Id.ToString();
        }
    }
}
