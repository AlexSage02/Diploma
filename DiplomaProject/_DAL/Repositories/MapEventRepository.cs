using DAL;

namespace Project_DAL.Repositories
{
    public class FormRepository : MainRepository<Guid, MapEvent>, IMapEventRepository
    {
        public FormRepository(DatabaseContext db) : base(db)
        {

        }
    }
}
