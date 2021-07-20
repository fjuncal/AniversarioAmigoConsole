using System;
using System.Collections.Generic;
using System.Data;
using aniversario_repository.Entity;

namespace aniversario_repository.RepositoryAmigo
{
    public interface IRepositoryAmigo
    {
        void Create(Amigo amigo);
        void Update(Amigo amigo, int id);
        void Delete(int id);
        DataTable GetAmigo(int id);
        DataTable GetAll();

        DataTable GetAmigoPalavraChave(String nome);
    }
}