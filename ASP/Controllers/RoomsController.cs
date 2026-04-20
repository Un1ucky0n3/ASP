using ASP.Models;
using Microsoft.AspNetCore.Mvc;
namespace ASP.Controllers;
//api/rooms
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    public static List<Room> _rooms = new List<Room>()
    {
        new Room(1, "A101", "A", 1, 20, true, true),
        new Room(2, "A102", "A", 1, 15, false, true),
        new Room(3, "B201", "B", 2, 30, true, true),
        new Room(4, "C301", "C", 3, 25, true, false),
        new Room(5, "D401", "D", 4, 40, false, true)
    };

    // GET api/rooms/{id}
    [HttpGet("{id}")]
    public ActionResult<Room> Get(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        return Ok(room);
    }
    
    // GET /api/rooms/building/{buildingCode}
    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding(string buildingCode)
    {
        var result = _rooms
            .Where(r => r.BuildingCode.ToLower() == buildingCode.ToLower())
            .ToList();

        return Ok(result);
    }

    // GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetFiltered(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var query = _rooms.AsQueryable();

        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly.HasValue && activeOnly.Value)
            query = query.Where(r => r.IsActive);

        return Ok(query.ToList());
    }

    // POST /api/rooms
    [HttpPost]
    public ActionResult<Room> Create(Room newRoom)
    {
        var exists = _rooms.Any(r =>
            r.Name == newRoom.Name &&
            r.BuildingCode == newRoom.BuildingCode
        );

        if (exists)
            return Conflict("Room already exists in this building");

        newRoom.Id = _rooms.Any()
            ? _rooms.Max(r => r.Id) + 1
            : 1;

        _rooms.Add(newRoom);

        return CreatedAtAction(nameof(Get), new { id = newRoom.Id }, newRoom);
    }

    // PUT /api/rooms/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updatedRoom)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return NoContent();
    }

    // DELETE /api/rooms/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();
        _rooms.Remove(room);
        return NoContent();
    }
}