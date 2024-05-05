using Business.Models;
using Business.Repositories;
using DataTransferContracts;
using FastEndpoints;

namespace Presentation.Endpoints.Toll.GetTollResults
{
    public class Handler(ITollRepository repo) : Endpoint<Request, ServiceResponse<IEnumerable<TollResult>>>
    {
        public override void Configure()
        {
            Get("/api/v1/tollresults");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
        {
            var products = await repo.GetAllAsync(request.From, request.To);
            await SendAsync(new ServiceResponse<IEnumerable<TollResult>>()
            {
                Data = products,
                IsSuccess = products is not null,
                ErrorMessage = products is null ? "No tolls found" : string.Empty
            }, cancellation: cancellationToken);
        }
    }
}
