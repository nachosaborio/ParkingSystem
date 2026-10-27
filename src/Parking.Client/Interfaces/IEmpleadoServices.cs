using Parking.Client.Models;

namespace Parking.Client.Interfaces
{
    public interface IEmpleadoServices
    {
        public Task<bool> AddEmpleado(Empleado empleado);
        public Task<List<Empleado>> GetAllEmpleados();
        public Task<Empleado> GetEmpleadoXId(int Id);
        public Task<bool> UpdateEmpleado(Empleado empleado);
        public Task<bool> DeleteEmpleado(int Id);
    }
}
