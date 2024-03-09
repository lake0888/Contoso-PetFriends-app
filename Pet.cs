namespace Model
{
    class Pet
    {
        public string ID { get; }
        public KindAnimal Specie { get; set; }
        public int Age { get; set; }

        public string? Characteristics { get; set; }

        public string? Personality { get; set; }

        public string? NickName { get; set; }

        public decimal Donation { get; set; }

        public Pet(string id, KindAnimal specie) : this(id, specie, 0, "", "", "", 45.00m) {}

        public Pet(string id, KindAnimal specie, int age, string characteristics, string personality,
                string nickName, decimal donation)
        {
            ID = id;
            Specie = specie;
            Age = age;
            Characteristics = characteristics;
            Personality = personality;
            NickName = nickName;
            Donation = donation;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Specie, Age, Characteristics, Personality, NickName, Donation);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || obj is not Pet) return false;
            Pet pet = (Pet)obj;
            return Specie == pet.Specie && Age == pet.Age && Characteristics == pet.Characteristics &&
            Personality == pet.Personality && NickName == pet.NickName && Donation == pet.Donation;
        }

        public override string ToString()
        {
            string data = "{" +
                                "\n\tID #: " + ID + "," +
                                "\n\tSpecies: " + Specie + "," +
                                "\n\tAge: " + (Age <= 0 ? "?" : Age) + "," +
                                "\n\tNickName: " + NickName + "," +
                                "\n\tPhysical description: " + Characteristics + "," +
                                "\n\tPersonality: " + Personality + "," +
                                $"\n\tDonation: {Donation:C2} \n" +
                            "}";
            return data;
        }
    }
}