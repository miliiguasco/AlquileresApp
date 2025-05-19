using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using AlquileresApp.Core.Servicios;
using AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core;

try
        {
            // Crear instancia de usuario registrado
            var usuario = new UsuarioRegistrado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan.perez@test.com",
                Telefono = "1234567890",
                Password = "Password123!",
                FechaNacimiento = new DateTime(1990, 1, 1),
                FechaRegistro = DateTime.Now
            };

            // Crear instancias necesarias
            var dbContext = new AppDbContext();
            var usuarioRepo = new UsuarioRepositorio(dbContext);
            var validador = new UsuarioValidador();
            var hashService = new ServicioHashPassword();

            // Crear caso de uso
            var registrarUsuario = new CasoDeUsoRegistrarUsuario(usuarioRepo, validador, hashService);

            // Ejecutar el registro
            Console.WriteLine("Intentando registrar usuario...");
            registrarUsuario.Ejecutar(usuario);
            Console.WriteLine("Usuario registrado exitosamente!");

            // Verificar que el usuario fue persistido
            Console.WriteLine("\nVerificando persistencia...");
            var usuarios = usuarioRepo.ListarUsuarios();
            var usuarioRegistrado = usuarios.FirstOrDefault(u => u.Email == "juan.perez@test.com");

            if (usuarioRegistrado != null)
            {
                Console.WriteLine("¡Usuario encontrado en la base de datos!");
                Console.WriteLine($"Nombre: {usuarioRegistrado.Nombre}");
                Console.WriteLine($"Email: {usuarioRegistrado.Email}");
                Console.WriteLine($"Fecha de registro: {((UsuarioRegistrado)usuarioRegistrado).FechaRegistro}");
            }
            else
            {
                Console.WriteLine("Error: Usuario no encontrado en la base de datos");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el proceso: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        