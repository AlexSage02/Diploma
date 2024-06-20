using Microsoft.AspNetCore.Identity;
using Project_DAL;
using Project_DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
		private readonly DatabaseContext db = new();

        private IEventRepository? eventGenRepository;
        private IMapEventRepository? mapEventGenRepository;
		
		public IEventRepository Events
        {
            get
            {
                eventGenRepository ??= new EventRepository(db);
                return eventGenRepository;
            }
        }

        public IMapEventRepository MapEvents
        {
            get
            {
                mapEventGenRepository ??= new FormRepository(db);
                return mapEventGenRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
		

		private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
