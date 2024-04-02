Console.WriteLine("Cleaning PATH variable...");

cleanPathVariable(EnvironmentVariableTarget.User);
cleanPathVariable(EnvironmentVariableTarget.Machine);

static void cleanPathVariable(EnvironmentVariableTarget target)
{
    Console.WriteLine($"Cleaning PATH variable for {target}...");

    try
    {
        string pathVariable = Environment.GetEnvironmentVariable("PATH", target) ?? string.Empty;

        if (pathVariable == string.Empty)
        {
            Console.WriteLine("PATH variable is empty");
            return;
        }

        var pathDirectories = pathVariable.Split(';').ToList();

        Console.WriteLine($"Found {pathDirectories.Count} directories in PATH variable");
        pathDirectories.ToList().ForEach(Console.WriteLine);

        ISet<string> uniqueDirectories = pathDirectories.ToHashSet();


        var numRemoved = pathDirectories.Count - uniqueDirectories.Count;
        Console.WriteLine($"Removed {numRemoved} duplicate directories");

        string cleanedPath = string.Join(";", uniqueDirectories);

        Environment.SetEnvironmentVariable("PATH", cleanedPath, target);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}