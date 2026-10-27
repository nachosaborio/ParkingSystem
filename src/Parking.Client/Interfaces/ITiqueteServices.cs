using Proyecto1.Models;

namespace Proyecto2Client.Interfaces
{
    public interface ITiqueteServices
    {
        public Task<bool> AddTiquete(Tiquete Tiquete);
        public Task<List<Tiquete>> GetAllTiquetes();
        public Task<Tiquete> GetTiqueteXId(int Id);
        public Task<bool> UpdateTiquete(Tiquete Tiquete);
        public Task<bool> DeleteTiquete(int Id);
    }
}
