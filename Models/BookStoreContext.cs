using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models;

public class BookStoreContext:DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options):base(options){}
    
    public DbSet<Book> Book { get; set; }
    public DbSet<BookInventory>  BookInventory { get; set; }
    public DbSet<Credit> Credit { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderBook>  OrderBook { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<PurchaseBookSupplier>  PurchaseBookSupplier { get; set; }
    public DbSet<StockRecord>  StockRecord { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<SupplierBook>  SupplierBook { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("book");
        modelBuilder.Entity<BookInventory>(entity =>
        {
            entity.ToTable("book_inventory");
            entity.HasKey(e => e.BookId);
            
        });
        modelBuilder.Entity<Credit>(entity =>
        {
            entity.ToTable("credit");
            entity.HasKey(e => e.Level);
        });
        modelBuilder.Entity<Customer>().ToTable("customer");
        modelBuilder.Entity<Order>().ToTable("order");
        modelBuilder.Entity<OrderBook>(entity =>
        {
            entity.ToTable("order_book");
            entity.HasKey(e => new { e.OrderId, e.BookId });
        });
        modelBuilder.Entity<Purchase>().ToTable("purchase");
        modelBuilder.Entity<PurchaseBookSupplier>(entity =>
        {
            entity.ToTable("purchase_book_supplier");
            entity.HasKey(e => new { e.PurchaseId, e.BookId,e.SupplierId });
        });
        modelBuilder.Entity<StockRecord>(entity =>
        {
            entity.ToTable("stock_record");
            entity.HasKey(e => e.BookId);
        });
        modelBuilder.Entity<Supplier>().ToTable("supplier");
        modelBuilder.Entity<SupplierBook>(entity =>
        {
            entity.ToTable("supplier_book");
            entity.HasKey(e => new { e.SupplierId, e.BookId });
        });
    }
}