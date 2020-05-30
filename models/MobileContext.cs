using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mobileAPI.Models
{
    public partial class MobileContext : DbContext
    {
        public MobileContext()
        {
        }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<BiometricAttendance> BiometricAttendance { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<HolidayMaster> HolidayMaster { get; set; }
        public virtual DbSet<LeaveApplDetails> LeaveApplDetails { get; set; }
        public virtual DbSet<LeaveBalanceMaster> LeaveBalanceMaster { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequest { get; set; }
        public virtual DbSet<LeaveTypes> LeaveTypes { get; set; }
        public virtual DbSet<PartnerCreation> PartnerCreation { get; set; }
        public virtual DbSet<PartnerType> PartnerType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<TourAdvance> TourAdvance { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<VisitResion> VisitResion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.2.26;Database=Mobile;User Id=sa; pwd=dotnet@!@#; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CompanyCode)
                    .HasColumnName("companyCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCode).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BiometricAttendance>(entity =>
            {
                entity.Property(e => e.CompanyCode).HasMaxLength(20);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Device).HasMaxLength(50);

                entity.Property(e => e.Direction)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ext)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.OutTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.CompanyCode)
                    .HasName("PK_Companies_1");

                entity.Property(e => e.CompanyCode).HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.Address1).HasMaxLength(40);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.Address4).HasMaxLength(40);

                entity.Property(e => e.BooksBeginFrom).HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Ext1).HasMaxLength(20);

                entity.Property(e => e.Ext2).HasMaxLength(20);

                entity.Property(e => e.Ext3).HasMaxLength(50);

                entity.Property(e => e.Ext4).HasMaxLength(50);

                entity.Property(e => e.FinacialYear).HasMaxLength(50);

                entity.Property(e => e.Gstno)
                    .HasColumnName("GSTNo")
                    .HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NatureOfBusiness).HasMaxLength(50);

                entity.Property(e => e.PanNo).HasMaxLength(20);

                entity.Property(e => e.Phone1)
                    .HasColumnName("Phone_1")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone2)
                    .HasColumnName("Phone_2")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone3)
                    .HasColumnName("Phone_3")
                    .HasMaxLength(20);

                entity.Property(e => e.PinCode).HasMaxLength(6);

                entity.Property(e => e.Place).HasMaxLength(30);

                entity.Property(e => e.State).HasMaxLength(20);

                entity.Property(e => e.TanNo).HasMaxLength(20);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyCode).HasMaxLength(50);

                entity.Property(e => e.CompanyDesc).HasMaxLength(50);

                entity.Property(e => e.CompanyGroupCode).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.ResponsiblePersonCode).HasMaxLength(50);

                entity.Property(e => e.ResponsiblePersonDesc).HasMaxLength(50);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesigCode);

                entity.Property(e => e.DesigCode)
                    .HasColumnName("Desig_Code")
                    .HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyCode).HasMaxLength(50);

                entity.Property(e => e.CompanyDesc).HasMaxLength(50);

                entity.Property(e => e.CompanyGroupCode).HasMaxLength(50);

                entity.Property(e => e.DesigName)
                    .HasColumnName("Desig_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.DesigShortName)
                    .HasColumnName("Desig_Short_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Formno)
                    .HasColumnName("formno")
                    .HasMaxLength(50);

                entity.Property(e => e.GradeCode)
                    .HasColumnName("Grade_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasMaxLength(50);

                entity.Property(e => e.Trmno)
                    .HasColumnName("trmno")
                    .HasMaxLength(50);

                entity.Property(e => e.Usrid)
                    .HasColumnName("usrid")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Aadhar).HasMaxLength(50);

                entity.Property(e => e.AccessCard).HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.ApprovedBy).HasMaxLength(50);

                entity.Property(e => e.BankAccNo).HasMaxLength(50);

                entity.Property(e => e.BankBranch).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BolldGroup).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CompCode).HasMaxLength(50);

                entity.Property(e => e.Designation).HasMaxLength(20);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Esinumber)
                    .HasColumnName("ESINumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Ext1).HasMaxLength(20);

                entity.Property(e => e.Ext2).HasMaxLength(20);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.Ifsccode)
                    .HasColumnName("IFSCCode")
                    .HasMaxLength(20);

                entity.Property(e => e.JoinDate).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Pan).HasMaxLength(50);

                entity.Property(e => e.Pfnumber)
                    .HasColumnName("PFNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone1)
                    .HasColumnName("Phone_1")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone2)
                    .HasColumnName("Phone_2")
                    .HasMaxLength(20);

                entity.Property(e => e.RecommendedBy).HasMaxLength(50);

                entity.Property(e => e.RelDate).HasMaxLength(50);

                entity.Property(e => e.ReportingTo).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Shift).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Uannumber)
                    .HasColumnName("UANNumber")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<HolidayMaster>(entity =>
            {
                entity.HasKey(e => e.Date);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyCode).HasMaxLength(50);

                entity.Property(e => e.CompanyDesc).HasMaxLength(50);

                entity.Property(e => e.Divisions).HasMaxLength(50);

                entity.Property(e => e.Holiday).HasMaxLength(50);

                entity.Property(e => e.Holidaytype).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ProfitCenterCode).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.TimeStamp).HasMaxLength(50);

                entity.Property(e => e.Userid).HasMaxLength(50);

                entity.Property(e => e.Year).HasMaxLength(50);
            });

            modelBuilder.Entity<LeaveApplDetails>(entity =>
            {
                entity.HasKey(e => e.Sno)
                    .HasName("PK_Leave_Appl_Details_1");

                entity.ToTable("Leave_Appl_Details");

                entity.Property(e => e.AcceptedRemarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApplDate)
                    .HasColumnName("Appl_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedId)
                    .HasColumnName("ApprovedID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasColumnName("Emp_Code")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.EmpName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ext1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ext2).HasMaxLength(50);

                entity.Property(e => e.HalfDayTo)
                    .HasColumnName("halfDayTo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HalfdayFrom)
                    .HasColumnName("halfdayFrom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveCode)
                    .HasColumnName("Leave_Code")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.LeaveDays).HasColumnName("Leave_Days");

                entity.Property(e => e.LeaveFrom)
                    .HasColumnName("Leave_From")
                    .HasColumnType("date");

                entity.Property(e => e.LeaveRemarks)
                    .HasColumnName("Leave_Remarks")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTo)
                    .HasColumnName("Leave_To")
                    .HasColumnType("date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RecommendedId)
                    .HasColumnName("RecommendedID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RecommendedName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveBalanceMaster>(entity =>
            {
                entity.HasKey(e => new { e.EmpCode, e.Year, e.LeaveCode });

                entity.Property(e => e.EmpCode).HasMaxLength(50);

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveCode).HasMaxLength(50);

                entity.Property(e => e.CompCode).HasMaxLength(50);

                entity.Property(e => e.Opbal).HasColumnName("OPBAL");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.Property(e => e.ApplyDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoOfDays)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.Property(e => e.ToDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Upload)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypes>(entity =>
            {
                entity.HasKey(e => e.LeaveCode);

                entity.Property(e => e.LeaveCode).HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CompanyCode).HasMaxLength(20);

                entity.Property(e => e.Ext1).HasMaxLength(50);

                entity.Property(e => e.LeaveName).HasMaxLength(50);
            });

            modelBuilder.Entity<PartnerCreation>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.AddWho).HasMaxLength(50);

                entity.Property(e => e.Address1).HasMaxLength(400);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.Address4).HasMaxLength(40);

                entity.Property(e => e.Balance).HasMaxLength(50);

                entity.Property(e => e.BalanceType).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CompCode).HasMaxLength(20);

                entity.Property(e => e.ContactPerson).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.EditDate).HasColumnType("datetime");

                entity.Property(e => e.EditWho).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Ext1).HasMaxLength(20);

                entity.Property(e => e.Ext2).HasMaxLength(20);

                entity.Property(e => e.Ext3).HasMaxLength(50);

                entity.Property(e => e.Ext4).HasMaxLength(50);

                entity.Property(e => e.Ext5).HasMaxLength(50);

                entity.Property(e => e.GlcontrolAcc)
                    .HasColumnName("GLControlAcc")
                    .HasMaxLength(50);

                entity.Property(e => e.Gstno)
                    .HasColumnName("GSTNo")
                    .HasMaxLength(20);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.Nacture).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Partnertype).HasMaxLength(50);

                entity.Property(e => e.Phone1)
                    .HasColumnName("Phone_1")
                    .HasMaxLength(20);

                entity.Property(e => e.Phone2)
                    .HasColumnName("Phone_2")
                    .HasMaxLength(20);

                entity.Property(e => e.PinCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(20);
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.AccountType).HasMaxLength(50);

                entity.Property(e => e.Active)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Ext1).HasMaxLength(20);

                entity.Property(e => e.Ext2).HasMaxLength(20);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Ext1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TourAdvance>(entity =>
            {
                entity.HasKey(e => e.ToureId)
                    .HasName("PK_Tour_Advance_1");

                entity.Property(e => e.AcceptedAmount).HasColumnName("Accepted_Amount");

                entity.Property(e => e.AcceptedBy)
                    .HasColumnName("Accepted_By")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AcceptedDate)
                    .HasColumnName("Accepted_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.AcceptedRemarks)
                    .HasColumnName("Accepted_Remarks")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AcceptedStatus)
                    .HasColumnName("Accepted_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ApplyDate)
                    .HasColumnName("Apply_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApprovedBy)
                    .HasColumnName("Approved_By")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ApprovedDate)
                    .HasColumnName("Approved_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApprovedRemarks)
                    .HasColumnName("Approved_Remarks")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedStatus)
                    .HasColumnName("Approved_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AuthorisedBy)
                    .HasColumnName("Authorised_By")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AuthorisedDate)
                    .HasColumnName("Authorised_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthorisedRemarks)
                    .HasColumnName("Authorised_Remarks")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorisedStatus)
                    .HasColumnName("Authorised_Status")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasColumnName("Emp_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JourneyDate)
                    .HasColumnName("Journey_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Jtime)
                    .HasColumnName("JTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModeOfTransport)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PlaceOfVisiting)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.Skip)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasColumnName("Email_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locatation)
                    .HasColumnName("locatation")
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VisitType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VisitedEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VisitResion>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ext1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VisitName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VisitType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
