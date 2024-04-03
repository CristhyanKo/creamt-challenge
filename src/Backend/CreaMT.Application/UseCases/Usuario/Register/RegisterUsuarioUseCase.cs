using AutoMapper;
using CreaMT.Application.Services.AutoMapper;
using CreaMT.Application.Services.Cryptography;
using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using CreaMT.Domain.Repositories;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;
using FluentValidation;

namespace CreaMT.Application.UseCases.Usuario.Register;
public  class RegisterUsuarioUseCase : IRegisterUsuarioUseCase
{   
    private readonly IUsuarioWriteOnlyRepository _writeOnlyRepository;
    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IMapper _mapper;

    public RegisterUsuarioUseCase(IUsuarioWriteOnlyRepository writeOnlyRepository, 
        IUsuarioReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        PasswordEncripter passwordEncripter)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _mapper = mapper;

    }

    public async Task<ResponseRegisteredUsuariosJson>  Execute(RequestRegisterUsuarioJson request)
    {
        await Validate(request);

        var Usuario = _mapper.Map<Domain.Entities.Usuario>(request);

        Usuario.Senha = _passwordEncripter.Encrypt(request.Senha);

        await _writeOnlyRepository.Add(Usuario);

        await _unitOfWork.Commit();
        return new ResponseRegisteredUsuariosJson
        {
            Nome = request.Nome,
        };
    }

    public async Task Validate(RequestRegisterUsuarioJson request)
    {
        var validator = new RegisterUsuarioValidator();
        var result = validator.Validate(request);

       var emailExist = await _readOnlyRepository.ExistActiveUsuarioWithEmail(request.Email);
       var cpfCnpjExist = await _readOnlyRepository.ExistActiveUsuarioWithCpfCnpj(request.CpfCnpj);


        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        if (cpfCnpjExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CPF_CNPJ_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {  
            var errorsMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
