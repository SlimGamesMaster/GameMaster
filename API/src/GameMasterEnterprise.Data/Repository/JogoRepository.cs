﻿using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.Data.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(MeuDbContext context) : base(context) { }

        public async Task<Jogo> ObterPorNome(string nome)
        {
            return await Db.Jogo.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<Jogo> ObterPorCodigo(string codigo)
        {
            return await Db.Jogo.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<string> ObterNomeJogo(Guid id)
        {
            var jogo = await Db.Jogo.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return jogo?.Nome;
        }
        public async Task<string> ObterCodigoJogo(Guid id)
        {
            var jogo = await Db.Jogo.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return jogo?.Codigo;
        }


    }
}