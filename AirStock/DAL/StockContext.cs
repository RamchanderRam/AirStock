using AirStock.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AirStock.Services;   
using AirStock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirStock.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AirStock.DAL;
using System;
using System.Collections.Generic;


#nullable disable

namespace AirStock.DAL
{

    public class StockContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        //private readonly IHttpClientBuilder _httpClientBuilder;
        private readonly IServiceRoutine _sr;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IStockService _stockService; 
        //private readonly DbContext _dbContext;
        private readonly ILogger _logger;
        //public StockContext(DbContextOptions<StockContext> options , IConfiguration configuration, IServiceRoutine sr, 
        //    IHttpContextAccessor httpContextAccessor,  DbContext dbContext) : base(options)
        public StockContext(DbContextOptions<StockContext> options , ILogger logger, IConfiguration configuration, IServiceRoutine sr/*, DbContext dbContext*/ ) : base(options)
        {
            _configuration = configuration;
            //_stockService = stockService;
            //_configuration = configuration;
            _sr = sr;
            //_httpContextAccessor = httpContextAccessor;
            //_dbContext = dbContext;
            _logger = logger;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("StockConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<JobCardModel>()
            //.HasOne(j => j.Vehicle)
            //.WithMany(v => v.JobCards)
            //.HasForeignKey(j => j.VehicleId);

            //// Configure the relationship between JobCardModel and SparePartModel
            //modelBuilder.Entity<SparePartModel>()
            //    .HasOne(s => s.JobCard)
            //    .WithMany(j => j.SpareParts)
            //    .HasForeignKey(s => s.JobCardId);

            //modelBuilder.Entity<VehicleModel>().ToTable("Vehicles");
            //modelBuilder.Entity<JobCardModel>().ToTable("JobCards");
            //modelBuilder.Entity<SparePartModel>().ToTable("SpareParts");
            //modelBuilder.Entity<SparePart>()
            //   .HasOne(sp => sp.JobCard)
            //   .WithMany(jc => jc.SpareParts)
            //   .HasForeignKey(sp => sp.JobCardId)
            //   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });

            // Configure IdentityUserLogin with a primary key
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
        }
        // DbSet properties for your entities...

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<NewProduct> Products { get; set; }

        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<JobCard> JobCards { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }



    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // Define the relationship between Stock and Product
    //    modelBuilder.Entity<Stock>()
    //        .HasOne(s => s.Product)
    //        .WithMany(p => p.Stocks)
    //        .HasForeignKey(s => s.ProductId);
    //}



    [Table("Stock")]
        public class Stock 
           {
            [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int StockId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }
            public DateTime? ProductDate { get; set; }
            public bool IsActive { get; set; } = true;

        }

    [Table("NewProduct")]
    public class NewProduct
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
       

    }

    [Table("UserModel")]
    public class UserModel
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } // Primary Key
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //public int UserRoleId { get; set; }

        public string RoleName { get; set; }
    }

    [Table("Vehicles")]
    public class Vehicle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string FleetNumber { get; set; }
        public string Division { get; set; }
        public int DriverKm { get; set; }
        public DateTime? DateOfLastService { get; set; }

        // Navigation property
        public List<JobCard> JobCards { get; set; }
    }

    // JobCard.cs
    [Table("JobCards")]
    public class JobCard
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobCardId { get; set; }
        public string BriefDescriptionOfWork { get; set; }
        public string SpareForService { get; set; }
        public string JobCardQuantity { get; set; }
        public int Cost { get; set; }
        public string FittedBy { get; set; }
        public string Total { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string ChiefMechanicSignature { get; set; }

        // Navigation properties
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public List<SparePart> SpareParts { get; set; }
    }

    // SparePart.cs
    [Table("SpareParts")]
    public class SparePart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SparePartId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        
        public string SparePartCost { get; set; }
        public string SparePartAmount { get; set; }
        public int JobCardId { get; set; }
        public JobCard JobCard { get; set; }
    }

    //[Table("DataImport")]
    //public class DataImport : ManageFields
    //{
    //    [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int ImportId { get; set; }
    //    public string ImportType { get; set; }
    //    public DateTime? CompletedOn { get; set; }
    //    public string Status { get; set; }
    //    public string FileName { get; set; }
    //    public double? FileSize { get; set; }
    //    public string FilePath { get; set; }

    //}

    public class ManageFields
        {
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
        }
    }


