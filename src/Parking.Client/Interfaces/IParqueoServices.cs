using Proyecto1.Models;

namespace Proyecto2Client.Interfaces
{
    public interface IParqueoServices
    {
        public Task<bool> AddParqueo(Parqueo Parqueo);
        public Task<List<Parqueo>> GetAllParqueos();
        public Task<Parqueo> GetParqueoXId(int Id);
        public Task<bool> UpdateParqueo(Parqueo Parqueo);
        public Task<bool> DeleteParqueo(int Id);
    }
}
