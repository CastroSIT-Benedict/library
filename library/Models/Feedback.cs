namespace library.Models;

public class Feedback
{
    public long FeedbackId { get; set; }          // BIGINT PRIMARY KEY
    public string? FullName { get; set; }         // VARCHAR(80), nullable
    public string? Body { get; set; }             // TEXT, nullable
    public DateTime CreatedOn { get; set; }       // DATE NOT NULL DEFAULT CURRENT_DATE
}