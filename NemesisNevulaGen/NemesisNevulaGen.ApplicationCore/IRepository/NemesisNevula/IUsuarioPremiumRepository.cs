
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IUsuarioPremiumRepository
{
void setSessionCP (GenericSessionCP session);

UsuarioPremiumEN ReadOIDDefault (int id
                                 );

void ModifyDefault (UsuarioPremiumEN usuarioPremium);

System.Collections.Generic.IList<UsuarioPremiumEN> ReadAllDefault (int first, int size);



int CrearUsuarioPremium (UsuarioPremiumEN usuarioPremium);

void ModificarUsuarioPremium (UsuarioPremiumEN usuarioPremium);


void BorrarUsuarioPremium (int id
                           );
}
}
