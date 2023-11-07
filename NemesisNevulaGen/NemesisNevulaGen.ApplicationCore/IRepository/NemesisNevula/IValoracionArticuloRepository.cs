
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IValoracionArticuloRepository
{
void setSessionCP (GenericSessionCP session);

ValoracionArticuloEN ReadOIDDefault (int id
                                     );

void ModifyDefault (ValoracionArticuloEN valoracionArticulo);

System.Collections.Generic.IList<ValoracionArticuloEN> ReadAllDefault (int first, int size);



int CrearValoracion (ValoracionArticuloEN valoracionArticulo);

void ModificarValoracion (ValoracionArticuloEN valoracionArticulo);


void BorrarValoracion (int id
                       );
}
}
