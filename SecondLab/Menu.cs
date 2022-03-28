using System;

namespace SecondLab
{
    public enum InputChoices
    {
        KEYBOARD,
        RANDOM,
        FILES,
        EXIT
    }
    public enum CipheringChoices
    {
        ENCODE,
        DECODE,
        DATA,
        EXIT
    }
    public enum TypeChoices
    {
        AFFINE,
        ATBASH
    }
    public enum DualChoice
    {
        YES,
        NO
    }
    public enum SavingChoices
    {
        SAVEDATA,
        SAVERESULT,
        NOLEAVEMEALONE
    }

    public static class Menu
    {
        public static void Greeting()
        {
            Console.WriteLine("Laboratory 2");
            Console.WriteLine("Realization of Affine Cipher - coding & decoding");
            Console.WriteLine("Student of 404 group Zakirov Ilyas, year 2022\n");
        }
        public static void AskForInput()
        {
            Console.WriteLine("How would you like to enter your sentence?");
            Console.WriteLine("0 - From keyboard | 1 - Random filling | 2 - From file | 3 - Exit");
            Console.Write("Your choice: ");
        }
        public static void AskForAction()
        {
            Console.WriteLine("What you would like to do?");
            Console.WriteLine("0 - Encode | 1 - Decode | 2 - Save | 3 - Exit");
            Console.Write("Your choice: ");
        }
        public static void AskForSaving()
        {
            Console.WriteLine("Do you want to save some of entered or recieved data?");
            Console.WriteLine("0 - Save data to file | 1 - Save result to file | 2 - Please don't");
            Console.Write("Your choice: ");
        }
        public static void AskForCodeType()
        {
            Console.WriteLine("Which code type do you want to use?");
            Console.WriteLine("0 - Affine | 1 - Atbash");
            Console.Write("Your choice: ");
        }
        public static void CodingInterface(String text, InputChoices inputChoice)
        {
            bool toExit = false;
            do
            {
                Atbash atbash = new Atbash();
                Affine affine = new Affine();
                String source = text;

                AskForAction();
                CipheringChoices cipheringChoices = (CipheringChoices)Input.GetNumber((Int32)CipheringChoices.ENCODE, (Int32)CipheringChoices.EXIT);
                Console.WriteLine();

                int a = 0, b = 0;

                switch (cipheringChoices)
                {
                    case CipheringChoices.ENCODE:
                        AskForCodeType();
                        TypeChoices typeChoices = (TypeChoices)Input.GetNumber((Int32)TypeChoices.AFFINE, (Int32)TypeChoices.ATBASH);
                        Console.WriteLine();
                        switch (typeChoices)
                        {
                            case TypeChoices.AFFINE:
                                if (IsKeyboard(inputChoice)) Input.KeyboardCoefficients(ref a, ref b);
                                else Input.RandomCoefficients(a, b);
                                String encodedAffine = affine.Encode(text, a, b);
                                text = encodedAffine;
                                Console.WriteLine(encodedAffine);
                                break;
                            case TypeChoices.ATBASH:
                                String encodedAtbash = atbash.Encode(text);
                                text = encodedAtbash;
                                Console.WriteLine(encodedAtbash);
                                break;
                            default: throw new ArgumentOutOfRangeException(nameof(typeChoices), typeChoices, null);
                        }
                        break;
                    case CipheringChoices.DECODE:
                        AskForCodeType();
                        TypeChoices typeChoice = (TypeChoices)Input.GetNumber((Int32)TypeChoices.AFFINE, (Int32)TypeChoices.ATBASH);
                        Console.WriteLine();
                        switch (typeChoice)
                        {
                            case TypeChoices.AFFINE:
                                if (IsKeyboard(inputChoice)) Input.KeyboardCoefficients(ref a, ref b);
                                else Input.RandomCoefficients(a, b);
                                String decodedAffine = affine.Decode(text, a, b);
                                text = decodedAffine;
                                Console.WriteLine(decodedAffine);
                                break;
                            case TypeChoices.ATBASH:
                                String decodedAtbash = atbash.Decode(text);
                                text = decodedAtbash;
                                Console.WriteLine(decodedAtbash);
                                break;
                            default: throw new ArgumentOutOfRangeException(nameof(typeChoice), typeChoice, null);
                        }
                        break;
                    case CipheringChoices.DATA:
                        AskForSaving();
                        SavingChoices savingChoices = (SavingChoices)Input.GetNumber((Int32)SavingChoices.SAVEDATA, (Int32)SavingChoices.NOLEAVEMEALONE);
                        switch (savingChoices)
                        {
                            case SavingChoices.SAVEDATA:
                                FileWork.SaveToFile(source);
                                break;
                            case SavingChoices.SAVERESULT:
                                FileWork.SaveToFile(text);
                                break;
                            case SavingChoices.NOLEAVEMEALONE:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(savingChoices), savingChoices, null);
                        }
                        break;
                    case CipheringChoices.EXIT:
                        toExit = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cipheringChoices), cipheringChoices, null);
                }
            }while (!toExit);
        }
        public static Boolean IsKeyboard(InputChoices inputChoice)
        {
            Boolean result = (inputChoice == InputChoices.KEYBOARD) ? true : false;
            return result;
        }
        public static void UserInterface()
        {
            Greeting();
            bool toExit = false;
            do
            {
                String text = "";

                AskForInput();

                InputChoices inputChoice = (InputChoices)Input.GetNumber();
                Console.WriteLine();

                switch(inputChoice)
                {
                    case InputChoices.KEYBOARD:
                        text = Input.KeyboardInput(text);
                        break;
                    case InputChoices.RANDOM:
                        text = Input.RandomInput(text);
                        break;
                    case InputChoices.FILES:
                        do { } while (!FileWork.FileInput(ref text));
                        break;
                    case InputChoices.EXIT:
                        toExit = true;
                        break;
                    default:
                        Console.WriteLine($"Please, enter a number between {(Int32)InputChoices.KEYBOARD} and {(Int32)InputChoices.EXIT}");
                        continue;
                }
                if (inputChoice != InputChoices.EXIT)
                {
                    CodingInterface(text, inputChoice);
                }

            } while(!toExit);
        }
        public static Boolean AskForRewriting()
        {
            Console.WriteLine("File is already exist, do you want to rewrite it?");
            Console.WriteLine("0 - Yes | 1 - No\n");
            DualChoice choice = (DualChoice)Input.GetNumber((Int32)DualChoice.YES, (Int32)DualChoice.NO);
            switch (choice)
            {
                case DualChoice.YES:
                    return true;
                case DualChoice.NO:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
            }
        }
    }
}