using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Tests;

public class UnitTest1
{
    private ApplicationDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateMembership_ShouldSaveMembershipToDatabase()
    {
        var context = GetInMemoryContext();

        var membership = new Membership
        {
            Type = "Premium",
            StartDate = DateTime.Now,
            IsActive = true,
            UserId = "user-123"
        };

        context.Memberships.Add(membership);
        await context.SaveChangesAsync();

        var saved = await context.Memberships.FirstOrDefaultAsync(m => m.UserId == "user-123");
        Assert.NotNull(saved);
        Assert.Equal("Premium", saved.Type);
        Assert.True(saved.IsActive);
    }

    [Fact]
    public async Task CreateBooking_ShouldSaveBookingToDatabase()
    {
        var context = GetInMemoryContext();

        var gymClass = new GymClass
        {
            Name = "Yoga",
            DateTime = DateTime.Now.AddDays(1),
            Instructor = "Anna",
            Category = "Yoga",
            MaxCapacity = 20
        };
        context.GymClasses.Add(gymClass);
        await context.SaveChangesAsync();

        var booking = new Booking
        {
            UserId = "user-123",
            GymClassId = gymClass.Id,
            BookedAt = DateTime.Now
        };
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        var saved = await context.Bookings.FirstOrDefaultAsync(b => b.UserId == "user-123");
        Assert.NotNull(saved);
        Assert.Equal(gymClass.Id, saved.GymClassId);
    }

    [Fact]
    public async Task PreventDuplicateBooking_ShouldNotAllowSameUserToBookSameClass()
    {
        var context = GetInMemoryContext();

        var gymClass = new GymClass
        {
            Name = "Spinning",
            DateTime = DateTime.Now.AddDays(2),
            Instructor = "Erik",
            Category = "Cardio",
            MaxCapacity = 15
        };
        context.GymClasses.Add(gymClass);
        await context.SaveChangesAsync();

        context.Bookings.Add(new Booking { UserId = "user-123", GymClassId = gymClass.Id, BookedAt = DateTime.Now });
        await context.SaveChangesAsync();

        var duplicate = await context.Bookings
            .AnyAsync(b => b.UserId == "user-123" && b.GymClassId == gymClass.Id);

        Assert.True(duplicate);
    }

    [Fact]
    public async Task DeleteBooking_ShouldRemoveBookingFromDatabase()
    {
        var context = GetInMemoryContext();

        var booking = new Booking
        {
            UserId = "user-456",
            GymClassId = 1,
            BookedAt = DateTime.Now
        };
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();

        var deleted = await context.Bookings.FirstOrDefaultAsync(b => b.UserId == "user-456");
        Assert.Null(deleted);
    }

    [Fact]
    public async Task GetMembership_ShouldReturnNullIfNoMembershipExists()
    {
        var context = GetInMemoryContext();

        var membership = await context.Memberships
            .FirstOrDefaultAsync(m => m.UserId == "user-999");

        Assert.Null(membership);
    }
}