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
        ATBASH,
        EXIT
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
            Console.WriteLine("0 - Affine | 1 - Atbash | 2 - Exit");
            Console.Write("Your choice: ");
        }
        public static void InputInterface(ICipher iCipher)
        {
            bool toExit = false;
            do
            {
                String text = "";
                
                AskForInput();
                InputChoices inputChoices = (InputChoices)Input.GetNumber((Int32)InputChoices.KEYBOARD, (Int32)InputChoices.EXIT);
                Console.WriteLine();

                switch (inputChoices)
                {
                    case InputChoices.KEYBOARD:
                        text = Input.KeyboardInput(text);
                        break;
                    case InputChoices.RANDOM:
                        text = Input.RandomInput(text);
                        break;
                    case InputChoices.FILES:
                        while (FileWork.FileInput(ref text));
                        break;
                    case InputChoices.EXIT:
                        toExit = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(inputChoices), inputChoices, null);
                }
                if (inputChoices != InputChoices.EXIT)
                {
                    CodingInterface(iCipher, text);
                }
            } while (!toExit);
        }
        public static void CodingInterface(ICipher iCipher, String text)
        {
            Boolean toExit = false;
            String source = text;
            do
            {
                AskForAction();
                CipheringChoices cipheringChoices = (CipheringChoices)Input.GetNumber((Int32)CipheringChoices.ENCODE, (Int32)CipheringChoices.EXIT);
                Console.WriteLine();
                switch (cipheringChoices)
                {
                    case CipheringChoices.ENCODE:
                        text = iCipher.Encode(text);
                        Console.WriteLine($"Encoded message: {text}\n");
                        break;
                    case CipheringChoices.DECODE:
                        text = iCipher.Decode(text);
                        Console.WriteLine($"Decoded message: {text}\n");
                        break;
                    case CipheringChoices.DATA:
                        AskForSaving();
                        SavingChoices saving = (SavingChoices)Input.GetNumber((Int32)SavingChoices.SAVEDATA, (Int32)SavingChoices.NOLEAVEMEALONE);
                        Console.WriteLine();

                        switch (saving)
                        {
                            case SavingChoices.SAVEDATA:
                                FileWork.SaveToFile(text);
                                break;
                            case SavingChoices.SAVERESULT:
                                FileWork.SaveToFile(source);
                                break;
                            case SavingChoices.NOLEAVEMEALONE:
                                break;
                        }
                        break;
                    case CipheringChoices.EXIT:
                        toExit = true;
                        break;
                }
            } while (!toExit);
        }
        public static void UserInterface()
        {
            Greeting();
            bool toExit = false;
            do
            {
                ICipher iCipher;

                AskForCodeType();
                TypeChoices typeChoice = (TypeChoices)Input.GetNumber((Int32)TypeChoices.AFFINE, (Int32)TypeChoices.EXIT);
                Console.WriteLine();

                switch (typeChoice)
                {
                    case TypeChoices.AFFINE:
                        iCipher = new Affine();
                        break;
                    case TypeChoices.ATBASH:
                        iCipher = new Atbash();
                        break;
                    case TypeChoices.EXIT:
                        iCipher = new Atbash();
                        toExit = true;
                        break;
                    default:
                        iCipher = new Atbash();
                        break;
                }
                if (typeChoice != TypeChoices.EXIT)
                {
                    InputInterface(iCipher);
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