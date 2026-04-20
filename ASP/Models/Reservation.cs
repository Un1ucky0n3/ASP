using System.ComponentModel.DataAnnotations;

namespace ASP.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; }
    [Required]
    public string Topic { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Status { get; set; }
    public Reservation(int Id, int RoomId, string OrganizerName, string Topic, DateTime Date, TimeSpan StartTime, TimeSpan EndTime, string Status)
    {
        this.Id = Id;
        this.RoomId = RoomId;
        this.OrganizerName = OrganizerName;
        this.Topic = Topic;
        this.Date = Date;
        this.StartTime = StartTime;
        this.EndTime = EndTime;
        this.Status = Status;
    }
}