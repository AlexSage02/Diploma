using DAL;

namespace Project_DAL.Repositories
{
    public class EventRepository : MainRepository<Guid, Event>, IEventRepository
    {
        public EventRepository(DatabaseContext db) : base(db)
        {

        }
    }
}
