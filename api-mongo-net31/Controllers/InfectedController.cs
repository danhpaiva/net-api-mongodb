using Api.Data.Collections;
using api_mongo_net31.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api_mongo_net31.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infected> _infectadosCollection;

        public InfectedController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpGet]
        public ActionResult GetInfected()
        {
            var infectados = _infectadosCollection.Find(Builders<Infected>.Filter.Empty).ToList();

            return Ok(infectados);
        }

        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedDTO dto)
        {
            var infectado = new Infected(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infected successfully added");
        }
    }
}

