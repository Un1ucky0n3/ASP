using ASP.Models;
using Microsoft.AspNetCore.Mvc;
namespace ASP.Controllers;
//api/reservations
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    public static List<Reservation> _reservations = new List<Reservation>()
    {
        new Reservation(1, 1, "Jan Kowalski", "Spotkanie zespołu", new DateTime(2026, 4, 16), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), "Confirmed"),
        new Reservation(2, 2, "Anna Nowak", "Prezentacja projektu", new DateTime(2026, 4, 16), new TimeSpan(10, 30, 0), new TimeSpan(12, 0, 0), "Pending"),
        new Reservation(3, 3, "Piotr Wiśniewski", "Szkolenie", new DateTime(2026, 4, 17), new TimeSpan(8, 0, 0), new TimeSpan(11, 0, 0), "Confirmed"),
        new Reservation(4, 1, "Katarzyna Zielińska", "Warsztaty", new DateTime(2026, 4, 17), new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0), "Cancelled"),
        new Reservation(5, 4, "Marek Lewandowski", "Spotkanie klienta", new DateTime(2026, 4, 18), new TimeSpan(9, 0, 0), new TimeSpan(10, 30, 0), "Confirmed"),
        new Reservation(6, 5, "Ewa Kamińska", "Rekrutacja", new DateTime(2026, 4, 18), new TimeSpan(11, 0, 0), new TimeSpan(13, 0, 0), "Pending")
    };

    // Get api/reservations/{id}
    [HttpGet("{id}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound();
        return Ok(reservation);
    }

    // Get api/reservations?date=2026-05-10&status=confirmed&roomId=2
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetFiltered(
        [FromQuery] string? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var query = _reservations.AsQueryable();

        if (!string.IsNullOrWhiteSpace(date))
            query = query.Where(r => r.Date == DateTime.Parse(date));

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(r => r.Status.ToLower() == status.ToLower());   

        if (roomId.HasValue)
            query = query.Where(x => x.RoomId == roomId.Value);

        return Ok(query.ToList());
    }

    // POST api/reservations
    [HttpPost]
    public ActionResult<Reservation> Create(Reservation newReservation)
    {
        
        var room = RoomsController._rooms.FirstOrDefault(r => r.Id == newReservation.RoomId);

        if (room == null)
            return NotFound("Room does not exist");

        if (!room.IsActive)
            return Conflict("Room is inactive");
        
        if (newReservation.EndTime <= newReservation.StartTime)
            return BadRequest("EndTime must be after StartTime");
        
        var conflict = _reservations.Any(r =>
            r.RoomId == newReservation.RoomId &&
            r.Date == newReservation.Date &&
            !(newReservation.EndTime <= r.StartTime || newReservation.StartTime >= r.EndTime)
        );

        if (conflict)
            return Conflict("Room is already booked in that time");

        newReservation.Id = _reservations.Max(x => x.Id) + 1;
        _reservations.Add(newReservation);

        return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
    }


    // PUT api/reservations/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation updatedReservation)
    {
        if (updatedReservation == null || updatedReservation.Id != id)
            return BadRequest();

        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        if (reservation == null)
            return NotFound();
        
        if (updatedReservation.EndTime <= updatedReservation.StartTime)
            return BadRequest("EndTime must be after StartTime");

        reservation.Date = updatedReservation.Date;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.OrganizerName = updatedReservation.OrganizerName;
        reservation.RoomId = updatedReservation.RoomId;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.Status = updatedReservation.Status;
        reservation.Topic = updatedReservation.Topic;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        if (reservation == null)
            return NotFound();
        _reservations.Remove(reservation);
        return NoContent();
    }
}