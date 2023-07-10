namespace PortalNotas.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaAluno> MateriaAluno { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.Materias)
                .WithMany(m => m.Alunos)
            .UsingEntity<MateriaAluno>();

            modelBuilder.Entity<Materia>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<Materia>()
            //    .HasOne(m => m.Professor)
            //    .WithMany()
            //    .HasForeignKey(m => m.ProfessorId)
            //    .IsRequired(false);

            modelBuilder.Entity<Professor>()
                .HasMany(p=>p.Materias)
                .WithOne(m=>m.Professor)
                .HasForeignKey(m=>m.ProfessorId)
                .IsRequired(false);
        }
    }
}
