namespace Proyecto_Venta_Productos_Lacteos.Models.Interfaces
{
    public interface ICRUDConexion<T, M>
    {
        bool create(T model);
        List<M> read();
        M read_by_code(int cod);
        bool update(T model);
        bool delete(int cod);
    }
}
