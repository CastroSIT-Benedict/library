namespace library.Models;

// One Member object holds one row of lending.member.
public class Member
{
    public long MemberId { get; set; }          // the primary key 
    public string FullName { get; set; } = "";  // NOT NULL; the list is sorted by it
    public string? Email { get; set; }          // may be NULL: DBNull.Value going in, IsDBNull coming out
    public string MemberType { get; set; } = "Regular";    // NOT NULL: no ?, and no DBNull.Value

}
