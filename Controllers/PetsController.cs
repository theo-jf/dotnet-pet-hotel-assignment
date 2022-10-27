using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            return new List<Pet>();
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }


        // POST /api/pets
        // .NET automatically converts our JSON request body
        // into a `Pet` object. 
        [HttpPost]
        public IActionResult Post(Pet pet) 
        {
            if (pet.petName == null)
                return BadRequest("Must include a name for each pet");

            // Tell the DB context about our new bread object
            _context.Add(pet);
            // ...and save the bread object to the database
            _context.SaveChanges();

            // Respond back with the created bread object
            return CreatedAtAction(nameof(Post), new { id = pet.id }, pet);
        }

        // PATCH action
        // Updates pet check-in true / false
        // Then does date time
        [HttpPatch("{id}")]
        public IActionResult changeChecked(int id, Pet pet)
        {
            // Ensure route parameter id and body id are the same
            if (id != pet.id)
                return BadRequest();

            var existingPet = _context.Pets.Find(id);
            if(existingPet is null)
                return NotFound();

            // Flips checked in status to the opposite result
            existingPet.checkedIn = !existingPet.checkedIn;
            _context.Update(existingPet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(changeChecked), new { id = pet.id }, pet);
        }
    }
}
