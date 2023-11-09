
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface ICompraRepository
{
void setSessionCP (GenericSessionCP session);

CompraEN ReadOIDDefault (int id
                         );

void ModifyDefault (CompraEN compra);

System.Collections.Generic.IList<CompraEN> ReadAllDefault (int first, int size);



int CrearCompra (CompraEN compra);

void ModificarCompra (CompraEN compra);


void BorrarCompra (int id
                   );




void UsuarioRegala (int p_Compra_OID, int p_usuarioRegalado_OID);
}
}
