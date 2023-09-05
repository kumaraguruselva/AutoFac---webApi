using WebAPI_Demo.Models;
using WebAPI_Demo.Services;
using AutoMapper;
using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Demo.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;

        public static List<car> cars = new List<car>();
        private readonly Iservice _iservice;

        public CarController(Iservice iservice, IMapper mapper)
        {
            _iservice = iservice;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Addproduct(carModel car)
        {

            var t = _mapper.Map<car>(car);
            Random r = new Random();
            t.CarId = r.Next();

            if (ModelState.IsValid)
            {
                //products.Add(t);
                _iservice.InsertRecords(t);
                return CreatedAtAction("GetCar", new { t.CarId }, t);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetCar(int CarId)
        {
            var car = _iservice.GetAllRecords();
            var car1 = cars.FirstOrDefault(x => x.CarId == CarId);
            var car2 = _mapper.Map<carModel>(car1);
            if (car == null)
                return NotFound();
            return Ok(car2);
        }



    }

}
