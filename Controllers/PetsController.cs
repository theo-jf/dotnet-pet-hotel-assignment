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

        // The `[HttpGet]` attribute defines this method as our `GET /api/pets` endpoint
        // This function returns a `IEnumerable<Pet>` object,
        // which is .NET's fancy way of saying "A list of pet objects"
        [HttpGet]
        public IEnumerable<Pet> GetPets() 
        {
            return _context.Pets
                // Include the `ownedBy` property
                // which is a list of `Pet` objects
                // .NET will do a JOIN for us!
                .Include(pet => pet.ownedBy);
        }

    // POST /api/pets
    // .NET automatically converts our JSON request body
    // into a `Pet` object. 
    [HttpPost]
    public IActionResult Post(Pet pet) 
    {
        if (pet.petName == null)
        return BadRequest("Must include a name for each pet");

        // Tell the DB context about our new pet object
        _context.Add(pet);
        // ...and save the pet object to the database
        _context.SaveChanges();

        // Respond back with the created pet object
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
            existingPet.checkedIn = !pet.checkedIn;
            _context.Update(existingPet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(changeChecked), new { id = pet.id }, pet);
        }
    }
}
