using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v1/[controller]")]
public class CurrenciesController : ControllerBase
{
    private readonly BackendDbContext db;
    private readonly IMapper mapper;

    public CurrenciesController(BackendDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Currency>> Get(int id)
    {
        var currency = await db.Currencies.FindAsync(id);
        if (currency is null)
            return NotFound();
        return currency;
    }

    [HttpGet("list")]
    public Task<List<Currency>> List()
    {
        return db.Currencies.ToListAsync();
    }

    [HttpPost("add")]
    public async Task<ActionResult<Currency>> Add(CurrencyCreateDTO data)
    {
        var currency = mapper.Map<Currency>(data);

        try
        {
            await db.Currencies.AddAsync(currency);
            await db.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            return new ObjectResult(new ProblemDetails
            {
                Status = 500,
                Title = "Unknow error"
            });
        }

        return Created(nameof(Add), currency);
    }


    [HttpPut("update/{id:int}")]
    public async Task<ActionResult<Currency>> Add(int id, CurrencyCreateDTO data)
    {
        // var currency = mapper.Map<Currency>(data);
        var currency = await db.Currencies.FindAsync(id);
        if (currency is null)
            return NotFound();

        currency = mapper.Map(data, currency);

        try
        {
            db.Currencies.Update(currency);
            await db.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ObjectResult(new ProblemDetails
            {
                Status = 500,
                Title = "Unknowerror"
            });
        }

        return Accepted(currency);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var currency = await db.Currencies.FindAsync(id);
        if (currency is null)
            return NotFound();

        db.Remove(currency);
        await db.SaveChangesAsync();

        return NoContent();
    }

}