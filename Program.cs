internal class Program
{
    static int maxPets = 8;
    static Pet[] ourAnimals = new Pet[maxPets];
    static int lastIndexAnimal = 0;

    private static void Main(string[] args)
    {
        initPetList(); //INIT ANIMAL'S ARRAY
        showMenu();
    }

    private static void showMenu()
    {
        string? menuSelection;
        int option = 0;
        do
        {
            Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
            Console.WriteLine("1 - List all of our current pet information");
            Console.WriteLine("2 - Assign values to the ourAnimals array fields");
            Console.WriteLine("3 - Ensure animal ages and physical descriptions are complete");
            Console.WriteLine("4 - Ensure animal nicknames and personality descriptions are complete");
            Console.WriteLine("5 - Display all cats with a specified characteristic");
            Console.WriteLine("6 - Display all dogs with a specified characteristic\n");
            Console.WriteLine("Enter menu item selection or type \"Exit\" to exit the program");

            menuSelection = Console.ReadLine();

            if (menuSelection != null && menuSelection.ToLower() != "exit")
            {
                try
                {
                    option = Int32.Parse(menuSelection);
                    if (option < 1 || option > 6)
                    {
                        Console.WriteLine("Choose an option between 1 and 8.");
                    }
                    else
                    {
                        switch (option)
                        {
                            case 1:
                                listAllAnimals();
                                break;
                            case 2:
                                confirmAddPet();
                                break;
                            case 3:
                                ensureAnimalAgeAndCharacteristics();
                                break;
                            case 4:
                                ensureNickNameAndPersonality();
                                break;
                            case 5:
                                showPetsByKindAndCharacteristics(KindAnimal.CAT);
                                break;
                            case 6:
                                showPetsByKindAndCharacteristics(KindAnimal.DOG);
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Choose an option between 1 and 8.");
                }
            }

        } while (!menuSelection.Equals("exit", StringComparison.OrdinalIgnoreCase));
    }

    private static void listAllAnimals()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (!pet.getId().Equals(""))
            {
                Console.WriteLine(pet);
            }
        }
    }

    private static KindAnimal chooseAnimal()
    {
        string? input;
        int option = 0;
        bool validAnimal = false;
        do
        {
            Console.WriteLine("Choose the animal:");
            Console.WriteLine("1 - Dog\n2 - Cat");

            input = Console.ReadLine();

            if (input != null)
            {
                try
                {
                    option = Int32.Parse(input);

                    if (option == 1 || option == 2)
                    {
                        validAnimal = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid number (1 or 2).");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a valid number (1 or 2).");
                }
            }
        } while (validAnimal == false);
        return option == 1 ? KindAnimal.DOG : KindAnimal.CAT;
    }

    private static void initPetList()
    {
        int i = 0;
        while (i < ourAnimals.Length)
        {
            ourAnimals[i] = i switch
            {
                0 => new Pet("d1", KindAnimal.DOG, 2,
                "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.",
                "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.",
                "lola", 85.00m),
                1 => new Pet("d2", KindAnimal.DOG, 9,
                "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.",
                "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.",
                "loki", 49.99m),
                2 => new Pet("c3", KindAnimal.CAT, 1,
                "small white female weighing about 8 pounds. litter box trained.",
                "friendly",
                "Puss", 40.00m),
                3 => new Pet("c4", KindAnimal.CAT, 0, "", "", "", 45.00m),
                _ => new Pet("", KindAnimal.UNKNOW, 0, "", "", "", 45.00m),
            };

            if (i < 4)
            {
                lastIndexAnimal++;
            }
            i++;
        }
    }

    private static void confirmAddPet()
    {
        Console.WriteLine($"We currently have {lastIndexAnimal} pets that need homes. We can manage {(maxPets - lastIndexAnimal)} more.");
        string option = "y";
        while (option == "y" && lastIndexAnimal < maxPets)
        {
            Console.WriteLine("Do you want to enter info for another pet (y/n)");

            string? readResult;
            do
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    option = readResult.ToLower();
                    if (option.Equals("y"))
                    {
                        addAnimal();
                        lastIndexAnimal++;
                    }
                }
            } while (option != "y" && option != "n");
        }

        if (lastIndexAnimal >= maxPets)
        {
            Console.WriteLine("We have reached our limit on the number of pets that we can manage.");
        }
    }

    private static void addAnimal()
    {
        KindAnimal specie = chooseAnimal();
        string ID = ((specie == KindAnimal.DOG) ? "d" : "c") + (lastIndexAnimal + 1);

        Pet pet = new Pet(ID, specie);
        //ADD AGE   
        addAge(pet, "Enter the pet's age or enter 0 if unknown");
        //ADD CHARACTERISTICS
        addCharacteristics(pet, "Enter a physical description of the pet (size, color, gender, weight, housebroken)");
        //ADD PERSONALITY
        addPersonality(pet, "Enter a description of the pet's personality (likes or dislikes, tricks, energy level)");
        //ADD NICKNAME
        addNickName(pet, "Enter a nickname for the pet");
        //ADD DONATION
        addDonation(pet, "Suggested donation");
        ourAnimals[lastIndexAnimal] = pet;
    }

    private static void addAge(Pet pet, string title)
    {
        Console.WriteLine(title);
        string? readResult;
        int age = -1;
        do
        {
            readResult = Console.ReadLine();
            if (readResult != null)
            {
                try
                {
                    age = Int32.Parse(readResult);
                }
                catch (FormatException)
                {
                    continue;
                }
            }
        } while (age == -1);

        pet.setAge(age);
    }
    private static void addCharacteristics(Pet pet, string title)
    {
        pet.setCharacteristics(getValidData(title));
    }

    private static void addPersonality(Pet pet, string title)
    {
        pet.setPersonality(getValidData(title));
    }

    private static void addNickName(Pet pet, string title)
    {
        pet.setNickName(getValidData(title));
    }

    private static string getValidData(string title)
    {
        Console.WriteLine($"{title}");
        string? readResult;
        string data = "";
        do
        {
            readResult = Console.ReadLine();
            if (readResult != null)
            {
                data = readResult.ToLower();
                if (data == "")
                {
                    data = "tbd";
                }
            }
        } while (data == "");
        return data;
    }

    private static void addDonation(Pet pet, string title)
    {
        Console.WriteLine($"{title}\n");
        string? readResult;
        int option = 0;
        decimal donation = 0;
        do
        {
            Console.WriteLine("1 - 85.00");
            Console.WriteLine("2 - 49.99");
            Console.WriteLine("3 - 40.00");
            Console.WriteLine("4 - 00.00");
            readResult = Console.ReadLine();
            if (readResult != null)
            {
                if (Int32.TryParse(readResult, out option))
                {
                    donation = option switch
                    {
                        1 => 85.00m,
                        2 => 49.99m,
                        3 => 40.00m,
                        4 => 00.00m,
                        _ => 45.00m
                    };
                }
                else
                {
                    donation = 45.00m;
                }
            }
        } while (donation == 0);

        pet.setSuggestedDonation(donation);
    }

    private static bool isInvalidAge(Pet pet)
    {
        return pet.getAge() <= 0 || pet.getAge() > 15;
    }

    private static void ensureAnimalAgeAndCharacteristics()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (pet.getId() != "")
            {
                bool invalidAge = isInvalidAge(pet);
                bool invalidCharacteristics = pet.getCharacteristics() == "tbd" || pet.getCharacteristics() == "";
                if (invalidAge || invalidCharacteristics)
                {
                    if (invalidAge)
                    {
                        do
                        {
                            string title = $"Enter a valid age for ID #: {pet.getId()}";
                            addAge(pet, title);
                        } while (isInvalidAge(pet));
                    }
                    if (invalidCharacteristics)
                    {
                        do
                        {
                            string title = $"Enter a physical description for ID #: {pet.getId()} (size, color, breed, gender, weight, housebroken)";
                            addCharacteristics(pet, title);
                        } while (pet.getCharacteristics() == "tbd" || pet.getCharacteristics() == "");
                    }
                }
            }
        }
        Console.WriteLine("\nAge and physical description fields are complete for all of our friends.");
    }

    private static void ensureNickNameAndPersonality()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (pet.getId() != "")
            {
                bool invalidNickName = pet.getNickName() == "tbd" || pet.getNickName() == "";
                bool invalidPersonality = pet.getPersonality() == "tbd" || pet.getPersonality() == "";
                if (invalidNickName || invalidPersonality)
                {
                    if (invalidNickName)
                    {
                        do
                        {
                            string title = $"Enter a nickname for ID #: {pet.getId()}";
                            addNickName(pet, title);
                        } while (pet.getNickName() == "tbd" || pet.getNickName() == "");
                    }
                    if (invalidPersonality)
                    {
                        do
                        {
                            string title = $"Enter a personality description for ID #: {pet.getId()} (likes or dislikes, tricks, energy level)";
                            addPersonality(pet, title);
                        } while (pet.getPersonality() == "tbd" || pet.getPersonality() == "");
                    }
                }
            }
        }
        Console.WriteLine("\nNickname and personality description fields are complete for all of our friends.");
    }

    private static void editAge(Pet pet, int age)
    {
        pet.setAge(age);
    }

    private static void editPersonality(Pet pet, string personality)
    {
        pet.setPersonality(personality);
    }

    private static void showPetsByKindAndCharacteristics(KindAnimal specie)
    {
        bool findPattern = false;
        string description = getValidData("Enter the desired characteristics to search for").ToLower().Trim();
        string[] patterns = description.Split(',');
        Array.Sort(patterns);
        foreach (Pet pet in ourAnimals)
        {
            if (pet.GetKindAnimal() == specie && findPatterns(pet, patterns))
            {
                Console.WriteLine(pet);
                
                if (!findPattern) {
                    findPattern = true;
                }                
            }
        }

        if (!findPattern) {
            Console.WriteLine("None of our pets are a match found");
        }
    }

    private static bool findPatterns(Pet pet, string[] patterns) {
        bool flag = false;
        string expression = pet.getCharacteristics() + " " + pet.getPersonality();
        foreach (string term in patterns) {
            animatedSearch(pet, term);

            if (expression.Contains(term)) {
                Console.WriteLine($"\nOur pet {pet.getNickName()} is a {term} match!");
                
                if (!flag) {
                    flag = true;
                }
            }
        }
        return flag;
    }

    private static void animatedSearch(Pet pet, string term) {
        string[] searchingIcons = {" |", " /", "--", " \\", " *"};

        foreach (string icon in searchingIcons)
        {
            Console.Write($"\rsearching our pet {pet.getNickName()} for {term.Trim()} {icon}");
            Thread.Sleep(500);
        }
    }
}