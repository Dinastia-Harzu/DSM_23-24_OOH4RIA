
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IUsuarioRepository
{
void setSessionCP (GenericSessionCP session);

UsuarioEN ReadOIDDefault (int id
                          );

void ModifyDefault (UsuarioEN usuario);

System.Collections.Generic.IList<UsuarioEN> ReadAllDefault (int first, int size);



int CrearUsuario (UsuarioEN usuario);

void ModificarUsuario (UsuarioEN usuario);


void BorrarUsuario (int id
                    );







void AnyadirFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs);

void QuitarFavorito (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulosFavs_OIDs);


System.Collections.Generic.IList<UsuarioEN> DameTodos (int first, int size);


UsuarioEN DamePorOID (int id
                      );


System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.ArticuloEN> DameArticulosComprados (int p_idUser);


void AnyadirArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulo_OIDs);

void QuitarArticulo (int p_Usuario_OID, System.Collections.Generic.IList<int> p_articulo_OIDs);

System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.UsuarioEN> DamePorNombre (string p_nombre);
}
}
