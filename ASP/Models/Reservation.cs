namespace ASP.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Status { get; set; }

    public Reservation(int Id, int RoomId, string OrganizerName, string Topic, string Date, string StartTime, string EndTime, string Status)
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