using Model;
internal class Program
{
    static int maxPets = 8;
    static Pet[] ourAnimals = new Pet[maxPets];
    static int lastIndexAnimal = 0;

    private static void Main(string[] args)
    {
        InitPetList(); //INIT ANIMAL'S ARRAY
        ShowMenu();
    }

    private static void ShowMenu()
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
                                ListAllAnimals();
                                break;
                            case 2:
                                ConfirmAddPet();
                                break;
                            case 3:
                                EnsureAnimalAgeAndCharacteristics();
                                break;
                            case 4:
                                EnsureNickNameAndPersonality();
                                break;
                            case 5:
                                ShowPetsByKindAndCharacteristics(KindAnimal.CAT);
                                break;
                            case 6:
                                ShowPetsByKindAndCharacteristics(KindAnimal.DOG);
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

    private static void ListAllAnimals()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (!pet.ID.Equals(""))
            {
                Console.WriteLine(pet);
            }
        }
    }

    private static KindAnimal ChooseAnimal()
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

    private static void InitPetList()
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

    private static void ConfirmAddPet()
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
                        AddAnimal();
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

    private static void AddAnimal()
    {
        KindAnimal specie = ChooseAnimal();
        string ID = ((specie == KindAnimal.DOG) ? "d" : "c") + (lastIndexAnimal + 1);

        Pet pet = new Pet(ID, specie);
        //ADD AGE   
        AddAge(pet, "Enter the pet's age or enter 0 if unknown");
        //ADD CHARACTERISTICS
        AddCharacteristics(pet, "Enter a physical description of the pet (size, color, gender, weight, housebroken)");
        //ADD PERSONALITY
        AddPersonality(pet, "Enter a description of the pet's personality (likes or dislikes, tricks, energy level)");
        //ADD NICKNAME
        AddNickName(pet, "Enter a nickname for the pet");
        //ADD DONATION
        AddDonation(pet, "Suggested donation");
        ourAnimals[lastIndexAnimal] = pet;
    }

    private static void AddAge(Pet pet, string title)
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

        pet.Age = age;
    }
    private static void AddCharacteristics(Pet pet, string title)
    {
        pet.Characteristics = GetValidData(title);
    }

    private static void AddPersonality(Pet pet, string title)
    {
        pet.Personality = GetValidData(title);
    }

    private static void AddNickName(Pet pet, string title)
    {
        pet.NickName = GetValidData(title);
    }

    private static string GetValidData(string title)
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

    private static void AddDonation(Pet pet, string title)
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

        pet.Donation = donation;
    }

    private static bool IsInvalidAge(Pet pet)
    {
        return pet.Age <= 0 || pet.Age > 15;
    }

    private static void EnsureAnimalAgeAndCharacteristics()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (pet.ID != "")
            {
                bool invalidAge = IsInvalidAge(pet);
                bool invalidCharacteristics = pet.Characteristics == "tbd" || pet.Characteristics == "";
                if (invalidAge || invalidCharacteristics)
                {
                    if (invalidAge)
                    {
                        do
                        {
                            string title = $"Enter a valid age for ID #: {pet.ID}";
                            AddAge(pet, title);
                        } while (IsInvalidAge(pet));
                    }
                    if (invalidCharacteristics)
                    {
                        do
                        {
                            string title = $"Enter a physical description for ID #: {pet.ID} (size, color, breed, gender, weight, housebroken)";
                            AddCharacteristics(pet, title);
                        } while (pet.Characteristics == "tbd" || pet.Characteristics == "");
                    }
                }
            }
        }
        Console.WriteLine("\nAge and physical description fields are complete for all of our friends.");
    }

    private static void EnsureNickNameAndPersonality()
    {
        foreach (Pet pet in ourAnimals)
        {
            if (pet.ID != "")
            {
                bool invalidNickName = pet.NickName == "tbd" || pet.NickName == "";
                bool invalidPersonality = pet.Personality == "tbd" || pet.Personality == "";
                if (invalidNickName || invalidPersonality)
                {
                    if (invalidNickName)
                    {
                        do
                        {
                            string title = $"Enter a nickname for ID #: {pet.ID}";
                            AddNickName(pet, title);
                        } while (pet.NickName == "tbd" || pet.NickName == "");
                    }
                    if (invalidPersonality)
                    {
                        do
                        {
                            string title = $"Enter a personality description for ID #: {pet.ID} (likes or dislikes, tricks, energy level)";
                            AddPersonality(pet, title);
                        } while (pet.Personality == "tbd" || pet.Personality == "");
                    }
                }
            }
        }
        Console.WriteLine("\nNickname and personality description fields are complete for all of our friends.");
    }

    private static void ShowPetsByKindAndCharacteristics(KindAnimal specie)
    {
        bool findPattern = false;
        string description = GetValidData("Enter the desired characteristics to search for").ToLower().Trim();
        string[] patterns = description.Split(',');
        Array.Sort(patterns);
        foreach (Pet pet in ourAnimals)
        {
            if (pet.Specie == specie && FindPatterns(pet, patterns))
            {
                Console.WriteLine(pet);

                if (!findPattern)
                {
                    findPattern = true;
                }
            }
        }

        if (!findPattern)
        {
            Console.WriteLine("None of our pets are a match found");
        }
    }

    private static bool FindPatterns(Pet pet, string[] patterns)
    {
        bool flag = false;
        string expression = pet.Characteristics + " " + pet.Personality;
        foreach (string term in patterns)
        {
            AnimatedSearch(pet, term);

            if (expression.Contains(term))
            {
                Console.WriteLine($"\nOur pet {pet.NickName} is a {term} match!");

                if (!flag)
                {
                    flag = true;
                }
            }
        }
        return flag;
    }

    private static void AnimatedSearch(Pet pet, string term)
    {
        string[] searchingIcons = [" |", " /", "--", " \\", " *"];

        foreach (string icon in searchingIcons)
        {
            Console.Write($"\rsearching our pet {pet.NickName} for {term.Trim()} {icon}");
            Thread.Sleep(500);
        }
    }
}