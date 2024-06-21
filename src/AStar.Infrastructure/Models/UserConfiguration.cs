namespace AStar.Infrastructure.Models;

/// <summary>
/// The <see href="UserConfiguration"></see> class containing User Configuration.
/// </summary>
public class UserConfiguration
{
    /// <summary>
    /// Gets or sets the Id of the configuration.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Login Email Address.
    /// </summary>
    public string LoginEmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
