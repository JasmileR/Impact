using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly PaymentDetailContext _context;
        public PaymentController(PaymentDetailContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetPaymentDetails()
        {
            var data = _context.paymentDetails.ToList();
            return await _context.paymentDetails.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetailsById(int Id)
        {
            var payment = await _context.paymentDetails.FindAsync(Id);
            if (payment == null)
                return NotFound();
            return payment;
        }
        [HttpPost()]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails paymentDetails)
        {
            _context.paymentDetails.Add(paymentDetails);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetails", new { id = paymentDetails.PaymentId, paymentDetails });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDetails>> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.PaymentId)
                return BadRequest();

            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailsExits(id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetails>> DeletePaymentDetail(int id)
        {
            var payementDetails = await _context.paymentDetails.FindAsync(id);
            if (payementDetails == null)
                return NotFound();
            _context.paymentDetails.Remove(payementDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailsExits(int id)
        {
            return _context.paymentDetails.Any(p => p.PaymentId == id);
        }
    }
}
