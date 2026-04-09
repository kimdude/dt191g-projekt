/* Model for Offered Services table */

using Microsoft.EntityFrameworkCore;

namespace bookingApplication;

//Composite primary key
[PrimaryKey(nameof(HairdresserId), nameof(ServiceId))]
public class OfferedService
{
    //Properties
    public int HairdresserId { get; set; }
    public int ServiceId { get; set; }

    //Foreign keys reference
    public Hairdresser Hairdresser { get; set; } = null!;
    public Service Service { get; set; } = null!;
}