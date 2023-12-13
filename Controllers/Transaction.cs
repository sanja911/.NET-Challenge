using System.Security.Cryptography;
using System.Text;
using DotnetWebApiWithEFCodeFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projects.Models;
using Projects.Services;
using Transaction.Detail.Services;
using Transaction.Services;

namespace Transaction.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    [Authorize]

    public class TransactionHeaderController : Controller
    {
        public TransactionHeaderService service;
        public TransactionDetailService detailService;

        public TransactionHeaderController(TransactionHeaderService _serviceHeader, TransactionDetailService _serviceDetail)
        {
            service = _serviceHeader;
            detailService = _serviceDetail;
        }

        [HttpPost("header")]
        public ActionResult<User> Create([FromBody] TransactionHeader transactionHeader)
        {
            var transactionHeaderData = service.Create(transactionHeader);
            return Json(transactionHeaderData);
        }

        [HttpPut("header/{id}")]
        public ActionResult Update(string id, [FromBody] TransactionHeader transactionHeader)
        {
            var existingTransactionHeader = service.GetTransactionHeader(id);
            if (existingTransactionHeader == null) {
                return Problem(
                    title: "Cannot find transaction header",
                    detail: "Cannot find transaction header with this id, use another valid id",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }

            var updatedData = service.Update(id, transactionHeader);
            return Json(updatedData);
        }

        [HttpGet("header/{id:length(24)}")]
        public ActionResult<TransactionHeader> GetTransactionHeader(string id)
        {
            var transactionData = service.GetTransactionHeader(id);
            return Json(transactionData);
        }

        [HttpGet("header")]
        public ActionResult<List<TransactionHeader>> GetTransactionHeaders()
        {
            return service.GetTransactionHeaders();
        }

        // Handle detail transaction
        [HttpPost("detail")]
        public ActionResult<User> CreateDetail([FromBody] TransactionDetail transactionDetail)
        {
            var existingTransactionHeader = service.GetTransactionHeader(transactionDetail.TransactionHeaderId);
            if (existingTransactionHeader == null) {
                return Problem(
                    title: "Cannot find transaction header with this TransactionHeaderId",
                    detail: "Cannot find transaction header with this TransactionHeaderId, use another valid TransactionHeaderId",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }

            transactionDetail.TransactionHeaderId = existingTransactionHeader.Id;
            var transactionData = detailService.Create(transactionDetail);
            return Json(transactionData);
        }

        [HttpPut("detail/{id}")]
        public ActionResult UpdateDetail(string id, [FromBody] TransactionDetail transactionDetail)
        {
            var existingTransactionHeader = service.GetTransactionHeader(transactionDetail.TransactionHeaderId);
            if (existingTransactionHeader == null) {
                return Problem(
                    title: "Cannot find transaction header with this TransactionHeaderId",
                    detail: "Cannot find transaction header with this TransactionHeaderId, use another valid TransactionHeaderId",
                    statusCode: StatusCodes.Status404NotFound
                    );
            }

            var transactionData = detailService.Update(id, transactionDetail);
            return Json(transactionDetail);
        }

        [HttpGet("detail/{id}")]
        public ActionResult GetTransactionDetail(string id, [FromBody] TransactionDetail transactionDetail)
        {
            var transactionData = detailService.GetTransactionDetail(id);
            return Json(transactionData);
        }

        [HttpGet("detail")]
        public ActionResult GetTransactionDetails()
        {
            var transactionData = detailService.GetTransactionDetails();
            return Json(transactionData);
        }
    }
    
}