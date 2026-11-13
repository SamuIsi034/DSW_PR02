using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    public List<Evento> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Evento? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public Evento Agregar(Evento nuevoEvento)
    {
        ValidarEvento(nuevoEvento);

        return _repository.Agregar(nuevoEvento);
    }

    public bool Actualizar(int id, Evento evento)
    {
        var existente = _repository.ObtenerPorId(id);
        if (existente == null)
        {
            return false;
        }

        ValidarEvento(evento);

        evento.Id = id;
        return _repository.Actualizar(id, evento);
    }

    public bool Eliminar(int id)
    {
        var existente = _repository.ObtenerPorId(id);
        if (existente == null)
        {
            return false;
        }

        return _repository.Eliminar(id);
    }

    private void ValidarEvento(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
        {
            throw new ArgumentException("El nombre del evento es obligatorio.");
        }

        if (evento.Cupo < 0)
        {
            throw new ArgumentException("El cupo del evento no puede ser menor a 0.");
        }
        
        if (evento.Fecha < DateTime.Now)
        {
            throw new ArgumentException("La fecha del evento no puede tener una fecha inválida.");
        }
    }
}
