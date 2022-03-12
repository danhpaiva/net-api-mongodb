using Api.Data.Collections;
using api_mongo_net31.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api_mongo_net31.Controllers
{
    public class InfectedController
    {
        [ApiController]
        [Route("[controller]")]
        public class InfectadoController : ControllerBase
        {
            Data.MongoDB _mongoDB;
            IMongoCollection<Infected> _infectadosCollection;

            public InfectadoController(Data.MongoDB mongoDB)
            {
                _mongoDB = mongoDB;
                _infectadosCollection = _mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
            }

            [HttpPost]
            public ActionResult SalvarInfectado([FromBody] InfectedDTO dto)
            {
                var infectado = new Infected(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

                _infectadosCollection.InsertOne(infectado);

                return StatusCode(201, "Infectado adicionado com sucesso");
            }

            [HttpGet]
            public ActionResult ObterInfectados()
            {
                var infectados = _infectadosCollection.Find(Builders<Infected>.Filter.Empty).ToList();

                return Ok(infectados);
            }
        }
    }
}
