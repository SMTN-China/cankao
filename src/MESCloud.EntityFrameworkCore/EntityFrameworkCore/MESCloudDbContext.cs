using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MESCloud.Authorization.Roles;
using MESCloud.Authorization.Users;
using MESCloud.MultiTenancy;
using MESCloud.Entities;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.Entities.WMS.ProduceData;

namespace MESCloud.EntityFrameworkCore
{
    public class MESCloudDbContext : AbpZeroDbContext<Tenant, Role, User, MESCloudDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public virtual DbSet<Menu> Menus { set; get; }

        public virtual DbSet<Org> Orgs { set; get; }

        public virtual DbSet<MenuRoleMap> MenuRoleMap { set; get; }

        public virtual DbSet<BarCodeAnalysis> BarCodeAnalysiss { set; get; }
        public virtual DbSet<BOM> BOMs { set; get; }
        public virtual DbSet<Customer> Customers { set; get; }
        public virtual DbSet<Line> Lines { set; get; }
        public virtual DbSet<MPN> MPNs { set; get; }
        public virtual DbSet<StorageLocation> StorageLocations { set; get; }
        public virtual DbSet<StorageLocationType> StorageLocationTypes { set; get; }

        public virtual DbSet<UPH> UPHs { set; get; }

        public virtual DbSet<Slot> Slots { set; get; }
        public virtual DbSet<Storage> Storages { set; get; }
        public virtual DbSet<Reel> Reels { set; get; }
        public virtual DbSet<ReelSendTemp> ReelSendTemps { set; get; }
        public virtual DbSet<ReelShortTemp> ReelShortTemps { set; get; }
        public virtual DbSet<ReelMoveLog> ReelAllocationLogs { set; get; }
        public virtual DbSet<ReelMoveMethod> ReelAllocationMethods { set; get; }
        public virtual DbSet<WorkBill> WorkBills { set; get; }

        public virtual DbSet<ReadySlot> ReadySlots { set; get; }

        public virtual DbSet<ReelSupplyTemp> ReelSupplyTemps { set; get; }

        public virtual DbSet<MPNStorageAreaMap> MPNStorageAreaMaps { set; get; }
        public virtual DbSet<StorageArea> StorageAreas { set; get; }

        public virtual DbSet<ReadyMBillDetailed> ReadyMBillDetaileds { set; get; }
        public virtual DbSet<WorkBillDetailed> WorkBillDetaileds { set; get; }

        public virtual DbSet<ReadyMBill> ReadyMBills { set; get; }

        public virtual DbSet<ReadyMBillWorkBillMap> ReadyMBillWorkBillMaps { set; get; }

        public virtual DbSet<ReceivedReelBill> ReceivedReelBills { set; get; }

        public virtual DbSet<RMMStorageMap> RMMStorageMaps { set; get; }

        public virtual DbSet<I18N> I18Ns { set; get; }



