using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExMoney.SharedLibs;
using ExMoney.SharedLibs.DTOs;
using ExMoney.Backend.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class CurrenciesController: ControllerBase
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
        if(currency is null)
            return NotFound();
        return currency;
    }

    [HttpGet("list")]
    public Task<List<Currency>> List()
    {
        return db.Currencies.ToListAsync();
    }

    [HttpPost("add")]
    public Task<ActionResult<Currency>> Add(CurrencyCreateDTO data)
    {
        var currency = mapper.Map<Currency>(data);
        if(ModelState.)
    }
}