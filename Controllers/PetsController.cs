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
                .Include(pet => pet.petOwner);
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
            if (pet.name == null)
                return BadRequest("Must include a name for each pet");

            Console.WriteLine("*********** {0} *************", pet.petOwnerid);
            Console.WriteLine("****** {0} ********", pet.name);

            // Tell the DB context about our new pet object
            _context.Add(pet);

            // Find the owner of this pet from the petOwners table
            PetOwner owner = _context.PetOwners.Find(pet.petOwnerid);

            // Update the owner's pet count!
            owner.petCount += 1;

            // ...and save the pet object to the database
            _context.SaveChanges();

            // Respond back with the created bread object
            return CreatedAtAction(nameof(Post), new { id = pet.id }, pet);
        }

        [HttpPut("{id}")]
        public IActionResult basicUpdate(int id, Pet pet)
        {
            if (id != pet.id)
                return BadRequest();

            Pet existingPet = _context.Pets.Find(id);

            if(existingPet is null)
                return NotFound();

            _context.Pets.Remove(existingPet);    

            _context.Update(pet);
            _context.SaveChanges();

            return Ok(pet);
        }


        // PUT action
        // Updates pet check-in true / false
        // Then does date time
        // YOU WILL NEED TO HAVE SEPARATE CHECK IN AND CHECK OUT ROUTESx
        [HttpPut("{id}/checkin")]
        public IActionResult changeCheckedIn(int id)
        {
            // Ensure route parameter id and body id are the same
            // if (id != pet.id)
            //     return BadRequest();

            Pet existingPet = _context.Pets.Find(id);

            if(existingPet is null)
                return NotFound();

            // Flips checked in status to the opposite result
            existingPet.checkedIn = true;
            
            
            // Create a check in time
            var dateTime = DateTime.Now;
            Console.WriteLine(dateTime);
            existingPet.checkedInAt = dateTime;
            Console.WriteLine("TIME STAMP!!: {0}", existingPet.checkedInAt);

            _context.Update(existingPet);
            _context.SaveChanges();

            return Ok(existingPet);
        }

        [HttpPut("{id}/checkout")]
        public IActionResult changeCheckedOut(int id)
        {
            Pet existingPet = _context.Pets.Find(id);

            if(existingPet is null)
                return NotFound();

            // Flips checked in status to the opposite result
            existingPet.checkedIn = false;

            existingPet.checkedInAt = null;

            _context.Update(existingPet);
            _context.SaveChanges();

            return Ok(existingPet);
        }



        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        //     // find the pet by id
        //     Pet pet = _context.Pets.Find(id);

        //     // tell DB that we want to remove this pet
        //     _context.Pets.Remove(pet);

        //     // save changes to the DB
        //     _context.SaveChanges();;
        // }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // find the pet by id
            Pet pet = _context.Pets.Find(id);

            // tell DB that we want to remove this pet
            _context.Pets.Remove(pet);

            // Find the owner of this pet from the petOwners table
            PetOwner owner = _context.PetOwners.Find(pet.petOwnerid);

            // Update the owner's pet count!
            owner.petCount -= 1;

            // save changes to the DB
            _context.SaveChanges();;

            return NoContent();
        }
    }
}
