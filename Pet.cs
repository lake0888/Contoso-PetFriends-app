internal class Pet
{
    private string ID;
    private KindAnimal specie;
    private int age;
    private string characteristics;
    private string personality;
    private string nickName;
    private decimal suggestedDonation;

    public Pet(string ID, KindAnimal specie) {
        this.ID = ID;
        this.specie = specie;
        this.age = 0;
        this.characteristics = string.Empty;
        this.personality = string.Empty;
        this.nickName = string.Empty;
        this.suggestedDonation = 0;
    }

    public Pet(string ID, KindAnimal specie, int age, string characteristics, 
                string personality, string nickName, decimal suggestedDonation)
    {
        this.ID = ID;
        this.specie = specie;
        this.age = age;
        this.characteristics = characteristics;
        this.personality = personality;
        this.nickName = nickName;
        this.suggestedDonation = suggestedDonation;
    }

    public string getId() => this.ID;
    public void setId(string ID) => this.ID = ID;

    public KindAnimal GetKindAnimal() => this.specie;
    public void setKindAnimal(KindAnimal specie) => this.specie = specie;
    public int getAge() => this.age;
    public void setAge(int age) => this.age = age;

    public string getCharacteristics() => this.characteristics;
    public void setCharacteristics(String characteristics) => this.characteristics = characteristics;

    public string getPersonality() => this.personality;
    public void setPersonality(string personality) => this.personality = personality;

    public string getNickName() => this.nickName;
    public void setNickName(string nickName) => this.nickName = nickName;

    public decimal getSuggestedDonation() => this.suggestedDonation;
    public void setSuggestedDonation(decimal suggestedDonation) => this.suggestedDonation = suggestedDonation;

    public override int GetHashCode()
    {
        return HashCode.Combine(ID, specie, age, characteristics, personality, nickName, suggestedDonation);
    }

    public override bool Equals(object? obj)
    {
        if (this == obj) return true;
        if (obj == null || obj is not Pet) return false;
        Pet pet = (Pet)obj;
        return specie == pet.specie && age == pet.age && characteristics == pet.characteristics && 
        personality == pet.personality && nickName == pet.nickName && suggestedDonation == pet.suggestedDonation;
    }

    public override string ToString()
    {
        string data =   "{" + 
                            "\n\tID #: " +  ID + "," +
                            "\n\tSpecies: " + specie + "," +
                            "\n\tAge: " + (age <= 0 ? "?" : age) + "," +
                            "\n\tNickName: " + nickName + "," +
                            "\n\tPhysical description: " + characteristics + "," +
                            "\n\tPersonality: " + personality + "," +
                            $"\n\tDonation: {suggestedDonation:C2} \n" +
                        "}";
        return data;
    }
}