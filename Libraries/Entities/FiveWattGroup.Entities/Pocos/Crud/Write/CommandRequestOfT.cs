namespace FiveWattGroup.Entities.Pocos.Crud.Write
{
    public class CommandRequest<TEntity> where TEntity : class
    {
        public TEntity Entity { get; set; }
        public bool SaveChanges { get; set; }
    }
}
