using Microsoft.EntityFrameworkCore;
using MvcCoreSegundoExamenSecurity.Context;
using MvcCoreSegundoExamenSecurity.Models;

namespace MvcCoreSegundoExamenSecurity.Repositories
{
    public class RepositoryExamen
    {
        private LibrosContext context;

        public RepositoryExamen(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Libro>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosGenero(int idgenero)
        {
            var consulta = from datos in context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }

        public async Task<List<Genero>> GetGeneroAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            Usuario user = await this.context.Usuarios.Where(
                z => z.Email == email && z.Pass == password
                ).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Usuario> FindUsuarioAsync(int idUser)
        {
            Usuario usuario = await this.context.Usuarios.FirstOrDefaultAsync(
                x => x.IdUsuario == idUser
                );
            return usuario;
        }

        public async Task<Libro> FindLibroAsync(int idLibro)
        {
            Libro libro = await this.context.Libros.FirstOrDefaultAsync(
                x => x.IdLibro == idLibro
                );
            return libro;
        }
    }
}
