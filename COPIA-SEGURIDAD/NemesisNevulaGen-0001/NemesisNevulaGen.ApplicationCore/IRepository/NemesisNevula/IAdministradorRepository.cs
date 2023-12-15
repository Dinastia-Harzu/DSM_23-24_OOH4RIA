
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IAdministradorRepository
{
void setSessionCP (GenericSessionCP session);

AdministradorEN ReadOIDDefault (int id
                                );

void ModifyDefault (AdministradorEN administrador);

System.Collections.Generic.IList<AdministradorEN> ReadAllDefault (int first, int size);



int CrearAdmin (AdministradorEN administrador);

void ModificarAdmin (AdministradorEN administrador);


void BorrarAdmin (int id
                  );


AdministradorEN DamePorOID (int id
                            );


System.Collections.Generic.IList<AdministradorEN> DameTodos (int first, int size);
}
}
