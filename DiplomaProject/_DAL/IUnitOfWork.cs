using Project_DAL.Repositories;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace Project_DAL
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        IMapEventRepository MapEvents { get; }
        public void Save();

	}
}
