
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface ITarjetaCreditoRepository
{
void setSessionCP (GenericSessionCP session);

TarjetaCreditoEN ReadOIDDefault (int id
                                 );

void ModifyDefault (TarjetaCreditoEN tarjetaCredito);

System.Collections.Generic.IList<TarjetaCreditoEN> ReadAllDefault (int first, int size);



int CrearTarjetaCredito (TarjetaCreditoEN tarjetaCredito);

void ModificarTarjetaCredito (TarjetaCreditoEN tarjetaCredito);


void BorrarTarjetaCredito (int id
                           );
}
}
