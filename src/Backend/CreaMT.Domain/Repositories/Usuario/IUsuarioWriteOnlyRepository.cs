﻿namespace CreaMT.Domain.Repositories.Usuario;
public interface IUsuarioWriteOnlyRepository
{
    public Task Add(Entities.Usuario usuario);
    public Task Delete(long usuarioId);
}
