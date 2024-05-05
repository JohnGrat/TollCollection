using Business.Repositories;
using Data.Models;
using DataTransferContracts;
using FastEndpoints;

namespace Presentation.Endpoints.Toll.Add
{
    public class Handler(ITollRepository repo) : Endpoint<Request, ServiceResponse<TollPassage>>
    {
        public override void Configure()
        {
            Post("/api/v1/tollpassage");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            var added = await repo.AddAsync(request.RegistrationNumber, request.Timestamp, request.VehicleTypeName ?? throw new InvalidOperationException());
            await SendAsync(new ServiceResponse<TollPassage>()
            {
                Data = added,
                IsSuccess = added is not null,
                ErrorMessage = added is null ? "Failed to add toll" : string.Empty
            }, cancellation: cancellationToken);
        }
    }
}
