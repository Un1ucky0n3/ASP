using ASP.Models;
using Microsoft.AspNetCore.Mvc;
namespace ASP.Controllers;
//api/reservations
[Route("api/[controller]")]
[ApiController]
public class ReservationsController
{
    public static List<Reservation> listOfReservations = new List<Reservation>()
    {
        new Reservation(1, 1, "Jan Kowalski", "Spotkanie zespołu", "2026-04-16", "09:00", "10:00", "Confirmed"),
        new Reservation(2, 2, "Anna Nowak", "Prezentacja projektu", "2026-04-16", "10:30", "12:00", "Pending"),
        new Reservation(3, 3, "Piotr Wiśniewski", "Szkolenie", "2026-04-17", "08:00", "11:00", "Confirmed"),
        new Reservation(4, 1, "Katarzyna Zielińska", "Warsztaty", "2026-04-17", "12:00", "14:00", "Cancelled"),
        new Reservation(5, 4, "Marek Lewandowski", "Spotkanie klienta", "2026-04-18", "09:00", "10:30", "Confirmed"),
        new Reservation(6, 5, "Ewa Kamińska", "Rekrutacja", "2026-04-18", "11:00", "13:00", "Pending")
    };
}