        public MESCloudDbContext(DbContextOptions<MESCloudDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BarCodeAnalysis>(b => b.ToTable("MesWMSBarCodeAnalysis").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<BOM>(b => b.ToTable("MesWMSBOM").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<Customer>(b => b.ToTable("MesWMSCustomer").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<Line>(b => b.ToTable("MesWMSLine").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<MPN>(b => b.ToTable("MesWMSMPN").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<StorageLocation>(b => { b.ToTable("MesWMSStorageLocation").HasIndex(r => r.ReelId); b.Property(t => t.Id).HasMaxLength(30); });
            modelBuilder.Entity<StorageLocationType>(b => b.ToTable("MesWMSStorageLocationType").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<Slot>(b => b.ToTable("MesWMSSlot"));  // int
            modelBuilder.Entity<Storage>(b => b.ToTable("MesWMSStorage").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<Reel>(b => { b.ToTable("MesWMSReel").HasIndex(r => r.StorageLocationId);b.Property(t => t.Id).HasMaxLength(60); });
            modelBuilder.Entity<ReelSendTemp>(b => b.ToTable("MesWMSReelSendTemp").Property(t => t.Id).HasMaxLength(60));
            modelBuilder.Entity<ReelShortTemp>(b => b.ToTable("MesWMSReelShortTemp").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<ReelSupplyTemp>(b => b.ToTable("MesWMSReelSupplyTemp").Property(t => t.Id).HasMaxLength(60));
            modelBuilder.Entity<ReadySlot>(b => b.ToTable("MesWMSReadySlot").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<StorageArea>(b => b.ToTable("MesWMSStorageArea").Property(t => t.Id).HasMaxLength(30));

            modelBuilder.Entity<ReelMoveLog>(b => b.ToTable("MesWMSReelMoveMethodLog").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<ReelMoveMethod>(b => b.ToTable("MesWMSReelMoveMethod").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<WorkBill>(b => b.ToTable("MesWMSWorkBill").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<ReadyMBill>(b => b.ToTable("MesWMSReadyMBill").Property(t => t.Id).HasMaxLength(30));
            modelBuilder.Entity<UPH>(b => b.ToTable("MesWMSUPH").Property(t => t.Id).HasMaxLength(36));

            modelBuilder.Entity<ReadyMBillDetailed>(b => b.ToTable("MesWMSReadyMBillDetailed").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<WorkBillDetailed>(b => b.ToTable("MesWMSWorkBillDetailed").Property(t => t.Id).HasMaxLength(36));
            modelBuilder.Entity<ReceivedReelBill>(b => b.ToTable("MesWMSReceivedReelBill").Property(t => t.Id).HasMaxLength(36));


            modelBuilder.Entity<ReadyMBillWorkBillMap>(b =>
            {
                b.ToTable("MesWMSReadyMBillWorkBillMap");

                b.Property(t => t.Id).HasMaxLength(36);

                b.HasOne(pt => pt.WorkBill).WithMany(t => t.ReadyMBills).HasForeignKey(pt => pt.WorkBillId);

                b.HasOne(pt => pt.ReadyMBill).WithMany(t => t.WorkBills).HasForeignKey(pt => pt.ReadyMBillId);
            }

            );

            modelBuilder.Entity<RMMStorageMap>(b =>
            {
                b.ToTable("MesWMSRMMStorageMap");

                b.Property(t => t.Id).HasMaxLength(36);

                b.HasOne(pt => pt.ReelMoveMethod).WithMany(t => t.OutStorages).HasForeignKey(pt => pt.ReelMoveMethodId);

                b.HasOne(pt => pt.Storage).WithMany(t => t.ReelMoveMethods).HasForeignKey(pt => pt.StorageId);
            }

           );

            modelBuilder.Entity<MPNStorageAreaMap>(b =>
            {
                b.ToTable("MesWMSMPNStorageAreaMap");

                b.Property(t => t.Id).HasMaxLength(36);

                b.HasOne(pt => pt.MPN).WithMany(t => t.StorageAreas).HasForeignKey(pt => pt.MPNId);

                b.HasOne(pt => pt.StorageArea).WithMany(t => t.MPNs).HasForeignKey(pt => pt.StorageAreaId);
            });


            modelBuilder.Entity<Menu>(b => b.ToTable("MesSysMenu"));

            modelBuilder.Entity<Org>(b => b.ToTable("MesSysOrg"));

            modelBuilder.Entity<I18N>(b => b.ToTable("MesSysI18N"));

            modelBuilder.Entity<Role>(b => b.HasOne(pt => pt.Org).WithMany(t => t.Roles).HasForeignKey(pt => pt.OrgId));

            modelBuilder.Entity<MenuRoleMap>(builder =>
            {

                builder.ToTable("MesSysMenuRoleMap");

                builder
                    .HasOne(pt => pt.Menu)
                    .WithMany(t => t.Roles)
                    .HasForeignKey(pt => pt.MenuId);

                builder
                    .HasOne(pt => pt.Role)
                    .WithMany(t => t.Menus)
                    .HasForeignKey(pt => pt.RoleId);
            });

        }
    }
}
