using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreSegundoExamenSecurity.Context;
using MvcCoreSegundoExamenSecurity.Models;
using System.Collections.Generic;

namespace MvcCoreSegundoExamenSecurity.Repositories
{
    #region PROCEDURES
    //    CREATE PROCEDURE SP_INSERTAR_PEDIDO
    //(@FACTURA INT,
    //@IDLIBRO INT,
    //@IDUSUARIO INT,
    //@FECHA DATE)
    //AS
    //    DECLARE @ID INT

    //    SELECT @ID = MAX(IDPEDIDO) + 1 FROM PEDIDOS

    //    INSERT INTO PEDIDOS VALUES(@ID, @FACTURA, @FECHA, @IDLIBRO, @IDUSUARIO, 1)
    //GO
    #endregion
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

        public async Task<List<Libro>>
            GetLibrosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
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

        public List<VistaPedido> GetVistaPedidos()
        {
            var consulta = from datos in context.VistaPedidos
                           select datos;
            return consulta.ToList();
        }

        public List<VistaPedido> GetVistaPedidosUsuario(int idUsuario)
        {
            var consulta = from datos in context.VistaPedidos
                           where datos.IdUsuario == idUsuario
                           select datos;
            return consulta.ToList();
        }

        public int GetNumFactura()
        {
            int maxFactura = context.Pedidos.Max(p => p.IdFactura);
            return maxFactura + 1;
        }

        public void InsertarPedido(int factura, int idlibro, int idusuario)
        {
            string sql = "SP_INSERTAR_PEDIDO @FACTURA, @IDLIBRO, @IDUSUARIO, @FECHA";
            SqlParameter pfac = new SqlParameter("@FACTURA", factura);
            SqlParameter plib = new SqlParameter("@IDLIBRO", idlibro);
            SqlParameter pusu = new SqlParameter("@IDUSUARIO", idusuario);
            SqlParameter pfe = new SqlParameter("@FECHA", DateTime.Now);

            this.context.Database.ExecuteSqlRaw(sql, pfac, plib, pusu, pfe);

        }

    }
}
