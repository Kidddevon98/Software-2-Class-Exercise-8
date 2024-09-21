using System;

namespace PetStore.Models
{
    public class DogLeash : Product
    {
        public int LengthInches { get; set; }

        public string Material { get; set; } = string.Empty;  // Default to avoid null errors
    }
}
