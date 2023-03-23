if (args.Length < 2 && args.Length < 3)
{
    Console.WriteLine("Too few arguments.");
    return;
}
if (args.Length > 2 && args.Length > 3)
{
    Console.WriteLine("Too many arguments.");
    return;
}

if (args[0] == "build")
{
    if (!CheckBuildArgumentLength(args[1], args[2])) { return; }
    if (!CheckIfBuildArgumentsContainLetters(args[1], args[2], out string wrongArgument))
    {
        Console.WriteLine($"{wrongArgument} must not contain letters.");
        return;
    }
    BuildIban(int.Parse(args[1]), int.Parse(args[2]));
}
else if (args[0] == "analyze")
{
    if (args[1].Length != 15)
    {
        Console.WriteLine("Wrong length of IBAN");
        return;
    }
    if (!CheckAnalyzeCountryCode(args[1])) { return; }
    if (!int.TryParse(args[1].Substring(args[1].Length - 1), out int result) || result != 7)
    {
        Console.WriteLine("Wrong national check digit, we currently only support \"7\"");
        return;
    }
    AnalyzeIban(args[1]);
}
else { Console.WriteLine("Invalid command, must be \"build\" or \"analyze\""); }

void BuildIban(int bankCode, int accountNumber)
{
    long checksum = CalculateChecksum(bankCode, accountNumber);
    Console.WriteLine($"NO{checksum}{bankCode}{accountNumber}7");
}

void AnalyzeIban(string Iban)
{
    int bankCode = int.Parse(Iban.Substring(4, 4));
    int accountNumber = int.Parse(Iban.Substring(8, 6));
    Console.WriteLine($"Bank code is {bankCode}\nAccount number is {accountNumber}");
}

bool CheckBuildArgumentLength(string arg1, string arg2)
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

bool CheckIfBuildArgumentsContainLetters(string arg1, string arg2, out string wrongString)
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

bool CheckAnalyzeCountryCode(string Iban)
{
    if (Iban.Substring(0, 2) != "NO")
    {
        Console.WriteLine("Wrong country code, we currently only support \"NO\"");
        return false;
    }
    return true;
}

long CalculateChecksum(int bankCode, int accountNumber)
{
    const long N = 23, O = 24;
    string dividendString = $"{bankCode}{accountNumber.ToString()}7{N}{O}00";
    long result = long.Parse(dividendString) % 97;
    return 98 - result;
}
