namespace library.Models;

// One Member object holds one row of lending.member.
public class Note
{
    public long NoteId { get; set; }          // BIGINT PRIMARY KEY 
    public string Title { get; set; } = "";  // VARCHAR(100) NOT NULL
    public string? Body { get; set; }          // TEXT, can be NULL
    public DateTime CreatedOn { get; set; }    // DATE NOT NULL DEFAULT CURRENT_DATE

}
