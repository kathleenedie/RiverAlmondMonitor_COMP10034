using Microsoft.EntityFrameworkCore;
using raag_api.Models;

namespace raag_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<TimeSeries> TimeSeries { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<Condition> Conditions { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasKey(p => p.StationId);
            modelBuilder.Entity<Station>().Property(p => p.StationId).ValueGeneratedNever();

            modelBuilder.Entity<Station>()
                .HasMany(s => s.TimeSeries)
                .WithOne(t => t.Station)
                .HasForeignKey(t => t.StationId);


            modelBuilder.Entity<TimeSeries>().HasKey(t => t.TimeSeriesid);
            modelBuilder.Entity<TimeSeries>().Property(t => t.TimeSeriesid).ValueGeneratedNever();

            modelBuilder.Entity<TimeSeries>().HasMany(t => t.Measurements)
                .WithOne(m => m.TimeSeries)
                .HasForeignKey(m => m.TimeSeriesId);


            modelBuilder.Entity<Station>().HasData(
                new Station
                {
                    StationName = "Craigiehall",
                    StationNo = 14867,
                    StationId = 36396,
                    Latitude = 55.96313343,
                    Longitude = -3.338532726
                },
                new Station
                {
                    StationName = "Almondell",
                    StationNo = 14869,
                    StationId = 36404,
                    Latitude = 55.90181846,
                    Longitude = -3.463017734
                },
                new Station
                {
                    StationName = "Whitburn",
                    StationNo = 14881,
                    StationId = 36432,
                    Latitude = 55.87157334,
                    Longitude = -3.682936938
                }

                );

