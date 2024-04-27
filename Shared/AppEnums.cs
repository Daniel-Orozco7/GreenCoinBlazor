using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCoinHealth.Shared
{
    public class AppEnums
    {
        public enum UserDocType
        {
            Seleccione,
            DNI,
            CC,
            CE,
            PAS
        }

        public enum UserRole : int
        {
            Seleccione = 0,
            Administrador = 1,
            Usuario = 2,
            Nutricionista = 3
        }

        public enum UserGender : int
        {
            Seleccione = 0,
            Masculino = 1,
            Femenino = 2,
            Otro = 3
        }

    }
}
