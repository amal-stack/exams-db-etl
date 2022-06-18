namespace ExamsDbDataEtl.Models;

public partial class Instructor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly Dob { get; set; }
    public string Email { get; set; } = null!;
    public DateOnly DateJoined { get; set; }
}
