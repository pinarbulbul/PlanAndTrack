using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.ApplicationIdentity;

public class ApplicationIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedAdministrator(builder);
        SeedManager(builder);
        SeedTester(builder);
        SeedRequester(builder);
    }

    private static void SeedAdministrator(ModelBuilder builder) {

        string roleId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
        string userId = "8e445865-a24d-4543-a6c6-9443d048cdb9";

        //Seeding a  'Administrator' role to AspNetRoles table
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleId,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        });

        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();


        //Seeding Admin to AspNetUsers table
        IdentityUser user = new IdentityUser
        {
            Id = userId, // primary key
            UserName = "admin@planandtrack.com",
            NormalizedUserName = "ADMIN@PLANANDTRACK.COM",
            Email = "admin@planandtrack.com",
            NormalizedEmail = "ADMIN@PLANANDTRACK.COM",
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Admin12*");

        builder.Entity<IdentityUser>().HasData(user);

        //Seeding the relation between our user and role to AspNetUserRoles table
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            }
        );
    }

    private static void SeedManager(ModelBuilder builder)
    {
        string roleId = "cfdcf05f-86fa-4ad9-8610-85aae7229e62";
        string userId = "9578dd3d-af52-419f-a24b-6e7259409d06";

        //Seeding a  'Manager' role to AspNetRoles table
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleId,
            Name = "Manager",
            NormalizedName = "MANAGER"
        });

        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();

        //Seeding Manager to AspNetUsers table
        IdentityUser user = new IdentityUser
        {
            Id = userId, // primary key
            UserName = "manager@planandtrack.com",
            NormalizedUserName = "MANAGER@PLANANDTRACK.COM",
            Email = "manager@planandtrack.com",
            NormalizedEmail = "MANAGER@PLANANDTRACK.COM",
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Manager12*");

        builder.Entity<IdentityUser>().HasData(user);

        //Seeding the relation between our user and role to AspNetUserRoles table
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            }
        );
    }

    private static void SeedTester(ModelBuilder builder)
    {
        string roleId = "7736c583-ea00-4315-99b1-39543dcdf947";
        string userId = "49f63f0f-8d3d-4daf-98c7-4a82b88e1a29";

        //Seeding a  'Tester' role to AspNetRoles table
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleId,
            Name = "Tester",
            NormalizedName = "TESTER"
        });

        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();


        //Seeding Admin to AspNetUsers table
        IdentityUser user = new IdentityUser
        {
            Id = userId, // primary key
            UserName = "tester@planandtrack.com",
            NormalizedUserName = "TESTER@PLANANDTRACK.COM",
            Email = "tester@planandtrack.com",
            NormalizedEmail = "TESTER@PLANANDTRACK.COM",
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Tester12*");

        builder.Entity<IdentityUser>().HasData(user);

        //Seeding the relation between our user and role to AspNetUserRoles table
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            }
        );
    }

    private static void SeedRequester(ModelBuilder builder)
    {
        string roleId = "f542aa5e-a76e-4f80-9ac3-77d40413fcdf";
        string userId = "85691b8a-93d0-4e5f-a411-389b421ba694";

        //Seeding a  'Tester' role to AspNetRoles table
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleId,
            Name = "Requester",
            NormalizedName = "REQUESTER"
        });

        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();


        //Seeding Admin to AspNetUsers table
        IdentityUser user = new IdentityUser
        {
            Id = userId, // primary key
            UserName = "requester1@planandtrack.com",
            NormalizedUserName = "REQUESTER1@PLANANDTRACK.COM",
            Email = "requester1@planandtrack.com",
            NormalizedEmail = "REQUESTER1@PLANANDTRACK.COM",
            EmailConfirmed = true,
            LockoutEnabled = false
        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Requester12*");

        builder.Entity<IdentityUser>().HasData(user);

        //Seeding the relation between our user and role to AspNetUserRoles table
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            }
        );
    }

}

