namespace ASP.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }

    public Room(int Id, string Name, string BuildingCode, int Floor, int Capacity, bool HasProjector, bool isActive)
    {
        this.Id = Id;
        this.Name = Name;
        this.BuildingCode = BuildingCode;
        this.Floor = Floor;
        this.Capacity = Capacity;
        this.HasProjector = HasProjector;
        this.IsActive = isActive;
    }
    public Room(){}
}