            modelBuilder.Entity<TimeSeries>().HasData(
                new TimeSeries
                {
                    TimeSeriesid = 54250010,
                    TimeSeriesName = "Day.Mean",
                    StationId = 36396,
                    ParameterTypeId = 560,
                    ParameterTypeName = "S",
                },
               new TimeSeries
               {
                   TimeSeriesid = 54283010,
                   TimeSeriesName = "Day.Mean",
                   StationId = 36404,
                   ParameterTypeId = 560,
                   ParameterTypeName = "S"
               },
                new TimeSeries
                {
                    TimeSeriesid = 54405010,
                    TimeSeriesName = "Day.Mean",
                    StationId = 36432,
                    ParameterTypeId = 560,
                    ParameterTypeName = "S"
                },
                new TimeSeries
                {
                    TimeSeriesid = 61865010,
                    TimeSeriesName = "Day.Mean",
                    StationId = 36396,
                    ParameterTypeId = 558,
                    ParameterTypeName = "Q"
                },
                new TimeSeries
                {
                    TimeSeriesid = 61873010,
                    TimeSeriesName = "Day.Mean",
                    StationId = 36404,
                    ParameterTypeId = 558,
                    ParameterTypeName = "Q"
                },
                new TimeSeries
                {
                    TimeSeriesid = 61896010,
                    TimeSeriesName = "Day.Mean",
                    StationId = 36432,
                    ParameterTypeId = 558,
                    ParameterTypeName = "Q"
                },
                new TimeSeries
                {
                    TimeSeriesid = 54393010,
                    TimeSeriesName = "Day.Total",
                    StationId = 36432,
                    ParameterTypeId = 523,
                    ParameterTypeName = "Precip"
                } 
            );
            modelBuilder.Entity<Measurement>().HasData(
                new Measurement {Id = 1, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 0.585, TimeSeriesId = 54283010},
                    new Measurement {Id = 2, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 11.625, TimeSeriesId =  61865010},
                    new Measurement {Id = 3, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 6.6, TimeSeriesId =  54393010},
                    new Measurement {Id = 4, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 0.808, TimeSeriesId =  54250010},
                    new Measurement {Id = 5, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 8.175, TimeSeriesId =  61873010},
                    new Measurement {Id = 6, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 0.797, TimeSeriesId =  61896010},
                    new Measurement {Id = 7, Timestamp = DateTime.Parse("2024-03-26 09:00:00.0000000"), Value = 0.4, TimeSeriesId =  54405010},
                    new Measurement {Id = 8, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0.456, TimeSeriesId =  54283010},
                    new Measurement {Id = 9, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0, TimeSeriesId =  61865010},
                    new Measurement {Id = 10, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0, TimeSeriesId =  54393010},
                    new Measurement {Id = 11, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0.542, TimeSeriesId =  54250010},
                    new Measurement {Id = 12, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 4.268, TimeSeriesId =  61873010},
                    new Measurement {Id = 13, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0.576, TimeSeriesId =  61896010},
                    new Measurement {Id = 14, Timestamp = DateTime.Parse("2024-03-24 09:00:00.0000000"), Value = 0.304, TimeSeriesId =  54405010},
                    new Measurement {Id = 15, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0.542, TimeSeriesId =  54283010},
                    new Measurement {Id = 16, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0, TimeSeriesId =  61865010},
                    new Measurement {Id = 17, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0, TimeSeriesId =  54393010},
                    new Measurement {Id = 18, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0.765, TimeSeriesId =  54250010},
                    new Measurement {Id = 19, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 6.754, TimeSeriesId =  61873010},
                    new Measurement {Id = 20, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0.781, TimeSeriesId =  61896010},
                    new Measurement {Id = 21, Timestamp = DateTime.Parse("2024-03-23 09:00:00.0000000"), Value = 0.392, TimeSeriesId =  54405010},
                    new Measurement {Id = 22, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0.493, TimeSeriesId =  54283010},
                    new Measurement {Id = 23, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0, TimeSeriesId =  61865010},
                    new Measurement {Id = 24, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 2.4, TimeSeriesId =  54393010},
                    new Measurement {Id = 25, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0.542, TimeSeriesId =  54250010},
                    new Measurement {Id = 26, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 5.251, TimeSeriesId =  61873010},
                    new Measurement {Id = 27, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0.659, TimeSeriesId =  61896010},
                    new Measurement {Id = 28, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0.336, TimeSeriesId =  54405010},
                    new Measurement {Id = 29, Timestamp = DateTime.Parse("2024-03-19 09:00:00.0000000"), Value = 0.492, TimeSeriesId =  54283010}
              );
            modelBuilder.Entity<UserReport>().HasData(
                new UserReport
                {
                    Id = 1, Timestamp = DateTime.Parse("2024-03-13 23:41:24.3354464"), Latitude = 55.891668,
                    Longitude = -3.492213, Report = "Rubbish at the edges", FirstName = "Hamish", LastName = "Scott",
                    Email = "hamham@email.com", IsImagePermission = true, ImageName = "almond244124319.jpg"
                },
                new UserReport
                {
                    Id = 2, Timestamp = DateTime.Parse("2024-03-14 13:34:34.2743198"), Latitude = 55.884978,
                    Longitude = -3.546458, Report = "Debris in the Almond", FirstName = "Kathleen", LastName = "Edie",
                    Email = "kted@email.com", IsImagePermission = false, ImageName = "almond243434262.jpg"
                },
                new UserReport
                {
                    Id = 3, Timestamp = DateTime.Parse("2024-03-18 17:49:48.7205339"), Latitude = 55.899417,
                    Longitude = -3.463460, Report = "Nellburn has some murky water being pumped into it",
                    FirstName = "Marge", LastName = "Simpson", Email = "bluehair@email.com", IsImagePermission = true,
                    ImageName = "nellburn244948708.jpg"
                },
                new UserReport
                {
                    Id = 4, Timestamp = DateTime.Parse("2024-03-18 17:54:06.2834519"), Latitude = 55.878767,
                    Longitude = -3.568688, Report = "Bad smell", FirstName = "Homer", LastName = "Simpson",
                    Email = "yellowman@email.com", IsImagePermission = true, ImageName = "nellburn245406280.jpg"
                });
        }
    }
}
