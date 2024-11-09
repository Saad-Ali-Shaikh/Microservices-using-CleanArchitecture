using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private IMediator mediator { get; }
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            _logger.LogInformation($"User: {userName}");
            _logger.LogDebug($"User: {userName}");
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var basket = await mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var deleteCommand= new DeleteBasketByUserNameCommand(createShoppingCartCommand.UserName);
            var basket = await mediator.Send(deleteCommand);
            return Ok(basket);
        }



    }
}
