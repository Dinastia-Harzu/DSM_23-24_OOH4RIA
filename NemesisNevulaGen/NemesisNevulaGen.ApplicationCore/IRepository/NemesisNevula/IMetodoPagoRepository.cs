
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IMetodoPagoRepository
{
void setSessionCP (GenericSessionCP session);

MetodoPagoEN ReadOIDDefault (int id
                             );

void ModifyDefault (MetodoPagoEN metodoPago);

System.Collections.Generic.IList<MetodoPagoEN> ReadAllDefault (int first, int size);



int CrearMetodoPago (MetodoPagoEN metodoPago);

void ModificarMetodoPago (MetodoPagoEN metodoPago);


void BorrarMetodoPago (int id
                       );


MetodoPagoEN DamePorOID (int id
                         );


System.Collections.Generic.IList<MetodoPagoEN> DameTodos (int first, int size);
}
}
