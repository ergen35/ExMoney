using System;
using AutoMapper;
using ExMoney.Backend.Data;
using ExMoney.SharedLibs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using ExMoney.SharedLibs.DTOs;

namespace ExMoney.Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExMoneySettingsController: ControllerBase
    {
        private readonly BackendDbContext db;
        private readonly IMapper mapper;

        public ExMoneySettingsController(BackendDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ExMoneySettings> Get()
        {
            var settings = db.ExMoneySettings.OrderBy(s => s.Id).FirstOrDefault();
            if(settings is null)
                return NotFound();
            
            return settings;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ExMoneySettings>> Update(int id, JsonPatchDocument<ExMoneySettingsUpdateDTO> data)
        {
            var settings = await db.ExMoneySettings.FindAsync(id);
            if(settings is null)
                return BadRequest();

            var objectToPatch = mapper.Map<ExMoneySettingsUpdateDTO>(settings); 
            data.ApplyTo(objectToPatch);

            var updatedSettings = mapper.Map<ExMoneySettings>(objectToPatch);
            updatedSettings.Id = id;

            db.Update(updatedSettings);
            await db.SaveChangesAsync();

            return updatedSettings;
        }
    }
}