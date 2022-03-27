namespace SecondLab
{
    public static class Menu
    {
        public static void UserInterface()
        {
            ICipher atbash = new Atbash();
            Affine affine = new Affine();

            System.Console.WriteLine(affine.Encode("Cras sed arcu sodales, viverra velit in, pharetra justo. Maecenas quis auctor arcu. Praesent semper. ", 7, 1));
        }
    }
}
