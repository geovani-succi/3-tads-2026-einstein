using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EinsteinGestaoAcademica.Dominio.Entidades;
using EinsteinGestaoAcademica.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace EinsteinGestaoAcademica.Dados.Data.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Criar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObterUsuarioPorEmailESenha(string email, string senha)
        {
            return await _context.Usuarios
                .FromSqlRaw("SELECT * FROM public.usuario WHERE email = {0} AND senha = {1}", email, senha)
                .FirstOrDefaultAsync();
        }
    }
}