using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel.Models
{
    public enum PetBreedType {
                                Shepherd, 
                                Poodle, 
                                Beagle, 
                                Bulldog, 
                                Terrier, 
                                Boxer, 
                                Labrador, 
                                Retriever,
                                [Display(Name="German Shorthair Pointer")]
                                German_Shorthair_Pointer
                            }
    public enum PetColorType {
                                White, 
                                Black, 
                                Golden, 
                                Tricolor, 
                                Spotted
                            }
    public class Pet {
        public int id { get; set; }

        public string petName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetBreedType breed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PetColorType color { get; set; }

        public bool checkedIn { get; set; }

        public DateTime? checkedInTime { get; set; }

        [ForeignKey("ownedBy")]
        public int ownedById { get; set; }

        public PetOwner petOwner { get; set; }

    }
}
