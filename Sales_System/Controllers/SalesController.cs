using Microsoft.AspNetCore.Mvc;
using SalesSystem.Sales.DTOs;
using SalesSystem.Core.Models;
using SalesSystem.Core.Services;
using AutoMapper;
using BookingSystem.Sales.Validations;

namespace SalesSystem.ApiSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _SaleService;
        private readonly IMapper _mapper;
        
        public SalesController(ISaleService SaleService, IMapper mapper)
        {
            this._mapper = mapper;
            this._SaleService = SaleService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ReadSalesDTO>>> GetAllSales()
        {
            var Sales = await _SaleService.GetAllSales();
            var SaleResults = _mapper.Map<IEnumerable<Sale>, IEnumerable<ReadSalesDTO>>(Sales);
            return Ok(SaleResults);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSalesDTO>> GetSaleById(int id)
        {
            try
            {
                var sales = await _SaleService.GetSaleById(id);
                var salesResult = _mapper.Map<Sale, ReadSalesDTO>(sales);
                if (salesResult == null)
                {
                    return Ok("{ \"code\": \"200\", \"message\": \"No data to show.\" }");
                }
                return Ok(salesResult);
            }
            catch (Exception ex)
            {
                return Ok("{ \"code\": \"400\", \"message\": \"Exception: " + ex.Message + "\" }");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadSalesDTO>> CreateSale([FromBody] SaveSalesDTO newsale)
        {
            var validationResult = await new SaveSaleResourceValidator().ValidateAsync(newsale);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var saleToCreate = _mapper.Map<SaveSalesDTO, Sale>(newsale);
            var newSale = await _SaleService.CreateSale(saleToCreate);
            var sale = await _SaleService.GetSaleById(newSale.SaleId);
            var saleResult = _mapper.Map<Sale, ReadSalesDTO>(sale);

            return CreatedAtAction(nameof(CreateSale), new { id = saleResult.SaleId }, saleResult);
        }

        [HttpPut]
        public async Task<ActionResult<ReadSalesDTO>> UpdateSale(SaveSalesDTO saveSale)
        {
            try
            {
                var validator = new SaveSaleResourceValidator();
                var validationResult = await validator.ValidateAsync(saveSale);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var saleToBeUpdate = await _SaleService.GetSaleById(saveSale.SaleId);

                if (saleToBeUpdate == null)
                    return Ok("{ 'code': '200', 'message': 'No data to update.' }");

                var sale = _mapper.Map<SaveSalesDTO, Sale>(saveSale);

                await _SaleService.UpdateSale(saleToBeUpdate, sale);

                var updatedSale = await _SaleService.GetSaleById(saleToBeUpdate.SaleId);
                var updatedsaleResult = _mapper.Map<Sale, ReadSalesDTO>(updatedSale);

                return Ok(updatedsaleResult);
            }
            catch (Exception ex)
            {
                return Ok("{ \"code\": \"400\", \"message\": \"Exception: " + ex.Message + "\" }");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                if (id == 0)
                    return Ok("{ 'code': '200', 'message': 'No data to process.' }");

                var sale = await _SaleService.GetSaleById(id);

                if (sale == null)
                    return Ok("{ 'code': '200', 'message': 'No data to delete.' }");

                await _SaleService.DeleteSale(sale);

                return Ok("{ 'code': '200', 'message': 'Data Deleted Successfully.' }");
            }
            catch (Exception ex)
            {
                return Ok("{ \"code\": \"400\", \"message\": \"Exception: " + ex.Message + "\" }");
            }
        }
    }
}