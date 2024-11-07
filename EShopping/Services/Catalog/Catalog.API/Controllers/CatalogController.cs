using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        public IMediator _mediator { get; }
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BrandResponse>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<ProductTypeResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductTypeResponse>>> GetAllTypes()
        {
            var query = new GetAllProductTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("[action]/{productId}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string productId)
        {
            var query = new GetProductByIdQuery(productId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productBrand}", Name = "GetProductByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrandName(string productBrand)
        {
            var query = new GetProductByBrandQuery(productBrand);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            var result = await _mediator.Send<ProductResponse>(createProductCommand);
            return Ok(result);
        }


        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var result = await _mediator.Send(updateProductCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(string id)
        {
            var deleteProdCommand = new DeleteProductByIdCommand(id);
            var result = await _mediator.Send(deleteProdCommand);
            return Ok(result);
        }





    }
}
