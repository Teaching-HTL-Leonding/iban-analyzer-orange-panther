if(!CheckNumberOfArguments()) { return; }
if (!CheckArgumentLength(args[0], args[1])) { return; }
if (!CheckIfArgumentsContainLetters(args[0], args[1], out string wrongArgument))
{
    Console.WriteLine($"{wrongArgument} must not contain letters.");
    return;
}

BuildIban(int.Parse(args[0]), int.Parse(args[1]));

void BuildIban(int bankCode, int accountNumber)
{
    Console.WriteLine($"NO00{bankCode}{accountNumber}7");
}

bool CheckNumberOfArguments()
{
    if (args.Length < 2)
    {
        Console.WriteLine("Too few arguments.");
        return false;
    }
    if (args.Length > 2)
    {
        Console.WriteLine("Too many arguments.");
        return false;
    }
    return true;
}

bool CheckArgumentLength(string arg1, string arg2)
{
    if (arg1.Length != 4)
    {
        Console.WriteLine("Bank code has wrong length, must contain 4 digits.");
        return false;
    }
    if (arg2.Length != 6)
    {
        Console.WriteLine("Account number has wrong length, must contain 6 digits.");
        return false;
    }
    return true;
}

bool CheckIfArgumentsContainLetters(string arg1, string arg2, out string wrongString)
{
    for (int i = 0; i < arg1.Length; i++)
    {
        if (Char.IsLetter(arg1[i]))
        {
            wrongString = "Bank code";
            return false;
        }
    }
    for (int i = 0; i < arg2.Length; i++)
    {
        if (Char.IsLetter(arg2[i]))
        {
            wrongString = "Account number";
            return false;
        }
    }
    wrongString = "";
    return true;
}
