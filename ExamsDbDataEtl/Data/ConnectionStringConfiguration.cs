namespace ExamsDbDataEtl.Data;

/// <summary>
/// Use the constants to specify the connection string parameters.
/// </summary>
/// <remarks>
/// To protect potentially sensitive information in your connection string, you should move it out of source code.see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
/// </remarks>
public static class ConnectionStringConfiguration
{
    private const string Host = "localhost";
    private const string Database = "DATABASE_NAME_HERE";
    private const string Username = "USERNAME_HERE";
    private const string Password = "PASSWORD_HERE";
    public static string GetConnectionString()
    {
        return $"Host={Host};Database={Database};Username={Username};Password={Password}";
    }
}