using Microsoft.EntityFrameworkCore;

namespace AirBB.Models
{
    public class AirBBDbcontext : DbContext
    {
        public AirBBDbcontext(DbContextOptions<AirBBDbcontext> options)
            : base(options) { }
        public DbSet<Client> Client { get; set; } = null!;
        public DbSet<Location> Location { get; set; } = null!;
        public DbSet<Residence> Residence { get; set; } = null!;
        public DbSet<Reservation> Reservation { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Chicago" },
                new Location { LocationId = 2, Name = "New York" },
                new Location { LocationId = 3, Name = "Miami" },
                new Location { LocationId = 4, Name = "Atlanta" }
            );

            modelBuilder.Entity<Residence>().HasData(
                new Residence { ResidenceId = 1, Name = "Chicago Loop Apartment", ResidencePicture = "chi_loop.png", LocationId = 1, GuestNumber = 4, BedroomNumber = 2, BathroomNumber = 1, PricePerNight = "100" },
                new Residence { ResidenceId = 2, Name = "New York Studio", ResidencePicture = "ny_studio.png", LocationId = 2, GuestNumber = 2, BedroomNumber = 1, BathroomNumber = 1, PricePerNight = "120" },
                new Residence { ResidenceId = 3, Name = "Miami Beach House", ResidencePicture = "miami_beach.png", LocationId = 3, GuestNumber = 8, BedroomNumber = 4, BathroomNumber = 3, PricePerNight = "50" },
                new Residence { ResidenceId = 4, Name = "Atlanta Suburban House", ResidencePicture = "atl_house.png", LocationId = 4, GuestNumber = 6, BedroomNumber = 3, BathroomNumber = 2, PricePerNight = "70" }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, ResidenceId = 1, ReservationStartDate = new DateTime(2026, 1, 5), ReservationEndDate = new DateTime(2026, 1, 8) },
                new Reservation { ReservationId = 2, ResidenceId = 2, ReservationStartDate = new DateTime(2026, 1, 1), ReservationEndDate = new DateTime(2026, 1, 4) },
                new Reservation { ReservationId = 3, ResidenceId = 3, ReservationStartDate = new DateTime(2026, 2, 1), ReservationEndDate = new DateTime(2026, 2, 10) }
            );
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    Name = "John Doe",
                    PhoneNumber = "555-000-0001",
                    Email = "john.Doe@airbb.com",
                    DOB = "07/08/2000",
                },
                new Client
                {
                    ClientId = 2,
                    Name = "Emy",
                    PhoneNumber = "555-000-0002",
                    Email = "emy@airbb.com",
                    DOB = "07/08/2001",
                },
                new Client
                {
                    ClientId = 3,
                    Name = "Ana Smith",
                    PhoneNumber = "555-000-0003",
                    Email = "anasmith@airbb.com",
                    DOB = "07/08/2002",
                }
            );

        }
    }
}
