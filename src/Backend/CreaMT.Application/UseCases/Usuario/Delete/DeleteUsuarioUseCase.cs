
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Repositories;
using CreaMT.Domain.Services.LoggerUser;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.Domain.Entities;

namespace CreaMT.Application.UseCases.Usuario.Delete;
public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
{
    private readonly IUsuarioUpdateOnlyRepository _updateOnlyRepository;
    private readonly IUsuarioWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUsuarioUseCase(IUsuarioUpdateOnlyRepository updateOnlyRepository,
        IUsuarioWriteOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _updateOnlyRepository = updateOnlyRepository;
        _writeOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(long usuarioId)
    {
        ValidateRequest(usuarioId);

        var usuario = await _updateOnlyRepository.GetById(usuarioId);

        Validate(usuario);

        await _writeOnlyRepository.Delete(usuarioId);

        await _unitOfWork.Commit();
    }

    private static void ValidateRequest(long usuarioId)
    {
        if (usuarioId <= 0)
        {
            throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.USUARIO_ID_INVALID });
        }
    }
    private static void Validate(Domain.Entities.Usuario usuario)
    {
        if (usuario == null)
        {
            throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.USER_NOT_FOUND });
        }
    }
}
