
using System;
using System.Text;

using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CP.NemesisNevula_UsuarioPremium_crearUsuarioPremium) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
    public partial class UsuarioPremiumCP : GenericBasicCP
    {
        public NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioPremiumEN CrearUsuarioPremium(string p_nombre, string p_correo, bool p_conGoogle, string p_foto_perfil, int p_puntosNevula, float p_cartera, String p_pass)
        {
            /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CP.NemesisNevula_UsuarioPremium_crearUsuarioPremium) ENABLED START*/

            UsuarioPremiumCEN usuarioPremiumCEN = null;
            UsuarioCEN usuarioCEN = null;

            NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioPremiumEN result = null;


            try
            {
                CPSession.SessionInitializeTransaction();
                usuarioPremiumCEN = new UsuarioPremiumCEN(CPSession.UnitRepo.UsuarioPremiumRepository);
                usuarioCEN = new UsuarioCEN(CPSession.UnitRepo.UsuarioRepository);

                IList<UsuarioEN> usuario = usuarioCEN.DamePorNombre(p_nombre);

                if (usuario[0].Cartera < 10)
                {
                    return null;
                }
                else { usuario[0].Cartera = usuario[0].Cartera - 10; }

                int oid;
                //Initialized UsuarioPremiumEN
                UsuarioPremiumEN usuarioPremiumEN;

                usuarioPremiumEN = new UsuarioPremiumEN();
                usuarioPremiumEN.Nombre = p_nombre;

                usuarioPremiumEN.Correo = p_correo;

                usuarioPremiumEN.ConGoogle = p_conGoogle;

                usuarioPremiumEN.Foto_perfil = p_foto_perfil;

                usuarioPremiumEN.PuntosNevula = p_puntosNevula;

                usuarioPremiumEN.Cartera = p_cartera;

                usuarioPremiumEN.Pass = Utils.Util.GetEncondeMD5(p_pass);

                IList<int> aux = new List<int>();

                foreach (ArticuloEN art in usuario[0].Articulo)
                {
                    aux.Add(art.Id);
                }
                foreach (MetodoPagoEN metodo in usuario[0].MetodoPago)
                {
                    usuarioPremiumEN.MetodoPago.Add(metodo);
                }

                oid = usuarioPremiumCEN.get_IUsuarioPremiumRepository().CrearUsuarioPremium(usuarioPremiumEN);
                usuarioCEN.AnyadirArticulo(oid, aux);

                result = usuarioPremiumCEN.get_IUsuarioPremiumRepository().ReadOIDDefault(oid);
                CPSession.Commit();
                usuarioCEN.BorrarUsuario(usuario[0].Id);
            }
            catch (Exception ex)
            {
                CPSession.RollBack();
                throw ex;
            }
            finally
            {
                CPSession.SessionClose();
            }
            return result;

            /*PROTECTED REGION END*/
        }
    }
}
