using MyProject.Debugging;

namespace MyProject;

public class MyProjectConsts
{
    public const string LocalizationSourceName = "MyProject";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3e8cbc2de7024a4789588c3f6047b925";
}
