using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/Banks")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly BankContext bankContext;
        public BanksController(BankContext bankContext)
        {
            this.bankContext = bankContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> Get()
        {
            if (bankContext.Banks == null)
            {
                return NotFound();
            }
            return await bankContext.Banks.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            if(bankContext.Banks == null)
            {
                return NotFound();
            }
            var bank=await bankContext.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            else
            {
                return bank;
            }

        }

        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            if(bankContext.Banks==null)
            {
                return Problem("Entity is null");
            }
            bankContext.Banks.Add(bank);
            await bankContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBank), new { id = bank.Id }, bank);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id,Bank bank)
        {
            if(id!=bank.Id)
            {
                return BadRequest();
            }
            bankContext.Entry(bank).State= EntityState.Modified;
            try
            {
                await bankContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool BankExists(int id)
        {
            return (bankContext.Banks?.Any(e=>e.Id==id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            if (bankContext.Banks == null)
            {
                return NotFound();

            }
            var bank =await bankContext.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            bankContext.Banks.Remove(bank);
            await bankContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
