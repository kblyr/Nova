namespace Nova.HRIS.Entities;

sealed class PositionETC : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position", "ETC");

        builder.HasOne(_ => _.Parent)
            .WithMany(_ => _.Children)
            .HasForeignKey(_ => _.ParentId);
    }
}
